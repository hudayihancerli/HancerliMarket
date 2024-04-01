using HancerliMarket.DataModels.Models;
using HancerliMarket.Webapi.Application.Products;
using HancerliMarket.Webapi.DbOperations.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HancerliMarket.Webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IWebApiDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public ProductController(IWebApiDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult CreateProduct([FromBody] ProductModel request)
        {
            try
            {
                CreateProduct operation = new(_dbContext)
                {
                    Product = request
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
        public ActionResult GetProducts()
        {
            try
            {
                GetProducts operation = new(_dbContext);

                var response = operation.Handle();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("detail"), Authorize(Roles = "Admin")]
        public ActionResult GetProductDetail([FromQuery] string barcode)
        {
            try
            {
                GetProductDetail operation = new(_dbContext)
                {
                    Barcode = barcode
                };

                var response = operation.Handle();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        public ActionResult DeleteProduct([FromQuery] string barcode)
        {
            try
            {
                DeleteProduct operation = new(_dbContext)
                {
                    Barcode = barcode
                };

                operation.Handle();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPatch, Authorize(Roles = "Admin")]
        public ActionResult UpdateProduct([FromQuery] int id, [FromBody] ProductModel request)
        {
            try
            {
                UpdateProduct operation = new(_dbContext)
                {
                    Id = id,
                    Model = request
                };

                var response = operation.Handle();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
