using FetchBackendTakehome.Models;
using FetchBackendTakehome.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FetchBackendTakehome.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PointsController : Controller
    {
        private readonly ITransactionManager _transactionManager;

        public PointsController(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }

        [HttpPost("add")]
        public IActionResult AddPoints([FromBody] Transaction transaction)
        {
            _transactionManager.AddTransaction(transaction);
            return Ok();
        }

        [HttpPost("spend")]
        public IActionResult SpendPoints([FromBody] Dictionary<string, int> request)
        {
            try
            {
                var spentPoints = _transactionManager.SpendPoints(request["points"]);
                return Ok(spentPoints);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("balance")]
        public IActionResult GetBalance()
        {
            return Ok(_transactionManager.GetBalance());
            
        }

    }
}
