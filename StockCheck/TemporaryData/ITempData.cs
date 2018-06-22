using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockCheck.TemporaryData
{
    public interface ITempData
    {
        Dictionary<string, int> Stock { get; set; }

        void AddStock(StockInput input);
        void RemoveStock(StockInput input);

        int GetStock(string item);
    }
}
