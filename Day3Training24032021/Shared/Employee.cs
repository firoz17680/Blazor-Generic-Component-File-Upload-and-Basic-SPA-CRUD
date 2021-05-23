using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3Training24032021.Shared
{
    public class Employee : LogInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please enter the name")]
        public string Name { get; set; }
        
        public Nullable<DateTime> JoiningDate { get; set; }

        public Nullable<decimal> Salary { get; set; }

        public Nullable<Guid> DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        public Nullable<Guid> DesignationId { get; set; }
        [ForeignKey("DesignationId")]
        public Designation Designation { get; set; }


        public Nullable<Guid> CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }


        public Nullable<Guid> ReligionId { get; set; }
        [ForeignKey("ReligionId")]
        public Religion Religion { get; set; }


        [EmailAddress]
        [CustomDomainValidation(AllowedDomain = "HOTMAIL.COM", ErrorMessage = "Sorry! you can use only hotmail")]
        public string Email { get; set; }
    }
}
