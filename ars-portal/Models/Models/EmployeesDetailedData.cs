using System.Collections.Generic;

namespace ars_portal.Models.Models
{
    public class EmployeesDetailedData
    {
        public EmployeesDetailedData()
        {

            skills = new HashSet<Skill>();

        }
        public int id { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string contactPreference { get; set; }
        public virtual ICollection<Skill> skills { get; set; }

    }
}
