<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="false" CodeFile="InterviewCandidateInfoList.aspx.cs" Inherits="Inverview_InterviewCandidateInfoList" %>

<%@ Register Src="~/Report_Pages/IVSearchControlReport.ascx" TagPrefix="uc1" TagName="IVSearchControl" %>


<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
<%--    <link href="../Assets/MaterialDT/dataTables.material.min.css" rel="stylesheet" />
    <link href="../Assets/MaterialDT/material.min.css" rel="stylesheet" />
    <link href="../Assets/assets/css/bootstrap.css" rel="stylesheet" />
    <style type="text/css">
        
        </style>--%>
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
       
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  />  Interview Candidate Information List</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                             
                            
                             <asp:LinkButton ID="HomeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                    
                            
                            <asp:LinkButton ID="btn_New"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="btn_New_OnClick" > <i class="fa fa-plus"></i>&nbsp;Add New</asp:LinkButton>
                        </div>
                    </div>
                  <div class="card">
                        <div class="card-body">
      <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                  <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div class="form-row">
               
                    <uc1:IVSearchControl runat="server" ID="IVSearchControl" />
                    <asp:LinkButton runat="server" ID="btn_LoadList" OnClick="btn_LoadList_OnClick" CssClass="btn btn-sm btnMyDesignSearch"><i class="fa fa-search-plus"></i>&nbsp; Search Matching Candidate List</asp:LinkButton>
                    
                          
<%--                         
                                         <asp:LinkButton ID="btnExportToExcel" runat="server"  target="_blank"  CssClass="btnexcel pull-right" OnClick="btnExportToExcel_Click" ><span aria-hidden="true" class="glyphicon glyphicon-ok"></span>Generate Report</asp:LinkButton>--%>
                                       
                                      
                                <style>
                                    .btnexcel {
                                        background-color: #4CAF50;
                                        border: none;
                                        color: white;
                                        padding: 8px 12px;
                                        text-align: center;
                                        text-decoration: none;
                                        display: inline-block;
                                        font-size: 12px;
                                        margin: 4px 2px;
                                        cursor: pointer;
                                    }
         </style>        
                       
                </div>
                  </ContentTemplate>
        </asp:UpdatePanel>
                                     <div class="row">
                 <div class="col-md-12">
                                       <label>  </label>
                                       </div>
                                   
                                   
                                   <div class="col-md-2">
                                       
                                       
                                       </div>
                                   <div class="col-md-2">
                                       
                                       
                                       </div>
                                   <div class="col-md-2">
                                       
                                       
                                       </div>
                                     <div class="col-md-1">
                                       
                                       
                                       </div>
                                   <div class="col-md-1">
                                       
                                       
                                       </div>
                                   <div class="col-md-1">
                                       
                                       
                                       </div>
                                   <div class="col-md-1">
                                       
                                       
                                       </div>
                                  
                                     <div class="col-md-2 ">
                                         <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel pull-right" OnClick="btnExportToExcel_Click" ><span aria-hidden="true" class="glyphicon glyphicon-ok"></span>Export To Excel</asp:LinkButton>
                                       
                                      
                                       
        
  </div>
                     </div>
                             <div class="row" runat="server" visible="false">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label> </label>
                                             <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True"  RepeatDirection="Horizontal" Enabled="False" >
                                        </asp:CheckBoxList>
                                            

                                        </div>
                                    </div>
                                </div>
                            
                            <br/>
                            
                              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                        CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"    DataKeyNames="CandidateID"
                                    OnRowCommand="loadGridView_RowCommand" OnRowCreated="loadGridView_OnRowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:BoundField DataField="ShortName" HeaderText="Company" Visible="False" />
                                        <asp:BoundField DataField="CandidateName" HeaderText="Candidate Name" />
                                        <asp:BoundField DataField="PhoneNo" HeaderText="Phone No." />
                                        <asp:BoundField DataField="EmailAdress" HeaderText="Email Adress" />
                                        <asp:BoundField DataField="Age" HeaderText="Age" />

                                        <asp:BoundField DataField="Address" Visible="False" HeaderText="Address" />
                                    <%--    <asp:BoundField DataField="PhoneNo" HeaderText="Phone No" />
                                        <asp:BoundField DataField="EmailAdress" HeaderText="Email" />--%>
                                        <asp:BoundField DataField="LastPassingYear" HeaderText="Passing Year" />   

                                        <asp:BoundField DataField="LastResultCGPA" HeaderText="Last Result" />   
                                        <asp:BoundField DataField="TotalYearsOfExp" Visible="False" HeaderText="Years Of Exp" />
                                        <asp:BoundField DataField="ExpectedSalary" HeaderText="Expected Salary" />
                                    
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="editImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData"
                                                    ImageUrl="~/Assets/img/rsz_edit.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="deleteImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="DeleteData"
                                                    ImageUrl="~/Assets/img/delete.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="viewImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="ViewData"
                                                    ImageUrl="~/Assets/img/list-view.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>
                       

                            <%-- <div id="tbl_container" style="width:100%" class="">

                        </div>--%>
                        </div>
                   </ContentTemplate>
        </asp:UpdatePanel>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            </div>
                       <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                    </div>
                    
            
          
    </div>
        </div>

    <%--    <script src="../Assets/MaterialDT/jquery.dataTables.min.js"></script>
    <script src="../Assets/MaterialDT/dataTables.material.min.js"></script>
    <script type="text/javascript">
        $.getScript('../AppJs/InterviewCandidateInfoList.js', function (data, textStatus, jqxhr) {
            console.log('Script File Load Status=' + jqxhr.status);
        });
    </script>--%>
</asp:Content>
