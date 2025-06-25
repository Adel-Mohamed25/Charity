﻿using Charity.Application.Constants;
using Charity.Models.ResponseModels;
using System.Net;

namespace Charity.Application.Helper.ResponseServices
{
    public sealed class ResponseHandler
    {
        public static Response<TData> Success<TData>(
            TData data = null,
            string message = null,
            string meta = null,
            string errors = null) where TData : class
        {
            return new Response<TData>(
                statusCode: HttpStatusCode.OK,
                issucceeded: true,
                message: message ?? ResponseMessage.SuccessMessage,
                data: data,
                errors: errors ?? "There are no errors",
                meta: meta
            );
        }

        public static Response<TData> Created<TData>(
            TData data = null,
            string message = null,
            string meta = null,
            string errors = null
            ) where TData : class
        {
            return new Response<TData>(
                statusCode: HttpStatusCode.Created,
                issucceeded: true,
                message: message ?? ResponseMessage.CreatedMessage,
                data: data,
                errors: errors ?? "There are no errors",
                meta: meta
                );
        }

        public static Response<TData> NoContent<TData>(
           TData data = null,
           string message = null,
           string meta = null,
           string errors = null
           ) where TData : class
        {
            return new Response<TData>(
                statusCode: HttpStatusCode.NoContent,
                issucceeded: true,
                message: message ?? ResponseMessage.NoContentMessage,
                data: data,
                errors: errors ?? "There are no errors",
                meta: meta
                );
        }

        public static Response<TData> NotFound<TData>(
            TData data = null,
            string message = null,
            string meta = null,
            string errors = null) where TData : class
        {
            return new Response<TData>(
                statusCode: HttpStatusCode.NotFound,
                issucceeded: true,
                message: message ?? ResponseMessage.NotFoundMessage,
                meta: meta,
                data: data,
                errors: errors ?? "Not found data"

            );
        }

        public static Response<TData> BadRequest<TData>(
            TData data = null,
            string message = null,
            string meta = null,
            string errors = null
            ) where TData : class
        {
            return new Response<TData>(
                statusCode: HttpStatusCode.BadRequest,
                issucceeded: false,
                message: message ?? ResponseMessage.BadRequestMessage,
                meta: meta,
                data: data,
                errors: errors ?? "Bad Request"
            );
        }

        public static Response<TData> Conflict<TData>(
            TData data = null,
            string message = null,
            string meta = null,
            string errors = null
            ) where TData : class
        {
            return new Response<TData>(
                statusCode: HttpStatusCode.Conflict,
                issucceeded: false,
                message: message ?? ResponseMessage.ConflictErrorMessage,
                meta: meta,
                data: data,
                errors: errors ?? "Data conflict detected"
            );
        }

        public static Response<TData> Unauthorized<TData>(
           TData data = null,
           string message = null,
           string meta = null,
           string errors = null
           ) where TData : class
        {
            return new Response<TData>(
                statusCode: HttpStatusCode.Unauthorized,
                issucceeded: false,
                message: message ?? ResponseMessage.UnAuthorizedMessage,
                meta: meta,
                data: data,
                errors: errors ?? "Unauthorized"
            );
        }

        public static Response<TData> Forbidden<TData>(
           TData data = null,
           string message = null,
           string meta = null,
           string errors = null
           ) where TData : class
        {
            return new Response<TData>(
                statusCode: HttpStatusCode.Forbidden,
                issucceeded: false,
                message: message ?? ResponseMessage.ForbiddenMessage,
                meta: meta,
                data: data,
                errors: errors ?? "Forbidden"
            );
        }
        public static Response<TData> InternalServerError<TData>(
            TData data = null,
            string message = null,
            string meta = null,
            string errors = null
            ) where TData : class
        {
            return new Response<TData>(
                statusCode: HttpStatusCode.InternalServerError,
                issucceeded: false,
                message: message ?? ResponseMessage.InternalServerErrorMessage,
                meta: meta,
                data: data,
                errors: errors ?? "Database connection failed"
            );
        }




    }
}
