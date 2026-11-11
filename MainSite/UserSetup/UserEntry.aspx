<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="UserEntry.aspx.cs" Inherits="UserSetup_UserEntry" %>

<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .form-group.required .control-label:after {
            color: #d00;
            content: "*";
            position: absolute;
            margin-left: 4px;
            top: 4px;
            font-size: large;
        }
    </style>

    <style>
        fieldset.for-panel {
            background-color: #fcfcfc;
            border: 1px solid #999;
            padding: 15px 10px;
            background-color: white;
            margin-bottom: 12px;
        }

            fieldset.for-panel legend {
                background-color: #fafafa;
                border: 1px solid #ddd;
                border-radius: 1px;
                font-size: 12px;
                font-weight: bold;
                line-height: 10px;
                margin: inherit;
                padding: 7px;
                width: auto;
                margin-bottom: 0;
                color: black;
            }
    </style>
    
    
    
   
    <style type="text/css">
        /*AutoComplete flyout */
      
    </style>
      <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                  <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />&nbsp;User Information</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                               <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="detailsViewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                            
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">
                           
                               <div class="row">
                                   <div class="col-md-6">
                                        <fieldset class="for-panel">
                                <legend>User Information</legend>


                                <div class="form-row">
                                    <div class="col-3" runat="server" Visible="False">
                                        <div class="form-group required">
                                            <label class="control-label">User Type</label> 
                                            <div  >
                                                <asp:RadioButtonList AutoPostBack="True" RepeatDirection="Horizontal" runat="server" ID="radUserType" CellPadding="5" CellSpacing="3" OnSelectedIndexChanged="radUserType_OnSelectedIndexChanged">
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    
                                        <div class="col-6" runat="server" id="iddivEmp" visible="False">
                                    <div class="form-group required">
                                        <label class="control-label">Employee Name</label>
                                        <asp:TextBox runat="server" AutoPostBack="True" ID="txt_Emp" class="form-control form-control-sm" OnTextChanged="txt_Emp_OnTextChanged"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender
                                            ID="at_txt_GroupName"
                                            TargetControlID="txt_Emp"
                                            runat="server"
                                            ServiceMethod="GetEmpSelectedALL"
                                            ServicePath="~/WebService.asmx"
                                            MinimumPrefixLength="1"
                                            CompletionInterval="1000"
                                            EnableCaching="false"
                                            CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionSetCount="1"
                                            FirstRowSelected="True">
                                        </ajaxToolkit:AutoCompleteExtender>
                                        <asp:HiddenField runat="server" ID="hdEmpInfoId" />
                                    </div>
                                </div>
                                <div class="col-6" runat="server" id="iddivEmpDesig" visible="False">
                                    <div class="form-group">
                                        <label>Designation</label>
                                        <asp:TextBox runat="server" ID="txt_EmpDesig" class="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                                </div>
                                
                                <div class="form-row">
                                        <div class="col-6">
                                    <div class="form-group required">
                                        <label class="control-label">Username</label>
                                        <asp:TextBox runat="server" AutoPostBack="True" ID="txt_Username" class="form-control form-control-sm" OnTextChanged="txt_Username_OnTextChanged"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group required">
                                        <label class="control-label">Password</label>
                                        <asp:TextBox runat="server" AutoPostBack="True" AutoCompleteType="None" ID="txt_Password" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                </div>
                                
                                <div class="form-row">
                                      <div class="col-6">
                                    <div class="form-group">

                                        <label>Email</label>
                                        <asp:TextBox runat="server" AutoPostBack="True" ID="txt_Email" class="form-control form-control-sm" OnTextChanged="txt_Email_OnTextChanged"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group">
                                        <label>Mobile</label>
                                        <asp:TextBox runat="server" ID="txt_Mobile" AutoPostBack="True" class="form-control form-control-sm" OnTextChanged="txt_Mobile_OnTextChanged"></asp:TextBox>
                                    </div>
                                </div>

                          

                               
                                </div>
                                <div class="form-row">
                                     <div class="col-6">
                                    <div class="form-group">
                                        
                                        <div>
                                            <asp:CheckBox runat="server" ID="chk_IsActive" Text="IsActive" Checked="True"></asp:CheckBox>
                                        </div>
                                    </div>
                                </div>
                                    
                                      <div class="col-6">
                                    <div class="form-group">
                                        
                                        <div>
                                            <asp:CheckBox runat="server" Text="Is Super Admin?" ID="IsSupperAdminCheckBox" Checked="False"></asp:CheckBox>
                                        </div>
                                    </div>
                                </div>
                                </div>
                                            
                                            
                                                    <div class="form-row">
                                     <div class="col-6">
                                    <div class="form-group">
                                        <asp:RadioButtonList ID="rbDashboard" runat="server" Height="21px" RepeatDirection="Horizontal" Width="270px">
                                            <asp:ListItem Value="IsMain"> Is Main Dashboard</asp:ListItem>
                                            <asp:ListItem Value="IsPortal"> Is Portal Dashboard</asp:ListItem>
                                        </asp:RadioButtonList>
                                        </div>
                                         </div>
                                                        </div>

                            </fieldset>
                            
                         
                                   </div>
                                   <div class="col-md-6">
                                          <fieldset class="for-panel">
                                <legend>Permission Information</legend>
                            <div class="form-row">
                                <div class="col-3">
                                    <div class="form-group required">
                                        <label class="control-label">Company</label>
                                        <%--<asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />--%>

                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="lchk_Company_OnSelectedIndexChanged">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                                 <div class="col-3" style="display:none">
                                    <div class="form-group required">
                                        <label class="control-label">Job Location</label>
                                        <asp:DropDownList runat="server" ID="ddlJobLocation" class="form-control form-control-sm" />
                                           <script type="text/javascript">
                                               function pageLoad() {
                                                   $('#cpFormBody_ddlJobLocation').chosen({ disable_search_threshold: 5, search_contains: true });
                                               }
                                               </script>
                                               </div>
                                </div>
                                </div>
                             <div class="form-row" runat="server" Visible="False">
                                    <div class="col-12">   <div style="overflow: scroll; height: 200px">
                                    <div class="form-group">
                                        <label class="control-label">Department</label>   <asp:CheckBox runat="server" ID="SelectUnseclect" AutoPostBack="True" OnCheckedChanged="SelectUnseclect_OnCheckedChanged"/>
                                        <div class="form-control">
                                            <asp:CheckBoxList runat="server" ID="lchk_Department" RepeatColumns="2" RepeatDirection="Horizontal">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                        </div>
                                </div>

                            
                               

                            </div>
                                </fieldset>
                                   </div>
                               </div>
                           
                            <br />

                            <div>
                                <asp:HiddenField runat="server" ID="hdpk" />
                                <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />
                                  <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                    <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                            </div>

                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
