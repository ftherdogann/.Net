using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Services
{
    public class RedisService
    {
        private readonly string _redisHost;
        private readonly string _redisPort;

        private ConnectionMultiplexer _redis;
        public IDatabase database { get; set; }
        public RedisService(IConfiguration configuration)
        {
            _redisHost = configuration["Redis:Host"];
            _redisPort = configuration["Redis:Port"];
            Connect();
        }

        public void Connect()
        {
            var configString = $"{_redisHost}:{_redisPort}";

            _redis = ConnectionMultiplexer.Connect(configString);
        }

        public IDatabase GetDb(int dbnumber)
        {
            return _redis.GetDatabase(dbnumber);
        }
    }
}
