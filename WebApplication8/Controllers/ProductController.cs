using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication8.Model;

namespace WebApplication8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ITIEntities context;

   
        public ProductController (ITIEntities context)
        {
            this.context = context;
        }


        [HttpGet]
        public IActionResult GetProducts()
        {
            IEnumerable<Claim> claims = User.Claims;

            Claim claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            // string user_id = claimId.Value;
            List<Product> ProdList = context.Products.ToList();
            if (ProdList == null)
            {
                return BadRequest("Empty Department");
            }
            return Ok(ProdList);
        }

        



        [HttpGet("{id:int}", Name = "getOneRoute")]
         public IActionResult GetProductByID(int id)
        {
            Product ProdList = context.Products.FirstOrDefault(d => d.id == id);
            if (ProdList == null)
            {
                return BadRequest("Empty Department");
            }
            return Ok(ProdList);
        }



        [HttpGet("{Name:alpha}")]
        public IActionResult GetProductByNAme(string Name)
        {
            Product ProdList =
                context.Products.FirstOrDefault(d => d.name.Contains(Name));
            if (ProdList == null)
            {
                return BadRequest("Empty Department");
            }
            return Ok(ProdList);

        }


        [HttpPost]
        public IActionResult CreateProduct(Product Prod)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Products.Add(Prod);
                    context.SaveChanges();
                    string url = Url.Link("getOneRoute", new { id = Prod.id });
                    return Created(url, Prod);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }


        [HttpPut("{id:int}")]
        public IActionResult EditProduct(int id, Product Prod)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Product ProdModel =
                    context.Products.FirstOrDefault(d => Prod.id == id);
                    ProdModel.name = Prod.name;
                    ProdModel.price = Prod.price;
                    ProdModel.img = Prod.img;
                    ProdModel.quantity = Prod.quantity;
                    ProdModel.categoryID = Prod.categoryID;

                    context.SaveChanges();

                  return StatusCode(StatusCodes.Status204NoContent, "Data Saved");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }



    }
}
