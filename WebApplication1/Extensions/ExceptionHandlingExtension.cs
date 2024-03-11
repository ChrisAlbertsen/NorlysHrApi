using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace HrApi.Extensions;

public static class ExceptionHandlingExtension
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

            var exceptionDetails = context.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionDetails?.Error;

            logger.LogError(exception,
                "Request could not process on machine: {Machine}. TradeId: {TradeId}",
                Environment.MachineName,
                Activity.Current?.Id);

            await Results.Problem(
                title: "An error occurred",
                statusCode: StatusCodes.Status500InternalServerError,
                extensions: new Dictionary<string, object?>
                {
                    {"tradeId", Activity.Current?.Id },
                    {"message", exception?.Message }
                }
            )
            .ExecuteAsync(context);
        });
    }
}
