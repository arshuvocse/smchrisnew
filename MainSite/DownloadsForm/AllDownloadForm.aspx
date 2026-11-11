<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="AllDownloadForm.aspx.cs" Inherits="DownloadsForm_AllDownloadForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <style>
        .wrimagecard {
            margin-top: 0;
            margin-bottom: 1.5rem;
            text-align: left;
            position: relative;
            background: #fff;
            box-shadow: 12px 15px 20px 0px rgba(46,61,73,0.15);
            border-radius: 4px;
            transition: all 0.3s ease;
        }

            .wrimagecard .fa {
                position: relative;
                font-size: 70px;
            }

        .wrimagecard-topimage_header {
            padding: 20px;
        }


        a.wrimagecard:hover, .wrimagecard-topimage:hover {
            box-shadow: 2px 4px 8px 0px rgba(46,61,73,0.2);
        }

        .wrimagecard-topimage a {
            width: 100%;
            height: 100%;
            display: block;
        }

        .wrimagecard-topimage_title {
            padding: 10px 14px;
            height: 40px;
        }

        .wrimagecard-topimage a {
            border-bottom: none;
            text-decoration: none;
            color: #525c65;
            transition: color 0.3s ease;
        }


        .gradient-text {
            text-shadow: .15em .15em .35em #888;
            font-size: 15px;
        }
    </style>
    <div class="content" id="content">
        <div class="page-heading">
            <div class="page-heading__container">
                <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                <span></span>
                <h1 class="title" style="font-size: 18px; padding-top: 9px;">Download Forms</h1>
            </div>
            <div class="page-heading__container float-right d-none d-sm-block">
                <br />
            </div>

        </div>

        <div class="container-fluid">
            <div class="card">
                <div class="card-body">

                    <div class="container-fluid">

                        <div class="row">
                            <div class="col-md-4 col-sm-4">
                                <div class="wrimagecard wrimagecard-topimage">
                                    <a href="#">
                                        <div class="wrimagecard-topimage_header" style="background-color: rgba(187, 120, 36, 0.1)">
                                            <center><i  style="color:#BB7824" class="gradient-text">Application for Car Loan</i></center>
                                        </div>
                                        <div class="wrimagecard-topimage_title">
                                            <h4 style="margin-top: -15px;"><a href="Application_for_Car_Loan.doc"><i class="fa fa-download" aria-hidden="true" style="color: #BB7824; font-size: 16px"></i>Download</a> </h4>
                                        </div>
                                    </a>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="wrimagecard wrimagecard-topimage">
                                    <a href="#">
                                        <div class="wrimagecard-topimage_header" style="background-color: rgba(22, 160, 133, 0.1)">
                                            <center><i  style="color:#16A085" class="gradient-text">RECOMMENDATION FOR UPGRADATION</i></center>
                                        </div>
                                        <div class="wrimagecard-topimage_title">
                                            <h4 style="margin-top: -15px;"><a href="RECOMMENDATION_FOR_UPGRADATION.docx"><i class="fa fa-download" aria-hidden="true" style="color: #BB7824; font-size: 16px"></i>Download</a> </h4>
                                        </div>
                                    </a>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="wrimagecard wrimagecard-topimage">
                                    <a href="#">
                                        <div class="wrimagecard-topimage_header" style="background-color: rgba(213, 15, 37, 0.1)">
                                            <center><i   style="color:#d50f25" class="gradient-text">RECOMMENDATION FOR PROMOTION </i></center>
                                        </div>
                                        <div class="wrimagecard-topimage_title">
                                            <h4 style="margin-top: -15px;"><a href="RECOMMENDATION_FOR_PROMOTION.doc"><i class="fa fa-download" aria-hidden="true" style="color: #BB7824; font-size: 16px"></i>Download</a> </h4>
                                        </div>

                                    </a>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-sm-4">
                                <div class="wrimagecard wrimagecard-topimage">
                                    <a href="#">
                                        <div class="wrimagecard-topimage_header" style="background-color: rgba(51, 105, 232, 0.1)">
                                            <center><i   style="color:#3369e8" class="gradient-text">EMPLOYEES’ PROVIDENT FUND FORM OF NOMINATION </i></center>
                                        </div>
                                        <div class="wrimagecard-topimage_title">
                                            <h4 style="margin-top: -15px;"><a href="EMPLOYEES’_PROVIDENT_FUND_FROM_OF_NOMINATION.doc"><i class="fa fa-download" aria-hidden="true" style="color: #BB7824; font-size: 16px"></i>Download</a> </h4>
                                        </div>

                                    </a>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="wrimagecard wrimagecard-topimage">
                                    <a href="#">
                                        <div class="wrimagecard-topimage_header" style="background-color: rgba(250, 188, 9, 0.1)">
                                            <center><i   style="color:black" class="gradient-text">অঙ্গিকারনামা</i></center>
                                        </div>
                                        <div class="wrimagecard-topimage_title">
                                            <h4 style="margin-top: -15px;"><a href="অঙ্গিকারনামা.docx"><i class="fa fa-download" aria-hidden="true" style="color: #BB7824; font-size: 16px"></i>Download</a> </h4>
                                        </div>

                                    </a>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="wrimagecard wrimagecard-topimage">
                                    <a href="#">
                                        <div class="wrimagecard-topimage_header" style="background-color: rgba(121, 90, 71, 0.1)">
                                            <center><i  style="color:#795a47" class="gradient-text">PRE EMPLOYMENT REFERENCE CHECK FORM </i></center>
                                        </div>
                                        <div class="wrimagecard-topimage_title">
                                            <h4 style="margin-top: -15px;"><a href="PRE_EMPLOYMENT_REFERENCE_CHECK_FORM.docx"><i class="fa fa-download" aria-hidden="true" style="color: #BB7824; font-size: 16px"></i>Download</a> </h4>
                                        </div>

                                    </a>
                                </div>
                            </div>

                        </div>



                        <div class="row">
                            <div class="col-md-4 col-sm-4">
                                <div class="wrimagecard wrimagecard-topimage">
                                    <a href="#">
                                        <div class="wrimagecard-topimage_header" style="background-color: rgba(51, 105, 232, 0.1)">
                                            <center><i   style="color:#3369e8" class="gradient-text">Appointment Letter </i></center>
                                        </div>
                                        <div class="wrimagecard-topimage_title">
                                            <h4 style="margin-top: -15px;"><a href="APT- Management employee.doc"><i class="fa fa-download" aria-hidden="true" style="color: #BB7824; font-size: 16px"></i>Download</a> </h4>
                                        </div>

                                    </a>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="wrimagecard wrimagecard-topimage">
                                    <a href="#">
                                        <div class="wrimagecard-topimage_header" style="background-color: rgba(250, 188, 9, 0.1)">
                                            <center><i   style="color:black" class="gradient-text"> Experience Certificate</i></center>
                                        </div>
                                        <div class="wrimagecard-topimage_title">
                                            <h4 style="margin-top: -15px;"><a href="Experience Certificate.doc"><i class="fa fa-download" aria-hidden="true" style="color: #BB7824; font-size: 16px"></i>Download</a> </h4>
                                        </div>

                                    </a>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="wrimagecard wrimagecard-topimage">
                                    <a href="#">
                                        <div class="wrimagecard-topimage_header" style="background-color: rgba(121, 90, 71, 0.1)">
                                            <center><i  style="color:#795a47" class="gradient-text">Letter of Introduction for AC Opening </i></center>
                                        </div>
                                        <div class="wrimagecard-topimage_title">
                                            <h4 style="margin-top: -15px;"><a href="Letter of Introduction for AC Opening.docx"><i class="fa fa-download" aria-hidden="true" style="color: #BB7824; font-size: 16px"></i>Download</a> </h4>
                                        </div>

                                    </a>
                                </div>
                            </div>

                        </div>



                        <div class="row">
                            <div class="col-md-4 col-sm-4">
                                <div class="wrimagecard wrimagecard-topimage">
                                    <a href="#">
                                        <div class="wrimagecard-topimage_header" style="background-color: rgba(51, 105, 232, 0.1)">
                                            <center><i   style="color:#3369e8" class="gradient-text">No Objection Certificate </i></center>
                                        </div>
                                        <div class="wrimagecard-topimage_title">
                                            <h4 style="margin-top: -15px;"><a href="No Objection Certificate.docx"><i class="fa fa-download" aria-hidden="true" style="color: #BB7824; font-size: 16px"></i>Download</a> </h4>
                                        </div>

                                    </a>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="wrimagecard wrimagecard-topimage">
                                    <a href="#">
                                        <div class="wrimagecard-topimage_header" style="background-color: rgba(250, 188, 9, 0.1)">
                                            <center><i   style="color:black" class="gradient-text">NOC of Upgradation</i></center>
                                        </div>
                                        <div class="wrimagecard-topimage_title">
                                            <h4 style="margin-top: -15px;"><a href="NOC of Upgradation.docx"><i class="fa fa-download" aria-hidden="true" style="color: #BB7824; font-size: 16px"></i>Download</a> </h4>
                                        </div>

                                    </a>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="wrimagecard wrimagecard-topimage">
                                    <a href="#">
                                        <div class="wrimagecard-topimage_header" style="background-color: rgba(121, 90, 71, 0.1)">
                                            <center><i  style="color:#795a47" class="gradient-text">Obituary Messege </i></center>
                                        </div>
                                        <div class="wrimagecard-topimage_title">
                                            <h4 style="margin-top: -15px;"><a href="Obituary Messege.docx"><i class="fa fa-download" aria-hidden="true" style="color: #BB7824; font-size: 16px"></i>Download</a> </h4>
                                        </div>

                                    </a>
                                </div>
                            </div>

                        </div>


                        <div class="row">
                            <div class="col-md-4 col-sm-4">
                                <div class="wrimagecard wrimagecard-topimage">
                                    <a href="#">
                                        <div class="wrimagecard-topimage_header" style="background-color: rgba(51, 105, 232, 0.1)">
                                            <center><i   style="color:#3369e8" class="gradient-text">PF Form Existing </i></center>
                                        </div>
                                        <div class="wrimagecard-topimage_title">
                                            <h4 style="margin-top: -15px;"><a href="PF Form Existing.docx"><i class="fa fa-download" aria-hidden="true" style="color: #BB7824; font-size: 16px"></i>Download</a> </h4>
                                        </div>

                                    </a>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="wrimagecard wrimagecard-topimage">
                                    <a href="#">
                                        <div class="wrimagecard-topimage_header" style="background-color: rgba(250, 188, 9, 0.1)">
                                            <center><i   style="color:black" class="gradient-text">Re-appointment letter</i></center>
                                        </div>
                                        <div class="wrimagecard-topimage_title">
                                            <h4 style="margin-top: -15px;"><a href="Re-appointment letter.doc"><i class="fa fa-download" aria-hidden="true" style="color: #BB7824; font-size: 16px"></i>Download</a> </h4>
                                        </div>

                                    </a>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="wrimagecard wrimagecard-topimage">
                                    <a href="#">
                                        <div class="wrimagecard-topimage_header" style="background-color: rgba(121, 90, 71, 0.1)">
                                            <center><i  style="color:#795a47" class="gradient-text">Reference Check From </i></center>
                                        </div>
                                        <div class="wrimagecard-topimage_title">
                                            <h4 style="margin-top: -15px;"><a href="Reference Check From.doc"><i class="fa fa-download" aria-hidden="true" style="color: #BB7824; font-size: 16px"></i>Download</a> </h4>
                                        </div>

                                    </a>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-md-4 col-sm-4">
                                <div class="wrimagecard wrimagecard-topimage">
                                    <a href="#">
                                        <div class="wrimagecard-topimage_header" style="background-color: rgba(121, 90, 71, 0.1)">
                                            <center><i  style="color:#795a47" class="gradient-text">Sample of Circular Holiday </i></center>
                                        </div>
                                        <div class="wrimagecard-topimage_title">
                                            <h4 style="margin-top: -15px;"><a href="Sample of Circular Holiday Shab-e-Quadar June 13, 2018.docx"><i class="fa fa-download" aria-hidden="true" style="color: #BB7824; font-size: 16px"></i>Download</a> </h4>
                                        </div>

                                    </a>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

