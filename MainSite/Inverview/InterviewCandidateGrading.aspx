<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="InterviewCandidateGrading.aspx.cs" Inherits="Inverview_InterviewCandidateGrading" %>

<%@ Register Src="~/Report_Pages/IVSearchControlReport.ascx" TagPrefix="uc1" TagName="IVSearchControl" %>


<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
    
   
       <style>
        .elegantshd {
            color: #131313;
            letter-spacing: .15em;
            text-shadow: 2px 2px 4px #000000;
            font-family: 'Kreon', serif;
            vertical-align: middle;
            text-decoration-style: wavy;
        }


        .elegantshd2 {
            color: #131313;
            letter-spacing: .15em;
            text-shadow: 2px 2px 4px #000000;
            font-family: 'Kreon', serif;
            vertical-align: auto;
            text-decoration-style: wavy;
        }
          .LockOn {
            position: fixed;
            left: 0px;
            top: 0px;
            z-index: 2147483647;
            width: 100%;
            height: 2215px;
            background-color: #676767;
            vertical-align: bottom;
            padding-top: 20%;
            filter: alpha(opacity=85);
            opacity: 0.85;
            font-size: large;
            color: #676767;
            font-style: italic;
            font-weight: 400;
            background-image: url("../Assets/img/LatestLoading.gif");
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-position: center;
        }
          .btnexcelcc {
            border: none;
            color: #131313;
            padding-left: 36px;
            padding-top: 8px;
            padding-bottom: 8px;
            padding-right: 36px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            margin: 4px 2px;
            cursor: pointer;
            background: url(../Assets/excel.png);
            background-position: center;
            background-repeat: no-repeat;
            box-shadow: 0 0 3px 1px rgba(0,0,0,.35);
        }
        
        </style>
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
      <div class="content" id="content">
    <asp:UpdatePanel ID="upFormBody" runat="server">
        <ContentTemplate>
          <%--    <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />&nbsp; Recruitment Approval </h1>
                        </div>
                    </div>
               <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                         <%--       <div id="coverScreen" class="LockOn">
                </div>--%>
                            <div class="form-row">
                                <uc1:IVSearchControl runat="server" ID="IVSearchControl" />
                                <div class="col-4"></div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <div></div>
                                        <asp:LinkButton runat="server" ID="btn_LoadList" OnClick="btn_LoadList_OnClick"    CssClass="btn btn-sm btnMyDesignSearch"><i class="fa fa-search-plus"></i>&nbsp;Search Tebulation</asp:LinkButton> 
                                      <%--  <asp:Button runat="server" ID="Button1" OnClick="Button1_OnClick"  Text="Search test" CssClass="btn btn-sm btn-outline-secondary"/>--%>
                                    </div>
                                </div>
                            </div>
                          
                            <br/>
                            <div class="page-header text-left">
                                    <h3 class="elegantshd">
                                             <br/>
                                        <asp:Label ID="Label2" runat="server">Tebulation Sheet</asp:Label>  <input type="button" id="btnExportEmpDetails" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                    </h3>
                                </div>
                              <div style="overflow: scroll;   width: 100%">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False" OnRowDataBound="loadGridView_OnRowDataBound"  CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="CandidateID,JobID">
                                        
                                       <Columns>
                                  <%-- <asp:TemplateField HeaderText="Assign to Merchandiser">
                                            <ItemTemplate>
                                                  
                                                                          
                                                <asp:DropDownList ID="ddlstatus" class="form-control form-control-sm" runat="server"  >
                                                    
                                                </asp:DropDownList>
                                            
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                            </Columns>
                                </asp:GridView>
                            </div>
                            <br/>
                                    <div class="page-header text-left">
                                    <h3 class="elegantshd">
                                             <br/>
                                        <asp:Label ID="Label1" runat="server">Select Candidate</asp:Label>
                                    </h3>
                                </div>
                             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="loadGridView_OnRowDataBound"
                                     CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender" >
                                        
                                      <Columns>
                                           
                                             <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                 <asp:HiddenField ID="CandidateID" runat="server" Value='<%#Eval("CandidateID") %>' />
                                                   <asp:HiddenField ID="JobID" runat="server" Value='<%#Eval("JobID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          
                                        <asp:BoundField DataField="CandidateName" HeaderText="Candidate Name" />
                                           
                                             <asp:BoundField DataField="PhoneNo" HeaderText="Phone No." />
                                        <asp:BoundField DataField="EmailAdress" HeaderText="Email Adress" />
                                    
                                        <asp:BoundField DataField="Address"   HeaderText="Address" />
                                   <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                  
                                                                          
                                                <asp:DropDownList ID="ddlstatus" class="form-control form-control-sm" runat="server"  >
                                                      <asp:ListItem Value="0">Select One</asp:ListItem>
                                                <asp:ListItem Value="Selected">Selected</asp:ListItem>
                                                <asp:ListItem Value="Waiting">Waiting</asp:ListItem>
                                                <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                                                </asp:DropDownList>
                                            
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                            </Columns>
                                </asp:GridView>
                            
                            
                            <asp:GridView ID="gv_selectedEmp" Width="100%" Visible="False"  CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"  DataKeyNames="BoardDetailsId" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Employee ID">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_empCode" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>

                                
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">


                                    <ItemTemplate>
                                        <asp:Label ID="txt_employee" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpName") %>'></asp:Label>



                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mail">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_OfficialEmail" runat="server" class="form-control form-control-sm" Text='<%#Eval("OfficialEmail") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            


                            </Columns>
                        </asp:GridView>

                            <div class="row">
                               <div class="col-md-12">
                                     <div class="form-group">
                                        
                                        <asp:Button ID="submitButton" Text="Submit" CssClass="btn btn-sm btn-info"     runat="server" OnClick="submitButton_Click" />
                                         
                                         <asp:Button ID="SendMail" Text="Send Mail to Board Member" CssClass="btn btn-sm btn-info"     runat="server" OnClick="SendMail_Click" />
                                         <%--  <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />--%>
                                     <%--   <asp:Button ID="cancelButton" Text="Back to List" CssClass="btn btn-sm warning" runat="server" Visible="False" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />--%>
                                       <%-- &nbsp; &nbsp;  &nbsp;<asp:HyperLink ID="hyperlink" NavigateUrl="~/ContractualEmployeeManagement_UI/ContractualEmpList.aspx" CssClass="btn btn-sm text-info" Text="&lt; &lt; &lt; &lt; Back to List" runat="server" />--%>
                                         
                                            

                                    </div>
                               </div>
                           </div>
                        </div>  <br/>
                          
                    </div>  <br/>
                            <br/>
                            <br/><br/>
                            <br/><br/>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

              </div>
    
    
       <script src="../Assets/table2excel.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnExportEmpDetails", function () {
            $("[id*=loadGridView]").table2excel({
                filename: "Tebulation_Sheet_Info.xls"
            });
        });

    </script>
    
      <script>
          $(window).on('load', function () {
              $("#coverScreen").hide();
          });


          $("#cpFormBody_SendMail").click(function () {
              $("#coverScreen").show();
          });
            </script>
    
    <%--  <style>
                             .table   thead th {
   background-color: #5B799E;
    color: white;
}
          </style>--%>
    <%--<script src="../Assets/MaterialDT/jquery-3.3.1.js"></script>--%>
       
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
       <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.css" />

    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.js"></script>
    <script type="text/javascript">
        $.getScript('../AppJs/InterviewCandidateGrading.js', function (data, textStatus, jqxhr) {
            console.log('Script File Load Status=' + jqxhr.status);
        });
    </script>--%>
</asp:Content>

