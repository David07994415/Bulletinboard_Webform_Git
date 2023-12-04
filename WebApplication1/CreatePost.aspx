<%@ Page Language="C#" AutoEventWireup="true" Title="撰寫貼文" MasterPageFile="~/Site.Master" CodeBehind="CreatePost.aspx.cs" Inherits="WebApplication1.CreatePost" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="撰寫文章">

        <h2 class="fw-bold">撰寫文章</h2>

        <div class="d-flex flex-column gap-3 mb-3">

        <div>
            <asp:Label ID="LabelCategloy" runat="server" Text="文章種類："></asp:Label>
            <asp:DropDownList ID="DropDownListCategloy" runat="server">
                <asp:ListItem Value="1">新聞</asp:ListItem>
                <asp:ListItem Value="2">問卦</asp:ListItem>
                <asp:ListItem Value="3">八卦</asp:ListItem>
                <asp:ListItem Value="select" Selected="True">請選擇發文類別</asp:ListItem>
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidatorSelect" runat="server" ErrorMessage="CompareValidator" ControlToValidate="DropDownListCategloy" Operator="NotEqual" ValueToCompare="select" ForeColor="#FF3300">請選擇種類</asp:CompareValidator>
        </div>

        <div class="d-flex align-items-center">
            <asp:Label ID="LabelTopic" runat="server" Text="文章主題："></asp:Label>
            <asp:TextBox CssClass="w-100" ID="TextTopicInput" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="TextTopicInput" Text="必填欄位" Font-Bold="True"></asp:RequiredFieldValidator>
        </div>

        <div class="d-flex align-items-center">
            <asp:Label ID="LabelContent" runat="server" Text="文章內容："></asp:Label>
            <asp:TextBox CssClass="w-100"  ID="TextContentInput" runat="server" TextMode="MultiLine" Height="300px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" Text="必填欄位" Font-Bold="True" ControlToValidate="TextContentInput"></asp:RequiredFieldValidator>
        </div>

     </div>

        <%--        <div>
            <asp:Label ID="LabelPostDatetime" runat="server" Text="發文時間"></asp:Label>
            <asp:Label ID="LabelPostDatetimeSQL" runat="server" Text=""></asp:Label>

        </div>--%>


        <div class="d-flex ms-5">
            <div>
                <asp:Button CssClass="btn btn-primary" ID="ButtonCreate" runat="server" Text="發布主題文章" OnClick="ButtonCreate_Click" />
                <asp:Button CssClass="btn btn-secondary" ID="ButtonRevise" runat="server" Text="修改主題文章" OnClick="ButtonRevise_Click" Visible="false" />
                <asp:Button CssClass="btn btn-danger" ID="ButtonReturn" runat="server" Text="返回文章留言板" OnClick="ButtonReturn_Click" CausesValidation="False" />
            </div>
        </div>

    </main>
</asp:Content>


