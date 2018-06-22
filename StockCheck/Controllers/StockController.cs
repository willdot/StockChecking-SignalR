using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StockCheck.Models;
using StockCheck.SignalRService.Hubs;
using StockCheck.TemporaryData;

namespace StockCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
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
            if (input.Name == "")
            {
                return BadRequest("Item name misssing");
            }

            StockItem item = new StockItem()
            {
                Name = input.Name,
                Amount = input.Amount
            };

            _tempData.AddStock(item);
            return Ok();
        }

        [HttpPost("remove")]
        public ActionResult RemoveStock([FromBody] StockInput input)
        {
            StockItem item = new StockItem()
            {
                Name = input.Name,
                Amount = input.Amount
            };

            try
            {
                _tempData.RemoveStock(item);
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

                if (stockAmount == 0)
                {
                    _context.Clients.All.BroadcastMessage($"Item: {item} is out of stock");
                }

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
