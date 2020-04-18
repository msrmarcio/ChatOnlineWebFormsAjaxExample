using ChatOnline.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChatOnline
{
    public partial class Default : System.Web.UI.Page
    {

        //
        // ErroExibir
        //
        private void ErroExibir(string msg)
        {
            erroLabel.Text = msg;
            erroLabel.Visible = true;
        }

        // ErroOcutar
        //
        private void ErroOcultar()
        {
            erroLabel.Visible = false;
        }

        //
        // Exibir Login
        //
        private void ExibirLogin()
        {
            MultiView1.ActiveViewIndex = 0;
        }

        //
        // Load
        //
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ExibirLogin();
            }
        }

        //
        // ExibirChat
        //
        private void ExibirChat()
        {
            try
            {
                ErroOcultar();
                chatRepeater.DataSource = ChatDb.UltimasMensagens();
                chatRepeater.DataBind();
                MultiView1.ActiveViewIndex = 1;
            }
            catch (Exception ex)
            {
                ErroExibir(ex.Message);
            }
        }

        // Entrar
        //
        protected void entrarButton_Click(object sender, EventArgs e)
        {
            try
            {
                ErroOcultar();
                string nome = nomeTextBox.Text;
                ChatDb.Entrar(nome);
                ViewState["nome"] = nome;
                ExibirChat();
                mensagemTextBox.Focus();
                mensagemTextBox.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ErroExibir(ex.Message);
            }
        }

        //
        // Enviar mensagem
        //
        protected void enviarMensagemButton_Click(
        object sender, EventArgs e)
        {
            string nome = (string)ViewState["nome"];
            string mensagem = mensagemTextBox.Text;
            try
            {
                ErroOcultar();
                ChatDb.EnviarMensagem(nome, mensagem);
                ExibirChat();
                mensagemTextBox.Focus();
                mensagemTextBox.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ErroExibir(ex.Message);
            }
        }

        protected void sairButton_Click(object sender, EventArgs e)
        {
            ErroOcultar();
            ViewState.Remove("nome");
            nomeTextBox.Text = string.Empty;
            ExibirLogin();
        }

        // Timer1
        //
        protected void GetTime(object sender, EventArgs e)
        {
            ExibirChat();
        }

    }
}