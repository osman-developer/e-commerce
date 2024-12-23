using Microsoft.AspNetCore.Mvc;
using Shopping.webAPI.Errors;

namespace Shopping.webAPI.Controllers {

  [Route ("errors/{code}")]
  [ApiExplorerSettings(IgnoreApi =true)]
  public class ErrorController : BaseApiController {
    public IActionResult Error (int code) {
      return new ObjectResult (new ApiResponse (code));
    }
  }
}