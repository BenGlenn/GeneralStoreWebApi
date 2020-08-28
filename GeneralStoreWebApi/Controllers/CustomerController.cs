using GeneralStoreWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GeneralStoreWebApi.Controllers
{
    public class CustomerController : ApiController
    {
        private StoreDbContext _context = new StoreDbContext();

        // Post -- Create
        public IHttpActionResult Post(Customer customer)
        {
            //if an empty Customer is passed in
            if (customer == null)
            {
                return BadRequest("Your request body cannot be empty.");
            }
            //if the ModelState is not Valid

            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // Get -- Read All
        public IHttpActionResult Get()
        {
            List<Customer> customers = _context.Customers.ToList();
            if (customers.Count != 0)
            {
                return Ok(customers);
            }
            return BadRequest("Your database contains no Customers");
        }
        // Get{id} -- Read By ID

        public IHttpActionResult Get(int id)
        {
            Customer customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }



        // Put {id} -- Update By ID
        public IHttpActionResult Put(int id, Customer updatedCustomer)
        {
            if (ModelState.IsValid)
            {
                Customer customer = _context.Customers.Find(id);
                if (customer != null)
                {
                    customer.FirstName = updatedCustomer.FirstName;
                    customer.LastName = updatedCustomer.LastName;
                    _context.SaveChanges();
                    return Ok("Customer has been updated.");
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        // Delete{id} -- Delete By ID

        public IHttpActionResult Delete(int id)
        {
            Customer entity = _context.Customers.Find(id);
            if (entity == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(entity);
            if (_context.SaveChanges() == 1)
            {
                return Ok("The customer was deleted.");
            }
            return InternalServerError();
        }
    }
}
