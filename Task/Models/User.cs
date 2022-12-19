using System.ComponentModel.DataAnnotations;
using Task.Models.ENUM;
namespace Task.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public long CompanyId { get; set; }

        public string CompanyName { get; set; }

        public DateTime DOB { get; set; }

        public PositionEnum Position { get; set; }
        public string PhoneNumber{ get; set; }
    }
}
