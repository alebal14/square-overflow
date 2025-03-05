using Microsoft.AspNetCore.Mvc;
using SquareOverFlowApi.Middlewares;
using SquareOverFlowCore.Interfaces;


namespace SquareOverFlowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ErrorHandling]
    public class SquareController : ControllerBase
    {
        private readonly ISquareService _squareService;
        private readonly ILogger<SquareController> _logger;

        public SquareController(ISquareService squareService, ILogger<SquareController> logger)
        {
            _squareService = squareService;
            _logger = logger;
        }

        // GetAllSquares endpoint
        /// <summary>
        /// Retrieves all squares from storage
        /// </summary>
        /// <returns>A collection of all stored squares</returns>
        /// <response code="200">Returns the collection of squares</response>
        /// <response code="404">If no squares are found</response>
        [HttpGet]
        public IActionResult GetAllSquares()
        {
            var result = _squareService.LoadSquaresFromStorage();
            return Ok(result);
        }

        // AddSquare endpoint
        /// <summary>
        /// Creates a new square with a unique color
        /// </summary>
        /// <returns>The newly created square with a unique color</returns>
        /// <response code="201">Returns the newly created square</response>
        /// <response code="500">If there was an error generating the square</response>
        [HttpPost]
        public IActionResult AddSquare()
        {
            var result = _squareService.GenerateSquare();
            return CreatedAtAction(nameof(GetAllSquares), result);
        }

        // DeleteAllSquares endpoint
        /// <summary>
        /// Deletes all squares from storage
        /// </summary>
        /// <returns>A success message or the count of deleted squares</returns>
        /// <response code="200">Operation was successful</response>
        /// <response code="500">If there was an error clearing the storage</response>
        [HttpDelete]
        public IActionResult DeleteAllSquares()
        {
            var result = _squareService.ClearSquaresStorage();
            return Ok(result);
        }
    }
}
