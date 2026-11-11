<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="SalaryMatrixReport.aspx.cs" Inherits="Report_Pages_SalaryMatrixReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server" >
    
    <style>
        .pagination .page-item.active .page-link  {
            background-color: #007BFF !important;
            border-style: none !important;
            /* W3C */
        }
        .title-widget {
              color: #898989;
              font-size: 20px;
              font-weight: 300;
              line-height: 1;
              position: relative;
              text-transform: uppercase;
              font-family: 'Fjalla One', sans-serif;
              margin-top: 0;
              margin-right: 0;
              margin-bottom: 25px;
	 
              padding-left: 12px;

          }

          .title-widget::before {
              background-color: #ea5644;
              content: "";
              height: 22px;
              left: 0px;
              position: absolute;
              top: -2px;
              width: 5px;
          }
        
    </style>
    
       <style>
           .table   thead th {
               background-color: #5B799E;
               color: white;
           }
       </style>
    
     <style>
        .dt-button.buttons-print,
        .dt-button.buttons-excel.buttons-html5,
        .dt-button.buttons-pdf.buttons-html5 {
            
            background-color: #4CAF50;
  border: none;
  color: white;
  padding: 5px 18px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  font-size: 16px;
  margin: 2px 1px;
  cursor: pointer;
  -webkit-transition-duration: 0.4s; 
  transition-duration: 0.4s;
  box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2), 0 3px 10px 0 rgba(0,0,0,0.19);
        }

 
.dt-buttons {
    align-content: center;
    text-align: center;
}
.dt-button.buttons-pdf.buttons-html5:hover {
  box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24),0 17px 50px 0 rgba(0,0,0,0.19);
}
</style>
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Salary Matrix</h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block" runat="server" Visible="False">
                         <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="addNewButton" Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid" >
                    <div class="card" >
                        <div class="card-body" >
                             <div class="row">
                                    <div class="col-md-3" runat="server" Visible="False">
                                        <div class="form-group">
                                            <label> </label>
                                             <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True"  RepeatDirection="Horizontal" Enabled="False" >
                                        </asp:CheckBoxList>
                                            

                                        </div>
                                    </div>
                                </div>
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark table-hover table-striped" DataKeyNames="SalaryMatrixId" ShowFooter="True"
                                    OnRowCommand="loadGridView_RowCommand" OnRowCreated="loadGridView_OnRowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="Version" HeaderText="Version" />
                                       

                                        <asp:TemplateField HeaderText="View Details">
                                            <ItemTemplate>
                                               
                                                <asp:ImageButton ID="editImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData"
                                                   ImageUrl="~/Assets/report_magnify.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                      
                                    </Columns>
                                </asp:GridView>

                                    <div class="row">
                        <div class="col-md-2">
                            
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-3">
                            <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click"><span aria-hidden="true" class="glyphicon glyphicon-ok"></span>Export To Excel</asp:LinkButton>
                            <%--<asp:LinkButton ID="btnExportToPDF" runat="server" Visible="False" CssClass="btnPDF  pull-right" OnClick="btnExportToPDF_Click"><span aria-hidden="true" class="glyphicon glyphicon-apple"></span>Export To PDF</asp:LinkButton>--%>
                        </div>

                    </div>
                                 <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Management</h2>
                                
                                  <asp:GridView ID="GridView4" runat="server"     AutoGenerateColumns="False" ShowHeader="False">
    <Columns>
       
        <asp:TemplateField  >
            <ItemTemplate>
                <asp:Label ID="lblsHello" runat="server" 
                Text='<%# Eval("Text1") %>' />
               
            </ItemTemplate>
        </asp:TemplateField>
                 
    </Columns>
</asp:GridView>
                   
                                      <asp:GridView ID="GridView1"   CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender" runat="server" AutoGenerateColumns="False"    OnRowDataBound="OnRowDataBound" DataKeyNames="SalaryGradeId" >
                                                         
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SL">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                                            <asp:HiddenField runat="server" ID="hfSalaryGradeId" Value='<%#Eval("SalaryGradeId")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                  <asp:TemplateField HeaderText="Grade"  >
                                                                    <ItemTemplate>
                                                                     
                                                                        
                                                                              <asp:Label ID="lblGradeName" runat="server"    Text='<%# Eval("GradeName")%>'  ></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                  <asp:TemplateField HeaderText="1"   >
                                                                     
                                                                    <ItemTemplate>
                                                                     
                                                                        
                                                                             <%-- <asp:Label ID="label1" runat="server"    Text='<%# Eval("1")%>'  ></asp:Label>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                </Columns>
                                          </asp:GridView>
                                
                                <br/>
                                <br/>
                                <asp:GridView ID="GridView3" runat="server"    OnPreRender="GridView1_PreRender"  AutoGenerateColumns="False" ShowHeader="False">
    <Columns>
       
        <asp:TemplateField  >
            <ItemTemplate>
                <asp:Label ID="lblHello" runat="server" 
                Text='<%# Eval("Text1") %>' />
               
            </ItemTemplate>
        </asp:TemplateField>
                 
    </Columns>
</asp:GridView>
                                 <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Graded</h2>
                                
                                          <asp:GridView ID="GridView2" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender" runat="server" AutoGenerateColumns="False"    OnRowDataBound="OnRowDataBoundGrad" DataKeyNames="SalaryGradeId" >
                                                         
                                                            <Columns>
                                                          
                                                                <asp:TemplateField HeaderText="SL">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                                           
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                  <asp:TemplateField HeaderText="Grade"  >
                                                                    <ItemTemplate>
                                                                     
                                                                        
                                                                              <asp:Label ID="lblGradeName2" runat="server"    Text='<%# Eval("GradeName")%>'  ></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                  <asp:TemplateField HeaderText="1"   >
                                                                     
                                                                    <ItemTemplate>
                                                                     
                                                                        
                                                                             <%-- <asp:Label ID="label1" runat="server"    Text='<%# Eval("1")%>'  ></asp:Label>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                       <asp:TemplateField HeaderText="2"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                  <asp:TemplateField HeaderText="STEP-31"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                
                                                                  <asp:TemplateField HeaderText="STEP-32"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                  <asp:TemplateField HeaderText="STEP-33"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                  <asp:TemplateField HeaderText="STEP-34"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                  <asp:TemplateField HeaderText="STEP-35"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                  <asp:TemplateField HeaderText="STEP-36"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                  <asp:TemplateField HeaderText="STEP-37"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                  <asp:TemplateField HeaderText="STEP-38"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                  <asp:TemplateField HeaderText="STEP-39"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                  <asp:TemplateField HeaderText="STEP-40"  >
                                                                    <ItemTemplate>
                                                                        
                                                                        <%-- <asp:Label ID="label2" runat="server"    Text='<%# Eval("2")%>'  ></asp:Label>  
                                                                       --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                </Columns>
                                          </asp:GridView>

                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
                <Triggers>
                 
                 <asp:PostBackTrigger ControlID="btnExportToExcel"/>
             </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

