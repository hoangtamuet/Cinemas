using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Newtonsoft.Json;

namespace Cinemas
{
    public class ExceptionResponseAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            if (context.Exception is UnauthorizedException)
            {
                response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                response.ReasonPhrase = "Token is invalid";
            }
            if (context.Exception is BadRequestException)
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.ReasonPhrase = "Wrong paramenters";
            }
            if (context.Exception is ConflictException)
            {
                response = new HttpResponseMessage(HttpStatusCode.Conflict);
                response.ReasonPhrase = "Conflict data";
            }

            if (context.Exception is ForbiddenException)
            {
                response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                response.ReasonPhrase = "Forbidden";
            }
            if (context.Exception is NotFoundException)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.ReasonPhrase = "Not Found";
            }

            var Message = JsonConvert.SerializeObject(new {context.Exception.Message});
            response.Content = new StringContent(Message);
            context.Response = response;
        }
    }


    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string Message) : base(Message)
        {
        }
    }
    public class BadRequestException : Exception
    {
        public BadRequestException(string Message) : base(Message)
        {
        }
    }

    public class ForbiddenException : Exception
    {
        public ForbiddenException(string Message) : base(Message)
        {
        }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string Message) : base(Message)
        {
        }
    }

    public class ConflictException : Exception
    {
        public ConflictException(string Message) : base(Message)
        {
        }
    }
}