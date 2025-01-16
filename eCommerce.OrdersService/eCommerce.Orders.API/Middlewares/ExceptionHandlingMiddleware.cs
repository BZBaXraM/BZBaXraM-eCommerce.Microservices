namespace eCommerce.Orders.API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError("{S}: {ExMessage}", ex.GetType().ToString(), ex.Message);

            if (ex.InnerException is not null)
            {
                logger.LogError("{S}: {ExMessage}", ex.InnerException.GetType().ToString(), ex.InnerException.Message);
            }

            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new
            {
                Message = ex.Message,
                Type = ex.GetType().ToString()
            });
        }
    }
}