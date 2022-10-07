using IDistributedCacheRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace IDistributedCacheRedisApp.Web.Controllers
{
    public class Products : Controller
    {
        private IDistributedCache _distributedCache;
        public Products(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            
        }
        public async Task< IActionResult> Index()
        {
            DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(30);
            //_distributedCache.SetString("name", "Fatih",cacheOptions);
            //await _distributedCache.SetStringAsync("surname", "Erdoğan", cacheOptions);

            Product product = new Product { Id = 2, Name = "Masa", Price = 200 };
            string jsonProduct = JsonConvert.SerializeObject(product);
            await _distributedCache.SetStringAsync("product:3", jsonProduct,cacheOptions);

            Byte[] byteProduct = Encoding.UTF8.GetBytes(jsonProduct);

            _distributedCache.Set("product:4", byteProduct);

            return View();
        }

        public IActionResult ShowValue()
        {
            byte[] byteProduct = _distributedCache.Get("product:4");
            string jsonProduct = Encoding.UTF8.GetString(byteProduct);
            //string jsonProduct = _distributedCache.GetString("product:1");
            Product p = JsonConvert.DeserializeObject<Product>(jsonProduct);
            ViewBag.product = p;
            string name = _distributedCache.GetString("name");
            ViewBag.name = name;
            return View();
        }

        public IActionResult RemoveValue()
        {
            _distributedCache.Remove("name");
            return View();
        }

        public IActionResult ImageUrl()
        {
            byte[] imageByte = _distributedCache.Get("image");
            return File(imageByte,"image/jpg");
        }
        public IActionResult ImageCache()
        {

            String path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/denemecar.jpg");
            byte[] imageByte = System.IO.File.ReadAllBytes(path);

            _distributedCache.Set("image", imageByte);
            return View();
        }
    }
}
