namespace Shop.Web.CountryCheck
{
    public class CountryCheckMiddleware
    {
        private readonly RequestDelegate _next;


        public CountryCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //string userIP = context.Connection.RemoteIpAddress.ToString();
            string userIP = "5.52.34.96";


            ;

            try
            {

            }
            catch (Exception ex)
            {
                // Handle any exceptions
                context.Response.Redirect("/ErrorPages/IpError");
                return;
            }

            await _next(context);
        }
    }
}




