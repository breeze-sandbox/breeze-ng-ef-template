using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Template.Web.Models
{
    public class AppException : HttpResponseException
    {
        public AppException(int code, string message) : this((HttpStatusCode) code, message)
        {
        }

        public AppException(HttpStatusCode code, string message) : base(code)
        {
            this.Response.StatusCode = code;
            this.Response.ReasonPhrase = message;
        }
    }
}