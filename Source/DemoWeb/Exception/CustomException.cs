using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWeb
{
    public class CustomException : Exception
    {
        public virtual int ErrorCode { get; set; }
        public virtual string ErrorDescription { get; set; }
        public Exception ex { get; set; }
    }

    public class BusinessLayerException : CustomException
    {
        public BusinessLayerException(int errorCode = 500, string errorDescription = "", Exception exception = null)
               : base()
        {
            base.ErrorCode = errorCode;
            base.ErrorDescription = errorDescription;
            base.ex = exception;
        }

    }
    public class DataLayerException : CustomException
    {
        public DataLayerException(int errorCode = 500, string errorDescription = "")
            : base()
        {
            base.ErrorCode = errorCode;
            base.ErrorDescription = errorDescription;
        }
    }
}