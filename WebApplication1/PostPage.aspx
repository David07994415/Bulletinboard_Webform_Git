
        <h2 class="fw-bold">文章主題</h2>

        <div class="border-bottom border-white border-3 text-bg-dark text-bg-light">
                <div class="d-flex justify-content-evenly border-bottom border-white border-1">
                    <div>
                        <asp:Label ID="LabelTopic" runat="server" Text="主題："></asp:Label>
                        <asp:Label ID="LabelTopicSQL" runat="server" Text=""></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="LabelCreateTime" runat="server" Text="發文時間："></asp:Label>
                        <asp:Label ID="LabelCreateTimeSQL" runat="server" Text=""></asp:Label>
                    </div>
                </div>
        <div class="p-2">
                <asp:Label ID="LabelContentSQL" runat="server" Text=""></asp:Label>
         </div>
   </div>

        <div class="mb-3">
    <asp:Literal ID="LiteralReplys" runat="server"></asp:Literal>
        </div>

        <div class="d-flex justify-content-center">
            <asp:Button CssClass="btn btn-secondary w-50" ID="ButtonAddReply" runat="server" Text="回應文章" OnClick="ButtonAddReply_Click" CausesValidation="False"  Visible="true" />
        </div>

        <div class="d-flex justify-content-center">
            <div class="d-flex gap-3  mb-3 align-content-center">
                <asp:Label ID="LabelAppraisal" runat="server" Text="評論：" Visible="False"></asp:Label>
               <asp:DropDownList ID="DropDownListAppraisal" runat="server" Visible="False">
                   <asp:ListItem Selected="True" Value="seleted">請選擇</asp:ListItem>
                   <asp:ListItem Value="6">Good</asp:ListItem>
                   <asp:ListItem Value="7">Bad</asp:ListItem>
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidatorAppraisal" runat="server" ErrorMessage="CompareValidator" ControlToValidate="DropDownListAppraisal" Text="*" Operator="NotEqual" ValueToCompare="seleted" Display="Dynamic"></asp:CompareValidator>
                <asp:Label ID="LabelReply" runat="server" Text="回覆內容"  Visible="False"></asp:Label>
                <asp:TextBox CssClass="w-100" ID="TextBoxReply" runat="server" TextMode="SingleLine"  Visible="False"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidatorReply" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="TextBoxReply" Display="Dynamic">*</asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="d-flex justify-content-center">
            <div>
                   <asp:Button CssClass="btn btn-primary" ID="ButtonSubmitReply" runat="server" Text="送出回覆" OnClick="ButtonSubmitReply_Click"  Visible="False" />
                   <asp:Button CssClass="btn btn-warning"  ID="ButtonCancel" runat="server" Text="取消" OnClick="ButtonCancel_Click" CausesValidation="False"  Visible="False" />
            </div>
        </div>

