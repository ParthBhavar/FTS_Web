namespace Email
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System;

    public interface IEmailSender
    {
        /// <summary>
        /// Sends the email asynchronous without attachment
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <returns>Async task</returns>
        Task SendEmailAsync(string email, string subject, string body);

        /// <summary>
        /// Sends the email asynchronous with attachment.
        /// </summary>
        /// <param name="emails">The emails.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="attachments"></param>
        /// <param name="body"></param>
        /// <returns>async task</returns>
        Task SendEmailAsync(List<string> emails, string subject, List<Attachments> attachments, string body);

        /// <summary>
        /// Sends the email asynchronous with attachment.
        /// </summary>
        /// <param name="emails">The emails.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="attachments"></param>
        /// <param name="body"></param>
        /// <returns>async task</returns>
        Task SendEmailAsync(List<string> emails, string subject, List<MemoryStream> attachments, string body);

        /// <summary>
        /// Sends bulk emails asynchronous with attachment.
        /// </summary>
        /// <param name="emails">The emails.</param>
        /// <param name="subject">The subject.</param>        
        /// <param name="attachment">The attachment.</param>
        /// <returns>async task</returns>
        Task SendBulkEmailAsync(string emails, string subject, List<Attachments> attachments,string tmDocumentString, string emailTemplatePath);

         Task SendEmailAsync(List<string> emails, string subject, Stream attachments, string body);
        Task SendEmailAsync1(List<string> emails, string subject, Stream attachments, string body);
    }
}
