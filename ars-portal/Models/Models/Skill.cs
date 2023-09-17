using System.ComponentModel.DataAnnotations.Schema;

namespace ars_portal.Models.Models
{
    public class Skill
    {
        public int id { get; set; }
        public string skillName { get; set; }
        public int experienceInYears { get; set; }
        public string proficiency { get; set; }
        [ForeignKey("employeesDetailedData")]
        public int employeesDetailedDataId { get; set; }
        public virtual EmployeesDetailedData employeesDetailedData { get; set; }
    }
}
