namespace FTS.Model.Constants
{
    public static class Constants
    {
        #region Logging

        public static class LogMessages
        {

            public const string GETERRORMESSAGE = "Error while getting data from";
            public const string ADDERRORMESSAGE = "Error while adding data from";
            public const string UPDATEERRORMESSAGE = "Error while updating data from";
            public const string DELETEERRORMESSAGE = "Error while deleting data from";
        }

        public static class ErrorMessages
        {

            #region User
            public const string REQUIREDFIELD = "This is required field.";
            public const string REQUIREDFIRSTNAME = "RequiredFirstName";

            public const string REQUIREDLASTNAME = "RequiredLastName";

            public const string REQUIREDEMAILID = "RequiredEmailId";

            public const string INVALIDEMAILID = "InvalidEmailId";

            public const string REQUIREDPASSWORD = "RequiredPassword";

            public const string PASSWORDVALIDATEREGEX = "PasswordValidateRegex";

            public const string REQUIREDCONFIRMPASSWROD = "RequiredConfirmPassword";

            public const string MATCHPASSWORDWITHCONFIRMPASSWORD = "MatchPasswordWithConfirmPassword";

            public const string REQUIREDROLE = "RequiredRole";

            public const string ERRORINCHANGINGPASSWORD = "ErrorInChangingPassword";

            public const string EMAILORKYBNOTCONFIRMED = "EmailOrKybNotConfirmed";

            public const string INVALIDCREDENTIAL = "InvalidCredential";

            public const string ERRORINREGISTERUSER = "ErrorInRegisterUser";

            public const string USERDOESNOTEXIST = "User does not exist.";

            public const string ERRORINIDENTIFYLINK = "ErrorInIdentifyLink";

            public const string ERRORINSENDINGMAIL = "ErrorInSendingEmail";

            public const string EMAILNOTEXISTORNOTCONFIRMED = "EmailNotExistOrNotConfirmed";

            public const string SUCCESSUPDATEPROFILE = "SuccessUpdateProfile";

            public const string ERRORUPDATEPROFILE = "ErrorUpdateProfile";

            public const string ERRORINCOMPLETINGKYBPROCESS = "ErrorInCompletingKybProcess";

            public const string ERRORINRESETPASSWORD = "ErrorInResetPassword";
            public const string EMAILNOTCONFIRMED = "Email not confirmed.";
            #endregion
        }

        #endregion

        public static class Cookie
        {
            #region Cookies
            public const string REMEMBERMECOOKIE = "RememberMeCookie";
            #endregion
        }

        public static class ClaimType
        {
            public const string Permission = "Permission";
        }
        public static class Formats
        {
            public const string DateFormat = "yyyy-MM-dd";
            public const string EuroSign = "\u20AC";

        }

        public static class RegexConstants
        {
            public const string UrlFormat = @"^(http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$";
            public const string EmailFormat = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,15}$";
            public const string PhoneNumberFormat = @"^[0-9]{2}\ [0-9]{3}\ [0-9]{2}\ [0-9]{2}\ [0-9]{2}$";
        }

        public static class DateTimeConstants
        {
            public const string ddMMyyyy = "dd/MM/yyyy";
            public const string MMddyyyy = "MM/dd/yyyy";
            public const string yyyyMMddHHmm = "yyyyMMddHHmm";
            public const string yyyyMMddHHmmss = "yyyyMMddHHmmss";
            public const string yyyyMMdd = "yyyyMMdd";
            public const string HHmm = "HH:mm";
            public const string HHmmtt = "HH:mm tt";
        }

        public static class PrefixConstants
        {
            public const string OR = "OR";
            public const string TXNOR = "TXNOR";
            public const string IN = "IN";
            public const string TXNQR = "TXNQR";
        }

        public static class FileExtensions
        {
            public const string JPGExtension = ".jpg";
            public const string JPEGExtension = ".jpeg";
            public const string PNGExtension = ".png";
        }

        public static class LanguageConstants
        {
            public const string NL = "nl";
            public const string FR = "fr";
            public const string EN = "en";
        }

        #region Email
        public static class Email
        {
            #region templates

            public const string CONFIRMATIONEMAILTEMPLATE_EN = "templates\\en\\emailTemplate.html";
            public const string CONFIRMATIONEMAILTEMPLATE_FR = "templates\\fr\\emailTemplate.html";
            public const string CONFIRMATIONEMAILTEMPLATE_NL = "templates\\nl\\emailTemplate.html";

            public const string REACTIVATIONACCOUNTEMAILTEMPLATE_EN = "templates\\en\\reactivationAccountEmailTemplate.html";
            public const string REACTIVATIONACCOUNTEMAILTEMPLATE_FR = "templates\\fr\\reactivationAccountEmailTemplate.html";
            public const string REACTIVATIONACCOUNTEMAILTEMPLATE_NL = "templates\\nl\\reactivationAccountEmailTemplate.html";

            public const string RESETPASSWORDTEMPLATE_EN = "templates\\en\\forgotPasswordTemplate.html";
            public const string RESETPASSWORDTEMPLATE_FR= "templates\\fr\\forgotPasswordTemplate.html";
            public const string RESETPASSWORDTEMPLATE_NL = "templates\\nl\\forgotPasswordTemplate.html";

            public const string USERSECURITYUPDATEDTEMPLATE_EN = "templates\\en\\userSecurityUpdated.html";
            public const string USERSECURITYUPDATEDTEMPLATE_FR = "templates\\fr\\userSecurityUpdated.html";
            public const string USERSECURITYUPDATEDTEMPLATE_NL = "templates\\nl\\userSecurityUpdated.html";

