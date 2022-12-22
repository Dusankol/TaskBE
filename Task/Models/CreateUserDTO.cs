using Task.Models.ENUM;

namespace Task.Models
{
    public class CreateUserDTO
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid CompanyId { get; set; }

        public DateTime DOB { get; set; }

        public PositionEnum Position { get; set; }
        public string PhoneNumber { get; set; }
    }
}
