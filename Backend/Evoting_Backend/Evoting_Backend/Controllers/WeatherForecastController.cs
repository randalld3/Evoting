using Evoting_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Evoting_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]/*
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
                var cmd = new SqlCommand("insert into Elections values (@electionID, @name, @description, @politicalParty, @location)", conn);
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

    }*/

    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public User Get(string username)
        {
            var user = FindUser(username);
            return user;
        }

        private User FindUser(string username)
        {
            var ReturnUser = new User();

            using (var conn = new SqlConnection(_configuration.GetConnectionString("EVotingDatabase")))
            {
                var sql = "SELECT * FROM Users where username ='" + username + "';";
                conn.Open();
                using SqlCommand command = new SqlCommand(sql, conn);

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ReturnUser = new User()
                    {
                        VoterId = (int)reader["voterID"],
                        Name = reader["name"].ToString().Trim(),
                        State = reader["state"].ToString().Trim(),
                        Username = reader["username"].ToString().Trim(),
                        Password = reader["password"].ToString().Trim(),
                        GovernmentId = (int)reader["governmentID"],
                        RecoveryQuestion = reader["recoveryQuestion"].ToString().Trim(),
                        RecoveryAnswer = reader["recoveryAnswer"].ToString().Trim(),
                    };
                }
            }
            return ReturnUser;
        }
        private int InsertUser(User user)
        {

            using (var conn = new SqlConnection(_configuration.GetConnectionString("EVotingDatabase")))
            {
                var cmd = new SqlCommand("insert into Users values (@voterID, @name, @state, @username, @password, @governmentID, @recoveryQuestion, @recoveryAnswer)", conn);
                cmd.Parameters.AddWithValue("@voterID", user.VoterId);
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@state", user.State);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@governmentID", user.GovernmentId);
                cmd.Parameters.AddWithValue("@recoveryQuestion", user.RecoveryQuestion);
                cmd.Parameters.AddWithValue("@recoveryAnswer", user.RecoveryAnswer);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return 0;
        }

        [HttpPost]
        public Task<ActionResult> NewUser(User user)
        {
            InsertUser(user);
            return Task.FromResult<ActionResult>(StatusCode(StatusCodes.Status202Accepted));
        }
    }/*
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
                var cmd = new SqlCommand("insert into VotesCast values (@voterID, @id1, @id1Vote, @id2, @id2Vote, @id3, @id3Vote, @id4, @id4Vote, @id5, @id5Vote)", conn);
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

    }*/



}