<%@ WebHandler Language="C#" Class="MeetingMinors_OcrHandler" %>

using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

public class MeetingMinors_OcrHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string text = string.Empty;

        try
        {
            string fileName = Path.GetFileName(context.Request.QueryString["fileName"]);

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                string filePath = context.Server.MapPath("~/UploadMeetingDocument/" + fileName);
                string tessDataPath = context.Server.MapPath("~/App_Data/tessdata");
                text = AttachmentOcrService.ExtractText(filePath, tessDataPath);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError("Meeting attachment OCR failed: " + ex);
        }

        string json = new JavaScriptSerializer().Serialize(new { text = text });
        context.Response.StatusCode = (int)HttpStatusCode.OK;
        context.Response.ContentType = "application/json";
        context.Response.Write(json);
        context.Response.End();
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
