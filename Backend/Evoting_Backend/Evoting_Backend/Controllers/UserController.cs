using System.Data.SqlClient;
using Evoting_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Evoting_Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public UserController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public ActionResult<User> Get(string username)
    {
        var user = FindUser(username);
        return user != null ? user : NotFound();
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
            if (!reader.HasRows)
            {
                return null;
            }
            while (reader.Read())
            {
                ReturnUser = new User
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

    internal int InsertUser(User user)
    {
        using (var conn = new SqlConnection(_configuration.GetConnectionString("EVotingDatabase")))
        {
            var cmd = new SqlCommand(
                "insert into Users values (@voterID, @name, @state, @username, @password, @governmentID, @recoveryQuestion, @recoveryAnswer)",
                conn);
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

        return user.VoterId;
    }

    [HttpPost]
    public Task<ActionResult> NewUser(User user)
    {
        InsertUser(user);
        return Task.FromResult<ActionResult>(StatusCode(StatusCodes.Status202Accepted));
    }

    private int resetPassword(User user) //changes User's password
    {
        using (var conn = new SqlConnection(_configuration.GetConnectionString("EVotingDatabase")))
        {
            var cmd = new SqlCommand("insert into Users values (@password)", conn);
            cmd.Parameters.AddWithValue("@password", user.Password);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        return 0;
    }
}