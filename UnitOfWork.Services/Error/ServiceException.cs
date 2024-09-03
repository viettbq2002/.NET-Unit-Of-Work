using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork.Services.Error
{
    public class ServiceException : Exception
    {


        public ServiceException(string? message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }





        public int StatusCode { get; set; }
    }
}
