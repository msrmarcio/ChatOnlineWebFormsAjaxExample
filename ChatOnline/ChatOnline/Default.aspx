<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ChatOnline.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <h1>ASP.NET - Chat</h1>

        <asp:Label ID="erroLabel" Visible="false" CssClass="erro" runat="server"></asp:Label>

        <asp:MultiView ID="MultiView1" ActiveViewIndex="1" runat="server">
            <asp:View ID="View1" runat="server">
                <section>
                    <asp:Label CssClass="legenda"
                        runat="server"
                        Text="Insira seu nome">
                    </asp:Label>
                    <asp:TextBox CssClass="campo"
                        runat="server"
                        ID="nomeTextBox">
                    </asp:TextBox>
                    <asp:Button CssClass="botao" runat="server" Text="Entrar no Chat" ID="entrarButton" OnClick="entrarButton_Click"/>
                    <asp:Label runat="server"
                        ID="mensagemLabel">
                    </asp:Label>
                </section>
            </asp:View>


            <asp:View ID="View2" runat="server">
                <section id="chat">
                    <asp:Label runat="server"
                        CssClass="legenda"
                        ID="digiteMensagemLabel">Digite sua mensagem</asp:Label>
                    <asp:TextBox runat="server"
                        CssClass="campo"
                        TextMode="MultiLine"
                        Rows="3"
                        ID="mensagemTextBox">
                    </asp:TextBox>
                    <asp:Button runat="server" CssClass="botao" Text="Enviar Mensagem" ID="enviarMensagemButton" OnClick="enviarMensagemButton_Click" />
                    <asp:Button runat="server" CssClass="botao" Text="Sair" ID="sairButton" OnClick="sairButton_Click" />

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Timer ID="Timer1" runat="server" Interval="4" OnTick="GetTime">
                            </asp:Timer>

                            <div class="chat-box">
                                <asp:Repeater runat="server" ID="chatRepeater" ItemType="ChatOnline.Models.ChatMensagem">
                                    <ItemTemplate>
                                        <div class='chat-mensagem-box'>
                                            <div class='chat-mensagem-participante'>
                                                <%# Item.DataHora.ToLongTimeString() %>
                                                <%# Item.Participante %>:
                                            </div>
                                            <div class="chat-mensagem-texto">
                                                <%# Item.Mensagem %>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </section>
            </asp:View>
        </asp:MultiView>
    </form>
</body>
</html>
