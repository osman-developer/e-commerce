using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Shopping.webAPI.Errors;
using SQLitePCL;

namespace Shopping.webAPI.Controllers {
  public class BuggyController : BaseApiController {
    private readonly StoreContext _context;

    public BuggyController (StoreContext context) {
      _context = context;
    }

    [HttpGet ("notfound")]
    public ActionResult GetNotFoundErrorequest () {
      var x = _context.Products.Find (42);
      if (x == null) {
        return NotFound (new ApiResponse (404));
      }
      return Ok ();
    }

    [HttpGet ("serverError")]
    public ActionResult GetServerErrorequest () {
      var x = _context.Products.Find (42);
      var y = x.ToString ();
      return Ok ();
    }

    [HttpGet ("badrequest")]
    public ActionResult GetBadRequest () {
      return BadRequest (new ApiResponse (400));
    }

    [HttpGet ("badrequest/{id}")]
    public ActionResult GetNotFoundRequest (int id) {
      return Ok ();
    }
  }
}