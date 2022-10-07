using InMemoryApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public IActionResult Index()
        {
            ////1.yol
            //if (string.IsNullOrEmpty(_memoryCache.Get<string>("time")))
            //{
            //    _memoryCache.Set<string>("time", DateTime.Now.ToString());
            //}

            //2.yol
            //if (_memoryCache.TryGetValue("time", out string timeValue))
            //{
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            //options.AbsoluteExpiration = DateTime.Now.AddMinutes(1);//cache üzerinde tutulma süresi
            //options.SlidingExpiration = TimeSpan.FromSeconds(10); // 10 saniye içerisinde bir istek gelirse cache ömrü 10 sny daha uzar
            options.AbsoluteExpiration = DateTime.Now.AddSeconds(10);
            options.Priority = CacheItemPriority.High;// önem derecesini belirtir. High önemli data memory dolduğunda en son sil. neverRemove cacheten silmez.
            options.RegisterPostEvictionCallback((object key, object value, EvictionReason reason, object state) =>
            {
                _memoryCache.Set("callback", $"{key}->{value}=> sebep:{reason}");
            });
            _memoryCache.Set<string>("time", DateTime.Now.ToString(), options);
            //}

            Product p = new Product { Id = 1, Name = "Kalem", Price = 200 };

            _memoryCache.Set<Product>("product:1", p);
            _memoryCache.Set<double>("money",100.25);
            return View();
        }

        public IActionResult ShowTime()
        {
            //_memoryCache.Remove("time");//cache teki değeri siler

            //_memoryCache.GetOrCreate<string>("time", entry =>
            //{
            //    return DateTime.Now.ToString();
            //});
            //ViewBag.time = _memoryCache.Get<string>("time");
            _memoryCache.TryGetValue("callback", out string callback);
            _memoryCache.TryGetValue("time", out string timeValue);
            ViewBag.time = timeValue;
            ViewBag.callback = callback;

            ViewBag.product = _memoryCache.Get<Product>("product:1");
            return View();

        }
    }
}
