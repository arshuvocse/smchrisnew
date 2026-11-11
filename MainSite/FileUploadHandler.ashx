<%@ WebHandler Language="C#" Class="FileUploadHandler" %>

using System;
using System.IO;
using System.Web;


    //public class FileUploadHandler : IHttpHandler
    //{

    //    public void ProcessRequest(HttpContext context)
    //    {
    //        string newFileName=String.Empty;
    //        if (context.Request.Files.Count > 0)
    //        {
    //            HttpFileCollection files = context.Request.Files;
    //            for (int i = 0; i < files.Count; i++)
    //            {
    //                HttpPostedFile file = files[i];
    //                var guidFileName = new Guid();
    //                var fileext = Path.GetExtension(file.FileName);
    //                newFileName = guidFileName + "." + fileext;
    //                //string fname = context.Server.MapPath("~/UploadImg/" + file.FileName);
    //                string fname = context.Server.MapPath("~/UploadImg/" + newFileName);
    //                file.SaveAs(fname);
    //            }
    //        }
    //        context.Response.ContentType = "text/plain";
    //        context.Response.Write(newFileName);
    //    }

    //    public bool IsReusable
    //    {
    //        get
    //        {
    //            return false;
    //        }
    //    }
    //}

using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

public class FileUploadHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        //Check if Request is to Upload the File.
        if (context.Request.Files.Count > 0)
        {
            //Fetch the Uploaded File.
            try
            {
                HttpPostedFile postedFile = context.Request.Files["EmpImage"];

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
            catch (Exception)
            {
                
              //  throw;
            }
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