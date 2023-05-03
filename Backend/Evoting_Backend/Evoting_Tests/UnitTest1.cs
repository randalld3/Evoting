using Evoting_Backend.Controllers;
using Evoting_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Evoting_Tests;

public class UnitTest1
{
    private readonly IConfiguration _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    private readonly User _testUser = new()
    {
        VoterId = 1,
        Name = "Bob",
        State = "CA",
        Username = "Robert",
        Password = "password123",
        RecoveryQuestion = "Recover?",
        RecoveryAnswer = "Yes"
    };
    
    [Fact]
    public void TestInsertUser()
    {
        var controller = new UserController(_config);

        // According to online research, there's no good solution to testing private methods aside from making them public.
        var res = controller.NewUser(_testUser);
        Assert.Equal(controller.StatusCode(StatusCodes.Status202Accepted), res.Result);
    }

    [Fact]
    public void TestFindUser()
    {
        var controller = new UserController(_config);

        var res = controller.Get("Robert");
        Assert.Equal(_testUser, res);
    }
}