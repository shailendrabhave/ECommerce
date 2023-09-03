﻿using ECommerce.API.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController: ControllerBase
    {
        private readonly ICustomerProvider _customersProvider;
        public CustomersController(ICustomerProvider customersProvider)
        {
            _customersProvider = customersProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await _customersProvider.GetCustomersAsync();

            if (result.IsSuccess)
                return Ok(result.Customers);

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await _customersProvider.GetCustomerAsync(id);

            if (result.IsSuccess)
                return Ok(result.Customer);

            return NotFound();
        }
    }
}
