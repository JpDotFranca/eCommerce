using FluentResults;
using Microsoft.AspNetCore.Http;

namespace Customers;

public static class ResultExtensions
{
    public static IResult Return<T>(this Result<T> result)
        => result.IsSuccess ? Results.Ok() : Results.BadRequest();

    public static IResult Return<PayloadType>(this Result result, PayloadType payload)
        => result.IsSuccess ? Results.Ok(payload) : Results.BadRequest(result.Errors);
}