using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApi.Controllers
{
    [Route("/encoders")]
    [ApiController]
    public class EncoderController : ControllerBase
    {
        /// <summary>
        /// Retrieves a list of values.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /encoders
        /// </remarks>
        /// <returns>An array of string values</returns>
        /// <response code="200">Returns the list of values</response>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
