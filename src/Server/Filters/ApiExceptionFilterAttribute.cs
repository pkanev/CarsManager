using System;
using System.Collections.Generic;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Models.Exceptions;
using CarsManager.Application.Vehicles.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarsManager.Server.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
                { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
                { typeof(NonMatchingMakeException), HandleNonMatchingMakeException },
                { typeof(InvalidDeleteOperationException), HandleInvalidDeleteOperationException },
                { typeof(InvalidImageTypeException), HandleInvalidImageTypeException },
                { typeof(VehicleAlreadyRegisteredException), HandleVehicleAlreadyRegisteredException },
                { typeof(InvalidLiabilityTypeException), HandleInvalidLiabilityTypeException },
                { typeof(InvalidRoadEntryDataException), HandleInvalidRoadEntryDataException },
                { typeof(IdentityException), HandleIdentityException },
                { typeof(ForbiddenVehicleDeletionException), HandleForbiddenVehicleDeletionException },
                { typeof(ForbiddenEmployeeDeletionException), HandleForbiddenEmployeeDeletionException },
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;

            var details = new ValidationProblemDetails(exception.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = exception.Message
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleUnauthorizedAccessException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };

            context.ExceptionHandled = true;
        }

        private void HandleForbiddenAccessException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "Forbidden",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

            context.ExceptionHandled = true;
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        private void HandleNonMatchingMakeException(ExceptionContext context)
        {
            var exception = context.Exception as NonMatchingMakeException;
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = exception.Message,
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleInvalidDeleteOperationException(ExceptionContext context)
        {
            var exception = context.Exception as InvalidDeleteOperationException;
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = exception.Message,
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleInvalidImageTypeException(ExceptionContext context)
        {
            var exception = context.Exception as InvalidImageTypeException;
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = exception.Message,
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleVehicleAlreadyRegisteredException(ExceptionContext context)
        {
            var exception = context.Exception as VehicleAlreadyRegisteredException;
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = exception.Message,
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleInvalidLiabilityTypeException(ExceptionContext context)
        {
            var exception = context.Exception as InvalidLiabilityTypeException;
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = exception.Message,
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleInvalidRoadEntryDataException(ExceptionContext context)
        {
            var exception = context.Exception as InvalidRoadEntryDataException;
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = exception.Message,
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleIdentityException(ExceptionContext context)
        {
            var exception = context.Exception as IdentityException;
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = exception.Message,
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleForbiddenVehicleDeletionException(ExceptionContext context)
        {
            var exception = context.Exception as ForbiddenVehicleDeletionException;
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = exception.Message,
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleForbiddenEmployeeDeletionException(ExceptionContext context)
        {
            var exception = context.Exception as ForbiddenEmployeeDeletionException;
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = exception.Message,
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
