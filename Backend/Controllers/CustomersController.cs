using Backend.Data;
using Backend.Models;
using Backend.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly SqlDataContext _context;

        public CustomersController(SqlDataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerRequest req)
        {
            try
            {
                if (!await _context.Customers.AnyAsync(x => x.Email == req.Email))
                {
                    var customerEntity = new CustomerEntity { FirstName = req.FirstName, LastName = req.LastName, Email = req.Email, Phone = req.Phone };
                    _context.Add(customerEntity);
                    await _context.SaveChangesAsync();

                    return new OkObjectResult(customerEntity);
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var customers = new List<CustomerResponse>();
                foreach (var customer in await _context.Customers.ToListAsync())
                    customers.Add(new CustomerResponse { Id = customer.Id, FirstName = customer.FirstName, LastName = customer.LastName, Email = customer.Email, Phone = customer.Phone });

                return new OkObjectResult(customers);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }


    }
}
