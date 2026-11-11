<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="InterviewCandidateInfo.aspx.cs" Inherits="Inverview_InterviewCandidateInfo" %>

<%@ Register Src="~/Report_Pages/IVSearchControlReport.ascx" TagPrefix="uc1" TagName="IVSearchControl" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>



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

        #cpFormBody_KeyResponGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }


        #cpFormBody_KeyResponGridView > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }


        .files input {
            outline: 2px dashed #92b0b3;
            outline-offset: -10px;
            -webkit-transition: outline-offset .15s ease-in-out, background-color .15s linear;
            transition: outline-offset .15s ease-in-out, background-color .15s linear;
            padding: 30px 0px 60px 10%;
            text-align: center !important;
            margin: 0;
            width: 100% !important;
        }

        .files input:focus {
            outline: 2px dashed #92b0b3;
            outline-offset: -10px;
            -webkit-transition: outline-offset .15s ease-in-out, background-color .15s linear;
            transition: outline-offset .15s ease-in-out, background-color .15s linear;
            border: 1px solid #92b0b3;
        }

        .files {
            position: relative;
        }

        .files:after {
            pointer-events: none;
            position: absolute;
            top: 60px;
            left: 0;
            width: 50px;
            right: 0;
            height: 33px;
            content: "";
            /*background-image: url(https://image.flaticon.com/icons/png/128/109/109612.png);*/
            display: block;
            margin: 0 auto;
            background-size: 100%;
            background-repeat: no-repeat;
        }

        .color input {
            background-color: #f1f1f1;
        }

        .files:before {
            position: absolute;
            bottom: 10px;
            left: 0;
            pointer-events: none;
            width: 100%;
            right: 0;
            height: 35px;
            content: " or drag it here. ";
            display: block;
            margin: 0 auto;
            color: #2ea591;
            font-weight: 600;
            text-transform: capitalize;
            text-align: center;
        }

        .upIcon {
        }
    </style>
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
                </ContentTemplate>
            </asp:UpdatePanel>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  /> Interview Candidate Information</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            
                            
                              <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="detailsViewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                         <%--    <asp:LinkButton ID="LinkButton1"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-plus"></i>&nbsp;Add New</asp:LinkButton> --%>

                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">
                              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                            <div class="form-row">
                                <%--<div class="col-2">
                                <div class="form-group required">
                                    <label class="control-label">Company</label>
                                    <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group required">
                                    <label class="control-label">Job Circulation</label>
                                    <asp:TextBox runat="server" AutoPostBack="True" ID="txt_JobCirculation" class="form-control form-control-sm" OnTextChanged="txt_JobCirculation_OnTextChanged"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hfJobID"/>
                                    <ajaxToolkit:AutoCompleteExtender
                                        ID="at_txt_JobCirculation"
                                        TargetControlID="txt_JobCirculation"
                                        runat="server"
                                        ServiceMethod="GetJobCirculationAuto"
                                        ServicePath="~/WebService.asmx"
                                        MinimumPrefixLength="1"
                                        CompletionInterval="10"
                                        EnableCaching="false"
                                        CompletionSetCount="1"
                                        FirstRowSelected="false">
                                    </ajaxToolkit:AutoCompleteExtender>
                                </div>
                            </div>
                             <div class="col-2" runat="server" >
                                <div class="form-group">
                               
                                    <label>Job Title</label>
                                    <asp:TextBox runat="server" ID="txt_JobTitle" class="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>--%>
                                <uc1:IVSearchControl runat="server" ID="IVSearchControl" />

                            </div>
           
                            <fieldset class="for-panel">
                                <legend>Candidate Information</legend>
                                  
                                <div class="form-row">

                                    <div class="col-3">
                                        <div class="form-group required">
                                            <label class="control-label">Candidate Mobile No.</label>
                                            <asp:TextBox runat="server" ID="txt_CandidatePhone" class="form-control form-control-sm" AutoPostBack="True" OnTextChanged="txt_CandidatePhone_OnTextChanged"></asp:TextBox>
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                    Enabled="True" TargetControlID="txt_CandidatePhone" FilterType="Custom"  ValidChars="0123456789-+"></cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group required">
                                            <label class="control-label">Candidate Name</label>
                                            <asp:TextBox runat="server" ID="txt_CandidateName" class="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-3" runat="server" visible="False">
                                        <div class="form-group">
                                            <label>Address</label>
                                            <asp:TextBox runat="server" ID="txt_CandidateAddress" class="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group required">
                                            <label class="control-label">Date Of Birth</label>
                                            <asp:TextBox runat="server" ID="dateofBirthTextBox" class="form-control form-control-sm" autocomplete="off"  placeholder="example : dd/MMM/Year"  AutoPostBack="True" OnTextChanged="dateofBirthTextBox_OnTextChanged"   ></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                Format="dd/MMM/yyyy" CssClass="MyCalendar"
                                                TargetControlID="dateofBirthTextBox" />
                                           
                                         
                                            
                              
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label class="control-label">Age</label>
                                            <asp:TextBox runat="server" ID="ageTextBox" class="form-control form-control-sm" ReadOnly="True"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-3">
                                        <div class="form-group">
                                            <label class="control-label">Email Address</label>
                                            <asp:TextBox runat="server" ID="txt_CandidateEmail" TextMode="Email" class="form-control form-control-sm" OnTextChanged="txt_CandidateEmail_OnTextChanged"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-1" runat="server" visible="False">
                                        <div class="form-group">
                                            <label class="control-label">Experience</label>
                                            <div>
                                                <asp:DropDownList runat="server" ID="ddlExpY" class="form-control form-control-sm">
                                                    <asp:ListItem Value="0" Text="0 Years"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="1 Years"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="2 Years"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="3 Years"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="4 Years"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="5 Years"></asp:ListItem>
                                                    <asp:ListItem Value="6" Text="6 Years"></asp:ListItem>
     

                                               <asp:ListItem Value="7" Text="7 Years"></asp:ListItem>
                                                    <asp:ListItem Value="8" Text="8 Years"></asp:ListItem>
                                                    <asp:ListItem Value="9" Text="9 Years"></asp:ListItem>
                                                    <asp:ListItem Value="10" Text="10 Years"></asp:ListItem>
                                                    <asp:ListItem Value="11" Text="11 Years"></asp:ListItem>
                                                    <asp:ListItem Value="12" Text="12 Years"></asp:ListItem>
                                                    <asp:ListItem Value="13" Text="13 Years"></asp:ListItem>
                                                    <asp:ListItem Value="14" Text="14 Years"></asp:ListItem>
                                                    <asp:ListItem Value="15" Text="15 Years"></asp:ListItem>
                                                    <asp:ListItem Value="16" Text="16 Years"></asp:ListItem>
                                                    <asp:ListItem Value="17" Text="17 Years"></asp:ListItem>
                                                    <asp:ListItem Value="18" Text="18 Years"></asp:ListItem>
                                                    <asp:ListItem Value="19" Text="19 Years"></asp:ListItem>
                                                    <asp:ListItem Value="20" Text="20 Years"></asp:ListItem>
                                                    <asp:ListItem Value="21" Text="21 Years"></asp:ListItem>
                                                    <asp:ListItem Value="22" Text="22 Years"></asp:ListItem>
                                                    <asp:ListItem Value="23" Text="23 Years"></asp:ListItem>
                                                    <asp:ListItem Value="24" Text="24 Years"></asp:ListItem>
                                                    <asp:ListItem Value="25" Text="25 Years"></asp:ListItem>
                                                    <asp:ListItem Value="26" Text="26 Years"></asp:ListItem>
                                                    <asp:ListItem Value="27" Text="27 Years"></asp:ListItem>
                                                    <asp:ListItem Value="28" Text="28 Years"></asp:ListItem>
                                                    <asp:ListItem Value="29" Text="29 Years"></asp:ListItem>
                                                    <asp:ListItem Value="30" Text="30 Years"></asp:ListItem>
                                                    <asp:ListItem Value="31" Text="31 Years"></asp:ListItem>
                                                    <asp:ListItem Value="32" Text="32 Years"></asp:ListItem>
                                                    <asp:ListItem Value="33" Text="33 Years"></asp:ListItem>
                                                    <asp:ListItem Value="34" Text="34 Years"></asp:ListItem>
                                                    <asp:ListItem Value="35" Text="35 Years"></asp:ListItem>
                                                    <asp:ListItem Value="36" Text="36 Years"></asp:ListItem>
                                                    <asp:ListItem Value="37" Text="37 Years"></asp:ListItem>
                                                    <asp:ListItem Value="38" Text="38 Years"></asp:ListItem>
                                                    <asp:ListItem Value="39" Text="39 Years"></asp:ListItem>
                                                    <asp:ListItem Value="40" Text="40 Years"></asp:ListItem>
                                                    <%--<asp:ListItem Value="41" Text="41 Years"></asp:ListItem>
                                            <asp:ListItem Value="42" Text="42 Years"></asp:ListItem>
                                            <asp:ListItem Value="43" Text="10 Years"></asp:ListItem>--%>
                                                </asp:DropDownList>

                                                <asp:DropDownList runat="server" ID="ddlExpM" class="form-control form-control-sm">
                                                    <asp:ListItem Value="0" Text="0 Months"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="1 Months"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="2 Months"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="3 Months"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="4 Months"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="5 Months"></asp:ListItem>
                                                    <asp:ListItem Value="6" Text="6 Months"></asp:ListItem>
                                                    <asp:ListItem Value="7" Text="7 Months"></asp:ListItem>
                                                    <asp:ListItem Value="8" Text="8 Months"></asp:ListItem>
                                                    <asp:ListItem Value="9" Text="9 Months"></asp:ListItem>
                                                    <asp:ListItem Value="10" Text="10 Months"></asp:ListItem>
                                                    <asp:ListItem Value="11" Text="11 Months"></asp:ListItem>
                                                    <asp:ListItem Value="12" Text="12 Months"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label class="control-label">Last Organization</label>
                                            <asp:TextBox runat="server" AutoPostBack="True" ID="txt_LastOrganization" class="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Last Position</label>
                                            <asp:TextBox runat="server" AutoPostBack="True" ID="txt_LastPosition" class="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Last Education</label>
                                            <asp:TextBox runat="server" ID="txt_LastExam" class="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Major</label>
                                            <asp:TextBox runat="server" ID="txt_MaxMajor" class="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-3">
                                        <div class="form-group">
                                            <label class="control-label">Passing Year</label>
                                            <asp:TextBox runat="server" ID="txt_LastPassingYear" class="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label class="control-label">Current Salary</label>
                                            <asp:TextBox runat="server" ID="txt_CurrentSalary" class="form-control form-control-sm"></asp:TextBox>
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                                    Enabled="True" TargetControlID="txt_CurrentSalary" FilterType="Custom"  ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label class="control-label">Expected Salary</label>
                                            <asp:TextBox runat="server" ID="txt_ExpectedSalary" class="form-control form-control-sm"></asp:TextBox>
                                            
                                              <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                                    Enabled="True" TargetControlID="txt_ExpectedSalary" FilterType="Custom"  ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>

                                </div>

                                <div class="form-row">

                                    <div class="col-3">
                                        <div class="form-group">
                                            <label class="control-label" style="margin-right: 20px;">Result</label>
                                            <asp:RadioButtonList AutoPostBack="True" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server" ID="radResult" OnSelectedIndexChanged="radResult_OnSelectedIndexChanged" Visible="false">
                                                <asp:ListItem Value="Grading" Text="Grading" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="Division" Text="Division"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:TextBox runat="server" ID="txt_LastResultGrading" class="form-control form-control-sm"></asp:TextBox>

                                            <%--   placeholder="CGPA"--%>




                                            <asp:DropDownList RepeatLayout="Flow" runat="server" ID="ddlLastResultDivision" Visible="False" class="form-control form-control-sm">
                                                <asp:ListItem Value="3" Text="3rd Division"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="2nd Division"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="1st Division"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-3">
                                         </div>
                                    

                                    <div class="col-3" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Image Upload</label>
                                            <div>
                                                <asp:TextBox runat="server" ID="txt_LastResultGradingOutOf" class="form-control form-control-sm" placeholder="Out Of" Visible="false"></asp:TextBox>
                                                <asp:FileUpload ID="fu_Image" runat="server" />
                                                <asp:Button ID="btn_ImageUpload" runat="server" Text="Image Upload" OnClick="btn_ImageUpload_OnClick" />
                                                <br />
                                                <asp:Image ID="Image1" runat="server" />
                                            </div>
                                        </div>
                                    </div>


                                </div>

                                <div class="form-row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Company Name</label>
                                            <asp:TextBox ID="txtCompany" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Experience</label>
                                            <asp:TextBox ID="txtExperience" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2" style="margin: 18px;">
                                        <div class="form-group">
                                            <asp:LinkButton ID="textButton"   OnClick="textButton_OnClick" CssClass="btn btnMyDesignAddtoList btn-sm" runat="server" ><i class="fa fa-plus"></i> &nbsp;Add To List</asp:LinkButton>
                                        </div>
                                    </div>



                                </div>
                                
                                
                                   <div class="form-row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Degree Name</label>
                                            <asp:TextBox ID="txtDegreeName" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>University Name</label>
                                            <asp:TextBox ID="txtUniversity" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                       
                                        <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Result</label>
                                            <asp:TextBox ID="txtResult" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-2" style="margin: 18px;">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lblDegree"   OnClick="lblDegree_OnClick" CssClass="btn btnMyDesignAddtoList btn-sm" runat="server" ><i class="fa fa-plus"></i> &nbsp;Add To List</asp:LinkButton>
                                        </div>
                                    </div>



                                </div>
                
                <div class="form-row">
                      <div class="col-md-6">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <asp:GridView ID="KeyResponGridView" runat="server" Width="100%"    CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"  AutoGenerateColumns="False">
                                                    <Columns>
                                                        <%--<asp:BoundField DataField="SizeId" HeaderText="Size Id" Visible="False" HtmlEncode="False" />--%>
                                                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" HtmlEncode="False" />
                                                        <asp:BoundField DataField="Experience" HeaderText="Experience" HtmlEncode="False" />

                                                        <asp:TemplateField HeaderText="Edit">
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
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                    </div>
                    
                     <div class="col-md-6">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <asp:GridView ID="gv_Degree" runat="server" Width="100%"    CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"  AutoGenerateColumns="False">
                                                    <Columns>
                                                        <%--<asp:BoundField DataField="SizeId" HeaderText="Size Id" Visible="False" HtmlEncode="False" />--%>
                                                        <asp:BoundField DataField="DegreeName" HeaderText="Degree Name" HtmlEncode="False" />
                                                        <asp:BoundField DataField="University" HeaderText="University Name" HtmlEncode="False" />
                                                        <asp:BoundField DataField="Result" HeaderText="Result" HtmlEncode="False" />
                                                     
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="addegv_Degree" runat="server" OnClick="addegv_Degree_OnClick"
                                                                    ImageUrl="~/Assets/img/rsz_edit.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="deleteImageButton" runat="server" OnClick="deleteImageButton_OnClick"
                                                                    ImageUrl="~/Assets/img/delete.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                    </div>
                </div>
                 
                                <div class="form-row">
                        
                                    <div class="col-6">
                                        <div class="form-group">
                                                    <label>CV Upload</label>
                                                    <div>
                                                        
                                            <asp:HiddenField runat="server" ID="hfDocFileName"/>
                                            <asp:HiddenField runat="server" ID="hfDocFile"/>
                                                        	<div class="input-group" >
   <input type="file" name="postedFile" id="file" onchange="return fileValidation()" class="form-control" />
  
                                                       &nbsp;
                                                        <input type="button"  class="btn btn-sm  btn-info" id="btnUpload" value="Upload Document" />
                                                   </div>
                                                        <br/>
                                                          <progress id="fileProgress"    style="display: none;height: 40px;width: 400px;"></progress>
                                                            <br/>
                                             <span id="lblMessage" style="color: Green"></span>
                                                       
                                                           
                                                        
                                                       
                                                    </div>
                                                </div>

                                        <div class="form-group files" runat="server" Visible="False">
                                           
                                            <div>
                                                <asp:FileUpload ID="fu_cv"  CssClass="form-control" runat="server" />

                                            </div>
                                        </div>
                                        <asp:Button ID="btn_CvUpload" Visible="False" runat="server" CssClass="btn  btn-sm" BackColor="#3276B1" ForeColor="White" Text="CV Upload" OnClick="btn_CvUpload_OnClick" />
                                    </div>
                                    <div class="col-md-2">
                                         <div class="form-group" style="padding: 17px;">
                                         <asp:HyperLink cssclass="btn btn-success btn-sm"  Visible="False" ID="HyperLink2" runat="server"
    
     Target="_blank" ToolTip="Click to Show Document"><i class="fa fa-download"></i> &nbsp;Download </asp:HyperLink> 
                                         <asp:LinkButton Visible="False" runat="server" ID="lb_Download"  cssclass="btn btn-primary btn-sm" backcolor="#4169E1" borderstyle="None"  OnClick="lb_Download_OnClick"><i class="fa fa-download"></i> Download</asp:LinkButton>
                                             </div>
                                    </div>


                                </div>
                            </fieldset>
                            <br />

                            <div>
                                <asp:HiddenField runat="server" ID="hdpk" />
                                
                                 <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                                        <div class="ui-group-buttons">
                                <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" OnClientClick="return confirm('Are you sure you want to Save ?')" Visible="False" Text=" Save " CssClass="btn btn-sm btn-info" />
                                                               <div class="or or-sm" runat="server" Visible="False" id="orBTN"></div>
                                                             <asp:Button runat="server" ID="btnSaveNew" OnClientClick="return confirm('Are you sure you want to  Save & Add New ?')" OnClick="btnSaveNew_OnClick" Visible="False" Text=" Save & Add New " CssClass="btn btn-sm btn-success" />
                                                              </div>
                                <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                <asp:Button ID="cancelButton" Text="Cancel" Visible="False" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                            </div>
                     </ContentTemplate>
            </asp:UpdatePanel>
                        </div>
                    </div>
          
            
                                                           
                                                                  
        
    </div>
        </div>
     <script type="text/javascript">
         
         function fileValidation() {
             var fileInput =
                 document.getElementById('file');

             var filePath = fileInput.value;

             // Allowing file type 
             var allowedExtensions =
                     /(\.XLS|\.xls|\.XLSX|\.xlsx|\.doc|\.DOC|\.docx|\.DOCX|\.pdf|\.PDF)$/i;


             if (filePath=="") {
                 alert('Please Select a file');
                  
                 return false;
             }

             if (!allowedExtensions.exec(filePath)) {
                 alert('Invalid file type');
                 fileInput.value = '';
                 return false;
             }  
                 return true;
            
             
         };
         $("body").on("click", "#btnUpload", function () {
             debugger;
             if (fileValidation()) {

           
                 
            

             $.ajax({
                 url: '/HandlerDocCV.ashx',
                 type: 'POST',
                 data: new FormData($('form')[0]),
                 cache: false,
                 contentType: false,
                 processData: false,
                 success: function (file) {
                     $("#fileProgress").hide();
                     $("#cpFormBody_hfDocFile").val('');
                     $("#cpFormBody_hfDocFileName").val('');
                 
                        
                        
                     $("#lblMessage").html("<b>" + file.name + "</b> has been uploaded.");
                     $("#cpFormBody_hfDocFile").val(file.dbfilename);
                     $("#cpFormBody_hfDocFileName").val(file.name);
                     
                    

                 },
                 xhr: function () {
                     var fileXhr = $.ajaxSettings.xhr();
                     if (fileXhr.upload) {
                         $("progress").show();
                         fileXhr.upload.addEventListener("progress", function (e) {
                             if (e.lengthComputable) {
                                 $("#fileProgress").attr({
                                     value: e.loaded,
                                     max: e.total
                                 });
                             }
                         }, false);
                     }
                     return fileXhr;
                 }
            
             });
             }
         });
     </script>
</asp:Content>
