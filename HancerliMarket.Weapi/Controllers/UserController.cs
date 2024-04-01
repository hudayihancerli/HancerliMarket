using HancerliMarket.DataModels.Models;
using HancerliMarket.Webapi.Application.User;
using HancerliMarket.Webapi.DbOperations.Interface;
using HancerliMarket.Webapi.TokenOperations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;

namespace HancerliMarket.Webapi.Controllers
{

    [Route("api/[controller]s")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IWebApiDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public UserController(IWebApiDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult CreateUser([FromBody] UserModel request)
        {
            Register operation = new(_dbContext)
            {
                Model = request
            };

            var response = operation.Handle();

            return Ok(response);
        }

        [HttpPost("login")]
        public ActionResult GetUser([FromBody] UserModel request)
        {

            Login operation = new(_dbContext, _configuration)
            {
                Model = request,
            };

            var response = operation.Handle();

            return Ok(response);
        }

        [HttpGet("refreshtoken")]
        public ActionResult RefreshToken([FromQuery] string token)
        {
            RefreshTokenOperation operation = new(_dbContext, _configuration)
            {
                refreshToken = token,
            };

            var response = operation.Handle();

            return Ok(response);
        }

        [HttpGet("GetUserDetail"), Authorize(Roles = "Admin")]
        public ActionResult GetUserDetail([FromQuery] string username)
        {
            GetUserDetail operation = new(_dbContext)
            {
                Username = username
            };

            var response = operation.Handle();

            return Ok(response);
        }

        [HttpGet("GetUserList"), Authorize(Roles = "Admin")]
        public ActionResult GetUserList()
        {
            GetUsers operation = new(_dbContext);

            var response = operation.Handle();

            return Ok(response);
        }

    }

}
