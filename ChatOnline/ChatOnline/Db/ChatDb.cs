using ChatOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatOnline.Db
{
    public class ChatDb
    {
        //Chaves para gravar em Application
        private const string chaveChat = "chat";
        private const string chaveparticipantes = "participantes";
        private const string adminNome = "admin";
        private const string adminMensagemInicial = "Chat Iniciado com sucesso";

        //
        // Mensagens
        //
        public static List<ChatMensagem> Mensagens
        {
            get
            {
                return
                (List<ChatMensagem>)HttpContext
                .Current
                .Application[chaveChat];
            }
        }

        //
        // Participantes
        //
        public static List<string> Participantes
        {
            get
            {
                return (List<string>)HttpContext
                .Current
                .Application[chaveparticipantes];
            }
        }

        internal static void Iniciar()
        {
            // Lista de Mensagens
            var lista = new List<ChatMensagem>();
            HttpContext.Current.Application.Add(chaveChat, lista);

            // Lista de Participantes On-Line
            var participantes = new List<string>();

            HttpContext.Current.Application.Add(chaveparticipantes, participantes);
            participantes.Add(adminNome);
        }

        // Validar Mensagem
        //
        private static void ValidarMensagem(string participante, string mensagem)
        {
            if (string.IsNullOrEmpty(participante) ||
            participante.Trim().Length == 0)
            {
                throw new Exception(
                "Participante não informado");
            }
            if (string.IsNullOrEmpty(mensagem) ||
            mensagem.Trim().Length == 0)
            {
                throw new Exception("Mensagem não informada");
            }
        }

        // Enviar Mensagem
        //
        //
        public static void EnviarMensagem(string participante, string mensagem)
        {
            ValidarMensagem(participante, mensagem);

            var msg = new ChatMensagem();
            msg.DataHora = DateTime.Now;
            msg.Participante = participante;
            msg.Mensagem = mensagem;

            Mensagens.Add(msg);
        }

        //
        // Validar Entrar
        //
        private static void ValidarEntrar(string participante)
        {
            if (string.IsNullOrEmpty(participante))
            {
                throw new Exception(
                "Informe o nome do participante");
            }
            foreach (string nome in Participantes)
            {
                if (participante == nome)
                {
                    throw new Exception(
                    "Este nome já está sendo usado.");
                }
            }
        }

        //
        // Entrar
        //
        public static void Entrar(string participante)
        {
            ValidarEntrar(participante);
            Participantes.Add(participante);
            string msg = string.Format("{0} entrou no chat.", participante);

            EnviarMensagem(adminNome, msg);
        }

        //
        // Últimas Mensagens
        //
        public static List<ChatMensagem> UltimasMensagens()
        {
            var lista = (from m in Mensagens
                         orderby m.DataHora descending
                         select m).Take(100);

            return lista.ToList();
        }
    }
}