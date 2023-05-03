using System.Data.SqlClient;
using Evoting_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Evoting_Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class VotesController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public VotesController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IEnumerable<Vote> Get()
    {
        var votes = GetVotes();
        return votes;
    }

    private IEnumerable<Vote> GetVotes()
    {
        var votes = new List<Vote>();

        using (var connection = new SqlConnection(_configuration.GetConnectionString("EVotingDatabase")))
        {
            var sql = "SELECT * FROM VotesCast;";
            connection.Open();
            using SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var vote = new Vote()
                {
                    VoterId = (int)reader["voterID"],
                    Id1 = (int)reader["id1"],
                    Id1Vote = reader["id1Vote"].ToString().Trim(),
                    Id2 = (int)reader["id2"],
                    Id2Vote = reader["id2Vote"].ToString().Trim(),
                    Id3 = (int)reader["id3"],
                    Id3Vote = reader["id3Vote"].ToString().Trim(),
                    Id4 = (int)reader["id4"],
                    Id4Vote = reader["id4Vote"].ToString().Trim(),
                    Id5 = (int)reader["id5"],
                    Id5Vote = reader["id5Vote"].ToString().Trim(),
                };
                votes.Add(vote);
            }
        }

        return votes;
    }

    private int InsertVote(Vote vote)
    {
        using (var conn = new SqlConnection(_configuration.GetConnectionString("EVotingDatabase")))
        {
            var cmd = new SqlCommand(
                "insert into VotesCast values (@voterID, @id1, @id1Vote, @id2, @id2Vote, @id3, @id3Vote, @id4, @id4Vote, @id5, @id5Vote)",
                conn);
            cmd.Parameters.AddWithValue("@voterID", vote.VoterId);
            cmd.Parameters.AddWithValue("@id1", vote.Id1);
            cmd.Parameters.AddWithValue("@id1Vote", vote.Id1Vote);
            cmd.Parameters.AddWithValue("@id2", vote.Id2);
            cmd.Parameters.AddWithValue("@id2Vote", vote.Id2Vote);
            cmd.Parameters.AddWithValue("@id3", vote.Id3);
            cmd.Parameters.AddWithValue("@id3Vote", vote.Id3Vote);
            cmd.Parameters.AddWithValue("@id4", vote.Id4);
            cmd.Parameters.AddWithValue("@id4Vote", vote.Id4Vote);
            cmd.Parameters.AddWithValue("@id5", vote.Id5);
            cmd.Parameters.AddWithValue("@id5Vote", vote.Id5Vote);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        return 0;
    }

    [HttpPost]
    public Task<ActionResult> NewVote(Vote vote)
    {
        InsertVote(vote);
        return Task.FromResult<ActionResult>(StatusCode(StatusCodes.Status202Accepted));
    }
}