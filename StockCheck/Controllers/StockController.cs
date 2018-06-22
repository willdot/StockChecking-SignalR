using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StockCheck.SignalRService.Hubs;
using StockCheck.TemporaryData;

namespace StockCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        // This is temporary.
        public static List<string> Source { get; set; } = new List<string>();

        private IHubContext<StockHub, IStockClient> _context;
        private ITempData _tempData;

        public StockController(IHubContext<StockHub, IStockClient> hub, ITempData tempData)
        {
            _context = hub;
            _tempData = tempData;
        }

        // GET api/values
        [HttpGet]
        public  ActionResult<IEnumerable<string>>Get()
        {
            return Ok(_tempData.Stock);
        }


        [HttpPost("add")]
        public ActionResult AddStock([FromBody] StockInput input)
        {
            _tempData.AddStock(input);
            return Ok();
        }

        [HttpPost("remove")]
        public ActionResult RemoveStock([FromBody] StockInput input)
        {
            try
            {
                _tempData.RemoveStock(input);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            SendStockMessage(input.Name);

            return Ok();
        }

        private void SendStockMessage(string item)
        {
            try
            {
                int stockAmount = _tempData.GetStock(item);

                if (stockAmount <= 5)
                {
                    _context.Clients.All.BroadcastMessage($"Item: {item} has {stockAmount} in stock");
                }
            }
            catch (Exception ex)
            {
                _context.Clients.All.BroadcastMessage($"Item: {item} {ex}");
            }   
        }
    }
}
