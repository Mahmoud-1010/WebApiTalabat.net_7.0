using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatApi.Errors;

namespace TalabatApi.Controllers
{
    [Route("Errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : BaseController
    {
        public ActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        } 
    }
}
