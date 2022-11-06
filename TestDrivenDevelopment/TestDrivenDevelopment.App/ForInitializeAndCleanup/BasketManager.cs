using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDrivenDevelopment.Apps
{
    public class BasketManager
    {
        private readonly List<BasketItem> basketList;
        public BasketManager()
        {
            basketList = new List<BasketItem>();
        }

        public void AddBasket(BasketItem item)
        {
            var basketItem = basketList.SingleOrDefault(x => x.Product.ProductId == item.Product.ProductId);
            if (basketItem == null)
            {
                basketList.Add(item);
            }
            else
            {
                // basketItem.Quatity+=item.Quatity;
                 new ArgumentException(); 
            }
        }

        public void RemoveBasket(int productId)
        {
            var product = basketList.FirstOrDefault(x=>x.Product.ProductId == productId);
            basketList.Remove(product);
        }
        public List<BasketItem> BasketList
        {
            get { return basketList; }
        }
        public void ClearBasket()
        {
            basketList.Clear();
        }
        public decimal TotalPrice
        {
            get { return basketList.Sum(t => t.Quatity * t.Product.Price); }
        }
        public int TotalQuantity
        {
            get { return basketList.Sum(t => t.Quatity); }
        }
        public int TotalItems
        {
            get { return basketList.Count; }
        }
    }
}
