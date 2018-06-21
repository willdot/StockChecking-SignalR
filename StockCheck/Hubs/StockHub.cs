using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockCheck.SignalRService.Hubs
{
    public class StockHub : Hub<IStockClient>
    {
        public async Task Send(string value) => await Clients.All.Send(value);
    }
}
