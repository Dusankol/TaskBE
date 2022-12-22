using Task.Models.ENUM;

namespace Task.Models
{
    public class GetUserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid CompanyId { get; set; }

        public string CompanyName { get; set; }

        public DateTime DOB { get; set; }

        public PositionEnum Position { get; set; }
        public string PhoneNumber { get; set; }
    }
}
