<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="true" CodeFile="InterviewCandidateInfoListReport.aspx.cs" Inherits="Report_Pages_InterviewCandidateInfoListReport" %>

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
        
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Interview Candidate Information List Report</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block" runat="server" Visible="False">
                            <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary" runat="server" OnClick="HomeButton_OnClick" />
                            <asp:Button ID="btn_New" Text="Create New" CssClass="btn btn-sm btn-outline-secondary" runat="server" OnClick="btn_New_OnClick" />
                        </div>
                    </div>
                <div class="card">
                <div class="card-body">

                <div class="form-row">
                    <uc1:IVSearchControl runat="server" ID="IVSearchControlReport" />
                    <asp:Button runat="server" ID="btn_LoadList" OnClick="btn_LoadList_OnClick"  Text="Search Matching Candidate List" CssClass="btn btn-sm btn-outline-secondary"/>
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
                                 
             <div class="row">
                 <div class="col-md-12">
                                   
                         <%--   <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">--%>
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark" DataKeyNames="CandidateID"
                                    OnRowCommand="loadGridView_RowCommand" OnRowCreated="loadGridView_OnRowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <%--<asp:BoundField DataField="ShortName" HeaderText="Company" />--%>
                                        <asp:BoundField DataField="CandidateName" HeaderText="Name" />
                                        <asp:BoundField DataField="DateOfBirth" DataFormatString="{0:dd-MMM-yyyy}"  HeaderText="Date Of Birth" />

                                        <asp:BoundField DataField="LastExam" HeaderText="Education" />
                                        <asp:BoundField DataField="LastPosition" HeaderText="Employment History" />
                                        <asp:BoundField DataField="TotalYearsOfExp" HeaderText="Total  Experience" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                        <asp:BoundField DataField="EmailAdress" HeaderText="E-mail" />
                                         
                                        <asp:BoundField DataField="PhoneNo" HeaderText="Mobile" />
                                        

                                    </Columns>
                                </asp:GridView>
                       

                            <%-- <div id="tbl_container" style="width:100%" class="">

                        </div>--%>  
                 </div>
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
                    <br/>
                        </div>
                    </div>
          
        </div>
                
                  </ContentTemplate>
               <Triggers>
                    <asp:PostBackTrigger ControlID="btnExportToExcel" /> 
                </Triggers>
        </asp:UpdatePanel>
    </div>
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
    <%--    <script src="../Assets/MaterialDT/jquery.dataTables.min.js"></script>
    <script src="../Assets/MaterialDT/dataTables.material.min.js"></script>
    <script type="text/javascript">
        $.getScript('../AppJs/InterviewCandidateInfoList.js', function (data, textStatus, jqxhr) {
            console.log('Script File Load Status=' + jqxhr.status);
        });
    </script>--%>
</asp:Content>
