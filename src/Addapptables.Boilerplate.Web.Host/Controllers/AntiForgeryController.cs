using Microsoft.AspNetCore.Antiforgery;
using Addapptables.Boilerplate.Controllers;

namespace Addapptables.Boilerplate.Web.Host.Controllers
{
    public class AntiForgeryController : BoilerplateControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
