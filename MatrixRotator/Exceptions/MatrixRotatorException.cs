using System;
using System.ComponentModel;
using MatrixRotator.Helpers;

namespace MatrixRotator.Exceptions
{
    public enum ErrorCode
    {
        [Description("Unknown error")]
        Undefined,

        [Description("Read file error")]
        ReadFileError,

        [Description("Parsing file error")]
        ParseError,

        [Description("Write file error")]
        WriteFileError
    }

    public class MatrixRotatorException : Exception
    {
        public MatrixRotatorException() : base()
        {
        }

        public MatrixRotatorException(string message, ErrorCode errorCode = ErrorCode.Undefined, Exception innerException = null)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        public MatrixRotatorException(ErrorCode errorCode = ErrorCode.Undefined, Exception innerException = null)
            : base(errorCode.GetDescription(), innerException)
        {
            ErrorCode = errorCode;
        }

        public ErrorCode ErrorCode { get; private set; }
    }
}
