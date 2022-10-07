using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class SetTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase database;

        private string listKey = "setnames";
        public SetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            database = _redisService.GetDb(2);
        }
        public IActionResult Index()
        {
            HashSet<string> namesList = new HashSet<string>();

            if (database.KeyExists(listKey))
            {
                database.SetMembers(listKey).ToList().ForEach(x =>
                {
                    namesList.Add(x);
                });
            }
           
            return View(namesList);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            //if (!database.KeyExists(listKey))
            //{
            database.KeyExpire(listKey, DateTime.Now.AddMinutes(5));
            //}
            database.SetAdd(listKey, name);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(string name)
        {
            database.SetRemoveAsync(listKey, name).Wait();
            return RedirectToAction("Index");
        }
    }
}
