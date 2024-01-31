using DbModel;
using DbModel.Dbcontext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JOINTABLE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly Datacontext _data;

        public CustomerController(Datacontext data)
        {
            _data = data;
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<CustomerWithOrdersDto>> GetAll()
        {
            var customers = _data.Customers.ToList();
            if (customers == null || customers.Count == 0)
            {
                return NotFound();
            }

            var dtos = new List<CustomerWithOrdersDto>();
            foreach (var customer in customers)
            {
                var orders = _data.Orders.Where(o => o.customer_id == customer.id)
                                         .Select(o => new Orders
                                         {
                                             OrderId = o.order_id,
                                             Item = o.item,
                                             Amount = o.amount,
                                             customer_id = o.customer_id
                                         }).ToList();

                var dto = new CustomerWithOrdersDto
                {
                    Id = customer.id,
                    FirstName = customer.first_name,
                    LastName = customer.last_name,
                    Age = customer.age,
                    Country = customer.country,
                    Orders = orders
                };

                dtos.Add(dto);
            }

            return dtos;
        }

        [HttpGet("GetID")]
        public ActionResult<CustomerWithOrdersDto> GetById(int id)
        {
            var customer = _data.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            var orders = _data.Orders.Where(o => o.customer_id == id).Select(o => new Orders
            {
                OrderId = o.order_id,
                Item = o.item,
                Amount = o.amount
            }).ToList();

            var dto = new CustomerWithOrdersDto
            {
                Id = customer.id,
                FirstName = customer.first_name,
                LastName = customer.last_name,
                Age = customer.age,
                Country = customer.country,
                Orders = orders
            };

            return dto;
        }
        [HttpPost("Order")]
        public IActionResult Create1(CustomerAndOrderDto1 dto)
        {
            var order = new OrderModel
            {
                order_id = dto.OrderId,
                item = dto.Item,
                amount = dto.Amount,
                customer_id = dto.customer_id
            };

            try
            {
                _data.Orders.Add(order);

                _data.SaveChanges();

                return CreatedAtAction(nameof(GetById), new CustomerAndOrderDto1
                {
                    OrderId = order.order_id,
                    Item = order.item,
                    Amount = order.amount,
                    customer_id = order.customer_id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(CustomerAndOrderDto dto)
        {
            var customer = new CustomerModel
            {
                id = dto.Id,
                first_name = dto.FirstName,
                last_name = dto.LastName,
                age = dto.Age,
                country = dto.Country
            };

            var order = new OrderModel
            {
                order_id = dto.OrderId,
                item = dto.Item,
                amount = dto.Amount,
                customer_id = dto.customer_id
            };

            try
            {
                _data.Customers.Add(customer);
                _data.Orders.Add(order);

                _data.SaveChanges();

                return CreatedAtAction(nameof(GetById), new { id = customer.id }, new CustomerAndOrderDto
                {
                    Id = customer.id,
                    FirstName = customer.first_name,
                    LastName = customer.last_name,
                    Age = customer.age,
                    Country = customer.country,
                    OrderId = order.order_id,
                    Item = order.item,
                    Amount = order.amount,
                    customer_id = order.customer_id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut("CustomerUpdate")]
        public IActionResult Update(int id, [FromBody] CustomerDto dto)
        {
            var customer = _data.Customers.FirstOrDefault(c => c.id == id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.first_name = dto.FirstName;
            customer.last_name = dto.LastName;
            customer.age = dto.Age;
            customer.country = dto.Country;

            _data.Customers.Update(customer);
            _data.SaveChanges();

            return NoContent();
        }

        [HttpPut("CustomerOrderUpdate")]
        public IActionResult Update(int id, [FromBody] CustomerOrderDto1 dto)
        {
            try
            {
                var customer = _data.Customers.Find(id);

                customer.first_name = dto.FirstName;
                customer.last_name = dto.LastName;
                customer.age = dto.Age;
                customer.country = dto.Country;

                var order = _data.Orders.FirstOrDefault(o => o.customer_id == id);

                order.item = dto.Item;
                order.amount = dto.Amount;
                order.customer_id = dto.customer_id;

                _data.SaveChanges();
                return Ok();

                /*return Ok(new CustomerOrderDto
                {
                    Customer = new CustomerDto
                    {
                        FirstName = customer.first_name,
                        LastName = customer.last_name,
                        Age = customer.age,
                        Country = customer.country
                    },
                    Order = new Orders
                    {
                        Item = order.item,
                        Amount = order.amount,
                        customer_id = order.customer_id
                    }
                });*/
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var customerToDelete = await _data.Customers.FindAsync(id);
                if (customerToDelete == null)
                {
                    return NotFound();
                }

                // Delete the related orders
                var ordersToDelete = await _data.Orders.Where(o => o.customer_id == id).ToListAsync();
                _data.Orders.RemoveRange(ordersToDelete);

                // Delete the customer
                _data.Customers.Remove(customerToDelete);

                await _data.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
    }
}