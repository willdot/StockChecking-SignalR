using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockCheck.SignalRService.Hubs
{
    public interface IStockClient
    {
        Task Send(string value); 
    }
}
