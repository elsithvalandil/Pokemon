namespace Pokemon.API.Errors
{
    /// <summary>
    /// 
    /// </summary>
    public class CodeErrorException : CodeErrorResponse
    {
        public CodeErrorException(Int32 statusCode, String? message = null) : base(statusCode, message)
        {
           
        }
    }
}
