<%@ WebHandler Language="C#" Class="HandlerHealthDoc" %>

using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

public class HandlerHealthDoc : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile postedFile = context.Request.Files[0];
                if (postedFile == null || postedFile.ContentLength == 0)
                {
                    SendResponse(context, HttpStatusCode.BadRequest, new { error = "No file uploaded." });
                    return;
                }

                string originalFileName = Path.GetFileName(postedFile.FileName);
                string fileExt = Path.GetExtension(originalFileName);
                string newFileName = Guid.NewGuid().ToString("N") + fileExt;

                // 1. Primary destination: ~/UploadHealthCareDoc/ inside application
                string localAppPath = context.Server.MapPath("~/UploadHealthCareDoc/");
                if (!Directory.Exists(localAppPath)) Directory.CreateDirectory(localAppPath);
                string localFilePath = Path.Combine(localAppPath, newFileName);
                postedFile.SaveAs(localFilePath);

                // 2. Secondary backup: D:\UploadHealthCareDoc\ (copy already saved file without re-reading stream)
                try
                {
                    string dDrivePath = @"D:\UploadHealthCareDoc\";
                    if (!Directory.Exists(dDrivePath)) Directory.CreateDirectory(dDrivePath);
                    File.Copy(localFilePath, Path.Combine(dDrivePath, newFileName), true);
                }
                catch { }

                // Send JSON Response
                SendResponse(context, HttpStatusCode.OK, new
                {
                    name = originalFileName,
                    dbfilename = newFileName
                });
            }
            else
            {
                SendResponse(context, HttpStatusCode.BadRequest, new { error = "No file received." });
            }
        }
        catch (Exception ex)
        {
            SendResponse(context, HttpStatusCode.InternalServerError, new { error = ex.Message });
        }
    }

    private void SendResponse(HttpContext context, HttpStatusCode statusCode, object data)
    {
        string json = new JavaScriptSerializer().Serialize(data);
        context.Response.Clear();
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";
        context.Response.Write(json);
        context.Response.Flush();
        context.ApplicationInstance.CompleteRequest();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}