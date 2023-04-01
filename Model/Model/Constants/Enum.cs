namespace FTS.Model
{   
    public static class Enum
    {

        #region Role

        public enum Role
        {
            SuperAdmin = 1,
            Organization = 2,
            Supplier = 3,
            Employee = 4
        }

        #endregion


        #region Token Type

        public enum TokenType
        {
            SetPassword = 1
        }

        #endregion

        #region LanguageType

        public enum LanguageType
        {
            EN = 1,
            FR = 2,
            NL = 3
        }

        #endregion
    }
}
