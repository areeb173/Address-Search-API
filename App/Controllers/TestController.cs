using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// Return "Hello"
        /// </summary>
        /// <param name="name">parameter description</param>
        /// <returns>string name</returns>
        /// <response code="200">Returns name</response>
        /// <response code="400">The input is empty or null</response>

        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> Get([FromQuery] string name)
        {
            //if (string.IsNullOrEmpty(name)) return BadRequest("no input");
            return $"hello, {name}";
        }
    }
}
