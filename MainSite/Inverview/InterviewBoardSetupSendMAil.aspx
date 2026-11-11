<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" validateRequest="false" AutoEventWireup="true" CodeFile="InterviewBoardSetupSendMAil.aspx.cs" Inherits="Inverview_InterviewBoardSetupSendMAil" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
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
            #cpFormBody_gv_selectedEmp  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #07619D;
            /*background-color: #98A9C0;*/
        }

        #cpFormBody_gv_selectedEmp > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

          #cpFormBody_gvGrading  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #07619D;
            /*background-color: #98A9C0;*/
        }

        #cpFormBody_gvGrading > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
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
                    <div class="page-heading" style="background-color: #F0FFFF; font-style: italic">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"> <img src="../Assets/img/gmail.png" /> Interview Committee Notification</h1>
                        </div>
                        <%--  <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Training Records" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                        </div>--%>

                        <div class="page-heading__container float-right d-none d-sm-block">
                       
                            
                             <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="ListViewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                        </div>

                    </div>
                    <div class="card">
                        <div class="card-body">
                             <div id="coverScreen" class="LockOn">
                </div>
                               <div class="page-header text-center">
                            <h2 class="elegantshd">
                                <asp:Label ID="lblHeader" runat="server">Borad Member Information</asp:Label></h2>
                        </div>
                        <br />
                               <asp:HiddenField runat="server" ID="hdpk" />
      <asp:GridView ID="gv_selectedEmp" Width="100%"   CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"  DataKeyNames="BoardDetailsId" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Employee ID">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_empCode" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>

                                
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">


                                    <ItemTemplate>
                                        <asp:Label ID="txt_employee" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpName") %>'></asp:Label>



                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mail">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_OfficialEmail" runat="server" class="form-control form-control-sm" Text='<%#Eval("OfficialEmail") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            


                            </Columns>
                        </asp:GridView>
                            
                            
                            <br/>
                            <br/>
                               <div class="page-header text-center">
                            <h2 class="elegantshd">
                                <asp:Label ID="Label1" runat="server"> Candidate Summery</asp:Label></h2>
                        </div>
                        <br />
                             <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"  DataKeyNames="CandidateID"
                                    OnRowCommand="loadGridView_RowCommand" OnRowCreated="loadGridView_OnRowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:BoundField DataField="ShortName" HeaderText="Company" Visible="False" />
                                        <asp:BoundField DataField="CandidateName" HeaderText="Candidate Name" />
                                        <asp:BoundField DataField="PhoneNo" HeaderText="Phone No." />
                                        <asp:BoundField DataField="EmailAdress" HeaderText="Email Adress" />
                                        <asp:BoundField DataField="Age" HeaderText="Age" />

                                        <asp:BoundField DataField="Address" Visible="False" HeaderText="Address" />
                                     
                                        <asp:BoundField DataField="LastPassingYear" HeaderText="Passing Year" />   

                                        <asp:BoundField DataField="LastResultCGPA" HeaderText="Last Result" />   
                                        <asp:BoundField DataField="TotalYearsOfExp" Visible="False" HeaderText="Years Of Exp" />
                                        <asp:BoundField DataField="ExpectedSalary" HeaderText="Expected Salary" />
                                    
                                 


                                    </Columns>
                                </asp:GridView>
                            <br/>
                            <br/>
                         <div class="page-header text-center">
                            <h2 class="elegantshd">
                                <asp:Label ID="Label4" runat="server">Score Sheet</asp:Label></h2>   
                        </div>
                              <div style="overflow: scroll;">
                               <asp:GridView ID="gvGrading" runat="server" AutoGenerateColumns="False" OnRowDataBound="loadGridView_OnRowDataBound" 
                                    CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"  DataKeyNames="CandidateID,JobID">
                                        
                                      
                                </asp:GridView>
                            <%-- <div id="tbl_container" style="width:100%" class="">

                        </div>--%></div>
                            
                                 <div class="row">
                                <div class="col-md-6">
                                     <div class="page-header text-left">
                                    <h2 class="elegantshd">
                                             <br/>
                                        <asp:Label ID="Label2" runat="server">Mail Body</asp:Label>
                                    </h2>
                                </div>
                                    <FTB:FreeTextBox ID="txtMailBody"  Height="250" runat="server"></FTB:FreeTextBox>
                                </div>
                                 <div class="col-3">
                                     <br/>
                                     <br/>
                                       <div class="page-header text-left">
                                    <h2 class="elegantshd">
                                        <asp:Label ID="Label3" runat="server">Attachment</asp:Label></h2>
                                </div>
                                        <div class="form-group files">
                                            
                                            
                                                <asp:FileUpload ID="fu_cv"  CssClass="form-control" runat="server" />

                                            
                                        </div>
                                       <br/>
                                      <asp:Button ID="submitButton" Text="Send Mail" CssClass="btn btn-sm btn-info" Visible="True" BackColor="#4183F1"   runat="server" OnClick="submitButton_Click" />
                                    </div>
                            </div>

                        </div>
                            
                       
                           
                        </div>
    </div>
        <script>
            $(window).on('load', function () {
                $("#coverScreen").hide();
            });


            $("#cpFormBody_submitButton").click(function () {
                $("#coverScreen").show();
            });
            </script>
</asp:Content>

