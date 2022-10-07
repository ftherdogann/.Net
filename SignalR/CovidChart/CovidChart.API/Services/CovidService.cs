using CovidChart.API.Hubs;
using CovidChart.API.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidChart.API.Services
{
    public class CovidService
    {

        private readonly AppDbContext _context;

        private readonly IHubContext<CovidHub> _hubContext;

        public CovidService(AppDbContext context, IHubContext<CovidHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IQueryable<Covid> GetList()
        {
            return _context.Covids.AsQueryable();
        }

        public async Task SaveCovid(Covid covid)
        {
            await _context.Covids.AddAsync(covid);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveCovidList", GetCovidChartList());
        }
        public List<CovidsChart> GetCovidChartList()
        {
            List<CovidsChart> covidsCharts = new List<CovidsChart>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT CovidDate,[1],[2],[3],[4],[5] FROM ( " +
                    "SELECT City,[Count], CAST(CovidDate AS DATE) AS CovidDate FROM  Covids" +
                    ") AS CovidTable PIVOT( SUM([Count]) FOR City IN([1],[2],[3],[4],[5])  ) AS PTable ORDER BY CovidDate ASC";
                command.CommandType = System.Data.CommandType.Text;

                _context.Database.OpenConnection();

                using(var reader = command.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        CovidsChart covids = new CovidsChart();
                        covids.CovidDate = reader.GetDateTime(0).ToShortDateString();
                        Enumerable.Range(1, 5).ToList().ForEach(x =>
                        {
                            if (System.DBNull.Value.Equals(reader[x]))
                            {
                                covids.Counts.Add(0);
                            }
                            else
                            {
                                covids.Counts.Add(reader.GetInt32(x));
                            }
                        });
                        covidsCharts.Add(covids);
                    }
                }
                _context.Database.CloseConnection();

            }

            return covidsCharts;
        }
    }
}
