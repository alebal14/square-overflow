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
        [HttpGet]
        public async Task<IActionResult> GetAllSquares()
        {
            var result = await _squareService.LoadSquaresFromStorageAsync();
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
        public async Task<IActionResult> AddSquare()
        {
            var result = await _squareService.GenerateSquareAsync();
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
        public async Task<IActionResult> DeleteAllSquares()
        {
            var result = await _squareService.ClearSquaresStorageAsync();
            return Ok(result);
        }
    }
}
