using ars_portal.Models.Models.Base;
using System;

namespace ars_portal.Models.Models
{
    public class EmployeesPrimaryData: BaseModel
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactPreference { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Department { get; set; }
        public string PhotoPath { get; set; }

    }
}
