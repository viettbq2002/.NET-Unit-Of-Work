using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork.Services.Error
{
    public class NotFoundException(string message) :ServiceException(message, statusCode: StatusCodes.Status404NotFound )
    {

    }
}
