using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UnitOfWork.Services.Error;

namespace WebAPI.Filter
{
    public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var problemsDetails = new ProblemDetails
            {
                Detail = exception.Message,
                Status=  exception switch
                {
                    ServiceException serviceException => serviceException.StatusCode,
                    _ => 500
                },
                
                
                
            };
            context.Result = new ObjectResult(problemsDetails);
            context.ExceptionHandled = true;
            
        }
    }
}
