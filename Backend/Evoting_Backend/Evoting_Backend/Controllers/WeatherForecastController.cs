using Evoting_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

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
        private IEnumerable<Election> InsertElection()
        {

            using (var conn = new SqlConnection(yourConnectionString))
            {
                var cmd = new SqlCommand("insert into Foo values (@bar)", conn);
                cmd.Parameters.AddWithValue("@bar", 17);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return;
        }



        [HttpPost]
        public Task<ActionResult> NewElection(Election election)
        {
            
            return Task.FromResult<ActionResult>(StatusCode(StatusCodes.Status202Accepted));
        }
    }

}