using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;

namespace HELPER_FUNCTIONS.HELPERS
{
    public class ManageUserOperationCredentials
    {
        readonly UserOperationPermissionCredentialsDal aCredentialsDal = new UserOperationPermissionCredentialsDal();
        public void MnageUserOperation(string pageType,string userId, Int32 menuId ,Page page)
        {
            DataTable aDataTable = aCredentialsDal.CheckUserOperationPermissionCredentials(userId, menuId);
            const int rowIndex = 0;

            var button = FindButton(page);

            if (aDataTable.Rows.Count > 0)
            {
                var add = aDataTable.Rows[rowIndex].Field<bool>("Add");
                var view = aDataTable.Rows[rowIndex].Field<bool>("View");

                if (pageType != "" && button != null)
                {
                    if (pageType == "VIEW")
                    {
                        button.Visible = view;
                    }

                    if (pageType == "ADD")
                    {
                        button.Visible = add;                      
                    }
                }
            }     
        }

        private Button FindButton(Page page)
        {
            Button button = null;

            if (page.Master != null)
            {
                if (page.Master.FindControl("ContentPlaceHolder1").FindControl("detailsViewButton") != null)
                {
                    button = (Button)page.Master.FindControl("ContentPlaceHolder1").FindControl("detailsViewButton");
                }

                if (page.Master.FindControl("ContentPlaceHolder1").FindControl("addNewButton") != null)
                {
                    button = (Button)page.Master.FindControl("ContentPlaceHolder1").FindControl("addNewButton");
                }
            }

            return button;
        }

        public DataTable MnageUserOperationOnGridView(string userId,Int32 manuId)
        {
            DataTable aDataTable = aCredentialsDal.CheckUserOperationPermissionCredentials(userId, manuId);
            return aDataTable;
        }
    }
}
