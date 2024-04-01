using HancerliMarket.DataModels.Models;
using HancerliMarket.Webapi.Application.Baskets;
using HancerliMarket.Webapi.DbOperations.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HancerliMarket.Webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IWebApiDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public BasketController(IWebApiDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult CreateBasket([FromBody] BasketModel request)
        {
            try
            {
                CreateBasket operation = new(_dbContext)
                {
                    Basket = request
                };

                var response = operation.Handle();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult GetBaskets()
        {
            try
            {
                GetBaskets operation = new(_dbContext);

                var response = operation.Handle();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete, Authorize(Roles = "Admin")]
        public ActionResult DeleteBasket([FromQuery] int id)
        {
            try
            {
                DeleteBasket operation = new(_dbContext)
                {
                    Id = id
                };

                operation.Handle();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpDelete("all"), Authorize(Roles = "Admin")]
        public ActionResult RemoveBasket()
        {
            try
            {
                RemoveBasket operation = new(_dbContext);

                operation.Handle();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
