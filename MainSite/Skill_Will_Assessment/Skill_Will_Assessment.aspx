<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="Skill_Will_Assessment.aspx.cs" Inherits="Skill_Will_Assessment_Skill_Will_Assessment" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
     <style type="text/css">
        
         </style>
    <div class="content">

        <asp:UpdatePanel runat="server" ID="upFormBody">
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
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  /> Skill Will Assessment </h1>
                        </div>

                        <%-- <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Appraisal List" OnClick="detailsViewButton_OnClick" CssClass="btn btn-sm btn-outline-secondary " runat="server"  />
                        </div>--%>

                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                            <asp:Button ID="detailsViewButton" Visible="True" Text="&#8920; Back To List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                        </div>

                    </div>
                    <div class="card">
                        <div class="card-body">
                            
                            
                            <div class="row">
                                <style>
                                    .tblTHColorChang{
                                        background-color: #EDF2F5!important;
                                        font-weight: bold;
                                        font-size: 13px;
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
                                     <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee ID</td>
                                                        <td> <asp:Label runat="server" ID="lblEmpId"></asp:Label>
                                                            <asp:HiddenField runat="server" ID="EmpInfoId"/>
                                                        </td>

                                                        
                                                         <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Supervisor</td>
                                                        <td>  <asp:Label ID="ReportingLabel"  runat="server"></asp:Label></td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee Name</td>
                                                        <td> <asp:Label runat="server" ID="lblEmployeeName"></asp:Label></td>

                                                         
                                                              
                                                         <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Department</td>
                                                        <td>  <asp:Label ID="deptNameLabel"  runat="server"></asp:Label></td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Designation</td>
                                                        <td>   <asp:Label ID="desigNameLabel" runat="server"></asp:Label></td>

                                                     <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Office</td>
                                                        <td>  <asp:Label ID="LocationLabel"   runat="server"></asp:Label></td>

                                                    </tr>

                                                    
                                                     <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Date Of Joining</td>
                                                        <td>     <asp:Label ID="joiningDateLabel"  runat="server"></asp:Label></td>
  <td style="width: 20%; padding: 10px;" class="tblTHColorChang" >Place</td>
                                                        <td> <asp:Label runat="server" ID="lblPlace"></asp:Label></td>
                                                    </tr>
                                                    
                                                    
                                                 
                                                    
                                                    
                                                    </table>
                            </div>

        
                        
                         
                            <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Skill Assessment</h2>
                            <hr/>
                        
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">

                            <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalFunc" CssClass="table table-bordered text-left thead-dark gridDatatable">
                                <Columns>
                                    <asp:TemplateField >
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Column1"   Text='<%#Eval("Column1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField >
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Column2"   Text='<%#Eval("Column2") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField >
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Column3"   Text='<%#Eval("Column3") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField >
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Column4"   Text='<%#Eval("Column4") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField >
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Column5"   Text='<%#Eval("Column5") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                         
                                </Columns>
                            </asp:GridView>

                            </div>

                           <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">WILL Assessment </h2>
                            <hr/>
                            <div id="gridContainer2" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                            <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%"  ID="gv_AppraisalPartB" CssClass="table table-bordered text-left thead-dark gridDatatable">

                                <Columns>

                                    <asp:TemplateField  >
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Column1"  Text='<%#Eval("Column1") %>'></asp:Label>
                                        </ItemTemplate>
                                     
                                    </asp:TemplateField>

                                    <asp:TemplateField >
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Column2"   Text='<%#Eval("Column2") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField >
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Column3"   Text='<%#Eval("Column3") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField >
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Column4"   Text='<%#Eval("Column4") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField >
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Column5"   Text='<%#Eval("Column5") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                            </asp:GridView> 
                            
                            </div>
                            
                             <style>
        .lblDesign {
            font-weight: bold;
            align-content: right;
            text-align: right;
            padding: 9px;
        }
                                 </style>
                        <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Rank </h2>
                        <h6 >(1-Poor to 5-Outstanding) </h6>
                        <hr/>

                        <div id="gridContainer3" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                            <asp:HiddenField runat="server" ID="hfEmpSkillWillMasterId"/>
                            <asp:HiddenField runat="server" ID="hfFinYear"/>
                            <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="GridView1" CssClass="table table-bordered text-left thead-dark gridDatatable">

                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="SL" >
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Key Accountabilities /KRA">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtKRA"  Text='<%#Eval("KRA") %>' CssClass="form-control form-control-sm" ></asp:TextBox>
                                        </ItemTemplate>
                                        
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="sdsd" CssClass="lblDesign" runat="server" Text="Overall Status (Average): " />
                                              <br/>
                                            <hr/>
                                            <asp:Label ID="lblssad_SkillT" CssClass="lblDesign" Text="Status in Skill-Will Matrix:"  runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="SKILL">
                                        <ItemTemplate>
                                         
                                            <asp:TextBox runat="server" ID="txtSkill"  Text='<%#Eval("Skill") %>' CssClass="form-control form-control-sm"  AutoPostBack="True" OnTextChanged="txtSkill_OnTextChanged"></asp:TextBox>
                                            
                                       <asp:FilteredTextBoxExtender ID="FilteredTextBoasdasxExtender2" runat="server"
                                                                                        Enabled="True" TargetControlID="txtSkill" FilterType="Custom" ValidChars="012345"></asp:FilteredTextBoxExtender>
                                                    

                                <%--            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"  ControlToValidate="txtSkill" ValidationExpression="^[0-9]{1}$"></asp:RegularExpressionValidator>--%>

                                        </ItemTemplate>
                                         <FooterStyle HorizontalAlign="Center" />
                                        <FooterTemplate>
                                            <asp:Label ID="lbl_SkillT" CssClass="lblDesign"  runat="server" />
                                            
                                             <br/>
                                            <hr/>
                                            <asp:Label ID="lbls_SkillT" CssClass="lblDesign" Text=""  runat="server" />
                                        </FooterTemplate>
                                         
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="WILL">
                                        <ItemTemplate>
                                    
                                            <asp:TextBox runat="server" ID="txtWill" AutoPostBack="True" OnTextChanged="txtWill_OnTextChanged"  Text='<%#Eval("Will") %>' CssClass="form-control form-control-sm" ></asp:TextBox>
                                            
                                       <asp:FilteredTextBoxExtender  ID="FilteredTextBoxExtender2" runat="server"
                                                                                        Enabled="True" TargetControlID="txtWill" FilterType="Custom" ValidChars="012345"></asp:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                          <FooterStyle HorizontalAlign="Center" />
                                        <FooterTemplate>
                                            <asp:Label ID="lbl_WilllT" CssClass="lblDesign" Text="" runat="server" />

                                       <br/>
                                            <hr/>
                                            <asp:Label ID="lbls_WilllTd" CssClass="lblDesign"  runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                
                               
                                      <asp:TemplateField HeaderText="Areas considered">
                                        <ItemTemplate>
                                               <asp:TextBox runat="server" ID="txtAreasconsidered"   Text='<%#Eval("Areasconsidered") %>' CssClass="form-control form-control-sm" ></asp:TextBox>
                                            
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            
                                   
                            

                                            <asp:LinkButton ID="btn_Add" CssClass="btn btn-info btn-sm" OnClick="itemaddImageButton_Click" runat="server"><i class="fa fa-plus"   aria-hidden="true"></i>

                                            </asp:LinkButton>
                                            
                                            <asp:LinkButton ID="lb_Remove"  CssClass="btn btn-danger btn-sm"  OnClick="itemdeleteImageButton_Click" runat="server"><i class="fa fa-trash" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                            
                                            

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    
                               
                                </Columns>
                            </asp:GridView>
                            
                            
                                </div>
                            
                        
                        
                        
                        <div class="form-row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                <div class="form-group" style="margin-left: 30px;">
                                            <asp:HiddenField runat="server" ID="HFCompanyId" />
                                    <asp:LinkButton runat="server" ID="submitButton"  OnClick="btn_Save_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-check-square"></span>  &nbsp; Submit Information </asp:LinkButton>
                                  
                                    <asp:LinkButton runat="server" ID="btnReset"  CssClass="btn btnMyDesignReset   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset Information</asp:LinkButton>
                                          
                                </div> 
                            </div>

   
        
                        </div>
                        


                                <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                


                     
                        </div>
                    </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>

    </div>
</asp:Content>

