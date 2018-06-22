using StockCheck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockCheck.TemporaryData
{
    public interface ITempData
    {
        List<StockItem> Stock { get; }

        void AddStock(StockItem input);
        void RemoveStock(StockItem input);

        int GetStock(string item);
    }
}
