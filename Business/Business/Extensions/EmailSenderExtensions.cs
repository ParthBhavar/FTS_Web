namespace FTS.Email
{
    using global::Email;
    using System.IO;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using FTS.Model.Constants;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using static FTS.Model.Constants.Constants;
    using static FTS.Model.Enum;

    public static class EmailSenderExtensions
    {
        public static string RootPath { get; set; }

        /// <summary>Sends the email confirmation asynchronous.</summary>
        /// <param name="emailSender">The email sender tool object</param>
        /// <param name="email">To email address</param>
        /// <param name="link">Confirm email link</param>
        /// <param name="name">The name of person in To</param>
        /// <returns>Async task</returns>
        public static Task SendEmailConfirmationAsync(
            this IEmailSender emailSender,
            string email,
            string link,
            string name)
        {

            string emailTemplate, subject;
            switch (CultureInfo.CurrentCulture.ToString())
            {
                case LanguageConstants.EN:
                    emailTemplate = Email.CONFIRMATIONEMAILTEMPLATE_EN;
                    subject = Email.CONFIRMATIONEMAILSUBJECT_EN;
                    break;

                case LanguageConstants.NL:
                    emailTemplate = Email.CONFIRMATIONEMAILTEMPLATE_NL;
                    subject = Email.CONFIRMATIONEMAILSUBJECT_NL;
                    break;

                case LanguageConstants.FR:
                    emailTemplate = Email.CONFIRMATIONEMAILTEMPLATE_FR;
                    subject = Email.CONFIRMATIONEMAILSUBJECT_FR;
                    break;

                default:
                    emailTemplate = Email.CONFIRMATIONEMAILTEMPLATE_EN;
                    subject = Email.CONFIRMATIONEMAILSUBJECT_EN;
                    break;
            }

            string body = CreateEmailBody(name, emailTemplate);
            string htmlLink = HtmlEncoder.Default.Encode(link);
            body = body.Replace("{Link}", htmlLink);
            return emailSender.SendEmailAsync(email, subject, body);
        }

        /// <summary>
        /// To send an Reactivation Link to User
        /// </summary>
        /// <param name="emailSender"></param>
        /// <param name="email"></param>
        /// <param name="link"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Task SendReactivationEmailConfirmationAsync(
            this IEmailSender emailSender,
            string email,
            string link,
            string name)
        {
            string emailTemplate, subject;
            switch (CultureInfo.CurrentCulture.ToString())
            {
                case LanguageConstants.EN:
                    emailTemplate = Email.REACTIVATIONACCOUNTEMAILTEMPLATE_EN;
                    subject = Email.REACTIVATIONACCOUNTEMAILSUBJECT_EN;
                    break;

                case LanguageConstants.NL:
                    emailTemplate = Email.REACTIVATIONACCOUNTEMAILTEMPLATE_NL;
                    subject = Email.REACTIVATIONACCOUNTEMAILSUBJECT_NL;
                    break;

                case LanguageConstants.FR:
                    emailTemplate = Email.REACTIVATIONACCOUNTEMAILTEMPLATE_FR;
                    subject = Email.REACTIVATIONACCOUNTEMAILSUBJECT_FR;
                    break;

                default:
                    emailTemplate = Email.REACTIVATIONACCOUNTEMAILTEMPLATE_EN;
                    subject = Email.REACTIVATIONACCOUNTEMAILSUBJECT_EN;
                    break;
            }

            string body = CreateEmailBody(name, emailTemplate);
            string htmlLink = HtmlEncoder.Default.Encode(link);
            body = body.Replace("{Link}", htmlLink);
            return emailSender.SendEmailAsync(email, subject, body);
        }

        /// <summary>Function to send reset password mail</summary>
        /// <param name="emailSender">Object of email sender tool</param>
        /// <param name="email">To email</param>
        /// <param name="link">Reset password link</param>
        /// <param name="name">Name of the person to mail</param>
        public static Task SendResetPasswordLinkAsync(
            this IEmailSender emailSender,
            string email,
            string link,
            string name,
            int languageId)
        {
            string emailTemplate, subject;
            switch (languageId)
            {
                case (int)LanguageType.EN:
                    emailTemplate = Email.RESETPASSWORDTEMPLATE_EN;
                    subject = Email.RESETPASSWORDSUBJECT_EN;
                    break;

                case (int)LanguageType.NL:
                    emailTemplate = Email.RESETPASSWORDTEMPLATE_NL;
                    subject = Email.RESETPASSWORDSUBJECT_NL;
                    break;

                case (int)LanguageType.FR:
                    emailTemplate = Email.RESETPASSWORDTEMPLATE_FR;
                    subject = Email.RESETPASSWORDSUBJECT_FR;
                    break;

                default:
                    emailTemplate = Email.RESETPASSWORDTEMPLATE_EN;
                    subject = Email.RESETPASSWORDSUBJECT_EN;
                    break;
            }

            string body = CreateEmailBody(name, emailTemplate);
            string htmlLink = HtmlEncoder.Default.Encode(link);
            body = body.Replace("{Link}", htmlLink);
            return emailSender.SendEmailAsync(email, subject, body);
        }

        /// <summary>Sends the email confirmation asynchronous.</summary>
        /// <param name="emailSender">The email sender tool object</param>
        /// <param name="email">To email address</param>        
        /// <param name="name">The name of person in To</param>
        /// <returns>Async task</returns>
        public static Task UserSecuritySendMailAsync(
            this IEmailSender emailSender,
            string email,
            string name)
        {
            string body = string.Empty;

            string emailTemplate, subject;
            switch (CultureInfo.CurrentCulture.ToString())
            {
                case LanguageConstants.EN:
                    emailTemplate = Email.USERSECURITYUPDATEDTEMPLATE_EN;
                    subject = Email.SECURITYSETTINGSSUBJECT_EN;
                    break;

                case LanguageConstants.NL:
                    emailTemplate = Email.USERSECURITYUPDATEDTEMPLATE_NL;
                    subject = Email.SECURITYSETTINGSSUBJECT_NL;
                    break;

                case LanguageConstants.FR:
                    emailTemplate = Email.USERSECURITYUPDATEDTEMPLATE_FR;
                    subject = Email.SECURITYSETTINGSSUBJECT_FR;
                    break;

                default:
                    emailTemplate = Email.USERSECURITYUPDATEDTEMPLATE_EN;
                    subject = Email.SECURITYSETTINGSSUBJECT_EN;
                    break;
            }

            using (var reader = new StreamReader(Path.Combine(RootPath, emailTemplate)))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{USERNAME}", name); //replacing the required things                                                                      
            body = body.Replace("{DATE}", DateTime.Now.ToString()); //replacing the required things                                                                      
            return emailSender.SendEmailAsync(email, subject, body);
        }

        /// <summary>Function to send user registration mail</summary>
        /// <param name="emailSender">Object of email sender tool</param>
        /// <param name="email">To email</param>
        /// <param name="link">Reset password link</param>
        /// <param name="name">Name of the person to mail</param>
        public static Task SendUserRegistrationMailAsync(
            this IEmailSender emailSender,
            string email,
            string link,
            string name,
            int languageId)
        {
            string emailTemplate, subject;
            switch (languageId)
            {
                case (int)LanguageType.EN:
                    emailTemplate = Email.REGISTERUSERTEMPLATE_EN;
                    subject = Email.USERSIGNUPSUBJECT_EN;
                    break;

                case (int)LanguageType.NL:
                    emailTemplate = Email.REGISTERUSERTEMPLATE_NL;
                    subject = Email.USERSIGNUPSUBJECT_NL;
                    break;

                case (int)LanguageType.FR:
                    emailTemplate = Email.REGISTERUSERTEMPLATE_FR;
                    subject = Email.USERSIGNUPSUBJECT_FR;
                    break;

                default:
                    emailTemplate = Email.REGISTERUSERTEMPLATE_EN;
                    subject = Email.USERSIGNUPSUBJECT_EN;
                    break;
            }


            string body = CreateEmailBody(name, emailTemplate);
            string htmlLink = HtmlEncoder.Default.Encode(link);
            body = body.Replace("{Link}", htmlLink);
            return emailSender.SendEmailAsync(email, subject, body);
        }

        /// <summary>Creates the email body.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <returns>Retuns email body</returns>
        private static string CreateEmailBody(string userName, string templateName)
        {
            string body = string.Empty;

            using (StreamReader reader = new StreamReader(Path.Combine(RootPath, templateName)))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{UserName}", userName); //replacing the required things                      
            return body;
        }

        /// <summary>
        ///  Send QR cheque to user in email
        /// </summary>
        /// <param name="emailSender">Object of email sender tool</param>
        /// <param name="email">To email</param>
        /// <param name="qrChequeUrl">QR Cheque URL</param>        
        /// <returns>Name of the person to mail</returns>
        public static Task SendQRChequeMailAsync(
            this IEmailSender emailSender,
            string email,
            string qrChequeUrl)
        {
            string emailTemplate, subject;
            switch (CultureInfo.CurrentCulture.ToString())
            {
                case LanguageConstants.EN:
                    emailTemplate = Email.QR_CHEQUE_TEMPLATE_EN;
                    subject = Email.QR_CHEQUE_SUBJECT_EN;
                    break;

                case LanguageConstants.NL:
                    emailTemplate = Email.QR_CHEQUE_TEMPLATE_NL;
                    subject = Email.QR_CHEQUE_SUBJECT_NL;
                    break;

                case LanguageConstants.FR:
                    emailTemplate = Email.QR_CHEQUE_TEMPLATE_FR;
                    subject = Email.QR_CHEQUE_SUBJECT_FR;
                    break;

                default:
                    emailTemplate = Email.QR_CHEQUE_TEMPLATE_EN;
                    subject = Email.QR_CHEQUE_SUBJECT_EN;
                    break;
            }


            string body = CreateEmailBody(string.Empty, emailTemplate);
            string qrChequeUrlLink = HtmlEncoder.Default.Encode(qrChequeUrl);
            body = body.Replace("{QRChequeUrl}", qrChequeUrlLink);
            return emailSender.SendEmailAsync(email, subject, body);
        }

        /// <summary>
        /// Send Invoice mail
        /// </summary>
        /// <param name="emailSender"></param>
        /// <param name="email"></param>
        /// <param name="attachments"></param>
        /// <returns></returns>
        public static Task SendInvoiceMailAsync(
            this IEmailSender emailSender,
            string email,
            List<Attachments> attachments)
        {
            string emailTemplate, subject;
            switch (CultureInfo.CurrentCulture.ToString())
            {
                case LanguageConstants.EN:
                    emailTemplate = Email.INVOICE_TEMPLATE_EN;
                    subject = Email.INVOICE_SUBJECT_EN;
                    break;

                case LanguageConstants.NL:
                    emailTemplate = Email.INVOICE_TEMPLATE_NL;
                    subject = Email.INVOICE_SUBJECT_NL;
                    break;

                case LanguageConstants.FR:
                    emailTemplate = Email.INVOICE_TEMPLATE_FR;
                    subject = Email.INVOICE_SUBJECT_FR;
                    break;

                default:
                    emailTemplate = Email.INVOICE_TEMPLATE_EN;
                    subject = Email.INVOICE_SUBJECT_EN;
                    break;
            }

            string body = CreateEmailBody(string.Empty, emailTemplate);
            return emailSender.SendEmailAsync(new List<string> { email }, subject, attachments, body);
        }
    }
}
