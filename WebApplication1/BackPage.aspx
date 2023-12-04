     <h2 class="fw-bold">後台系統</h2>
        <div>
            <asp:GridView ID="GridViewAllPost" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CellSpacing="30" OnRowEditing="GridViewAllPost_RowEditing" OnRowCancelingEdit="GridViewAllPost_RowCancelingEdit" OnRowUpdating="GridViewAllPost_RowUpdating" OnRowDeleting="GridViewAllPost_RowDeleting" CssClass="w-100">
                <AlternatingRowStyle BackColor="#99CCFF" />
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                    <asp:BoundField DataField="Categloy" HeaderText="文章類型" SortExpression="Categloy" ReadOnly="True" />
                    <asp:TemplateField HeaderText="文章主題" SortExpression="PostTheme">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxPostThemeEdit" runat="server"  Text='<%# Bind("PostTheme") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox6" runat="server"  Text='<%# Bind("PostTheme") %>'  ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="文章內容" SortExpression="PostContent">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxPostContentEdit" runat="server" Text='<%# Bind("PostContent") %>' Height="250px" TextMode="MultiLine" Width="400px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("PostContent") %>' Height="250px" TextMode="MultiLine" Width="400px" ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="UserName" HeaderText="發文者" SortExpression="UserName" ReadOnly="True" />
                    <asp:BoundField DataField="AuthPostId" HeaderText="審核狀態ID" SortExpression="AuthPostId" Visible="False" />
                    <asp:BoundField DataField="AuthPostId" HeaderText="審核狀態ID" SortExpression="AuthPostId" Visible="False" />
                    <asp:TemplateField HeaderText="審核狀態" SortExpression="State">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownListAuth" runat="server" DataSourceID="SqlDataSource1" DataTextField="State" DataValueField="Id" SelectedValue='<%# Bind("AuthPostId") %>' >
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Partice_Bulletin_Board_SystemConnectionString %>" SelectCommand="SELECT DISTINCT [State], [Id] FROM [AuthPost]"></asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ControlStyle-CssClass="btn btn-info" ButtonType="Button" ShowEditButton="True" >
<ControlStyle CssClass="btn btn-info"></ControlStyle>
                    </asp:CommandField>
                    <asp:CommandField ControlStyle-CssClass="btn btn-danger " ButtonType="Button" ShowDeleteButton="True" >
<ControlStyle CssClass="btn btn-danger "></ControlStyle>
                    </asp:CommandField>
                </Columns>


            </asp:GridView>

      

        </div>