            public const string REGISTERUSERTEMPLATE_EN = "templates\\en\\registerUser.html";
            public const string REGISTERUSERTEMPLATE_FR = "templates\\fr\\registerUser.html";
            public const string REGISTERUSERTEMPLATE_NL = "templates\\nl\\registerUser.html";

            public const string QR_CHEQUE_TEMPLATE_EN = "templates\\en\\qrChequeEmailTemplate.html";
            public const string QR_CHEQUE_TEMPLATE_FR = "templates\\fr\\qrChequeEmailTemplate.html";
            public const string QR_CHEQUE_TEMPLATE_NL = "templates\\nl\\qrChequeEmailTemplate.html";

            public const string INVOICE_TEMPLATE_EN = "templates\\en\\invoiceTemplate.html";
            public const string INVOICE_TEMPLATE_FR = "templates\\fr\\invoiceTemplate.html";
            public const string INVOICE_TEMPLATE_NL = "templates\\nl\\invoiceTemplate.html";

            #endregion

            #region Subject

            public const string CONFIRMATIONEMAILSUBJECT_EN = "Confirm your email";
            public const string CONFIRMATIONEMAILSUBJECT_FR = "confirmez votre email";
            public const string CONFIRMATIONEMAILSUBJECT_NL = "bevestig je email";

            public const string REACTIVATIONACCOUNTEMAILSUBJECT_EN = "Say Goodbye";
            public const string REACTIVATIONACCOUNTEMAILSUBJECT_FR = "Dites au revoir";
            public const string REACTIVATIONACCOUNTEMAILSUBJECT_NL = "Zeg vaarwel";

            public const string RESETPASSWORDSUBJECT_EN = "Reset Password";
            public const string RESETPASSWORDSUBJECT_FR = "réinitialiser le mot de passe";
            public const string RESETPASSWORDSUBJECT_NL = "Reset wachtwoord";

            public const string USERSIGNUPSUBJECT_EN = "Welcome To Demo";
            public const string USERSIGNUPSUBJECT_FR = "Bienvenue chez Demo";
            public const string USERSIGNUPSUBJECT_NL = "Welkom bij Demo";

            public const string SECURITYSETTINGSSUBJECT_EN = "Security Settings";
            public const string SECURITYSETTINGSSUBJECT_FR = "Les paramètres de sécurité";
            public const string SECURITYSETTINGSSUBJECT_NL = "Veiligheidsinstellingen";

            public const string QR_CHEQUE_SUBJECT_EN = "Received QR Cheque";
            public const string QR_CHEQUE_SUBJECT_FR = "Reçu QR Check";
            public const string QR_CHEQUE_SUBJECT_NL = "QR-controle ontvangen";

            public const string INVOICE_SUBJECT_EN = "Invoice Copy";
            public const string INVOICE_SUBJECT_FR = "Copie facture";
            public const string INVOICE_SUBJECT_NL = "Factuurkopie";

            #endregion

            #region Links

            public const string CONFIRMATIONEMAILLINK = "confirmemail";

            public const string REACTIVATIONCONFIRMATIONMAILLINK = "reactivationaccountmail";

            public const string FORGOTPASSWORDLINK = "resetpassword";

            public const string SETPASSWORDLINK = "set-password";

            #endregion
        }

        #endregion

        #region Invoice

        public static class Templates
        {
            public const string INVOICETEMPLATE_EN = "Templates\\InvoiceTemplates\\en\\invoiceTemplate.html";
            public const string INVOICETEMPLATE_NL = "Templates\\InvoiceTemplates\\nl\\invoiceTemplate.html";
            public const string INVOICETEMPLATE_FR = "Templates\\InvoiceTemplates\\fr\\invoiceTemplate.html";

            public const string ORG_USER_ORDER_TEMPLATE_EN = "Templates\\en\\organizationUserOrderTemplate.html";
            public const string ORG_USER_ORDER_TEMPLATE_NL = "Templates\\nl\\organizationUserOrderTemplate.html";
            public const string ORG_USER_ORDER_TEMPLATE_FR = "Templates\\fr\\organizationUserOrderTemplate.html";

            public const string SUP_ORDER_TEMPLATE_EN = "Templates\\en\\supplierOrderTemplate.html";
            public const string SUP_ORDER_DETAIL_TEMPLATE_EN = "Templates\\en\\supplierOrderDetailTemplate.html";            
            public const string SUP_ORDER_TEMPLATE_NL = "Templates\\nl\\supplierOrderTemplate.html";
            public const string SUP_ORDER_DETAIL_TEMPLATE_NL = "Templates\\nl\\supplierOrderDetailTemplate.html";
            public const string SUP_ORDER_TEMPLATE_FR = "Templates\\fr\\supplierOrderTemplate.html";
            public const string SUP_ORDER_DETAIL_TEMPLATE_FR = "Templates\\fr\\supplierOrderDetailTemplate.html";
            public const string FOOTER_TEMPLATE = "Templates\\footer.html";
        }

        #endregion

        #region Payconiq Api Constants

        public static class PayconiqApiConstants
        {
            public const string GetPaymentDetails = "payments";
        }

        #endregion

        #region Images

        public static class DefaultImages
        {
            public const string NOIMGAVAILABLE = "img\\no-image-available.jpg";
            public const string NOIMGAVAILABLEPNG = "img\\no-image-available.png";
            public const string LOGO = "img\\logo.png";
        }

        #endregion

        #region AmountConversion

        public static class AmountConversion
        {
            public const int Cent = 100;
        }
        #endregion

    }
}
