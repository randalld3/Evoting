using Evoting_Backend.Models;
using Microsoft.AspNetCore.Mvc;
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
            var elections = GetElections();
            return elections;
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
                        Name = reader["name"].ToString().Trim(),
                        Description = reader["description"].ToString().Trim(),
                        PoliticalParty = reader["politicalParty"].ToString().Trim(),
                        Location = reader["location"].ToString().Trim(),
                    };
                    elections.Add(election);
                }
            }

            return elections;
        }

        private int InsertElection(Election election)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("EVotingDatabase")))
            {
                var cmd = new SqlCommand(
                    "insert into Elections values (@electionID, @name, @description, @politicalParty, @location)",
                    conn);
                cmd.Parameters.AddWithValue("@electionID", election.ElectionId);
                cmd.Parameters.AddWithValue("@name", election.Name);
                cmd.Parameters.AddWithValue("@description", election.Description);
                cmd.Parameters.AddWithValue("@politicalParty", election.PoliticalParty);
                cmd.Parameters.AddWithValue("@location", election.Location);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return 0;
        }

        [HttpPost]
        public Task<ActionResult> NewElection(Election election)
        {
            InsertElection(election);
            return Task.FromResult<ActionResult>(StatusCode(StatusCodes.Status202Accepted));
        }
    }
}