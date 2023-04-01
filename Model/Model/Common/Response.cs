namespace FTS.Model.Common
{
    using System.ComponentModel;

    public enum HttpStatus
    {
        [Description("Ok")]
        Ok=1,
        [Description("Fail")]
        Fail=0,
        [Description("Success")]
        Success=2,
        [Description("AlreadyExist")]
        AlreadyExist=3,
        LockedOut=4
    }

    public class JsonResponse
    {
        public HttpStatus Status { get; set; }

        public dynamic Data { get; set; }

        public string Message { get; set; }
    }
}
