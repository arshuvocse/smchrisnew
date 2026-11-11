<%@ Page Title="Approval Notification" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ApprovalNotificationHome.aspx.cs" Inherits="DashBoard_UI_ApprovalNotificationHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .approval-home {
            position: relative;
            z-index: 1;
            flex: 1 1 auto;
            align-self: stretch;
            width: 100%;
            min-width: calc(100vw - 215px);
            min-height: calc(100vh - 52px);
            display: flex;
            align-items: center;
            justify-content: center;
            overflow: hidden;
            margin: 0;
            padding: 40px 18px;
            box-sizing: border-box;
            background: linear-gradient(135deg, #edf7ff 0%, #f3fbf5 46%, #fff6e8 100%);
        }

        .approval-home:before {
            content: "";
            position: absolute;
            z-index: 0;
            inset: 0;
            background:
                radial-gradient(circle at 12% 18%, rgba(20, 160, 111, .2) 0, rgba(20, 160, 111, 0) 26%),
                radial-gradient(circle at 88% 12%, rgba(15, 110, 167, .2) 0, rgba(15, 110, 167, 0) 28%),
                radial-gradient(circle at 78% 88%, rgba(255, 173, 58, .18) 0, rgba(255, 173, 58, 0) 30%);
        }

        .approval-home__panel {
            position: relative;
            z-index: 2;
            width: 100%;
            max-width: 980px;
            overflow: hidden;
            border-radius: 8px;
            background: linear-gradient(125deg, #124c7f 0%, #0b7b8b 48%, #22a06b 100%);
            box-shadow: 0 28px 70px rgba(18, 38, 63, .22);
            animation: approvalSlideDown .72s ease-out both;
        }

        .approval-home__hero {
            position: relative;
            z-index: 1;
            padding: 58px 60px 52px;
            color: #ffffff !important;
        }

        .approval-home__hero:before,
        .approval-home__hero:after {
            content: "";
            position: absolute;
            border-radius: 50%;
            background: rgba(255, 255, 255, .13);
        }

        .approval-home__hero:before {
            width: 260px;
            height: 260px;
            right: -80px;
            top: -110px;
        }

        .approval-home__hero:after {
            width: 150px;
            height: 150px;
            right: 130px;
            bottom: -72px;
        }

        .approval-home__content {
            position: relative;
            z-index: 3;
            max-width: 650px;
        }

        .approval-home__eyebrow {
            display: inline-block;
            margin-bottom: 16px;
            padding: 7px 13px;
            border: 1px solid rgba(255, 255, 255, .34);
            border-radius: 999px;
            background: rgba(255, 255, 255, .12);
            font-size: 12px;
            font-weight: 700;
            letter-spacing: .08em;
            text-transform: uppercase;
        }

        .approval-home__title {
            margin: 0;
            color: #ffffff !important;
            font-size: 36px;
            line-height: 1.16;
            font-weight: 800;
            letter-spacing: 0;
        }

        .approval-home__text {
            margin: 18px 0 0;
            max-width: 560px;
            color: rgba(255, 255, 255, .9);
            font-size: 16px;
            line-height: 1.7;
        }

        .approval-home__actions {
            display: flex;
            align-items: center;
            gap: 14px;
            margin-top: 30px;
        }

        .approval-home__button {
            min-width: 460px;
            border: 0;
            border-radius: 8px;
            padding: 24px 46px;
            color: #ffffff;
            background: linear-gradient(135deg, #f57c00 0%, #e53935 48%, #8e24aa 100%);
            box-shadow: 0 18px 38px rgba(229, 57, 53, .34);
            font-size: 20px;
            font-weight: 800;
            cursor: pointer;
            letter-spacing: 0;
            animation: approvalButtonPop .72s ease-out .42s both, approvalButtonPulse 2.4s ease-in-out 1.3s infinite;
            transition: transform .18s ease, box-shadow .18s ease, filter .18s ease;
        }

        .approval-home__button:hover,
        .approval-home__button:focus {
            color: #ffffff;
            transform: translateY(-2px) scale(1.02);
            filter: brightness(1.05);
            box-shadow: 0 24px 48px rgba(142, 36, 170, .42);
        }

        .approval-home__hint {
            margin: 0;
            color: rgba(255, 255, 255, .82);
            font-size: 13px;
            line-height: 1.5;
        }

        @keyframes approvalSlideDown {
            from {
                opacity: 0;
                transform: translateY(-95px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        @keyframes approvalButtonPop {
            from {
                opacity: 0;
                transform: translateY(18px) scale(.92);
            }

            to {
                opacity: 1;
                transform: translateY(0) scale(1);
            }
        }

        @keyframes approvalButtonPulse {
            0%, 100% {
                box-shadow: 0 18px 38px rgba(229, 57, 53, .34);
            }

            50% {
                box-shadow: 0 22px 46px rgba(245, 124, 0, .46);
            }
        }

        @media (max-width: 767px) {
            .approval-home {
                min-width: 100%;
                padding: 18px 10px;
            }

            .approval-home__hero {
                padding: 38px 26px 34px;
            }

            .approval-home__title {
                font-size: 28px;
            }

            .approval-home__actions {
                display: block;
                margin-top: 24px;
            }

            .approval-home__button {
                width: 100%;
                min-width: 0;
                padding: 20px 20px;
                font-size: 17px;
            }

            .approval-home__hint {
                margin-top: 14px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="approval-home">
        <section class="approval-home__panel">
            <div class="approval-home__hero">
                <div class="approval-home__content">
                    <span class="approval-home__eyebrow">HRIS Approval Center</span>
                    <h1 class="approval-home__title">Approval notifications are ready for review</h1>
                    <p class="approval-home__text">
                        Continue to the approval dashboard to check pending notifications, review requests, and take the required action.
                    </p>
                    <div class="approval-home__actions">
                        <asp:Button ID="approvalNotificationButton" runat="server" CssClass="approval-home__button" Text="Go to Approval Notification" OnClick="approvalNotificationButton_Click" />
                       
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
