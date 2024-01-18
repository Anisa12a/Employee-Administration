using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Model.Models
{
    public class AdministratorModel
    {
        public int AdministratorId { get; set; }

        [Required(ErrorMessage = "Administrator's first name is required!")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Administrator's first name must be between 2 and 20 characters!")]
        public string AdministratorFirstName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Administrator's last name is required!")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Administrator's last name must be between 2 and 20 characters!")]
        public string AdministratorLastName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Administrator's email address is required!")]
        [StringLength(50, MinimumLength = 12, ErrorMessage = "This format is invalid for the email address!")]
        public string AdministratorEmail { get; set; } = string.Empty;


        [Required(ErrorMessage = "Administrator's password is required!")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Administrator's password must be at least 8 characters!")]
        [DataType(DataType.Password)]
        public string AdministratorPassword { get; set; } = string.Empty;

        public string AdministratorProfilePicture { get; set; } = string.Empty;
    }
}
