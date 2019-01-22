using ChessTion.Modele.MTournoi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace ChessTion.Utilitaires
{
    /// <summary>
    /// Classe gérant l'envoi de mails.
    /// </summary>
    class Mail
    {
        /// <summary>
        /// Addresse mail de signature.
        /// </summary>
        private static readonly MailAddress FROM = new MailAddress("chesstion.noreply@gmail.com", "Chesstion NoReply");

        /// <summary>
        /// Mot de passe du compte mail.
        /// </summary>
        private static readonly string PASSWORD = "Chesstion1";

        /// <summary>
        /// Envoie un mail.
        /// </summary>
        /// <param name="toAddress">Adresse mail de destination.</param>
        /// <param name="subject">Sujet du message.</param>
        /// <param name="body">Message.</param>
        /// <param name="attachments">Pièces jointes.</param>
        public static void Send(MailAddress toAddress, string subject, string body, List<Attachment> attachments)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(FROM.Address, PASSWORD)
            };
            using (var message = new MailMessage(FROM, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                if (attachments != null)
                    foreach (Attachment a in attachments)
                        message.Attachments.Add(a);

                smtp.Send(message);
            }

        }

        /// <summary>
        /// Envoie un mail.
        /// </summary>
        /// <param name="toAddress">Adresse mail de destination.</param>
        /// <param name="subject">Sujet du message.</param>
        /// <param name="body">Message.</param>
        /// <param name="attachmentsFileNames">Chemin d'accès des pièces jointes.</param>
        public static void Send(MailAddress toAddress, string subject, string body, List<string> attachmentsFileNames = null)
        {
            List<Attachment> a = new List<Attachment>();

            if (attachmentsFileNames != null)
                foreach (string s in attachmentsFileNames)
                    if (File.Exists(s))
                        a.Add(new Attachment(s));

            Send(toAddress, subject, body, a);
        }
    }
}
