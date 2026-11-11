<%@ WebHandler Language="C#" Class="HandlerDoc" %>

using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.Script.Serialization;

public class HandlerDoc : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //Check if Request is to Upload the File.
        if (context.Request.Files.Count > 0)
        {
            //Fetch the Uploaded File.
            HttpPostedFile postedFile = context.Request.Files[0];

            //Set the Folder Path.
            string fileName = Path.GetFileName(postedFile.FileName);
         //   string folderPath = @"D:\UploadMeetingDocument\"; 
            string folderPath = context.Server.MapPath("~/UploadMeetingDocument/");

            var guidFileName = Guid.NewGuid().ToString("N");
            var fileext = Path.GetExtension(postedFile.FileName);
            string newFileName = "App_"+guidFileName + fileext;

            //Save the File in Folder.
            //postedFile.SaveAs(folderPath + fileName);
            postedFile.SaveAs(folderPath + newFileName);

          
            //Send File details in a JSON Response.
            string json = new JavaScriptSerializer().Serialize(
                new
                {
                    name = fileName,
                    dbfilename = newFileName
                });
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "text/json";
            context.Response.Write(json);
            context.Response.End();
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