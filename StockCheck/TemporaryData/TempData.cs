using StockCheck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockCheck.TemporaryData
{
    public class TempData : ITempData
    {
        private static List<StockItem> _stock = new List<StockItem>();

        public List<StockItem> Stock
        {
            get
            {
                return _stock;
            }
        }

        public TempData()
        {
            _stock.Add(new StockItem() { Name = "Water", Amount = 5 });
            _stock.Add(new StockItem() { Name = "Coffee", Amount = 2 });
            _stock.Add(new StockItem() { Name = "Chocolate", Amount = 10 });
        }

        public void AddStock(StockItem input)
        {
            if (_stock.Count(p => p.Name == input.Name) == 0)
            {
                _stock.Add(input);
            }
            else
            {
                _stock.FirstOrDefault(p => p.Name == input.Name).Amount += input.Amount;
            }

        }

        public void RemoveStock(StockItem input)
        {
            // Not something that is currently stocked
            if (_stock.Count(p => p.Name == input.Name) == 0)
            {
                throw new Exception("Item not stocked");
            }
            // There is nothing left of this item
            else if (_stock.FirstOrDefault(p => p.Name == input.Name).Amount <= 0)
            {
                throw new Exception("Out of Stock");
            }
            // The amount that has been requested to be removed is more than what's available
            else if(_stock.FirstOrDefault(p => p.Name == input.Name).Amount < input.Amount)
            {
                throw new Exception("Stock amount less than required amount");
            }
            // In stock and the requested amount is available to be removed
            else
            {
                _stock.FirstOrDefault(p => p.Name == input.Name).Amount -= input.Amount;
            }
        }

        public int GetStock(string item)
        {
            if(_stock.Count(p => p.Name == item) > 0)
            {
                return _stock.FirstOrDefault(p => p.Name == item).Amount;
            }

            throw new Exception("not currently stocked");
        }
    }
}
