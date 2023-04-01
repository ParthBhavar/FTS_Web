namespace Email
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Reflection;
    using System.Threading.Tasks;
    using Email.Options;
    using Logger;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json.Linq;
    using Org.BouncyCastle.Utilities;


    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogging _logger;
        //public ICommonListBI _Commompository;

        /// <summary>Initializes a new instance of the <see cref="EmailSender"/> class.</summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="loggerFactory"></param>
        public EmailSender(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<EmailSender>();
            //_Commompository = commompository;
            var emailSection = configuration.GetSection("EmailSettings");
            _emailSettings = new EmailSettings
            {
                Host = emailSection["Host"],
                Port = Convert.ToInt32(emailSection["Port"]),
                From = emailSection["From"],
                Password = emailSection["Password"],
                UserName = emailSection["UserName"],
                SSL = Convert.ToBoolean(emailSection["SSL"])
            };
        }

        /// <summary>
        /// Sends the email asynchronous without attachment
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <returns>Async task</returns>
        public async Task SendEmailAsync(string email, string subject, string body)
        {

            //MailMessage Msg = new MailMessage();
            //// Sender e-mail address.
            //Msg.From = new MailAddress("rajgorchirag823@gmail.com");
            //// Recipient e-mail address.
            //Msg.To.Add("rajgorchirag823@gmail.com");
            //Msg.CC.Add("rajgorchirag823@gmail.com");
            //Msg.Subject = "Timesheet Payment Instruction updated";
            //Msg.IsBodyHtml = true;
            //Msg.Body = "Test";

            //var userName = _emailSettings.UserName;
            //var password = _emailSettings.Password;
            //var Credentials = new NetworkCredential(userName, password);

            //SmtpClient smtp = new SmtpClient();
            //smtp.Credentials = Credentials;
            //smtp.EnableSsl = false;
            //smtp.Host = _emailSettings.Host;
            //smtp.Port = _emailSettings.Port;
            //smtp.Send(Msg);

            try
            {
                var userName = _emailSettings.UserName;
                var password = _emailSettings.Password;
                var message =
                    new MailMessage(_emailSettings.From, email, subject, body) { IsBodyHtml = true };

                var client =
                    new SmtpClient(_emailSettings.Host, _emailSettings.Port)
                    {
                        Credentials = new NetworkCredential(userName, password),
                        EnableSsl= Convert.ToBoolean(_emailSettings.SSL),
                    };
               
                client.SendCompleted += (s, e) =>
                {
                    client.Dispose();
                    message.Dispose();
                };

                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                _logger.Error(GetType().Name + " => " + MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        /// <summary>
        /// Sends the email asynchronous with attachment.
        /// </summary>
        /// <param name="emails">The emails.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="attachments"></param>
        /// <param name="body"></param>
        /// <returns>async task</returns>
        public async Task SendEmailAsync(List<string> emails, string subject, List<Attachments> attachments, string body)
        {
            var userName = _emailSettings.UserName;
            var password = _emailSettings.Password;
            var message = new MailMessage();
            foreach (var email in emails)
            {
                message.To.Add(email);
            }
            message.From = new MailAddress(_emailSettings.From);
            message.Subject = subject;
            message.Body = body;

            //foreach (Attachments item in attachments)
            //{
            //    System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
            //    Attachment attach = new Attachment(item.FileName, ct)
            //    {
            //        Name = item.FileName
            //    };

                
            //    message.Attachments.Add(attach);
            //}

            foreach (var attachmentFile in attachments)
            {
                //string result = Path.GetFileName(attachmentFile.FileName);
                message.Attachments.Add(new System.Net.Mail.Attachment(attachmentFile.FileName));
            }
            message.IsBodyHtml = true;

            var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = Convert.ToBoolean(_emailSettings.SSL)
            };

            client.SendCompleted += (s, e) =>
            {
                client.Dispose();
                message.Dispose();
            };
           
            await client.SendMailAsync(message);
        }



        /// <summary>
        /// Sends the email asynchronous with attachment.
        /// </summary>
        /// <param name="emails">The emails.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="attachments"></param>
        /// <param name="body"></param>
        /// <returns>async task</returns>
        public async Task SendEmailAsync(List<string> emails, string subject, List<MemoryStream> attachments, string body)
        {
            var userName = _emailSettings.UserName;
            var password = _emailSettings.Password;
            var message = new MailMessage();
            foreach (var email in emails)
            {
                message.To.Add(email);
            }
            message.From = new MailAddress(_emailSettings.From);
            message.Subject = subject;
            message.Body = body;

            //foreach (Attachments item in attachments)
            //{
            //    System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
            //    Attachment attach = new Attachment(item.FileName, ct)
            //    {
            //        Name = item.FileName
            //    };


            //    message.Attachments.Add(attach);
            //}
            System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
            System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(attachments[0], ct);
            attach.ContentDisposition.FileName = "test.pdf";
            message.Attachments.Add(attach);
            //foreach (MemoryStream attachmentFile in attachments)
            //{
            //    //string result = Path.GetFileName(attachmentFile.FileName);
                
            //    //message.Attachments.Add(new Attachment(attachmentFile, "iTextSharpPDF.pdf"));
               
            //}
            message.IsBodyHtml = true;

            var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = Convert.ToBoolean(_emailSettings.SSL)
            };

            client.SendCompleted += (s, e) =>
            {
                client.Dispose();
                message.Dispose();
            };
            await client.SendMailAsync(message);
        }

        /// <summary>
        /// Sends the email asynchronous with attachment.
        /// </summary>
        /// <param name="emails">The emails.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="attachments"></param>
        /// <param name="tmDocumentString"></param>
        /// <param name="emailTemplatePath"></param>
        /// <returns>async task</returns>
        public async Task SendBulkEmailAsync(string emails, string subject, List<Attachments> attachments, string tmDocumentString, string emailTemplatePath)
        {
            var userName = _emailSettings.UserName;
            var password = _emailSettings.Password;
            var message = new MailMessage();
            string to = emails;
            message.From = new MailAddress(_emailSettings.From);
            message.To.Add(to);
            message.Subject = subject;
            JObject objContract = JObject.Parse(tmDocumentString);
            message.Body = CreateBody(objContract, subject, emailTemplatePath);
            foreach (Attachments item in attachments)
            {
                AddAttachments(message, item);
            }
            message.IsBodyHtml = true;
            var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = Convert.ToBoolean(_emailSettings.SSL)
            };
            client.SendCompleted += (s, e) =>
            {
                client.Dispose();
                message.Dispose();
            };
            await client.SendMailAsync(message);
        }

        private void AddAttachments(MailMessage message, Attachments item)
        {
            try
            {
                System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
                Attachment attach = new Attachment(item.File, ct)
                {
                    Name = item.FileName
                };
                message.Attachments.Add(attach);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected string CreateBody(JObject objContract, string subject, string emailTemplatePath)
        {
            string body = String.Empty;
            subject = subject.Substring(1, 24);
            var path = emailTemplatePath + "\\Templates\\emailTemplate.html";
            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{client}", objContract["ClientName"].ToString());
            body = body.Replace("{year}", DateTime.Now.Year.ToString());
            body = body.Replace("{date}", subject);
            return body;
        }

        public async Task SendEmailAsync(List<string> emails, string subject, Stream attachments, string body)
        {
            var userName = _emailSettings.UserName;
            var password = _emailSettings.Password;
            var message = new MailMessage();
            foreach (var email in emails)
            {
                message.To.Add(email);
            }
            message.From = new MailAddress(_emailSettings.From);
            message.Subject = subject;
            message.Body = body;

            System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);

            message.Attachments.Add(new Attachment(attachments, "iTextSharpPDF.pdf"));
            message.IsBodyHtml = true;

            var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = Convert.ToBoolean(_emailSettings.SSL)
            };

            client.SendCompleted += (s, e) =>
            {
                client.Dispose();
                message.Dispose();
            };
            await client.SendMailAsync(message);
        }

        public async Task SendEmailAsync1(List<string> emails, string subject, Stream attachments, string body)
        {
            var userName = _emailSettings.UserName;
            var password = _emailSettings.Password;
            var message = new MailMessage();
            foreach (var email in emails)
            {
                message.To.Add(email);
            }
            message.From = new MailAddress(_emailSettings.From);
            message.Subject = subject;
            message.Body = body;

            System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);

            message.Attachments.Add(new Attachment(attachments, "iTextSharpPDF.pdf"));
            message.IsBodyHtml = true;

            var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = Convert.ToBoolean(_emailSettings.SSL)
            };

            client.SendCompleted += (s, e) =>
            {
                client.Dispose();
                message.Dispose();
            };
            await client.SendMailAsync(message);
        }
    }
}
