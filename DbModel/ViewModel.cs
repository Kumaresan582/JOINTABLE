namespace DbModel
{
    public class CustomerWithOrdersDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public List<Orders> Orders { get; set; }
    }

    public class Orders
    {
        public int OrderId { get; set; }
        public string Item { get; set; }
        public int Amount { get; set; }
        public int customer_id { get; set; }
    }

    public class CustomerAndOrderDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public int OrderId { get; set; }
        public string Item { get; set; }
        public int Amount { get; set; }
        public int customer_id { get; set; }
    }

    public class CustomerAndOrderDto1
    {
        public int OrderId { get; set; }
        public string Item { get; set; }
        public int Amount { get; set; }
        public int customer_id { get; set; }
    }

    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
    }

    public class CustomerOrderDto1
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string Item { get; set; }
        public int Amount { get; set; }
        public int customer_id { get; set; }
    }
}