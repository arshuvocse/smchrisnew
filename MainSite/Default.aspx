<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta http-equiv="Content-Language" content="en">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login - HRIS</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, shrink-to-fit=no" />
    <meta name="description" content="SMC HRIS Login">

    <meta name="msapplication-tap-highlight" content="no">

    <link href="Assets/Login/main.3089f45f256aee6af5da.css" rel="stylesheet" />

    <style>
        * {
            box-sizing: border-box;
        }

        html,
        body,
        #form {
            min-height: 100%;
        }

        body {
            margin: 0;
            color: #16233a;
            font-family: "Segoe UI", Arial, sans-serif;
            background: #0f334a;
            overflow-x: hidden;
        }

        .login-page {
            position: relative;
            isolation: isolate;
            display: flex;
            min-height: 100vh;
            align-items: center;
            justify-content: center;
            padding: 48px 18px;
            background:
                radial-gradient(circle at 92% 12%, rgba(20, 190, 176, .38) 0, rgba(20, 190, 176, 0) 360px),
                linear-gradient(116deg, #4c5149 0%, #12375b 45%, #079398 100%);
        }

        .login-page::before {
            position: absolute;
            inset: 0;
            z-index: -2;
            content: "";
            background-image:
                repeating-linear-gradient(135deg, rgba(255, 255, 255, .08) 0 1px, transparent 1px 13px),
                repeating-linear-gradient(135deg, rgba(255, 255, 255, .04) 0 1px, transparent 1px 36px);
            opacity: .72;
        }

        .login-page::after {
            position: absolute;
            inset: 0;
            z-index: -1;
            content: "";
            background:
                linear-gradient(90deg, rgba(0, 0, 0, .30), rgba(0, 0, 0, 0) 48%, rgba(0, 139, 150, .20)),
                linear-gradient(180deg, rgba(255, 255, 255, .12) 0, rgba(255, 255, 255, 0) 56px);
        }

        .login-card {
            position: relative;
            width: 100%;
            max-width: 560px;
            width: min(100%, 560px);
            overflow: hidden;
            padding: 38px 34px 28px;
            border: 1px solid rgba(255, 255, 255, .72);
            border-radius: 16px;
            background: rgba(241, 247, 252, .95);
            box-shadow: 0 30px 70px rgba(3, 19, 43, .38), inset 0 1px 0 rgba(255, 255, 255, .82);
            -webkit-backdrop-filter: blur(12px);
            backdrop-filter: blur(12px);
        }

        .login-card::before {
            position: absolute;
            top: 0;
            right: 14px;
            left: 14px;
            height: 5px;
            border-radius: 0 0 999px 999px;
            content: "";
            background: linear-gradient(90deg, #2677e8 0%, #16b5b0 72%, #f3c84f 100%);
        }

        .login-heading {
            text-align: center;
        }

        .login-heading h1 {
            margin: 0;
            color: #101d34;
            font-size: 30px;
            font-weight: 800;
            line-height: 1.15;
            letter-spacing: 0;
        }

        .login-heading p {
            margin: 8px 0 24px;
            color: #647083;
            font-size: 14px;
            font-weight: 600;
        }

        .brand-mark {
            display: flex;
            width: 110px;
            height: 110px;
            align-items: center;
            justify-content: center;
            margin: 0 auto 34px;
            border: 1px solid rgba(225, 234, 244, .95);
            border-radius: 50%;
            background: rgba(255, 255, 255, .96);
            box-shadow: 0 18px 36px rgba(22, 47, 78, .20);
        }

        .brand-mark img {
            display: block;
            width: 76px;
            max-height: 86px;
            object-fit: contain;
        }

        .login-form {
            display: grid;
            gap: 16px;
        }

        .field-group {
            display: grid;
            gap: 8px;
        }

        .field-group label {
            margin: 0;
            color: #253249;
            font-size: 14px;
            font-weight: 800;
        }

        .login-input {
            width: 100%;
            height: 46px;
            padding: 0 14px;
            border: 1px solid #dbe5f1;
            border-radius: 10px;
            outline: none;
            color: #16233a;
            background: #f3f8ff;
            box-shadow: inset 0 1px 1px rgba(17, 34, 55, .03);
            font-size: 16px;
            transition: border-color .18s ease, box-shadow .18s ease, background .18s ease;
        }

        .login-input:focus {
            border-color: #6bb7ee;
            background: #ffffff;
            box-shadow: 0 0 0 4px rgba(38, 119, 232, .12);
        }

        .password-field {
            position: relative;
        }

        .password-input {
            padding-right: 52px;
        }

        .password-toggle {
            position: absolute;
            top: 1px;
            right: 1px;
            display: inline-flex;
            width: 46px;
            height: 44px;
            align-items: center;
            justify-content: center;
            border: 0;
            border-left: 1px solid #e5edf6;
            border-radius: 0 10px 10px 0;
            color: #2d7fe2;
            background: #f6faff;
            cursor: pointer;
        }

        .password-toggle:hover,
        .password-toggle:focus {
            color: #0e68d0;
            background: #eef6ff;
            outline: none;
        }

        .password-toggle svg {
            width: 18px;
            height: 18px;
            stroke: currentColor;
        }

        .validation-message {
            display: block;
            min-height: 20px;
            color: #ba1f35;
            font-size: 13px;
            font-weight: 700;
            line-height: 1.35;
        }

        .signin-action {
            position: relative;
            margin-top: 2px;
        }

        .signin-action::before,
        .signin-action::after {
            position: absolute;
            z-index: 2;
            pointer-events: none;
            content: "";
        }

        .signin-action::before {
            top: 23px;
            left: calc(50% - 58px);
            width: 12px;
            height: 9px;
            border: 2px solid rgba(255, 255, 255, .95);
            border-radius: 2px;
        }

        .signin-action::after {
            top: 14px;
            left: calc(50% - 55px);
            width: 6px;
            height: 9px;
            border: 2px solid rgba(255, 255, 255, .95);
            border-bottom: 0;
            border-radius: 8px 8px 0 0;
        }

        .signin-button {
            width: 100%;
            height: 48px;
            border: 0;
            border-radius: 9px;
            color: #ffffff;
            background: linear-gradient(100deg, #2677e8 0%, #12b7af 100%);
            box-shadow: 0 16px 32px rgba(37, 117, 232, .22);
            font-size: 16px;
            font-weight: 800;
            cursor: pointer;
            transition: transform .18s ease, box-shadow .18s ease, filter .18s ease;
        }

        .signin-button:hover,
        .signin-button:focus {
            filter: brightness(1.03);
            box-shadow: 0 18px 34px rgba(18, 183, 175, .25);
            outline: none;
            transform: translateY(-1px);
        }

        .signin-button:active {
            transform: translateY(0);
        }

        .download-action {
            margin-top: 32px;
        }

        .download-button {
            display: flex;
            min-height: 38px;
            align-items: center;
            justify-content: center;
            padding: 8px 16px;
            border: 1px solid #d4e6ed;
            border-radius: 9px;
            color: #0b7a6f;
            background: linear-gradient(90deg, rgba(236, 255, 247, .95), rgba(255, 255, 255, .95));
            box-shadow: inset 0 1px 0 rgba(255, 255, 255, .85);
            font-size: 15px;
            font-weight: 800;
            text-align: center;
            text-decoration: none;
        }

        .download-button:hover,
        .download-button:focus {
            color: #06655d;
            border-color: #b8dce5;
            text-decoration: none;
            outline: none;
        }

        @media (max-width: 575px) {
            .login-page {
                align-items: flex-start;
                padding: 28px 12px;
            }

            .login-card {
                padding: 34px 20px 24px;
            }

            .login-heading h1 {
                font-size: 27px;
            }

            .brand-mark {
                width: 98px;
                height: 98px;
                margin-bottom: 28px;
            }

            .brand-mark img {
                width: 68px;
                max-height: 78px;
            }
        }
    </style>
</head>

<body>
    <form id="form" runat="server">
        <main class="login-page">
            <section class="login-card" aria-label="Login form">
                <div class="login-heading">
                    <h1>SMC-HRIS</h1>
                    <p>Secure business access</p>
                </div>

                <div class="brand-mark">
                    <img src="Assets/login page.jpg" alt="SMC Live better" />
                </div>

                <div class="login-form">
                    <div class="field-group">
                        <label for="userNameTextBox">User Name</label>
                        <asp:TextBox ID="userNameTextBox" runat="server" ClientIDMode="Static" CssClass="login-input" MaxLength="20" autocomplete="username"></asp:TextBox>
                    </div>

                    <div class="field-group">
                        <label for="passwordTextBox">Enter Password</label>
                        <div class="password-field">
                            <asp:TextBox ID="passwordTextBox" runat="server" ClientIDMode="Static" CssClass="login-input password-input" TextMode="Password" MaxLength="20" autocomplete="current-password"></asp:TextBox>
                            <button id="togglePassword" class="password-toggle" type="button" aria-label="Show password">
                                <svg viewBox="0 0 24 24" fill="none" aria-hidden="true">
                                    <path d="M2.5 12s3.4-6 9.5-6 9.5 6 9.5 6-3.4 6-9.5 6-9.5-6-9.5-6Z" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
                                    <path d="M12 15a3 3 0 1 0 0-6 3 3 0 0 0 0 6Z" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
                                </svg>
                            </button>
                        </div>
                    </div>

                    <asp:Label CssClass="validation-message" ID="msgLabel" runat="server" ClientIDMode="Static" Text=""></asp:Label>

                    <div class="signin-action">
                        <asp:Button ID="loginbtn" runat="server" CssClass="signin-button" Text="Sign in" OnClick="loginButton_Click" />
                    </div>
                </div>
 
            </section>
        </main>
    </form>

    <script>
        (function () {
            var form = document.getElementById('form');
            var userName = document.getElementById('userNameTextBox');
            var password = document.getElementById('passwordTextBox');
            var toggle = document.getElementById('togglePassword');
            var message = document.getElementById('msgLabel');
            var userNamePattern = /^[A-Za-z0-9]{1,20}$/;

            function setMessage(text) {
                if (message) {
                    message.textContent = text;
                }
            }

            function cleanUserName(value) {
                return value.replace(/[^A-Za-z0-9]/g, '').substring(0, 20);
            }

            if (userName) {
                userName.setAttribute('maxlength', '20');
                userName.addEventListener('input', function () {
                    var cleanedValue = cleanUserName(userName.value);

                    if (userName.value !== cleanedValue) {
                        userName.value = cleanedValue;
                        setMessage('User Name allows only letters and numbers, maximum 20 characters.');
                        return;
                    }

                    if (userName.value.length > 0) {
                        setMessage('');
                    }
                });
            }

            if (password) {
                password.setAttribute('maxlength', '20');
            }

            if (form && userName) {
                form.addEventListener('submit', function (event) {
                    var value = userName.value.trim();
                    var passwordValue = password ? password.value : '';

                    if (!userNamePattern.test(value)) {
                        event.preventDefault();
                        setMessage(value.length === 0
                            ? 'Input User Name Please!!!'
                            : 'User Name allows only letters and numbers, maximum 20 characters.');
                        userName.focus();
                        return;
                    }

                    if (passwordValue.length > 20) {
                        event.preventDefault();
                        setMessage('Password cannot be more than 20 characters.');
                        password.focus();
                    }
                });
            }

            if (!password || !toggle) {
                return;
            }

            toggle.addEventListener('click', function () {
                var isPassword = password.getAttribute('type') === 'password';
                password.setAttribute('type', isPassword ? 'text' : 'password');
                toggle.setAttribute('aria-label', isPassword ? 'Hide password' : 'Show password');
            });
        }());
    </script>
    <script src="Assets/Login/assets/scripts/main.3089f45f256aee6af5da.js"></script>
</body>

</html>
