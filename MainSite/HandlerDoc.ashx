<%@ WebHandler Language="C#" Class="HandlerDoc" %>

using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

public class HandlerDoc : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.Files.Count > 0)
        {
            HttpPostedFile postedFile = null;
            for (int i = 0; i < context.Request.Files.Count; i++)
            {
                var fileCandidate = context.Request.Files[i];
                if (fileCandidate != null && !string.IsNullOrEmpty(fileCandidate.FileName) && fileCandidate.ContentLength > 0)
                {
                    postedFile = fileCandidate;
                    break;
                }
            }

            if (postedFile == null)
            {
                postedFile = context.Request.Files[0];
            }

            string fileName = Path.GetFileName(postedFile.FileName);
            if (string.IsNullOrEmpty(fileName))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                context.Response.Write(new JavaScriptSerializer().Serialize(new { error = "No file selected" }));
                return;
            }

            string folderPath = context.Server.MapPath("~/UploadMeetingDocument/");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var guidFileName = Guid.NewGuid().ToString("N");
            var fileext = Path.GetExtension(fileName);
            string newFileName = guidFileName + fileext;

            postedFile.SaveAs(Path.Combine(folderPath, newFileName));

            string json = new JavaScriptSerializer().Serialize(
                new
                {
                    name = fileName,
                    dbfilename = newFileName
                });
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "application/json";
            context.Response.Write(json);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}