﻿namespace Task.Models
{
    public class GetCompanyDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public List<User> Users { get; set; }
    }
}
