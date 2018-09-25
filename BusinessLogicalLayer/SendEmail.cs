using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    class SendEmail
    {
        public static void Send(Ocorrencia ocorrencia)
        {
            MailMessage mail = new MailMessage();
            string corpo = @"<html><body>
                         <h1>Notificação do site Sentinela do Vale</h1>
                         <p>Temos uma nova ocorrência informando um problema {0} em sua área de atuação.</p>
                         <p>Endereço: {1}</p>
                         <p>Descrição: {2}</p>
                         </body>
                         </html>";
            CategoriaBLL categoriaBLL = new CategoriaBLL();
            Categoria categoria = categoriaBLL.GetByID(ocorrencia);
            mail.Body = string.Format(corpo, categoria.Nome, ocorrencia.Endereco, ocorrencia.Descricao);
            mail.IsBodyHtml = true;
            mail.Subject = "Nova ocorrência em Sentinela do Vale!";
            mail.To.Add("sac.reclamacoes.sdv@gmail.com");
            mail.From = new MailAddress("TRABALHOENTRA21@GMAIL.COM");

            SmtpClient servidor = new SmtpClient("smtp.gmail.com", 587);
            servidor.EnableSsl = true;
            servidor.UseDefaultCredentials = false;
            servidor.Credentials = new NetworkCredential("trabalhoentra21@gmail.com", "csharp>java");
            servidor.Send(mail);
        }
    }
}
