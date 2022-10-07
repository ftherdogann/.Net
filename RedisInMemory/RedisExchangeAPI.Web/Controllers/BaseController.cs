using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IDatabase database;
        private readonly RedisService _redisService;
       
        public BaseController(RedisService redisService)
        {
            _redisService = redisService;
            database = _redisService.GetDb(4);
        }
    }
}
