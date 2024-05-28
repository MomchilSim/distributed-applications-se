namespace MoneyTracker.Authentication
{
    public class ApiAuthMiddleWare
    {
        private readonly RequestDelegate _next;

        public ApiAuthMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(AuthConstants.Key, out var extractedKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("no Api key");
                return;
            }
            var _apiKey = AuthConstants.Val;
            if (!_apiKey.Equals(extractedKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid api key");
                return;

            }
            await _next(context);
        }
    }
}
