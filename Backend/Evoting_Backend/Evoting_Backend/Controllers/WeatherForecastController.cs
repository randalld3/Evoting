using Evoting_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using static System.Collections.Specialized.BitVector32;

namespace Evoting_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ElectionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ElectionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<Election> Get()
        {
            var employees = GetElections();
            return employees;
        }

        private IEnumerable<Election> GetElections()
        {
            var elections = new List<Election>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("EVotingDatabase")))
            {
                var sql = "SELECT * FROM Elections;";
                connection.Open();
                using SqlCommand command = new SqlCommand(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var election = new Election()
                    {
                        ElectionId = (int)reader["electionID"],
                        Name = reader["name"].ToString(),
                        Description = reader["description"].ToString(),
                        PoliticalParty = reader["politicalParty"].ToString(),
                        Location = reader["location"].ToString(),
                    };
                    elections.Add(election);
                }
            }

            return elections;
        }
    }
}