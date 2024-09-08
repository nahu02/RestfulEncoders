using System.Collections.Generic;
using EncoderHub;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApi.Controllers
{
    [Route("/encoders")]
    [ApiController]
    public class EncoderController : ControllerBase
    {
        private readonly EncoderFactory ef = new EncoderFactory();

        /// <summary>
        /// Retrieves a list of available encoders.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /encoders
        ///
        /// Sample response:
        ///
        ///     200 OK
        ///     [
        ///         "ROT13",
        ///         "MsTranslateToEnglish"
        ///     ]
        /// </remarks>
        /// <returns>An array of encoder names</returns>
        /// <response code="200">Returns the list of available encoders</response>
        [HttpGet]
        public ActionResult<IEnumerable<string>> ListEncoders()
        {
            IEnumerable<string> encoders = ef.ListAllEncoders();
            return encoders.ToList();
        }

        /// <summary>
        /// Retrieves the description of a specific encoder.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /encoders/ROT13
        ///
        /// Sample response:
        ///
        ///     200 OK
        ///     "The ROT13 algorithm is about shifting characters by 13 ..."
        /// </remarks>
        /// <param name="encoderName">The name of the encoder to describe</param>
        /// <returns>The description of the specified encoder</returns>
        /// <response code="200">Returns the description of the encoder</response>
        /// <response code="404">If the specified encoder is not found</response>
        [HttpGet("{encoderName}")]
        public ActionResult<string> GetEncoderDescription(string encoderName)
        {
            try
            {
                IEncoder encoder = ef.GetEncoder(encoderName);
                return Ok(encoder.Description);
            }
            catch (EncoderHub.Exceptions.EncoderNotFoundException)
            {
                return NotFound($"Encoder '{encoderName}' not found");
            }
        }

        /// <summary>
        /// Encodes a message using the specified encoder.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /encoders/ROT13
        ///     {
        ///         "message": "Hello, World!"
        ///     }
        ///
        /// Sample response:
        ///
        ///     200 OK
        ///     "Uryyb, Jbeyq!"
        /// </remarks>
        /// <param name="encoderName">The name of the encoder to use (e.g. ROT13)</param>
        /// <param name="message">The message to encode</param>
        /// <returns>The encoded message</returns>
        /// <response code="200">Returns the successfully encoded message</response>
        /// <response code="404">If the specified encoder is not found</response>
        /// <response code="400">If there's an error during the encoding process</response>
        [HttpPost("{encoderName}")]
        public async Task<ActionResult<string>> EncodeMessage(
            string encoderName,
            [FromBody] string message
        )
        {
            IEncoder encoder = null;

            try
            {
                encoder = ef.GetEncoder(encoderName);
            }
            catch (EncoderHub.Exceptions.EncoderNotFoundException)
            {
                return NotFound($"Encoder '{encoderName}' not found");
            }

            try
            {
                string encodedMessage = await encoder!.Encode(message);
                return Ok(encodedMessage);
            }
            catch (EncoderHub.Exceptions.EncodingException ex)
            {
                return BadRequest($"Error encoding message: {ex.Message}");
            }
        }
    }
}
