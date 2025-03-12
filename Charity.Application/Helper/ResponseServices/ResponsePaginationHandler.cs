using Charity.Application.Constants;
using Charity.Models.ResponseModels;
using System.Net;

namespace Charity.Application.Helper.ResponseServices
{
    public sealed class ResponsePaginationHandler
    {
        public static ResponsePagination<TData> Success<TData>(
           TData data = null,
           string message = null,
           string meta = null,
           string errors = null,
           int totalCount = 0,
           int pageNumber = 1,
           int pageSize = 10) where TData : class
        {
            return new ResponsePagination<TData>(
                statusCode: HttpStatusCode.OK,
                issucceeded: true,
                message: message ?? ResponseMessage.SuccessMessage,
                meta: meta,
                data: data,
                errors: errors ?? "There are no errore",
                totalCount: totalCount,
                currentPage: pageNumber,
                pageSize: pageSize

            );
        }

        public static ResponsePagination<TData> NotFound<TData>(
            string message = null,
            string meta = null,
            string errors = null,
            TData data = null,
            int totalCount = 0,
            int pageNumbre = 1,
            int pageSize = 10) where TData : class
        {
            return new ResponsePagination<TData>(
                statusCode: HttpStatusCode.NotFound,
                issucceeded: true,
                message: message ?? ResponseMessage.NotFoundMessage,
                meta: meta,
                data: data,
                errors: errors ?? "Not found data",
                totalCount: totalCount,
                currentPage: pageNumbre,
                pageSize: pageSize

            );
        }


    }
}
