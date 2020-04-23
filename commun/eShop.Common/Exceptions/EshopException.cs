using System;

namespace eShop.Common.Exceptions
{
    public class EshopException : Exception
    {
        public string Code { get; }

        public EshopException()
        {
        }

        public EshopException(string code)
        {
            Code = code;
        }

        public EshopException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        public EshopException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        public EshopException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public EshopException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }        
    }
}