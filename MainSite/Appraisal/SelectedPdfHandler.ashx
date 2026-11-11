<%@ WebHandler Language="C#" Class="SelectedPdfHandler" %>

using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using HRIS.Appraisal;

public class SelectedPdfHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string action = context.Request.QueryString["action"];

        try
        {
            if (action == "start")
            {
                Start(context);
            }
            else if (action == "status")
            {
                Status(context);
            }
            else if (action == "download")
            {
                Download(context);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "text/json";
            context.Response.Write(new JavaScriptSerializer().Serialize(new { error = ex.Message }));
        }
    }

    private void Start(HttpContext context)
    {
        string body = new System.IO.StreamReader(context.Request.InputStream).ReadToEnd();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        List<SelectedPdfRequestItem> items = serializer.Deserialize<List<SelectedPdfRequestItem>>(body);

        if (items == null || items.Count == 0)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "text/json";
            context.Response.Write(serializer.Serialize(new { error = "No rows selected." }));
            return;
        }

        string reportsBasePath = context.Server.MapPath("~/Reports/CrystalReports/");
        string jobId = SelectedPdfJobService.StartJob(items, reportsBasePath);

        context.Response.ContentType = "text/json";
        context.Response.Write(serializer.Serialize(new { jobId = jobId }));
    }

    private void Status(HttpContext context)
    {
        string jobId = context.Request.QueryString["jobId"];
        SelectedPdfJob job = SelectedPdfJobService.GetJob(jobId);

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        context.Response.ContentType = "text/json";

        if (job == null)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.Response.Write(serializer.Serialize(new { error = "Job not found." }));
            return;
        }

        context.Response.Write(serializer.Serialize(new
        {
            total = job.Total,
            done = job.Done,
            failed = job.Failed,
            currentName = job.CurrentName,
            completed = job.IsCompleted,
            error = job.Error,
            itemErrors = job.ItemErrors
        }));
    }

    private void Download(HttpContext context)
    {
        string jobId = context.Request.QueryString["jobId"];
        SelectedPdfJob job = SelectedPdfJobService.GetJob(jobId);

        if (job == null || !job.IsCompleted || job.ZipBytes == null)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return;
        }

        context.Response.Clear();
        context.Response.ContentType = "application/zip";
        context.Response.AddHeader("Content-Disposition", "attachment; filename=AppraisalPDFs_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".zip");
        context.Response.BinaryWrite(job.ZipBytes);
        context.Response.Flush();

        SelectedPdfJobService.RemoveJob(jobId);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
