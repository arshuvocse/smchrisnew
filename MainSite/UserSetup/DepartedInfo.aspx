<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="DepartedInfo.aspx.cs" Inherits="UserSetup_DepartedInfo" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
    <link href="ButtonGrups.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
    <div class="container-fluid">
        <div class="page-heading">
            <div class="page-heading__container">
                <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />Update Information</h1>
            </div>
            <div class="page-heading__container">
                <asp:Label runat="server" ID="lbl_Mode"></asp:Label>
                <span>
                    <label class="btn infoN" style="font-size: 13px;">Employee ID:  &nbsp;
                        <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 13px;" ID="empMasterCode"></asp:Label></label>
                   
                    
                  <label class="btn infoN" style="font-size: 13px;">Employee Name: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 13px;" ID="lblEmpName"></asp:Label></label>
                    
                    <label class="btn infoN" style="font-size: 11px;">Designation: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 9px;" ID="lblDesignation"></asp:Label></label>
                </span>
            </div>
            <div class="page-heading__container float-right d-none d-sm-block">
                 <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton1"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
              <%--  <asp:Button ID="detailsViewButton" Text="Update Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                   <asp:Button ID="btnEditInfo" Text="Back to List" Visible="False"  CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btnEditInfo_OnClick" />--%>
            </div>
        </div>
        <div class="card">
            <div class="card-body">
                
                
                   <style>
                            .NavbarAcc {
                                color: white!important;
    background-color: #5078B3!important;
    font-family: Arial, Sans-Serif!important;
    font-size: 14px!important;
    font-weight: bold!important;
    padding: 10px!important;
    margin-top: 5px!important;
    cursor: pointer!important;
                            }
                        </style>
                        <nav class="navbar navbar-light bg-light NavbarAcc">
 <%--<span>11. Salary</span>--%>
</nav>
                 
                                <br />

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        
                                        
                                       <%--          <asp:UpdateProgress ID="UpdateProgress4" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait11" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
                                                 <asp:HiddenField runat="server" ID="hfMasterId" />
                                                 <asp:HiddenField runat="server" ID="hdpkEmpId" />
                                        
                                                 <asp:HiddenField runat="server" ID="hffSignature" />
                                        
                                          <div class="row">
               <div class="col-md-1">
                   </div>
    
                        </div>
                                        
                                           <br />
                                        
                                        
                                        
                                          <div class="row">
                                  <div class="col-md-12">
                                        <asp:GridView Width="100%" ShowHeader="True" ID="gv_EmpListSearch" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                    
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" CssClass="form-control-sm"   runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                

                                                <asp:TemplateField HeaderText="Relative">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Relative" runat="server" Text='<%#Eval("Family") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                

                                 
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Family" runat="server" Text='<%#Eval("Family") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                

                                                
                                                <asp:TemplateField HeaderText="Death of Date">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server"  class="form-control form-control-sm"   ID="DeathofDate"></asp:TextBox>
                                                        
                                                        
                                                        <cc1:CalendarExtender ID="CalendarExtender" runat="server" PopupPosition="TopLeft"
                                                                              Format="dd-MMM-yyyy" PopupButtonID="ImageButtosn1" CssClass="MyCalendar"
                                                                              TargetControlID="DeathofDate" />
                                                        

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server"  class="form-control form-control-sm"   ID="txt_Remark"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                
                                            <%--    <asp:TemplateField HeaderText="Certificate Upload">
                                                    <ItemTemplate>
                                                        <asp:FileUpload ID="FileUpload1" runat="server"  Visible="False" />
                                                        <input type="file" name="SignatureFile" accept="*"  />
                                                        <input type="button" id="btnSignatureUpload" value="Upload" />

                                                  

                                                        <progress id="SignaturefileProgress" style="display: none"></progress>
                                                        <hr />

                                                        <asp:HiddenField runat="server" ID="hfSignature" />
                                                        <span id="lblMessageSignature" style="color: Green"></span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                
                                            </Columns>
                                        </asp:GridView>
                                  </div>
                              </div>

                                        <br/>
                                        

                                        <div class="form-row">
                                            <div class="col-md-10">

                                                <link href="ButtonGrups.css" rel="stylesheet" />
                                                <div class="ui-group-buttons">

                                                    <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="  Save  " OnClientClick="return confirm('Are you sure you want to Save ?')"
                                                        CssClass="btn btn-sm btn-info" />
                                                   <%-- <div class="or or-sm"></div>
                                                    <asp:Button runat="server" ID="btnNext" OnClick="btn_Next_OnClick" Text="   Save & Next   " OnClientClick="return confirm('Are you sure you want to Save & Next ?')" CssClass="btn btn-sm btn-success" />--%>

                                                    <div class="or or-sm"></div>
                                                    <asp:Button ID="cancelButton" Text="  Exit  " OnClick="cancelButton_OnClick" OnClientClick="return confirm('Are you sure you want to Exit ?')" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                                                </div>
                                            </div>

                                            <div class="col-md-2">
                                                <asp:LinkButton CssClass="hh previous pull-right" OnClick="lbPrevious_OnClick" ID="lbPrevious" runat="server">&laquo; Previous</asp:LinkButton>
                                                <%--<asp:LinkButton CssClass="hh next" runat="server" ID="lblNext" Visible="False" OnClick="lblNext_OnClick">Next &raquo;</asp:LinkButton>--%>

                                            </div>
                                        </div>
                                        
                                           </ContentTemplate>
                                </asp:UpdatePanel>

                            


            </div>
        </div>
    </div>



    <script type="text/javascript">


        $("body").on("click", "#btnSignatureUpload", function (val) {


            $.ajax({
                url: '/SignatureUploadHandler.ashx',
                type: 'POST',
                data: new FormData($('form')[0]),
                cache: false,
                contentType: false,
                processData: false,
                success: function (file) {

                            $("#lblMessageSignature").html("Signature has been uploaded.");
                            $('#cpFormBody_SignatureImage').prop({ src: '/UploadImg/' + file.dbfilename });
                            $('#cpFormBody_hffSignature').val(file.dbfilename);

                    //$("#lblMessageSignature").html("Signature has been uploaded.");
                    //$('#cpFormBody_SignatureImage').prop({ src: '/UploadImg/' + file.dbfilename });
                    //$('#cpFormBody_gv_EmpListSearch_hfSignature_0').val(file.dbfilename);

                },
                xhr: function () {
                    var fileXhr = $.ajaxSettings.xhr();
                    if (fileXhr.upload) {
                        $("#fileProgress").hide();
                        $("#NomineeImageProgress").hide();
                        $("#SignaturefileProgress").show();
                        fileXhr.upload.addEventListener("SignaturefileProgress", function (e) {
                            if (e.lengthComputable) {
                                $("#SignaturefileProgress").attr({
                                    value: e.loaded,
                                    max: e.total
                                });
                            }
                        }, false);
                    }
                    return fileXhr;
                }
            });
        });



        </script>
        <script>
            function MutExChkList(chk) {
                var chkList = chk.parentNode.parentNode.parentNode;
                var chks = chkList.getElementsByTagName("input");
                for (var i = 0; i < chks.length; i++) {
                    if (chks[i] != chk && chk.checked) {
                        chks[i].checked = false;
                    }
                }
            }
    </script>
</asp:Content>

