<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="UserProfile_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .change-password-page {
            min-height: calc(100vh - 120px);
            padding: 24px 0 36px;
            background:
                radial-gradient(circle at top left, rgba(40, 167, 69, .18), transparent 32%),
                radial-gradient(circle at bottom right, rgba(0, 123, 255, .18), transparent 34%),
                linear-gradient(135deg, #eef7f4 0%, #f7fbff 48%, #fff6ef 100%);
        }

        .password-shell {
            width: min(980px, 100%);
            margin: 0 auto;
        }

        .password-hero {
            position: relative;
            overflow: hidden;
            border-radius: 8px;
            padding: 22px 26px;
            color: #ffffff;
            background: linear-gradient(135deg, #0f766e 0%, #1d4ed8 55%, #7c3aed 100%);
            box-shadow: 0 18px 45px rgba(29, 78, 216, .22);
        }

        .password-hero::after {
            position: absolute;
            inset: 1px;
            content: "";
            border-radius: 8px;
            background: linear-gradient(180deg, rgba(255, 255, 255, .34), rgba(255, 255, 255, 0) 45%);
            pointer-events: none;
        }

        .password-hero-content {
            position: relative;
            z-index: 1;
            display: flex;
            align-items: center;
            gap: 14px;
        }

        .password-hero-icon {
            display: flex;
            align-items: center;
            justify-content: center;
            width: 46px;
            height: 46px;
            border-radius: 8px;
            background: rgba(255, 255, 255, .2);
            box-shadow: inset 0 1px 0 rgba(255, 255, 255, .35);
        }

        .password-hero h1 {
            margin: 0;
            font-size: 22px;
            font-weight: 700;
            line-height: 1.2;
            letter-spacing: 0;
        }

        .password-hero p {
            margin: 5px 0 0;
            color: rgba(255, 255, 255, .82);
            font-size: 13px;
        }

        .password-panel {
            display: grid;
            grid-template-columns: 320px minmax(0, 1fr);
            gap: 0;
            overflow: hidden;
            margin-top: 18px;
            border: 1px solid rgba(15, 118, 110, .14);
            border-radius: 8px;
            background: rgba(255, 255, 255, .84);
            box-shadow: 0 20px 55px rgba(15, 23, 42, .12);
            backdrop-filter: blur(12px);
        }

        .profile-panel {
            padding: 28px 24px;
            color: #ffffff;
            background:
                linear-gradient(180deg, rgba(255, 255, 255, .18), rgba(255, 255, 255, 0) 32%),
                linear-gradient(145deg, #155e75 0%, #0f766e 56%, #2563eb 100%);
        }

        .profile-photo-wrap {
            width: 124px;
            height: 124px;
            margin: 0 auto 18px;
            padding: 5px;
            border-radius: 50%;
            background: linear-gradient(135deg, #ffffff, rgba(255, 255, 255, .35));
            box-shadow: 0 15px 35px rgba(0, 0, 0, .18);
        }

        .profile-photo-wrap img,
        .profile-photo-wrap .img-responsive {
            width: 100%;
            height: 100%;
            border-radius: 50%;
            object-fit: cover;
            display: block;
        }

        .profile-name {
            margin-bottom: 20px;
            text-align: center;
        }

        .profile-name strong {
            display: block;
            font-size: 18px;
            line-height: 1.3;
        }

        .profile-name span {
            display: block;
            margin-top: 4px;
            color: rgba(255, 255, 255, .78);
            font-size: 13px;
        }

        .profile-meta {
            margin: 0;
            padding: 0;
            list-style: none;
        }

        .profile-meta li {
            display: flex;
            justify-content: space-between;
            gap: 12px;
            padding: 11px 0;
            border-top: 1px solid rgba(255, 255, 255, .18);
            font-size: 13px;
        }

        .profile-meta label {
            margin: 0;
            color: rgba(255, 255, 255, .72);
            font-weight: 600;
        }

        .profile-meta span {
            color: #ffffff;
            font-weight: 700;
            text-align: right;
        }

        .password-form-panel {
            padding: 30px;
            background:
                linear-gradient(180deg, rgba(255, 255, 255, .95), rgba(255, 255, 255, .82)),
                linear-gradient(135deg, rgba(14, 165, 233, .08), rgba(16, 185, 129, .08));
        }

        .form-section-title {
            margin: 0 0 20px;
            color: #0f172a;
            font-size: 18px;
            font-weight: 700;
        }

        .password-form-panel .form-group {
            margin-bottom: 18px;
        }

        .password-form-panel .control-label {
            margin-bottom: 7px;
            color: #334155;
            font-size: 13px;
            font-weight: 700;
        }

        .password-input-group {
            position: relative;
        }

        .password-input-group .form-control {
            height: 42px;
            padding-right: 44px;
            border: 1px solid #cbd5e1;
            border-radius: 8px;
            background: linear-gradient(180deg, #ffffff 0%, #f8fafc 100%);
            color: #0f172a;
            font-size: 14px;
            box-shadow: inset 0 1px 2px rgba(15, 23, 42, .06);
            transition: border-color .2s ease, box-shadow .2s ease, background .2s ease;
        }

        .password-input-group .form-control:focus {
            border-color: #0ea5e9;
            background: #ffffff;
            box-shadow: 0 0 0 3px rgba(14, 165, 233, .14);
        }

        .password-toggle-btn {
            position: absolute;
            top: 1px;
            right: 1px;
            width: 42px;
            height: calc(100% - 2px);
            min-height: 0;
            padding: 0;
            border: 0;
            border-left: 1px solid #e2e8f0;
            border-radius: 0 8px 8px 0;
            background: linear-gradient(180deg, #ffffff 0%, #edf2f7 100%);
            color: #475569;
            cursor: pointer;
            z-index: 2;
        }

        .password-toggle-btn:focus {
            outline: none;
            color: #0369a1;
        }

        .password-rule-list {
            display: grid;
            grid-template-columns: repeat(2, minmax(0, 1fr));
            gap: 8px;
            margin: 12px 0 0;
            padding: 0;
            list-style: none;
            font-size: 12px;
            line-height: 1.35;
        }

        .password-rule-list li {
            position: relative;
            min-height: 32px;
            padding: 8px 10px 8px 28px;
            border: 1px solid #fecdd3;
            border-radius: 8px;
            background: #fff5f5;
            color: #b42318;
            font-weight: 600;
        }

        .password-rule-list li::before {
            position: absolute;
            top: 8px;
            left: 10px;
            content: "\f00d";
            font-family: FontAwesome;
            font-weight: normal;
        }

        .password-rule-list li.valid {
            border-color: #bbf7d0;
            background: #f0fdf4;
            color: #15803d;
        }

        .password-rule-list li.valid::before {
            content: "\f00c";
        }

        .password-validation-message {
            display: block;
            min-height: 18px;
            margin-top: 7px;
            color: #b42318;
            font-size: 12px;
            font-weight: 700;
        }

        .password-validation-message.valid {
            color: #15803d;
        }

        .password-submit {
            position: relative;
            overflow: hidden;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            min-height: 42px;
            width: 100%;
            border: 0;
            border-radius: 8px;
            background: linear-gradient(135deg, #0f766e 0%, #0284c7 58%, #4f46e5 100%);
            color: #ffffff !important;
            font-size: 14px;
            font-weight: 700;
            text-decoration: none !important;
            box-shadow: 0 12px 26px rgba(2, 132, 199, .28);
            transition: transform .18s ease, box-shadow .18s ease;
        }

        .password-submit::after {
            position: absolute;
            inset: 1px 1px auto;
            height: 45%;
            content: "";
            border-radius: 8px 8px 18px 18px;
            background: linear-gradient(180deg, rgba(255, 255, 255, .34), rgba(255, 255, 255, 0));
            pointer-events: none;
        }

        .password-submit:hover {
            transform: translateY(-1px);
            box-shadow: 0 16px 32px rgba(2, 132, 199, .32);
        }

        .password-submit .fa {
            margin-right: 8px;
        }

        @media (max-width: 767.98px) {
            .change-password-page {
                padding: 14px 0 24px;
            }

            .password-hero {
                padding: 18px;
            }

            .password-panel {
                grid-template-columns: 1fr;
            }

            .password-form-panel {
                padding: 22px 18px;
            }

            .password-rule-list {
                grid-template-columns: 1fr;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    <div class="content change-password-page" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

                <div class="container-fluid">
                    <div class="password-shell">
                        <div class="password-hero">
                            <div class="password-hero-content">
                                <div class="password-hero-icon">
                                    <span class="fa fa-lock"></span>
                                </div>
                                <div>
                                    <h1>Change Your Password</h1>
                                    <p>Use a strong password and confirm it before updating your account.</p>
                                </div>
                            </div>
                        </div>

                        <div class="password-panel">
                            <aside class="profile-panel">
                                <div class="profile-photo-wrap">
                                    <asp:Image ID="UserImage" runat="server" CssClass="img-responsive" alt="" />
                                </div>
                                <div class="profile-name">
                                    <strong><asp:Label runat="server" ID="lblshortName" /></strong>
                                    <span><asp:Label runat="server" ID="lblDesignation" /></span>
                                </div>
                                <ul class="profile-meta">
                                    <li>
                                        <label>ID</label>
                                        <span><asp:Label runat="server" ID="lblID" /></span>
                                    </li>
                                    <li>
                                        <label>Status</label>
                                        <span>Password Update</span>
                                    </li>
                                </ul>
                            </aside>

                            <section class="password-form-panel">
                                <h2 class="form-section-title">Set New Password</h2>

                                <div class="form-group">
                                    <label class="control-label">New Password&nbsp;<span style="color: #b42318">*</span></label>
                                    <div class="password-input-group">
                                        <asp:TextBox runat="server" AutoCompleteType="None" ID="txt_Password" ClientIDMode="Static" TextMode="Password" MaxLength="20" class="form-control form-control-sm"></asp:TextBox>
                                        <button type="button" class="password-toggle-btn" data-target="txt_Password" title="Show password">
                                            <span class="fa fa-eye"></span>
                                        </button>
                                    </div>
                                    <ul id="passwordRules" class="password-rule-list">
                                        <li data-rule="length">12 to 20 characters</li>
                                        <li data-rule="lower">At least 1 lowercase letter</li>
                                        <li data-rule="upper">At least 1 uppercase letter</li>
                                        <li data-rule="number">At least 1 numeric digit</li>
                                        <li data-rule="special">At least 1 special character such as *, -, or #</li>
                                    </ul>
                                    <span id="passwordStrengthMessage" class="password-validation-message"></span>
                                </div>

                                <div class="form-group">
                                    <label class="control-label">Confirm Password&nbsp;<span style="color: #b42318">*</span></label>
                                    <div class="password-input-group">
                                        <asp:TextBox runat="server" AutoCompleteType="None" ID="txtConfirm" ClientIDMode="Static" TextMode="Password" MaxLength="20" class="form-control form-control-sm"></asp:TextBox>
                                        <button type="button" class="password-toggle-btn" data-target="txtConfirm" title="Show password">
                                            <span class="fa fa-eye"></span>
                                        </button>
                                    </div>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password is not Matched" ForeColor="#B42318" Font-Bold="True" ControlToCompare="txt_Password" ControlToValidate="txtConfirm"></asp:CompareValidator>
                                    <span id="confirmPasswordMessage" class="password-validation-message"></span>
                                </div>

                                <div class="form-group mb-0">
                                    <asp:LinkButton runat="server" ID="btn_Save" ClientIDMode="Static" OnClick="btn_Save_OnClick" CssClass="password-submit" OnClientClick="return validateChangePasswordForm();">
                                        <span class="fa fa-refresh"></span> Update Password
                                    </asp:LinkButton>
                                </div>
                            </section>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <script type="text/javascript">
            function getPasswordValidation(password) {
                return {
                    length: password.length >= 12 && password.length <= 20,
                    lower: /[a-z]/.test(password),
                    upper: /[A-Z]/.test(password),
                    number: /\d/.test(password),
                    special: /[^A-Za-z0-9]/.test(password)
                };
            }

            function isStrongPassword(password) {
                var rules = getPasswordValidation(password);
                return rules.length && rules.lower && rules.upper && rules.number && rules.special;
            }

            function updatePasswordValidation() {
                var password = $('#txt_Password').val();
                var confirmPassword = $('#txtConfirm').val();
                var rules = getPasswordValidation(password);

                $('#passwordRules li').each(function () {
                    var rule = $(this).data('rule');
                    $(this).toggleClass('valid', !!rules[rule]);
                });

                $('#passwordStrengthMessage')
                    .toggleClass('valid', isStrongPassword(password))
                    .text(password.length === 0 ? '' : (isStrongPassword(password) ? 'Strong password' : 'Password is not strong enough'));

                $('#confirmPasswordMessage')
                    .toggleClass('valid', confirmPassword !== '' && password === confirmPassword)
                    .text(confirmPassword === '' ? '' : (password === confirmPassword ? 'Password matched' : 'Password is not matched'));
            }

            function validateChangePasswordForm() {
                updatePasswordValidation();

                if (!isStrongPassword($('#txt_Password').val())) {
                    alert('Password must be 12 to 20 characters and contain lowercase, uppercase, numeric digit, and special character.');
                    $('#txt_Password').focus();
                    return false;
                }

                if ($('#txt_Password').val() !== $('#txtConfirm').val()) {
                    alert('Password is not Matched');
                    $('#txtConfirm').focus();
                    return false;
                }

                return confirm('Are you sure you want to Update Password?');
            }

            $(document).ready(function () {
                $('#txt_Password, #txtConfirm').on('keyup change input', updatePasswordValidation);
                $('.password-toggle-btn').on('click', function () {
                    var target = $('#' + $(this).data('target'));
                    var icon = $(this).find('.fa');
                    var isPassword = target.attr('type') === 'password';

                    target.attr('type', isPassword ? 'text' : 'password');
                    icon.toggleClass('fa-eye', !isPassword);
                    icon.toggleClass('fa-eye-slash', isPassword);
                    $(this).attr('title', isPassword ? 'Hide password' : 'Show password');
                });
                updatePasswordValidation();
            });
        </script>
    </div>
</asp:Content>
