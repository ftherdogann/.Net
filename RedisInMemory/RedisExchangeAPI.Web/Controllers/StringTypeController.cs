using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase database;
        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            database = _redisService.GetDb(1);
        }
        public IActionResult Index()
        {

            database.StringSet("name1", "fatih");
            database.StringSet("name2", "sinem");
            database.StringSet("ziyaretci", 1000);

            return View();
        }

        public IActionResult ShowStackValue()
        {
            var name1 = database.StringGet("name1");
            database.StringIncrement("ziyaretci", 1);
            var count = database.StringDecrementAsync("ziyaretci", 15).Result;
            database.StringDecrementAsync("ziyaretci", 12).Wait();
            var name2 = database.StringGetRange("name2", 0, 3);

            var length = database.StringLength("name1");

            String path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/denemecar.jpg");
            byte[] imageByte = System.IO.File.ReadAllBytes(path);
            database.StringSet("image", imageByte);
            if (name1.HasValue)
            {
                ViewBag.name1 = length.ToString();
                ViewBag.name2=name2.ToString();
            }
            return View();
        }
    }
}
