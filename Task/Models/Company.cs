using System.ComponentModel.DataAnnotations;

namespace Task.Models
{
    public class Company
    {
        [Key]
        public long Id { get; set; }

        public string CompanyName { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
