using GeneralStoreWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GeneralStoreWebApi.Controllers
{
    public class ProductController : ApiController
    {
        private StoreDbContext _context = new StoreDbContext();

        //Post 
        public IHttpActionResult Post(Product product)
        {
            //if an empty Customer is passed in
            if (product == null)
            {
                return BadRequest("Your request body cannot be empty.");
            }

            product.SKU = GenerateSku(product.Name);
            //if the ModelState is not Valid

            if (ModelState.IsValid && product.SKU != null)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        //Get
        public IHttpActionResult Get()
        {
            List<Product> products = _context.Products.ToList();
            if (products.Count != 0)
            {
                return Ok(products);
            }
            return BadRequest("Your database contains no Products");
        }

        //Get{id}
        public IHttpActionResult Get(string sku)
        {
            Product product = _context.Products.Find(sku);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        //Put {id}
        public IHttpActionResult Put(string sku, Product updatedProduct) // We don't have to update all the required properties, we just need to pass all of them
                                                                         // through the method for the ModelState to be valid??
        {
            if (ModelState.IsValid)
            {
                Product product = _context.Products.Find(sku);
                if (product != null)
                {
                    product.Name = updatedProduct.Name;
                    product.Cost = updatedProduct.Cost;
                    product.NumberInInventory = updatedProduct.NumberInInventory;
                    _context.SaveChanges();
                    return Ok("This Product has been updated.");
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        //Delete{id}

        public IHttpActionResult DeleteProduct(string sku)
        {
            Product entity = _context.Products.Find(sku);
            if (entity == null)
            {
                return NotFound();
            }
            _context.Products.Remove(entity);
            if (_context.SaveChanges() == 1)
            {
                return Ok("Your Product was deleted.");
            }
            return InternalServerError();
        }




        private string GenerateSku(string productName)
        {
            // Initialize a Random object so we can randomly assign this a number
            Random random = new Random();
            // Get a Random number and turn it into a string
            var randItemNum = random.Next(0, 1000).ToString();
            // Construct a 3 character string based on the above number. If the number is less than 3 digits, add 0's in front of it to make it 3 digits
            var itemId = new string('0', 3 - randItemNum.Length) + randItemNum;
            // Create the entire SKU and return it
            return $"EFA-{productName.Substring(0, 3)}-{itemId}";
        }
    }
}
