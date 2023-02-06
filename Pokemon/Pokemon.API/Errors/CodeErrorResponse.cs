namespace Pokemon.API.Errors
{
    /// <summary>
    /// Class with the response structure for the client.
    /// </summary>
    public class CodeErrorResponse
    {
        public Int32 StatusCode { get; set; }

        public String? Message { get; set; }

        public CodeErrorResponse(Int32 statusCode, String? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        private String GetDefaultMessageStatusCode(Int32 statusCode)
        {
            return statusCode switch
            {
                400 => "The Request sent has errors",
                401 => "You do not have authorization for this resource",
                404 => "The requested resource was not found",
                500 => "Server errors occur",
                _ => String.Empty
            };
        }
    }
}
