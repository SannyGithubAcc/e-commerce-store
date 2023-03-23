namespace e_commerce_store.Exceptions
{
    public class CustomException : Exception
    {
        private readonly int  v;

        public int StatusCode { get; set; }

        public CustomException(string message, Exception ex, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public CustomException(string? message, int v) : base(message)
        {
            this.v = v;
        }
    }

}
