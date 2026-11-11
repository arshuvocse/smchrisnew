using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HELPER_FUNCTIONS.HELPERS
{
    public class ShowMessage
    {
        public void ShowMessageBox(string message,Page page)
        {
            message = message.Replace("'", "\'");
            string sScript = String.Format("alert('{0}');", message);
            ScriptManager.RegisterStartupScript(page, this.GetType(), "alert", sScript, true);
        }
    }
}
