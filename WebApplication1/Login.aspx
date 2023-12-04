<%@ Page Language="C#" AutoEventWireup="true" Title="登入" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="登入">

     <h2 class="fw-bold">登入頁面</h2>

    <div class="d-flex flex-column gap-3">
        <div>
            <asp:Label ID="LabelAccount" runat="server" Text="登入帳號："></asp:Label>
            <asp:TextBox ID="TextBoxAccount" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAccount" runat="server" ErrorMessage="RequiredFieldValidator" Text="請輸入帳號" Font-Bold="True" ControlToValidate="TextBoxAccount"></asp:RequiredFieldValidator>
        </div>
        <div>
            <asp:Label ID="LabelPassword" runat="server" Text="登入密碼："></asp:Label>
            <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" CausesValidation="False"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ErrorMessage="RequiredFieldValidator" Text="請輸入密碼" Font-Bold="True" ControlToValidate="TextBoxPassword"></asp:RequiredFieldValidator>
        </div>
         <div>
             <asp:Button ID="BtnLogin" Text="登入" runat="server" OnClick="BtnLogin_Click" />
               <asp:HyperLink ID="HyperLinkRegister" runat="server" NavigateUrl="~/Register.aspx" CssClass="text-primary"  Text="[ 前往註冊頁面 ]" Font-Bold="True"></asp:HyperLink>
         </div>
    </div>


        </main >
    </asp:Content>
