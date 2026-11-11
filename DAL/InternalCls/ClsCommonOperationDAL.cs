using System.Data;
using System.Web.UI.WebControls;

namespace DAL.InternalCls
{
    public class ClsCommonOperationDAL
    {
        public void CancelDataMark(GridView aGridView, DataTable aDataTable)
        {
            for (int i = 0; i < aDataTable.Rows.Count; i++)
            {
                if (aDataTable.Rows[i]["ActionStatus"].ToString().Trim() == "Cancel")
                {
                   // aGridView.Rows[i].BackColor = System.Drawing.Color.Fuchsia;
                }
            }
        }

    }
}
