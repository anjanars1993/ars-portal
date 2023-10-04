using ars_portal.Models.Models.Base;
using System.Collections.Generic;

namespace ars_portal.Models.Models
{
    public class EmployeesDetailedData: BaseModel
    {
        public EmployeesDetailedData()
        {

            skills = new HashSet<Skill>();

        }
        public string fullName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string contactPreference { get; set; }
        public virtual ICollection<Skill> skills { get; set; }

    }
}
