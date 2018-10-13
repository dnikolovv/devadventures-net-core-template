using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Optional;

namespace $safeprojectname$.Controllers
{
    public class OptionController : ApiController
    {
        /// <summary>
        /// Demonstrates the Option model binding with query string parameters. Cannot be tested from the Swagger UI.
        /// </summary>
        /// <param name="text">A string query parameter.</param>
        /// <param name="number">A number query parameter.</param>
        /// <param name="flag">A boolean query parameter.</param>
        /// <returns>A model showing the bound data.</returns>
        /// <response code="200">The model was bound successfully.</response>
        /// <response code="400">When it could not parse some of the inputted data.</response>
        [HttpGet]
        public IActionResult Get([FromQuery] [Required] Option<string> text, Option<int> number, Option<bool> flag) =>
            Ok(new
            {
                message = "You gave me query parameters:",
                text = text.ToString(),
                number = number.ToString(),
                flag = flag.ToString()
            });
    }
}
