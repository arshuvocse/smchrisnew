using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Tesseract;

public static class AttachmentOcrService
{
    private static readonly HashSet<string> ImageExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        ".bmp", ".gif", ".jpg", ".jpeg", ".png", ".tif", ".tiff"
    };

    public static string ExtractText(string filePath, string tessDataPath)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            return string.Empty;
        }

        string extension = System.IO.Path.GetExtension(filePath);
        string text;

        if (string.Equals(extension, ".pdf", StringComparison.OrdinalIgnoreCase))
        {
            text = ExtractPdfText(filePath, tessDataPath);
        }
        else if (ImageExtensions.Contains(extension))
        {
            text = OcrImageFile(filePath, tessDataPath);
        }
        else
        {
            return string.Empty;
        }

        return Normalize(text);
    }

    private static string ExtractPdfText(string filePath, string tessDataPath)
    {
        var result = new StringBuilder();

        using (var reader = new PdfReader(filePath))
        using (var engine = CreateEngine(tessDataPath))
        {
            for (int pageNumber = 1; pageNumber <= reader.NumberOfPages; pageNumber++)
            {
                try
                {
                    result.AppendLine(PdfTextExtractor.GetTextFromPage(reader, pageNumber));
                }
                catch
                {
                    // A scanned page may not contain a PDF text layer.
                }

                PdfDictionary page = reader.GetPageN(pageNumber);
                PdfDictionary resources = page == null ? null : page.GetAsDict(PdfName.RESOURCES);
                ExtractImagesFromResources(resources, engine, result, new HashSet<int>());
            }
        }

        return result.ToString();
    }

    private static void ExtractImagesFromResources(PdfDictionary resources, TesseractEngine engine,
        StringBuilder result, HashSet<int> processedObjects)
    {
        if (resources == null)
        {
            return;
        }

        PdfDictionary xObjects = resources.GetAsDict(PdfName.XOBJECT);
        if (xObjects == null)
        {
            return;
        }

        foreach (PdfName name in xObjects.Keys)
        {
            PdfObject rawObject = xObjects.Get(name);
            PRIndirectReference reference = rawObject as PRIndirectReference;
            if (reference != null && !processedObjects.Add(reference.Number))
            {
                continue;
            }

            PdfObject pdfObject = PdfReader.GetPdfObject(rawObject);
            PRStream stream = pdfObject as PRStream;
            if (stream == null)
            {
                continue;
            }

            PdfName subtype = stream.GetAsName(PdfName.SUBTYPE);
            if (PdfName.IMAGE.Equals(subtype))
            {
                try
                {
                    var image = new PdfImageObject(stream);
                    byte[] bytes = image.GetImageAsBytes();
                    result.AppendLine(OcrImageBytes(bytes, engine));
                }
                catch
                {
                    // Unsupported embedded image encodings are skipped safely.
                }
            }
            else if (PdfName.FORM.Equals(subtype))
            {
                ExtractImagesFromResources(stream.GetAsDict(PdfName.RESOURCES), engine, result, processedObjects);
            }
        }
    }

    private static string OcrImageFile(string filePath, string tessDataPath)
    {
        using (var engine = CreateEngine(tessDataPath))
        using (var image = Pix.LoadFromFile(filePath))
        using (var page = engine.Process(image))
        {
            return page.GetText();
        }
    }

    private static string OcrImageBytes(byte[] bytes, TesseractEngine engine)
    {
        if (bytes == null || bytes.Length == 0)
        {
            return string.Empty;
        }

        string temporaryFile = System.IO.Path.GetTempFileName();
        try
        {
            File.WriteAllBytes(temporaryFile, bytes);
            using (var image = Pix.LoadFromFile(temporaryFile))
            using (var page = engine.Process(image))
            {
                return page.GetText();
            }
        }
        finally
        {
            try
            {
                File.Delete(temporaryFile);
            }
            catch
            {
                // Temporary cleanup must not fail the OCR request.
            }
        }
    }

    private static TesseractEngine CreateEngine(string tessDataPath)
    {
        if (string.IsNullOrWhiteSpace(tessDataPath) || !Directory.Exists(tessDataPath))
        {
            throw new DirectoryNotFoundException("Tesseract language-data folder was not found.");
        }

        return new TesseractEngine(tessDataPath, "eng+ben", EngineMode.Default);
    }

    private static string Normalize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return Regex.Replace(value, @"\s+", " ").Trim();
    }
}
