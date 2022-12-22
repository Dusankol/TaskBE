using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Task.Models.ENUM;
namespace Task.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid CompanyId { get; set; }

        public Company Company { get; set; }

        public DateTime DOB { get; set; }

        public PositionEnum Position { get; set; }
        public string PhoneNumber{ get; set; }
    }
}
