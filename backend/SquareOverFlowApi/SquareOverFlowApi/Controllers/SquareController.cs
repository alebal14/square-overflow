using Microsoft.AspNetCore.Mvc;
using SquareOverFlowCore;


namespace SquareOverFlowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SquareController : ControllerBase
    {
        private readonly ISquareService _squareService;

        public SquareController(ISquareService squareService)
        {
            _squareService = squareService;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _squareService.GetAllSquares();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post()
        {
            var result = _squareService.AddSquare();
            return Ok(result);

        }

        [HttpDelete]
        public IActionResult Delete()
        {
            var result = _squareService.DeleteSquares();
            return Ok(result);
        }
    }
}
