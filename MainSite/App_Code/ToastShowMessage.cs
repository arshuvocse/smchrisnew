using System;
using System.Web.UI;

public class ToastShowMessage
{
    public void ShowMessageBox(string message, Page page)
    {
        // Auto-detect message type based on keywords
        string lower = (message ?? string.Empty).ToLowerInvariant();
        if (lower.Contains("success") || lower.Contains("saved") || lower.Contains("updated"))
        {
            ShowSuccess(message, page);
        }
        else if (lower.Contains("error") || lower.Contains("failed"))
        {
            ShowError(message, page);
        }
        else
        {
            ShowWarning(message, page);
        }
    }

    public void ShowSuccess(string message, Page page, string title = "Success")
    {
        ShowToast(message, page, "success", title);
    }

    public void ShowError(string message, Page page, string title = "Validation Error")
    {
        ShowToast(message, page, "error", title);
    }

    public void ShowWarning(string message, Page page, string title = "Validation")
    {
        ShowToast(message, page, "warning", title);
    }

    public void ShowInfo(string message, Page page, string title = "Information")
    {
        ShowToast(message, page, "info", title);
    }

    private void ShowToast(string message, Page page, string type, string title)
    {
        if (page == null) return;

        string cleanMsg = (message ?? string.Empty)
            .Replace("\\", "\\\\")
            .Replace("'", "\\'")
            .Replace("\"", "\\\"")
            .Replace("\r", "")
            .Replace("\n", "\\n");

        string cleanTitle = (title ?? string.Empty)
            .Replace("\\", "\\\\")
            .Replace("'", "\\'")
            .Replace("\"", "\\\"")
            .Replace("\r", "")
            .Replace("\n", " ");

        string script = string.Format(
            @"if (typeof showToast === 'function') {{
                showToast('{0}', '{1}', '{2}');
            }} else if (typeof toastr !== 'undefined') {{
                toastr.options = {{
                    'closeButton': true,
                    'progressBar': true,
                    'positionClass': 'toast-top-right',
                    'timeOut': 5000,
                    'extendedTimeOut': 2000
                }};
                toastr['{1}']('{0}', '{2}');
            }} else {{
                alert('{0}');
            }}",
            cleanMsg, type, cleanTitle);

        string key = "toast_" + Guid.NewGuid().ToString("N");
        ScriptManager.RegisterStartupScript(page, page.GetType(), key, script, true);
    }
}

