<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="TrainingRecordSendMAil.aspx.cs" Inherits="Training_TrainingRecordSendMAil" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style>
        .files input {
            outline: 2px dashed #92b0b3;
            outline-offset: -10px;
            -webkit-transition: outline-offset .15s ease-in-out, background-color .15s linear;
            transition: outline-offset .15s ease-in-out, background-color .15s linear;
            padding: 30px 0px 60px 10%;
            text-align: center !important;
            margin: 0;
            width: 400px !important;
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
                width: 250px;
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
            width: 250px;
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
    </style>
    <style>
        #cpFormBody_gv_selectedEmp > tbody > tr > th {
            padding: 9px ;
            color: #fff;
            background-color: #07619D;
            /*background-color: #98A9C0;*/
        }

         #cpFormBody_gv_selectedEmp > tbody > tr > td {
            padding: 7px ;
            
            /*background-color: #98A9C0;*/
        }

        
    </style>

    <style>
        .elegantshd {
            color: #131313;
            letter-spacing: .15em;
            text-shadow: 2px 2px 4px #000000;
            font-family: 'Kreon', serif;
            vertical-align: middle;
            text-decoration-style: wavy;
        }


        .elegantshd2 {
            color: #131313;
            letter-spacing: .15em;
            text-shadow: 2px 2px 4px #000000;
            font-family: 'Kreon', serif;
            vertical-align: auto;
            text-decoration-style: wavy;
        }




        .LockOn {
            position: fixed;
            left: 0px;
            top: 0px;
            z-index: 2147483647;
            width: 100%;
            height: 2215px;
            background-color: #676767;
            vertical-align: bottom;
            padding-top: 20%;
            filter: alpha(opacity=85);
            opacity: 0.85;
            font-size: large;
            color: #676767;
            font-style: italic;
            font-weight: 400;
            background-image: url("../Assets/img/LatestLoading.gif");
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-position: center;
        }
    </style>

    <div class="container-fluid">
        <div class="page-heading">
            <div class="page-heading__container">
                <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                   <img src="../Assets/img/email-1.png" width="20px"  />
                    Training Invitation</h1>
            </div>
            <%--  <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Training Records" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                        </div>--%>

            <div class="page-heading__container float-right d-none d-sm-block">
                <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                <asp:Button ID="ListViewButton" Visible="True" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
            </div>

        </div>
        <div class="card">
            <div class="card-body">

                <div id="coverScreen" class="LockOn">
                </div>

                <div class="row">
                    <div class="col-md-7">
                        <asp:HiddenField runat="server" ID="hdpk" />
                        <br />
                        <div class="page-header text-center">
                            <h3 class="elegantshd">
                                <asp:Label ID="lblHeader" runat="server">Participant Information</asp:Label></h3>
                        </div>
                        <br />
                        <hr/>
                         <div style="overflow: scroll; height: 500px; width: 100%">
                        <asp:GridView ID="gv_selectedEmp" Width="100%" CssClass="table table-bordered text-center thead-dark" DataKeyNames="TrainingRecordMasterId" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Employee ID">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_empCode" runat="server"     Text='<%#Eval("EmpMasterCode") %>'></asp:Label>


                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">


                                    <ItemTemplate>
                                        <asp:Label ID="txt_employee" runat="server"     Text='<%#Eval("EmpName") %>'></asp:Label>



                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mail ID">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_OfficialEmail" runat="server"     Text='<%#Eval("OfficialEmail") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                
                                <asp:TemplateField HeaderText="Reporting Employee ID">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_ReportingMasterCode" runat="server"     Text='<%#Eval("ReportingMasterCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reporting Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_ReportingEmp" runat="server"     Text='<%#Eval("ReportingEmp") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             

                                   <asp:TemplateField HeaderText="Reporting Mail ID">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_ReportingEmail" runat="server"     Text='<%#Eval("ReportingEmail") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                             </div>

                    </div>

                    <div class="col-md-5">
                   
                        <div class="row">
                            <div class="col-md-12">
                                <div class="page-header text-left">
                                    <h3 class="elegantshd">
                                             <br/>
                                        <asp:Label ID="Label1" runat="server">Mail Body</asp:Label>
                                    </h3>
                                </div>

                                <FTB:FreeTextBox ID="txtMailBody" Height="250" Width="100%" runat="server"></FTB:FreeTextBox>
                            </div>


                            <div class="col-md-12">
                                <br />
                                <div class="page-header text-left">
                                    <h3 class="elegantshd">
                                        <asp:Label ID="Label2" runat="server">Attachment</asp:Label></h3>
                                </div>

                                <div class="form-group files">


                                    <asp:FileUpload ID="fu_cv" CssClass="form-control" runat="server" />


                                </div>

                            </div>
                        </div>

                        <div class="row">
                            <div class="col-3">



                                <asp:LinkButton ID="submitButton" Text="Send Mail" CssClass="btn btn-sm btn-info" Visible="True" BackColor="#4183F1" runat="server" OnClick="submitButton_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <%--  <script type="text/javascript">
        var width = 10;

        $(document).ready(function () {
            setTimeout("$('somediv').html('width,'" + width + 10 + "')", 600);
        });--%>

            <%--</script>--%>

            <script>
                $(window).on('load', function () {
                    $("#coverScreen").hide();
                });


                $("#cpFormBody_submitButton").click(function () {
                    $("#coverScreen").show();
                });
            </script>
        </div>
    </div>
</asp:Content>

