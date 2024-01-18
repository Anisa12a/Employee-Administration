using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Model.Models
{
    public class EmployeeForUpdateModel
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee's first name is required!")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Employee's first name must be between 2 and 20 characters!")]
        public string EmployeeFirstName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Employee's last name is required!")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Employee's last name must be between 2 and 20 characters!")]
        public string EmployeeLastName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Employee's email address is required!")]
        [StringLength(50, MinimumLength = 12, ErrorMessage = "This format is invalid for the email address!")]
        public string EmployeeEmail { get; set; } = string.Empty;


        [Required(ErrorMessage = "Employee's password is required!")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Employee's password must be at least 8 characters!")]
        [DataType(DataType.Password)]
        public string EmployeePassword { get; set; } = string.Empty;

        public string EmployeeProfilePicture { get; set; } = string.Empty;

        public int ProjectId { get; set; }

        public int TaskId { get; set; }
    }
}
