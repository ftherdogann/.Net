using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class HashTypeController : BaseController
    {
        private string hashKey = "hashdictionary";

        public HashTypeController(RedisService redisService) : base(redisService)
        {
           
        }

        public IActionResult Index()
        {
            Dictionary<string, string> hashlist = new Dictionary<string, string>();
            if (database.KeyExists(hashKey))
            {
                database.HashGetAll(hashKey).ToList().ForEach(x => {
                    hashlist.Add(x.Name, x.Value);
                });
            }
            //database.HashGet(hashKey, "pen");
            return View(hashlist);
        }
        [HttpPost]
        public IActionResult Add(string name, string value)
        {

            database.HashSet(hashKey, name, value);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(string name)
        {
            database.HashDelete(hashKey, name);
            return RedirectToAction("Index");
        }
    }
}
