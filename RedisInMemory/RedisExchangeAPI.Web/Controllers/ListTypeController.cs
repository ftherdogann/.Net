using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class ListTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase database;

        private string listKey = "names";
        public ListTypeController(RedisService redisService)
        {
            _redisService = redisService;
            database = _redisService.GetDb(0);
        }
        public IActionResult Index()
        {
            List<string> namelist = new List<string>();
            if (database.KeyExists(listKey))
            {
                database.ListRange(listKey).ToList().ForEach(x =>
                {
                    namelist.Add(x.ToString());
                });

            }
            return View(namelist);
        }
        [HttpPost]
        public IActionResult Add(string name)
        {
            database.ListLeftPush(listKey, name);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteItem(string name)
        {
            database.ListRemoveAsync(listKey,name).Wait();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteFirstItem()
        {
            database.ListLeftPop(listKey);
            return RedirectToAction("Index");
        }
    }
}
