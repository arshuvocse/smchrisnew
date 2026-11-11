<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="CommiteeSetupEdit.aspx.cs" Inherits="HealthCare_UI_CommiteeSetupEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
    
      
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/fancybox/3.5.7/jquery.fancybox.css" integrity="sha512-nNlU0WK2QfKsuEmdcTwkeh+lhGs6uyOxuUs+n+0oXSYDok5qy0EI0lt01ZynHq6+p/tbgpZ7P+yUb+r71wqdXg==" crossorigin="anonymous" referrerpolicy="no-referrer" />

    <script src="https://cdnjs.cloudflare.com/ajax/libs/fancybox/3.5.7/jquery.fancybox.js" integrity="sha512-j7/1CJweOskkQiS5RD9W8zhEG9D9vpgByNGxPIqkO5KrXrwyDAroM9aQ9w8J7oRqwxGyz429hPVk/zR6IOMtSA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    

    <style>
        .shrinkToFit {
            width: 100% !important;
            height: 100% !important;
        }
    </style>
    
    <script>
        $(document).ready(function () {
            $(".fancybox").fancybox({

                'width': 1000, // or whatever
                'height': 700,
                'type': 'iframe',
                'autoSize': false
            });


        });

    </script>
    

    <style>

        #cpFormBody_inlineRadio1_0,
        #cpFormBody_inlineRadio1_1 {
            margin-right: 5px !important;
            margin-left: 5px !important;
           
        }

    </style>
    
    <style>

        .star{
            color:red;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">

      <div class="content" id="content">

          <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>

            <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                <ProgressTemplate>
                    <div class="divWaiting">
                        <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" 
                        />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

                 <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Committee Setup </h1>
                        </div>
                  <div class="page-heading__container float-right d-none d-sm-block">
                      <asp:LinkButton ID="HomeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="HomeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton3"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                    </div>
                    </div>
                 
                 
                 <div class="card">
   <div class="card-body">

       <div class="row">
               <div class="col-md-12">

                     <div class="form-group ">

                          <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> Type:  <span class="star">*</span></label></div>
                                <div class="col-md-4" style="padding-top:8px">
                                    <asp:RadioButtonList ID="inlineRadio1" RepeatDirection="Horizontal"  runat="server" >
                                        <asp:ListItem Value="OPD" Selected="True">OPD</asp:ListItem>
                                        <asp:ListItem Value="IPD">IPD</asp:ListItem>
                                        <asp:ListItem Value="Special">Special</asp:ListItem>
                                    </asp:RadioButtonList>

                                </div>

                            </div>

                          <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Office: <span class="star">*</span></label></div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlSalarylocation" runat="server"  class="form-control form-control-sm selectme" ></asp:DropDownList> 
                                    <asp:HiddenField ID="hfMasterId" runat="server" />
                                </div>
                              
                              <script type="text/javascript">
                                  function pageLoad() {
                                      $('.selectme').chosen({ disable_search_threshold: 5, search_contains: true });

                                  }
                              </script>
                            </div>
                          
                           <div style="padding-top: 5px;"></div>
                          <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> Company:  <span class="star">*</span></label></div>
                                <div class="col-md-4">
                                     <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged"  class="form-control form-control-sm" ></asp:DropDownList> 
                                    
                                    <asp:HiddenField runat="server" ID="AppType"/>
                                     <asp:HiddenField runat="server" ID="Salary"/>
                                     <asp:HiddenField runat="server" ID="Company"/>
              
                                </div>
                            </div>
                         
                      <%--   <div style="padding-top: 5px;"></div>
                         <div class="row">
                             <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> IsActive:  <span class="star">*</span></label></div>
                             <div class="col-md-4" style="padding-top: 6px">
                                  <asp:CheckBox runat="server" AutoPostBack="True" OnCheckedChanged="IsActive_OnCheckedChanged" ID="IsActive"/>
                             </div>
                         </div>--%>

                       </div>
                   </div>         
         </div>

       <br/>
       <div class="form-row" >
           <div class="col-md-3">
                                
               <label class="title-widget"><h3>Employee Information </h3> </label>
           </div>
       </div>  

       <div class="row">
           <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> Employee Name:  <span class="star">*</span></label></div>
           <div class="col-md-4"> <asp:DropDownList ID="ddlEmployee" runat="server"  class="form-control form-control-sm selectme" ></asp:DropDownList> </div>
           <div class="col-md-4">  <asp:LinkButton runat="server" ID="AddToList"  OnClick="AddToList_OnClick"  Class="btn btn-info btn-sm"><span aria-hidden="true" class="fa fa-plus"></span>  &nbsp; Add To List </asp:LinkButton> </div>
       </div>

 <hr />

        <div class="row">
                                            <div class="col-md-12">
                                                <div id="MainGradeDiv" style="text-align: center !important">
                                                    <asp:GridView ID="itemsGridView" runat="server" AutoGenerateColumns="False"
                                                        PageIndex="0" CssClass="table table-bordered text-left table-condensed custom-table-style" DataKeyNames="EmpInfoId,IsForward,IsApproved,IsDoctor,IsConvenor,IsMemberSecretory,IsMember,ddlEmpId" EmptyDataText="There are no data records to display.">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SL">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                                    <asp:HiddenField runat="server" ID="EmpId" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                            
                                                            
                                                            <asp:TemplateField HeaderText="Forward To HR">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ckForward"  runat="server" AutoPostBack="True"  OnCheckedChanged="ckForward_OnCheckedChanged" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Approved Person">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ckApproved" AutoPostBack="True" OnCheckedChanged="ckApproved_OnCheckedChanged"  runat="server"  />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            
                                                            <asp:TemplateField HeaderText="Doctor Person">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ckDoctor" AutoPostBack="True" OnCheckedChanged="ckDoctor_OnCheckedChanged"  runat="server"  />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            
                                                              <asp:TemplateField HeaderText="Forward Any Person">
                                                                <ItemTemplate>
                                                             
                                                                    
                                                                      <asp:DropDownList Width="300px"  runat="server"   ID="ddlForwordEmp" CssClass="form-control form-control-sm selectme"></asp:DropDownList>
                                                  

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                               
                                                               <asp:TemplateField HeaderText="Convener">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ckConvenor" AutoPostBack="True" OnCheckedChanged="ckConvenor_OnCheckedChanged"  runat="server"  />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                               <asp:TemplateField HeaderText="Member Secretary">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ckMemberSecretory" AutoPostBack="True" OnCheckedChanged="ckMemberSecretory_OnCheckedChanged"  runat="server"  />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            
                                                               <asp:TemplateField HeaderText="Member">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ckMember"   runat="server"  />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Remove">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="itemdeleteImageButton" runat="server" class="btn btn-danger btn-sm btnTextShadow" CommandName="DeleteData" OnClick="itemdeleteImageButton_Click"><i class="fa fa-trash" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
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
      
      
       <div class="form-row">
           <div class="col-md-5">
           </div>
           <div class="col-md-3">
               <div class="form-group" style="margin-top: 17px;">
                                            
                   <asp:LinkButton runat="server" ID="SearchButton" Visible="True"  OnClick="SearchButton_OnClick"  CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-check-square"></span>  &nbsp; Save Information </asp:LinkButton>
                   
                   <asp:LinkButton runat="server" ID="UpdateBtn" Visible="False"  OnClick="SearchButton_OnClick"  CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-check-square"></span>  &nbsp; Update Information </asp:LinkButton>

                   <asp:LinkButton runat="server" ID="btnReset"  CssClass="btn btnMyDesignReset   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset Information</asp:LinkButton>
                                          
               </div> 
           </div>

   
        
       </div>
        
        </div>
                               </div>
        </ContentTemplate>

        </asp:UpdatePanel>
        </div>

</asp:Content>

