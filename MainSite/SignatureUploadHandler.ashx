<%@ WebHandler Language="C#" Class="SignatureUploadHandler" %>
using System;
using System.IO;
using System.Web;
using System.Net;
using System.Web.Script.Serialization;

public class SignatureUploadHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        //Check if Request is to Upload the File.
        if (context.Request.Files.Count > 0)
        {
            //Fetch the Uploaded File.
            HttpPostedFile postedFile = context.Request.Files["SignatureFile"];

            //Set the Folder Path.
            string folderPath = context.Server.MapPath("~/UploadImg/");

            //Set the File Name.
            string fileName = Path.GetFileName(postedFile.FileName);

            var guidFileName = Guid.NewGuid().ToString("N");
            var fileext = Path.GetExtension(postedFile.FileName);
            string newFileName = guidFileName + fileext;

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