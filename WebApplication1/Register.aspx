   <h2 class="fw-bold">註冊頁面</h2>

    <div class="d-flex flex-column gap-3">
        <div>
            <asp:Label ID="LabelUser" runat="server" Text="使用者暱稱："></asp:Label>
            <asp:TextBox ID="TextBoxUser" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUser" runat="server" ErrorMessage="RequiredFieldValidator" Text="請輸入暱稱" Font-Bold="True" ControlToValidate="TextBoxUser" CssClass="text-danger"></asp:RequiredFieldValidator>
        </div>
        <div>
            <asp:Label ID="LabelAccount" runat="server" Text="註冊帳號："></asp:Label>
            <asp:TextBox ID="TextBoxAccount" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAccount" runat="server" ErrorMessage="RequiredFieldValidator" Text="請輸入帳號" Font-Bold="True" ControlToValidate="TextBoxAccount"></asp:RequiredFieldValidator>
        </div>
        <div>
            <asp:Label ID="LabelPassword" runat="server" Text="註冊密碼："></asp:Label>
            <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" CausesValidation="False"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorNum" runat="server" ErrorMessage="RegularExpressionValidator" ValidationExpression="^(?=.*[A-Z])(?=.*!).{5,}$" Text="請至少輸入5個字，請包含1大寫英文字與！符號" Display="Dynamic" Font-Bold="True" ControlToValidate="TextBoxPassword"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ErrorMessage="RequiredFieldValidator" Text="請輸入密碼" Font-Bold="True" ControlToValidate="TextBoxPassword" CssClass="text-danger"></asp:RequiredFieldValidator>
        </div>
         <div>
            <asp:Label ID="LabelREP" runat="server" Text="重新輸入密碼："></asp:Label>
            <asp:TextBox ID="TextBoxREP" runat="server" TextMode="Password" CausesValidation="False"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorREP" runat="server" ErrorMessage="RequiredFieldValidator" Text="請再次輸入密碼" Font-Bold="True" ControlToValidate="TextBoxREP" Display="Dynamic" ForeColor="#FF3300" CssClass="text-danger"></asp:RequiredFieldValidator>
             <asp:CompareValidator ID="CompareValidatorREP" runat="server" ControlToCompare="TextBoxPassword" ControlToValidate="TextBoxREP" Text="兩次密碼不相同" Display="Dynamic" CssClass="text-danger"></asp:CompareValidator>
         </div>
         <div>
             <asp:Button ID="BtnRegis" Text="註冊" runat="server" OnClick="BtnRegis_Click" />
              <asp:HyperLink ID="HyperLinkRegister"  runat="server" CssClass="text-primary" NavigateUrl="~/Login.aspx" Text="[ 前往登入頁面 ]" Font-Bold="True"></asp:HyperLink>
         </div>
       </div>

