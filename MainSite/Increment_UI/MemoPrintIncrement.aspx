<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="MemoPrintIncrement.aspx.cs" Inherits="Increment_UI_MemoPrintIncrement" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style type="text/css">
        /*AutoComplete flyout */
       

         #cpFormBody_KeyResponGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }


          #cpFormBody_KeyResponGridView  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
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

              #cpFormBody_gvSalaryStep  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }

       #cpFormBody_gvSalaryStep > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
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
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Annual Increment Letter Information </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                         <asp:Button ID="detailsViewButton" Text="Back to List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                          

                            <asp:HiddenField runat="server" ID="MasterIdHiddenField" />
                            <asp:HiddenField runat="server" ID="IncrementIdHiddenField" />
                            <asp:HiddenField runat="server" ID="ComName" />
                            <asp:HiddenField runat="server" ID="ComId" />
                            <asp:HiddenField runat="server" ID="empCatId" />
                            <asp:HiddenField runat="server" ID="EmpIdHiddenfield" />

                            <div class="row">

                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-6">
                                              <asp:TextBox ID="lblLabelInfo" CssClass="form-control form-control-sm col-md-4" runat="server" Text=""></asp:TextBox>

                                        </div>
                                          <asp:HiddenField runat="server" ID="hfEmpID"/>
                                        <div class="col-md-6"> <asp:Label ID="lblDate" CssClass="form-control form-control-sm col-md-4 pull-right" runat="server" Text=""></asp:Label></div>
                                    </div>
                                    <br/>
                                    <fieldset class="for-panel">
                                        <legend>Employee Information</legend>
                                        <div class="row">

                                            <div class="col-md-6">


                                                <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">
                                                        <label>Employee ID: </label>
                                                    </div>

                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblEmployeeCode" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>


                                                <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">

                                                        <label>Employee Name: </label>
                                                    </div>

                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblEmp" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>


                                                <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">

                                                        <label>Designation: </label>
                                                    </div>

                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblDesignation" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                
                                                   <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">

                                                        <label>Company: </label>
                                                    </div>

                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblCompany" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">
                                                        <label>Division: </label>
                                                    </div>

                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblDepartment" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>



                                                <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">
                                                        <label>Place of Posting: </label>
                                                    </div>

                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblOffice" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">

                                                    <label>Previous Step: </label>



                                                    <asp:Label ID="txtPreSalStep" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                </div>


                                                <div class="form-group">

                                                    <label>Incremental Step: </label>



                                                    <asp:Label ID="txtIncrementalStep" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-1">
                                            </div>

                                        </div>
                                    </fieldset>
                                </div>


                            </div>

                            <div class="row">

                                <div class="col-md-6">
                                    
                                      
                                    <div class="form-row">
                                        
                                        <div class="col-md-3" style="padding: 10px;">
                                            <label>Subject: </label>
                                        </div>

                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtSubject" CssClass="form-control form-control-sm" runat="server" Text="Annual Increment"></asp:TextBox>
                                        </div>
                                    </div>

                                      
                                    <div class="form-row">
                                        
                                        <div class="col-md-3" style="padding: 10px;">
                                            <label>Salutation: </label>
                                        </div>

                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtSalutation" CssClass="form-control form-control-sm" runat="server" Text=""></asp:TextBox>
                                        </div>
                                    </div>

                                      <div class="form-row" runat="server" Visible="False">
                                        
                                        <div class="col-md-3" style="padding: 10px;">
                                            <label>Congratulations: </label>
                                        </div>

                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtCongra" CssClass="form-control form-control-sm" runat="server" Text="Congratulations !!!"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-md-3" style="padding: 10px;">
                                            <label>Body of the letter: </label>
                                        </div>

                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtBodyofletter" CssClass="form-control" TextMode="MultiLine" placeholder="Write first paragraph" runat="server" Text="" Font-Size="12px" Rows="6"></asp:TextBox>
                                        </div>
                                    </div>

                                    <br/>
                                    
                                     <div class="form-row" runat="server" Visible="False">
                                            <div class="col-4">
                                                <div class="form-group">
                                                    <label>Particulars</label>
                                                    <asp:TextBox runat="server" ID="txtPName" CssClass="form-control form-control-sm" />
                                                </div>
                                            </div>
                                         
                                         
                                            <div class="col-4">
                                                <div class="form-group">
                                                    <label>Salary Break-Up</label>
                                                    <asp:TextBox runat="server" ID="txtPAmount" CssClass="form-control form-control-sm" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                    Enabled="True" TargetControlID="txtPAmount" FilterType="Custom"  ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                         
                                          <div class="col-2" style="margin-top: 18px;">
                                                <div class="form-group">
                                                      <asp:Button ID="EducationRequirementImageButton" CssClass="btn btn-outline-success btn-block disabled btn-sm"  runat="server"  Text="Add To List"   OnClick="EducationRequirementImageButton_Click" />
                                                    </div>
                                              </div>
                                         </div>
                                    <div class="form-row">
                                   <div class="col-md-12">
                                       <asp:HiddenField runat="server" ID="GradeCode"/>
                                       <asp:HiddenField runat="server" ID="HFSalaryGradeId"/>
                                     
                                       <asp:HiddenField runat="server" ID="HFIncrementalStepId"/>
                                           <div style="max-height: 300px; overflow: scroll">
                                                    <asp:GridView ID="KeyResponGridView" runat="server" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="False">
                                                            <Columns>
                                                                                                                                 <asp:TemplateField HeaderText="Particulars">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Particulars" runat="server" Text='<%#Eval("ParticularsName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                                
                                                                
                                                                <asp:TemplateField HeaderText=" From October 1, 2022 Salary breakdown">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_SalaryBreakUpPre"     CssClass="form-control form-control-sm"  runat="server" Text='<%#Eval("SalaryBreakUpPre") %>'></asp:Label>
                                                            
                                                        
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText=" From From July 1, 2023 Salary breakdown">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_SalaryBreakUp" ReadOnly="false" AutoPostBack="True" OnTextChanged="lbl_SalaryBreakUp_OnTextChanged" CssClass="form-control form-control-sm"  runat="server" Text='<%#Eval("SalaryBreakUp") %>'></asp:TextBox>
                                                            
                                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                    Enabled="True" TargetControlID="lbl_SalaryBreakUp" FilterType="Custom"  ValidChars="0123456789.-"></cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                                



                                                             <%--   <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="addeduImageButton" runat="server" OnClick="editImageButton_OnClick"
                                                                            ImageUrl="~/Assets/img/rsz_edit.png" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="deleteImageButton" runat="server" OnClick="deleteImageButton_OnClick"
                                                                            ImageUrl="~/Assets/img/delete.png" />
                                                                    </ItemTemplate>
                                                                    

                                                                </asp:TemplateField>--%>
                                                            </Columns>
                                                        </asp:GridView>
                                            </div>

                                         
                                   </div>
                                    </div>

                                </div>
                                  <div class="col-md-6">
                                      <div class="form-row">
                                        
                                        <div class="col-md-3" style="padding: 10px;">
                                        
                                            <label>Complimentary Close: </label>
                                          </div>

                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtComplimentaryClose"  CssClass="form-control" TextMode="MultiLine" placeholder="Write first paragraph" runat="server" Text="" Font-Size="12px" Rows="4"></asp:TextBox>
                                        </div>
                                    </div>
                                      <br/>
                                          <div class="form-row">
                                        
                                        <div class="col-md-3" style="padding: 10px;">
                                              
                                            </div>
                                           
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtSincerely" CssClass="form-control form-control-sm" runat="server" Text="Yours sincerely,"></asp:TextBox>
                                            </div>
                                           </div>
                                      <br/>
                                      
                                   
                                      <div class="form-row">
                                        
                                        <div class="col-md-3" style="padding: 10px;">
                                             <label>Signature Person: </label>
                                            </div>

                                        <div class="col-md-9">
                                            
                                                 <asp:TextBox ID="EmployeeNameTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="EmployeeNameTextBox_OnTextChanged"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                                        ServiceMethod="GetCompanyWiseEmployeeInfoActiveInactiveAll" ServicePath="~/WebService.asmx" TargetControlID="EmployeeNameTextBox"
                                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                                    </cc1:AutoCompleteExtender>

                                                                    <asp:HiddenField ID="repEmpIdHiddenField" runat="server" />
                                            <asp:TextBox ID="txtName" Font-Size="12px" Rows="3" Visible="False" TextMode="MultiLine" CssClass="form-control" runat="server" Text=""></asp:TextBox>
                                           </div>
                                         </div>
                                      
                                   <br/>
                                      <div class="form-row">
                                        
                                        <div class="col-md-3" style="padding: 10px;">
                                        
                                            <label>Copy To: </label>
                                             </div>

                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtCopyTO"    CssClass="form-control" TextMode="MultiLine" placeholder="Write first paragraph" runat="server" Text="Chief/ Head of Division 		
Supervisor
Personal File.	 " Font-Size="12px" Rows="4"></asp:TextBox>
                                        </div>
                                    </div>
                                      </div>

                            </div>
                               <div class="form-row">
                             <div class="form-group">
                                            <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" Visible="False"   runat="server" OnClick="submitButton_Click" />
                                  <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                     <asp:Button class=" btn btn-sm btn-success " Text="Print" runat="server" ForeColor="black" ID="btnPrint" BackColor="#54C3A7" OnClick="btnPrint_Click"  />  
                                 <br/>
                                 <br/>
                                     
                                          
                                      <%--     <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                            <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" Visible="False" runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />--%>

                                        </div>
                                   </div>
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
