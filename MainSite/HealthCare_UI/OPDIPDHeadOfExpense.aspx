<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="OPDIPDHeadOfExpense.aspx.cs" Inherits="HealthCare_UI_OPDIPDHeadOfExpense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
    <style>
        .star {
            color: red;
        }
    </style>
    
    

     <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                  <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"> Head Of Expense</h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                             <asp:LinkButton ID="HomeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="HomeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                                 <asp:LinkButton ID="LinkButton3"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card" >
                        <div class="card-body" >
                            
                            
                            
                            
              <div class="row">
               <div class="col-md-3">
                   </div>
                    <div class="col-md-6">
                        <div class="form-group ">
                                                 
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Company:<span class="star">*</span></label> </div>
                                <div class="col-md-6">
                                      <asp:DropDownList runat="server"   ID="ddlCompany"  class="form-control form-control-sm" />
                                      <asp:HiddenField runat="server" ID="hfMasterId"/>
                                    <asp:HiddenField runat="server" ID="HFCount"/>
                                </div>
                         
                            </div>
                            
                            
                           <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right"> Type:</label></div>
                                <div class="col-md-6" style="padding-top:8px">
                                    
                                    
                                     <div class="form-check form-check-inline" >
   <asp:RadioButton  CssClass="form-check-input" ID="inlineRadio1" value="option1" name="inlineRadioOptions" AutoPostBack="True" OnCheckedChanged="IPD_Click"  runat="server"/>  
  <label class="form-check-label" for="inlineRadio1" style="padding-top: 3px">IPD</label>
</div>

<div class="form-check form-check-inline">

     <asp:RadioButton  CssClass="form-check-input" ID="inlineRadio2" value="option2" name="inlineRadioOptions" AutoPostBack="True" OnCheckedChanged="OPD_Click"  runat="server"/> 
  <label class="form-check-label" for="inlineRadio2" style="padding-top: 3px">OPD</label>
</div>
                                      
                                </div>
                            </div>
                                  
                            </div>

                            </div>
                  </div>

                            
   <%--                <div class="row">
           
           <div class="col-md-12">
                                 




  <div class="form-check form-check-inline" style="padding-left:600px">
  
      
   <asp:RadioButton  CssClass="form-check-input" ID="inlineRadio1"  value="option1"  name="inlineRadioOptions" AutoPostBack="True" OnCheckedChanged="IPD_Click" runat="server"/>       

  <label class="form-check-label" for="inlineRadio1">IPD</label>
</div>
<div class="form-check form-check-inline">
     <asp:RadioButton  CssClass="form-check-input" ID="inlineRadio2" value="option2" name="inlineRadioOptions" AutoPostBack="True" OnCheckedChanged="OPD_Click"  runat="server"/>  

  <label class="form-check-label" for="inlineRadio2">OPD</label>
</div>

                                </div>
       </div>--%>
                            
                            <hr/>
                            
                            
                                               
<div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: hidden;">     
                            
  <asp:GridView ID="loadGridView" runat="server" ShowFooter="true" AutoGenerateColumns="false"  CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender" 
     DataKeyNames="IsActive,IsMaternity,OIPDHeadOfExpenseId" OnRowCreated="loadGridView_RowCreated">
    <Columns>
       <%-- <asp:BoundField DataField="RowNumber" HeaderText="SL" />--%>
        
       <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                 <asp:HiddenField runat="server" ID="hfOIPDHeadOfExpenseId" Value='<%#Eval("OIPDHeadOfExpenseId")%>' />
                                            </ItemTemplate>
             </asp:TemplateField>


        <asp:TemplateField HeaderText="Head Of Expense">
            <ItemTemplate>
                <asp:TextBox ID="HeadOfExpense"  Text='<%#Eval("HeadOfExpense")%>' runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Amount">
            <ItemTemplate>
                <asp:TextBox ID="Amount" runat="server" Text='<%#Eval("Amount")%>' CssClass="form-control form-control-sm"></asp:TextBox>
                
                <ajaxToolkit:FilteredTextBoxExtender ID="freqQtyTextBox" runat="server"
                                                    TargetControlID="Amount"
                                                    FilterType="Custom, Numbers"
                                                    ValidChars="." />
            </ItemTemplate>
        </asp:TemplateField>
        

        <asp:TemplateField HeaderText="Remarks">
            <ItemTemplate>
                <asp:TextBox ID="Remarks" runat="server"  Text='<%#Eval("Remarks")%>' CssClass="form-control form-control-sm"></asp:TextBox>
            </ItemTemplate>  
        </asp:TemplateField>
        
         <asp:TemplateField HeaderText="Is Maternity" >
            <ItemTemplate>
                <asp:CheckBox runat="server" ID="IsMaternity"  />
            </ItemTemplate>  
        </asp:TemplateField>
        
            <asp:TemplateField HeaderText="Is Active">
            <ItemTemplate>
                
              <asp:CheckBox ID="IsActive" runat="server" />
            </ItemTemplate>  
        </asp:TemplateField>
        
        
       
        
                  
           <asp:TemplateField HeaderText="Add">
            
            
            <ItemTemplate>               
                <asp:LinkButton ID="ButtonAdd" runat="server"   CssClass="btn btn-sm btn-info" OnClick="ButtonAdd_Click"><i class="fa fa-plus"></i> </asp:LinkButton>
            </ItemTemplate>
            

        
        </asp:TemplateField>
        
        

        <asp:TemplateField HeaderText="Remove">
                   
            <ItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server"   CssClass="btn btn-sm btn-danger" OnClick="LinkButton1_Click"><i class="fa fa-minus-circle"></i></asp:LinkButton>
            </ItemTemplate>

        </asp:TemplateField>
    </Columns>
</asp:GridView>

                            </div>
                            
                            
                            
      <div class="form-row">
               <div class="col-md-5">
       </div>
                <div class="col-md-6">
                            <div class="form-group" style="margin-top: 17px;">
                                            
                                   <asp:LinkButton runat="server" ID="SearchButton"   OnClick="BtnSave_Click"  CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-check-square"></span>  &nbsp; Save Information </asp:LinkButton>
                                  <asp:LinkButton runat="server" ID="btnUpdate" Visible="False"  OnClick="BtnUpdate_Click"  CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-check-square"></span>  &nbsp; Update Information </asp:LinkButton>
                                   <asp:LinkButton runat="server" ID="btnReset"  CssClass="btn btnMyDesignReset   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset Information</asp:LinkButton>
                                          
                     </div> 
       </div>

        </div>
                            


                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
    

    

</asp:Content>

