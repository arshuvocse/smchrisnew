<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingReport.aspx.cs" Inherits="Report_Pages_TrainingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">


    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">

                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Disciplinary Action Report </h1>
                    </div>
                    <%--<div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>--%>
                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company Name </label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" runat="server"></asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Training Start Date </label>
                                        <label style="color: #a52a2a">* </label>
                                        <asp:DropDownList ID="actionDate" runat="server" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="actionDate_SelectedIndexChanged">
                                            <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                            <asp:ListItem Value="2"> = </asp:ListItem>
                                            <asp:ListItem Value="3"> < </asp:ListItem>
                                            <asp:ListItem Value="4"> > </asp:ListItem>
                                            <asp:ListItem Value="5"> Between </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2" runat="server" id="singleDate" visible="False">
                                    <div class="form-group">
                                        <label>Date: </label>
                                        <asp:TextBox ID="dateTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                            TargetControlID="dateTextBox" />
                                    </div>
                                </div>


                                <div runat="server" id="dateRange" visible="False" class="col-md-4">

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>From Date: </label>
                                                <asp:TextBox ID="fromDateTextbox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                    TargetControlID="fromDateTextbox" />
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>To Date: </label>
                                                <asp:TextBox ID="toDateTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                    TargetControlID="toDateTextBox" />
                                            </div>
                                        </div>
                                    </div>





                                </div>
                            </div>


                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Training End Date </label>
                                        <label style="color: #a52a2a">* </label>
                                        <asp:DropDownList ID="endDateDropDownList" runat="server" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="endDateDropDownList_SelectedIndexChanged">
                                            <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                            <asp:ListItem Value="2"> = </asp:ListItem>
                                            <asp:ListItem Value="3"> < </asp:ListItem>
                                            <asp:ListItem Value="4"> > </asp:ListItem>
                                            <asp:ListItem Value="5"> Between </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2" runat="server" id="singleEndDate" visible="False">
                                    <div class="form-group">
                                        <label>Date: </label>
                                        <asp:TextBox ID="endDateTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                            TargetControlID="dateTextBox" />
                                    </div>
                                </div>


                                <div runat="server" id="endDateRange" visible="False" class="col-md-4">

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>From Date: </label>
                                                <asp:TextBox ID="endFromDateTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server"
                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                    TargetControlID="fromDateTextbox" />
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>To Date: </label>
                                                <asp:TextBox ID="endToDateTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server"
                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                    TargetControlID="toDateTextBox" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>



                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Training Dayes </label>
                                        <label style="color: #a52a2a">* </label>
                                        <asp:DropDownList ID="trainingDayesDropDownList" runat="server" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="trainingDayesDropDownList_SelectedIndexChanged">
                                            <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                            <asp:ListItem Value="2"> = </asp:ListItem>
                                            <asp:ListItem Value="3"> < </asp:ListItem>
                                            <asp:ListItem Value="4"> > </asp:ListItem>
                                            <asp:ListItem Value="5"> Between </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2" runat="server" id="noOfDay" visible="False">
                                    <div class="form-group">
                                        <label>Number of Days: </label>
                                        <asp:TextBox ID="noOfDaysTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                    </div>
                                </div>


                                <div runat="server" id="dayesRange" visible="False" class="col-md-4">

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Minimum </label>
                                                <asp:TextBox ID="minTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>

                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Maximum </label>
                                                <asp:TextBox ID="maxTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Training Fees </label>
                                        <label style="color: #a52a2a">* </label>
                                        <asp:DropDownList ID="feesDropDownList" runat="server" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="feesDropDownList_SelectedIndexChanged">
                                            <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                            <asp:ListItem Value="2"> = </asp:ListItem>
                                            <asp:ListItem Value="3"> < </asp:ListItem>
                                            <asp:ListItem Value="4"> > </asp:ListItem>
                                            <asp:ListItem Value="5"> Between </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2" runat="server" id="singleFee" visible="False">
                                    <div class="form-group">
                                        <label>Amount: </label>
                                        <asp:TextBox ID="TextBox1" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                    </div>
                                </div>


                                <div runat="server" id="feeRange" visible="False" class="col-md-4">

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Minimum Amount </label>
                                                <asp:TextBox ID="TextBox2" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>

                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Maximum Amount </label>
                                                <asp:TextBox ID="TextBox3" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Country </label>
                                     
                                        <asp:DropDownList ID="discActionDropDownList" class="form-control form-control-sm" runat="server">
                                            <asp:ListItem> In </asp:ListItem>
                                            <asp:ListItem> Not In </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="color: #fff;">Date: </label>
                                        <asp:TextBox ID="countryTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                            TargetControlID="dateTextBox" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Training type </label>
                                        <label style="color: #a52a2a"></label>
                                        <asp:DropDownList ID="trainingTypeDropDownList" class="form-control form-control-sm" runat="server">
                                            <asp:ListItem> In </asp:ListItem>
                                            <asp:ListItem> Not In </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="color: #fff;">Date: </label>
                                        <asp:TextBox ID="trainingTypeTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                            TargetControlID="dateTextBox" />
                                    </div>
                                </div>
                            </div>
                            
                            <div class="row">
                                
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label> Score For </label>
                                     
                                        <asp:DropDownList ID="scoreDropDownList" class="form-control form-control-sm" runat="server">
                                            <asp:ListItem> None </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label> Score  </label>
                                        <label style="color: #a52a2a">* </label>
                                        <asp:DropDownList ID="scoreForDropDownList" runat="server" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="scoreForDropDownList_SelectedIndexChanged">
                                            <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                            <asp:ListItem Value="2"> = </asp:ListItem>
                                            <asp:ListItem Value="3"> < </asp:ListItem>
                                            <asp:ListItem Value="4"> > </asp:ListItem>
                                            <asp:ListItem Value="5"> Between </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2" runat="server" id="singleScore" visible="False">
                                    <div class="form-group">
                                        <label>Score: </label>
                                        <asp:TextBox ID="scoreTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                    </div>
                                </div>


                                <div runat="server" id="scoreRange" visible="False" class="col-md-4">

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Minimum Score </label>
                                                <asp:TextBox ID="minScoreTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Maximum Score </label>
                                                <asp:TextBox ID="maxScoreTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                            
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label> Announcement Year  </label>
                                        <label style="color: #a52a2a">* </label>
                                        <asp:DropDownList ID="yearDropDownList" runat="server" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="yearDropDownList_SelectedIndexChanged">
                                            <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                            <asp:ListItem Value="2"> = </asp:ListItem>
                                            <asp:ListItem Value="3"> < </asp:ListItem>
                                            <asp:ListItem Value="4"> > </asp:ListItem>
                                            <asp:ListItem Value="5"> Between </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2" runat="server" id="singleYear" visible="False">
                                    <div class="form-group">
                                        <label> Year: </label>
                                        <asp:TextBox ID="TextBox4" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                    </div>
                                </div>


                                <div runat="server" id="yearRange" visible="False" class="col-md-4">

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label> From </label>
                                                <asp:TextBox ID="fromYearTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label> To </label>
                                                <asp:TextBox ID="toYearTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <br />
                            <br />


                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" runat="server" OnClick="submitButton_Click" />
                                        <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />
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

