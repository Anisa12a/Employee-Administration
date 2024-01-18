using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAdministration.Entities
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Project Name is required!")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "Project Name must be between 5 and 40 characters!")]
        public string ProjectName { get; set; } = string.Empty;


        [StringLength(60, ErrorMessage = "Project description cannot exceed 60 characters!")]
        public string ProjectDescription { get; set; } = string.Empty;


        [Required(ErrorMessage = "Start Date of the project is required!")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format!")]
        public DateTime StartDate { get; set; }


        [Required(ErrorMessage = "End Date of the project is required!")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format!")]
        public DateTime EndDate { get; set; }

        [ForeignKey("Task")]
        public int TaskId { get; set; }
        public Task? Task { get; set; }  //Navigation property
    }
}
