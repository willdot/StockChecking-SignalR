using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StockCheck.SignalRService.Hubs;

namespace StockCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        // This is temporary.
        public static List<string> Source { get; set; } = new List<string>();

        private IHubContext<StockHub, IStockClient> _context;

        public StockController(IHubContext<StockHub, IStockClient> hub)
        {
            _context = hub;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>>Get()
        {
            foreach(string value in Source)
            {
                await _context.Clients.All.BroadcastMessage(value);
            }
            return Source;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody] StockInput value)
        {
            Source.Add(value.Value);
            await _context.Clients.All.BroadcastMessage(value.Value);
        }
    }
}
