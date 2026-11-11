<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="MiscellaneousInformationViewDetailsAudit.aspx.cs" Inherits="MeetingMinors_MiscellaneousInformationViewDetailsAudit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">



    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>


    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <style>
        .table tbody th, .table thead th {
            background: #2F4F4F !important;
            background: -moz-linear-gradient(top, #637b7b 0%, #436060 66%, #2F4F4F 100%) !important;
            background: -webkit-linear-gradient(top, #637b7b 0%, #436060 66%, #2F4F4F 100%) !important;
            background: linear-gradient(to bottom, #637b7b 0%, #436060 66%, #2F4F4F 100%) !important;
            border-bottom: 2px solid #2F4F4F !important;
            padding: 10px 8px !important;
        }

        .table tbody td, .table thead td {
            padding: 10px 8px !important;
        }

        .tblTHColorChang {
            background-color: #EDF2F5 !important;
            font-weight: bold;
            font-size: 13px;
        }

        .Label_Title {
            background-color: #C7C7C7;
            width: 100%;
            text-align: center;
            margin: 0px;
            padding: 5px;
            text-align: center;
            color: #000;
            margin-right: 5%;
            font-weight: bold;
            font-size: 13px;
        }

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



        .chkChoice label {
            padding-left: 4px;
            padding-right: 4px;
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





    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog  modal-lg" role="document">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>

                    <div class="modal-content">
                        <div class="modal-header">



                            <h3 class="modal-title" id="exampleModalLabel" style="color: #2196F3; text-shadow: 0 0 1px black;">Routing Path</h3>




                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-2">
                                    <div class="form-group">
                                    </div>

                                </div>


                                <div class="col-md-12">

                                    <div class="Label_Title">Routing Path List</div>

                                    <br />
                                    <div class="form-group">
                                        <div style="overflow: scroll; height: 230px">
                                            <asp:RadioButtonList ID="rbRoutingPath" CssClass="chkChoice" RepeatColumns="5" RepeatDirection="Horizontal" runat="server" />
                                        </div>




                                    </div>


                                </div>



                            </div>


                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton runat="server" class="btn btn-success" OnClick="btnOkayRoutingPath_OnClick" ID="btnOkayRoutingPath">Okay</asp:LinkButton>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog  modal-lg" style="width: 90% !important;" role="document">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">



                            <h3 class="modal-title" id="exampleModalLabel2" style="color: #2196F3; text-shadow: 0 0 1px black;">Add More Member</h3>




                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-12">

                                    <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">Search Member</h2>

                                    <div class="row">


                                        <div class="col-md-1" style="padding-top: 8px">
                                            <label class="control-label">Division:</label></div>
                                        <div class="col-md-4">
                                            <asp:DropDownList runat="server" ID="ddlDivision" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" AutoPostBack="True" class="form-control form-control-sm"></asp:DropDownList>

                                            <%--<script type="text/javascript">
                                          function pageLoad() {
                                              $('#<%=ddlDivision.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });

                         $('#<%=ddlDepartment.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                     }
               </script>--%>
                                        </div>

                                        <div class="col-md-1" style="padding-top: 8px">
                                            <label class="control-label ">Department:</label></div>
                                        <div class="col-md-4">
                                            <asp:DropDownList runat="server" ID="ddlDepartment" class="form-control form-control-sm"></asp:DropDownList></div>
                                        <div class="col-md-2">
                                            <asp:LinkButton runat="server" ID="btnSearch" CssClass="btn btn-success   btn-sm" OnClick="btnSearch_OnClick"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>
                                        </div>

                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView Width="100%" ShowHeader="True" ID="gv_EmpListSearch" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL#">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="form-control-sm" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" CssClass="form-control-sm" runat="server" />

                                                            <asp:HiddenField runat="server" ID="hfEmpInfoId" Value='<%#Eval("EmpInfoId")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Employee ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_EmpMasterCode" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Employee Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_EmpName" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Designation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Designation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>



                                    <br />
                                    <div class="row">
                                        <div class="col-4"></div>
                                        <div class="col-4"></div>
                                        <div class="col-4">
                                            <asp:Button runat="server" CssClass="btn btn-outline-success btn-block disabled btn-sm" ID="btnAddToListEmp" OnClick="btnAddToListEmp_OnClick" Text="Add To List" />
                                        </div>
                                    </div>
                                </div>






                            </div>


                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="content" id="content">
        <div class="container-fluid">
            <div class="page-heading">
                <div class="page-heading__container">

                    <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                        <img src="../Report_Pages/app.png" width="20px" />
                        Document Information   </h1>
                </div>
                <div class="page-heading__container float-right d-none d-sm-block">
                    <asp:Button ID="detailsViewButton" Text="Back to List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                </div>
            </div>

            <div class="card">
                <div class="card-body">

                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            
                            

                            <asp:HiddenField runat="server" ID="id_mastetID" />

                            <div class="row">
                                <div class="col-md-12">
                                    <table class="table table-bordered table-striped">


                                        <tr>
                                            <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Company</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblCompany"></asp:Label></td>



                                        </tr>

                                        <tr>
                                            <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Title</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblTitle"></asp:Label></td>




                                        </tr>

                                        <tr>
                                            <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Purpose</td>
                                            <td>
                                                <asp:Label ID="lblPurpose" runat="server"></asp:Label></td>



                                        </tr>






                                        <tr>
                                            <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Document List</td>
                                            <td style="padding: 10px;">

                                                <asp:GridView Width="100%" ShowHeader="True" ID="gv_DocumentUpload" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex + 1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Document">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HLDocumentLink" Target="_blank" runat="server" NavigateUrl='<%# Eval("DocumentLink") %>' Text='Download'>
                                                                </asp:HyperLink>
                                                                <asp:Label ID="lbl_DocumentLink" Visible="False" runat="server" Text='<%#Eval("DocumentLink")%>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hfDocumentLink" Value='<%#Eval("DocumentLink")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Summary Note	">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_DocumentNote" runat="server" Text='<%#Eval("DocumentNote") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <%--                                                <asp:TemplateField HeaderText="Remove">
                                                    <ItemTemplate>
                                                      <asp:LinkButton runat="server" ID="btnDocRemove" OnClick="btnDocRemove_OnClick"   CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    </Columns>
                                                </asp:GridView>

                                            </td>


                                        </tr>


                                        <tr>
                                            <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Approval Status List</td>
                                            <td style="padding: 10px;">

                                                <asp:GridView Width="100%" ShowHeader="True" ID="gv_ApprovalList" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex + 1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PreEmp" HeaderText="Initiator" HtmlEncode="False" />

                                                        <asp:BoundField DataField="Comments" HeaderText="Comments" HtmlEncode="False" />

                                                        <asp:BoundField DataField="ForEmp" HeaderText="Approval Person" HtmlEncode="False" />

                                                        <asp:BoundField DataField="ActionStatus" HeaderText="Action Status" HtmlEncode="False" />

                                                        <%--<asp:BoundField DataField="Version" HeaderText="Version" HtmlEncode="False" />--%>



                                                        <asp:BoundField DataField="ApproveBy" HeaderText="Approved By" HtmlEncode="False" />
                                                        <asp:BoundField DataField="ApprovedDate" HeaderText="Approved Date" DataFormatString="{0:dd-MMM-yyyy}" />



                                                    </Columns>
                                                </asp:GridView>

                                            </td>


                                        </tr>


                                    </table>
                                </div>
                            </div>
                            <div class="row" runat="server" visible="False">




                                <div class="col-md-3">
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group ">


                                        <div class="row">
                                            <div class="col-md-3" style="padding-top: 8px">
                                                <label class="control-label pull-right">Company:</label></div>
                                            <div class="col-md-6">
                                                <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" class="form-control form-control-sm" /></div>


                                        </div>
                                        <div style="padding-top: 5px;"></div>
                                        <%--  <div style="padding-top: 5px;"></div>--%>
                                        <div class="row">
                                            <div class="col-md-3" style="padding-top: 8px">
                                                <label class="control-label pull-right">Title:</label></div>
                                            <div class="col-md-6">
                                                <asp:TextBox runat="server" ID="txtTitle" class="form-control form-control-sm" /></div>
                                        </div>
                                        <div style="padding-top: 5px;"></div>
                                        <div class="row">
                                            <div class="col-md-3" style="padding-top: 8px">
                                                <label class="control-label pull-right">Purpose:</label></div>
                                            <div class="col-md-6">
                                                <asp:TextBox runat="server" TextMode="MultiLine" Rows="2" ID="txtPurpose" class="form-control  " /></div>
                                        </div>




                                    </div>
                                </div>
                            </div>




                            <fieldset class="for-panel" runat="server" visible="False">
                                <legend>Document </legend>
                                <div class="row">





                                    <asp:HiddenField runat="server" ID="hfDocFile" />
                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Document Upload</label>
                                            <div>
                                                <input type="file" name="postedFile" class="form-control form-control-sm" />
                                                <asp:FileUpload ID="FUDocument" Visible="False" CssClass="form-control form-control-sm" runat="server" />
                                                <br />
                                                <input type="button" class="btn btn-sm  btn-info" id="btnUpload" value="Upload Document" />
                                                <asp:LinkButton runat="server" Visible="False" OnClick="btnDocUp_OnClick" ID="btnDocUp" CssClass="btn btn-sm  btn-info">
                                                          
                                                      
              &nbsp;    <span class="btn-label"><i class="fa fa-upload"></i></span>  &nbsp;   &nbsp;Upload Document
                                                </asp:LinkButton>
                                                <progress id="fileProgress" style="display: none"></progress>
                                                <span id="lblMessage" style="color: Green"></span>
                                                <asp:HyperLink ID="HyperLink2" runat="server"
                                                    Target="_blank" ToolTip="Click to Show Document"></asp:HyperLink>



                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Summary Note</label>
                                            <div>

                                                <asp:TextBox runat="server" ID="txtSummaryNote" TextMode="MultiLine" Rows="2" class="form-control" />

                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="brnAddDoc" OnClick="brnAddDoc_OnClick" CssClass="btn btn-success   btn-sm pull-right"><span aria-hidden="true" class="fa fa-plus"></span>  &nbsp; Add to List </asp:LinkButton>
                                        </div>
                                    </div>

                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-8">
                                    </div>
                                </div>
                            </fieldset>

                            <div class="row" runat="server" visible="False">

                                <div class="col-md-3">
                                    <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">Approval Path Setup</h2>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">

                                        <asp:LinkButton runat="server" ID="LinkButtaon2" data-toggle="modal" data-target="#exampleModal" CssClass="btn btn-sm btn-success">Choose Approval Path  </asp:LinkButton>
                                    </div>

                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">

                                        <asp:LinkButton runat="server" ID="LinkButton3" data-toggle="modal" data-target="#exampleModal2" CssClass="btn btn-sm btn-secondary">Add More Member  </asp:LinkButton>
                                    </div>

                                </div>

                            </div>




                            <div class="row" runat="server" visible="False">


                                <div class="col-md-12">

                                    <asp:GridView Width="100%" ShowHeader="True" ID="gv_Details_Save" runat="server" AutoGenerateColumns="false" CssClass="blueTableNew" OnPreRender="gv_DocumentUpload_PreRender">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Mimimum Count for Approval">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMimimumCount" CssClass="form-control-sm" runat="server" />


                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sequence">
                                                <ItemTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlSequenceList" class="form-control form-control-sm">


                                                        <asp:ListItem Value="1"> 1  </asp:ListItem>
                                                        <asp:ListItem Value="2">2 </asp:ListItem>
                                                        <asp:ListItem Value="3">3 </asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Employee ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="Slbl_EmpMasterCode" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>

                                                    <asp:HiddenField runat="server" ID="ShfEmpInfoId" Value='<%#Eval("EmpInfoId")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Employee Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="Slbl_EmpName" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="Slbl_Designation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Is Edit">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkIsEdit" CssClass="form-control-sm" runat="server" />


                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remove">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btn_DetailsRemove" OnClick="btn_DetailsRemove_OnClick" CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkEmail" CssClass="form-control-sm" runat="server" />


                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="SMS">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSMS" CssClass="form-control-sm" runat="server" />


                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>


                                    <br />
                                </div>

                            </div>

                            <br />
                            <div class="row" runat="server" visible="False">


                                <div class="col-md-4">
                                    <div class="form-row">

                                        <div class="col-md-6">
                                            <asp:LinkButton ID="submitButton" OnClick="btnSave_OnClick" Visible="False" OnClientClick="return confirm('Are you sure you want to Save ?')" CssClass="btn btn-sm btn-info" runat="server"> <i class="fa fa-floppy-o" aria-hidden="true"></i>
 &nbsp;Save</asp:LinkButton>


                                            <asp:LinkButton ID="editButton" OnClientClick="return confirm('Are you sure you want to Update ?')" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick"><i class="fa fa-pencil-square-o" aria-hidden="true"></i>&nbsp; Update</asp:LinkButton>
                                            <asp:LinkButton ID="delButton" OnClientClick="return confirm('Are you sure you want to Delete ?')" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick"><i class="fa fa-trash-o" aria-hidden="true"></i>&nbsp; Delete</asp:LinkButton>

                                        </div>

                                    </div>
                                    <div class="form-group">
                                    </div>
                                </div>

                                <div class="col-md-4">
                                </div>
                            </div>



                        </ContentTemplate>



                        <Triggers>

                            <asp:PostBackTrigger ControlID="btnDocUp" />

                        </Triggers>
                    </asp:UpdatePanel>



                </div>

            </div>
        </div>

    </div>


    <script type="text/javascript">
       $("body").on("click", "#btnUpload", function () {
                                   debugger;
                                   $.ajax({
                                       url: '/HandlerDoc.ashx',
                                       type: 'POST',
                                       data: new FormData($('form')[0]),
                                       cache: false,
                                       contentType: false,
                                       processData: false,
                                       success: function (file) {
                                           $("#cpFormBody_hfDocFile").val('');
                                           $("#fileProgress").hide();
                                           $("#lblMessage").html("<b>" + file.dbfilename + "</b> has been uploaded.");
                                           $("#cpFormBody_hfDocFile").val(file.dbfilename);
                 
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
                                   });
                                   </script>
                               </asp:Content>

