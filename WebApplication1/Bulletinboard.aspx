
        <h2 class="fw-bold">留言板文章</h2>
        
        <div class="my-3">   
            <asp:Label ID="LabelSearch" runat="server" Text="搜尋類別："></asp:Label>
            <asp:DropDownList ID="DropDownListSearch" runat="server">
                <asp:ListItem Value="name">作者</asp:ListItem>
                <asp:ListItem Value="post">文章</asp:ListItem>
                <asp:ListItem Selected="True" Value="selected">請選擇</asp:ListItem>
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidatorSearch" runat="server" ErrorMessage="CompareValidator" ControlToValidate="DropDownListSearch" Operator="NotEqual" ValueToCompare="seleted" Display="Dynamic">*</asp:CompareValidator>
            <asp:TextBox ID="TextBoxSearch" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSearch" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="TextBoxSearch" Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:Button ID="ButtonSearch" runat="server" Text="搜尋" OnClick="ButtonSearch_Click" />
        </div>

         <asp:Literal ID="LiteralTable" runat="server"></asp:Literal>
        <div class="d-flex justify-content-center mt-2">
             <asp:Button CssClass="btn btn-success w-50" ID="ButtonCreatePost" runat="server" Text="發表文章" OnClick="ButtonCreatePost_Click" CausesValidation="False" />
        </div>

