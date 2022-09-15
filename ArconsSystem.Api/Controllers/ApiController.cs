using Microsoft.AspNetCore.Mvc;

namespace ArconsSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        public IActionResult Problem(int code, string message)
        {
            var statusCode = code switch {
                400 => StatusCodes.Status400BadRequest,
                401 => StatusCodes.Status401Unauthorized,
                404 => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(statusCode: statusCode, title: message);
        }
    }
}