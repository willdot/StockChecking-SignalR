using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockCheck.TemporaryData
{
    public class TempData : ITempData
    {
        private static Dictionary<string, int> _stock = new Dictionary<string, int>();

        public Dictionary<string, int> Stock
        {
            get
            {
                return _stock;
            }
            set
            {

            }

        }

        public TempData()
        {
            _stock.Add("Water", 10);
            _stock.Add("Coffee", 5);
            _stock.Add("Chocolate", 6);
        }

        public void AddStock(StockInput input)
        {
            if (_stock.ContainsKey(input.Name))
            {
                _stock[input.Name] += input.Amount;
            }
            else
            {
                _stock.Add(input.Name, input.Amount);
            }
        }

        public void RemoveStock(StockInput input)
        {
            if (_stock.ContainsKey(input.Name))
            {
                if (_stock[input.Name] <= 0)
                {
                    throw new Exception("Out of Stock");
                }

                if (_stock[input.Name] < input.Amount)
                {
                    throw new Exception("Stock amount less than required amount");
                }

                _stock[input.Name] -= input.Amount;
            }
            else
            {
                _stock.Add(input.Name, input.Amount);
            }
        }

        public int GetStock(string item)
        {
            if (_stock.ContainsKey(item))
            {
                return _stock[item];
            }

            throw new Exception("not currently stocked");
        }
    }
}
