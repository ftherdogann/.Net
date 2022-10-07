using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class SortedSetTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase database;

        private string listKey = "sortedsetnames";
        public SortedSetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            database = _redisService.GetDb(3);
        }
        public IActionResult Index()
        {
            HashSet<string> list = new HashSet<string>();
            if (database.KeyExists(listKey))
            {
                //database.SortedSetScan(listKey).ToList().ForEach(x =>
                //{
                //    list.Add(x.ToString());
                //});

                database.SortedSetRangeByRank(listKey, 0, 5, order: Order.Descending).ToList().ForEach(x =>
                {
                    list.Add(x.ToString());
                });
            }
            return View(list);
        }

        [HttpPost]
        public IActionResult Add(string name,int score)
        {

            database.SortedSetAdd(listKey, name, score);
            //database.KeyExpire(listKey, DateTime.Now.AddMinutes(1));
            return RedirectToAction("Index");
        }
        public IActionResult DeleteItem(string name)
        {
            database.SortedSetRemove(listKey, name);
            return RedirectToAction("Index");
        }
    }
}
