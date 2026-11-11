using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.PanalCls;


public partial class MasterPages_MainMasterPage : System.Web.UI.MasterPage
{
    private PanalClsDAL aDal = new PanalClsDAL();
    private DataTable aDataTableMenu = new DataTable();
    private DataTable aDataTableSubItem = new DataTable();
    private DataTable aTableSubSubItem = new DataTable();
    private DataTable aTableSubSubChildItem = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null || Session["UserId"].ToString() == "")
        {
            Response.Redirect("../Default.aspx");

        }


      
         
        if (!IsPostBack)
        {
            Menu();
            SetUserLogIninfo();
            
        }
    }

    public DataTable MenuData()
    {
        string filepath = Path.GetDirectoryName(Request.Path);
        filepath = filepath.TrimStart('\\');

        filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);

        DataTable dtdata = aDal.FindParant(filepath);
        return dtdata;
    }

    public string ParantId(DataTable aDataTable, string id)
    {
       
            if (aDataTable.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(aDataTable.Rows[0]["ParantId"].ToString()))
                {
                    return aDataTable.Rows[0]["SL"].ToString();
                }
                else
                {
                    DataTable dtdata = aDal.MenuSL(id);

                    if (dtdata.Rows.Count>0)
                    {
                        return aDataTable.Rows[0]["SL"].ToString() + "," + ParantId(dtdata, dtdata.Rows[0]["ParantId"].ToString());
                    }
                    else
                    {
                        return "";
                    }
                    
                }
            }
            else
            {
                return "";
            }
        
      
      
    }
    private void SetUserLogIninfo()
    {
        lblUserName.Text = Convert.ToString(Session["LoginName"]);
        hfSesUserID.Value = Session["UserId"].ToString();
        hfForcePassChange.Value = IsPasswordChangeRequired() ? "1" : "0";
      // lblUserDes.Text = Convert.ToString(Session["Designation"]);
       lblLoginTime.Text = "Login Time: "+ Convert.ToString(Session["UserTime"]);
    }

    private bool IsPasswordChangeRequired()
    {
        string isPassChanged = Convert.ToString(Session["isPassChanged"]).Trim();
        return string.IsNullOrEmpty(isPassChanged)
            || isPassChanged == "0"
            || isPassChanged.Equals("false", StringComparison.OrdinalIgnoreCase);
    }
    
    private void Menu()
    {
        string clM = "menu";
        string page = @"#";
        string parent = "halflings white home";
        string item = @"Product Info";
        string responsive = @"responsive";
        string clMainMenu = @"halflings white home";
        string navigation = @"navigation";
        string leftC = @"left-corner";
        string rightC = @"right-corner";


        //
        string openable = @"""openable""";
        string openableopen = @"""openable open""";
        string icon = @"icon li-home";
        string text = @"""text""";
        string noIcone = @"no-icon";

        if (Session["UserTypeId"].ToString() == "3" || Session["UserTypeId"].ToString() == "4")
        {
            aDataTableMenu = aDal.MainMenu();
        }
        else
        {
            aDataTableMenu = aDal.MainMenu(Convert.ToInt32(Session["UserId"]));    
        }
        
        

        if (aDataTableMenu.Rows.Count > 0)
        {
            ///////////////////////////////Start//////////////////////////////////////////////////////
            /// 

            string parantid = "0";
            string menurHtml = "<ul>";
            DataTable dtdata = MenuData();
            string id = "";
            if (dtdata.Rows.Count>0)
            {
                id = ParantId(dtdata,dtdata.Rows[0]["ParantId"].ToString()); ;    
            }
            string[] mainids = id.Split(',');
            int mainidindex = mainids.Length-1;

            for (int i = 0; i < aDataTableMenu.Rows.Count; i++)
            {
                if (aDataTableMenu.Rows[i]["URL"].ToString().Trim() != "#")
                {
                    menurHtml = menurHtml + @" <li ><a href=" + aDataTableMenu.Rows[i]["URL"].ToString().Trim() + "><span class=" + aDataTableMenu.Rows[i]["icon"].ToString().Trim() + "></span><span class=" + text + ">" + aDataTableMenu.Rows[i]["menuName"].ToString().Trim() + "</span></a>";
                    //menurHtml = menurHtml + @" <li><a href=" + aDataTableSubItem.Rows[j]["URL"].ToString().Trim() + " class=\"no-icon\"><span class=\"text\">" + aDataTableSubItem.Rows[j]["menuName"].ToString().Trim() + "</span></a>";
                }
                else
                {
                    if (mainids[mainidindex]==aDataTableMenu.Rows[i]["SL"].ToString())
                    {
                        menurHtml = menurHtml + @" <li class=" + openableopen + "><a href=" + aDataTableMenu.Rows[i]["URL"].ToString().Trim() + "><span class=" + aDataTableMenu.Rows[i]["icon"].ToString().Trim() + "></span><span class=" + text + ">" + aDataTableMenu.Rows[i]["menuName"].ToString().Trim() + "</span></a>";
                        mainidindex--;
                        if (mainidindex<0)
                        {
                            mainidindex = 0;
                        }
                    }
                    else
                    {
                        menurHtml = menurHtml + @" <li class=" + openable + "><a href=" + aDataTableMenu.Rows[i]["URL"].ToString().Trim() + "><span class=" + aDataTableMenu.Rows[i]["icon"].ToString().Trim() + "></span><span class=" + text + ">" + aDataTableMenu.Rows[i]["menuName"].ToString().Trim() + "</span></a>";
                    }
                    
                }
                if (Session["UserTypeId"].ToString() == "3" || Session["UserTypeId"].ToString() == "4")
                {
                    aDataTableSubItem = aDal.SubItem(aDataTableMenu.Rows[i]["SL"].ToString().Trim());
                }
                else
                {
                    aDataTableSubItem = aDal.SubItem(aDataTableMenu.Rows[i]["SL"].ToString().Trim(), Convert.ToInt32(Session["UserId"]));
                }
                //aDataTableSubItem = Convert.ToInt32(Session["UserId"]) == 1 ? aDal.SubItem(aDataTableMenu.Rows[i]["SL"].ToString().Trim()) : aDal.SubItem(aDataTableMenu.Rows[i]["SL"].ToString().Trim(), Convert.ToInt32(Session["UserId"]));

                if (aDataTableSubItem.Rows.Count > 0)
                {
                    menurHtml = menurHtml + @"<ul>";

                    for (int j = 0; j < aDataTableSubItem.Rows.Count; j++)
                    {

                        //////////////////////////////////////////////////SubItemBind/////////////////////////////////////////////////////////

                        if (aDataTableSubItem.Rows[j]["URL"].ToString().Trim() != "#")
                        {
                            menurHtml = menurHtml + @" <li ><a href=" + aDataTableSubItem.Rows[j]["URL"].ToString().Trim() + "><span class=" + aDataTableSubItem.Rows[j]["icon"].ToString().Trim() + "></span><span class=" + text + ">" + aDataTableSubItem.Rows[j]["menuName"].ToString().Trim() + "</span></a>";
                            //menurHtml = menurHtml + @" <li><a href=" + aDataTableSubItem.Rows[j]["URL"].ToString().Trim() + " class=\"no-icon\"><span class=\"text\">" + aDataTableSubItem.Rows[j]["menuName"].ToString().Trim() + "</span></a>";
                        }
                        else
                        {
                            if (mainids[mainidindex] == aDataTableSubItem.Rows[j]["SL"].ToString())
                            {
                                menurHtml = menurHtml + @" <li class=" + openableopen + "><a href=" + aDataTableSubItem.Rows[j]["URL"].ToString().Trim() + "><span class=" + aDataTableSubItem.Rows[j]["icon"].ToString().Trim() + "></span><span class=" + text + ">" + aDataTableSubItem.Rows[j]["menuName"].ToString().Trim() + "</span></a>";
                                mainidindex--;
                                if (mainidindex < 0)
                                {
                                    mainidindex = 0;
                                }
                            }
                            else
                            {
                                menurHtml = menurHtml + @" <li class=" + openable + "><a href=" + aDataTableSubItem.Rows[j]["URL"].ToString().Trim() + "><span class=" + aDataTableSubItem.Rows[j]["icon"].ToString().Trim() + "></span><span class=" + text + ">" + aDataTableSubItem.Rows[j]["menuName"].ToString().Trim() + "</span></a>";    
                            }
                            
                        }




                        //aTableSubSubItem = aDal.SubSubItem(aDataTableSubItem.Rows[j]["SL"].ToString().Trim());
                        if (Session["UserTypeId"].ToString() == "3" || Session["UserTypeId"].ToString() == "4")
                        {
                            aTableSubSubItem = aDal.SubSubItem(aDataTableSubItem.Rows[j]["SL"].ToString().Trim());
                        }
                        else
                        {
                            aTableSubSubItem = aDal.SubSubItem(aDataTableSubItem.Rows[j]["SL"].ToString().Trim(), Convert.ToInt32(Session["UserId"]));
                        }
                        //aTableSubSubItem = Convert.ToInt32(Session["UserId"]) == 1 ? aDal.SubSubItem(aDataTableSubItem.Rows[j]["SL"].ToString().Trim()) : aDal.SubSubItem(aDataTableSubItem.Rows[j]["SL"].ToString().Trim(), Convert.ToInt32(Session["UserId"]));


                        if (aTableSubSubItem.Rows.Count > 0)
                        {
                            menurHtml = menurHtml + @"<ul>";
                            for (int k = 0; k < aTableSubSubItem.Rows.Count; k++)
                            {
                                //////////////////////////////////////////////////SubSubItemBind/////////////////////////////////////////////////////////
                                try
                                {
                                    if (aTableSubSubItem.Rows[k]["URL"].ToString() != "#")
                                    {
                                        menurHtml = menurHtml + @" <li ><a href=" + aTableSubSubItem.Rows[k]["URL"].ToString().Trim() + "><span class=" + aTableSubSubItem.Rows[k]["icon"].ToString().Trim() + "></span><span class=" + text + ">" + aTableSubSubItem.Rows[k]["menuName"].ToString().Trim() + "</span></a>";
                                        //menurHtml = menurHtml + @" <li><a href=" + aTableSubSubItem.Rows[k]["URL"].ToString().Trim() + " class=\"no-icon\"><span class=\"text\">" + aTableSubSubItem.Rows[k]["menuName"].ToString().Trim() + "</span></a>";
                                    }
                                    else
                                    {
                                        menurHtml = menurHtml + @" <li class=" + openable + "><a href=" + aTableSubSubItem.Rows[k]["URL"].ToString().Trim() + "><span class=" + aTableSubSubItem.Rows[k]["icon"].ToString().Trim() + "></span><span class=" + text + ">" + aTableSubSubItem.Rows[k]["menuName"].ToString().Trim() + "</span></a>";
                                    }
                                }
                                catch (Exception)
                                {

                                    //  throw;
                                }

                                //aTableSubSubChildItem = aDal.SubSubChildItem(aTableSubSubItem.Rows[k]["SL"].ToString().Trim());

                                aTableSubSubChildItem = Convert.ToInt32(Session["UserId"]) == 1 ? aDal.SubSubChildItem(aTableSubSubItem.Rows[k]["SL"].ToString().Trim()) : aDal.SubSubChildItem(aTableSubSubItem.Rows[k]["SL"].ToString().Trim(), Convert.ToInt32(Session["UserId"]));


                                if (aTableSubSubChildItem.Rows.Count > 0)
                                {
                                    menurHtml = menurHtml + @"<ul>";
                                    for (int l = 0; l < aTableSubSubChildItem.Rows.Count; l++)
                                    {
                                        //////////////////////////////////////////////////SubSubChildItemBind/////////////////////////////////////////////////////////

                                        if (aTableSubSubChildItem.Rows[j]["URL"].ToString() != "#")
                                        {
                                            menurHtml = menurHtml + @" <li ><a href=" + aTableSubSubChildItem.Rows[l]["URL"].ToString().Trim() + "><span class=" + aTableSubSubChildItem.Rows[l]["icon"].ToString().Trim() + "></span><span class=" + text + ">" + aTableSubSubChildItem.Rows[l]["menuName"].ToString().Trim() + "</span></a></li>";
                                            //menurHtml = menurHtml + @" <li><a href=" + aTableSubSubChildItem.Rows[l]["URL"].ToString().Trim() + " class=\"no-icon\"><span class=\"text\">" + aTableSubSubChildItem.Rows[l]["menuName"].ToString().Trim() + "</span></a>";
                                        }
                                        else
                                        {
                                            menurHtml = menurHtml + @" <li class=" + openable + "><a href=" + aTableSubSubChildItem.Rows[l]["URL"].ToString().Trim() + "><span class=" + aTableSubSubChildItem.Rows[l]["icon"].ToString().Trim() + "></span><span class=" + text + ">" + aTableSubSubChildItem.Rows[l]["menuName"].ToString().Trim() + "</span></a></li>";
                                        }

                                        //////////////////////////////////////////////////SubSubChildItemBindEnd/////////////////////////////////////////////////////////
                                    }

                                    menurHtml = menurHtml + @"</ul>";
                                }

                                //////////////////////////////////////////////////SubSubItemBind/////////////////////////////////////////////////////////
                                menurHtml = menurHtml + @"</li>";
                            }

                            menurHtml = menurHtml + @"</ul>";
                        }

                        //////////////////////////////////////////////////SubItemBindEnd/////////////////////////////////////////////////////////
                        menurHtml = menurHtml + @"</li>";
                    }

                    menurHtml = menurHtml + @"</ul>";
                }


                menurHtml = menurHtml + @"</li>";

                //////////////////////////////////////////////////MainMenuBindEnd/////////////////////////////////////////////////////////
            }
            menurHtml = menurHtml + @" </ul>";

            //menurHtml = menurHtml + @"</ul></nav>";

            //////////////////////////////////////////end////////////////////////////////////////
            navigation_default.InnerHtml = menurHtml;
        }
    }
    public void LoadEmployeeInfo()
    {
        //nameTextBox.Text = Convert.ToString(Session["EmpName"]);
        //surNameTextBox.Text = Convert.ToString(Session["ShortName"]);
        //addressTextBox.Text = Convert.ToString(Session["AddressPermanent"]);
        //emailTextBox.Text = Convert.ToString(Session["Email"]);
        //cellNumberTextBox.Text = Convert.ToString(Session["CellNumber"]);
        //unitTextBox.Text = Convert.ToString(Session["UnitName"]);
        //departmentTextBox.Text = Convert.ToString(Session["DeptName"]);
        //designationTextBox.Text = Convert.ToString(Session["DesigName"]);
    }


    
}

