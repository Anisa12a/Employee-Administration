using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Model.Models
{
    public class TaskForCreationModel
    {
        public int TaskId { get; set; }

        [Required(ErrorMessage = "Task Title si required!")]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "Task Title must be between 4 and 40 characters!")]
        public string TaskTitle { get; set; } = string.Empty;


        [StringLength(60, ErrorMessage = "Task Description cannot exceed 60 characters!")]
        public string TaskDescription { get; set; } = string.Empty;


        [Required(ErrorMessage = "Due Date of the task is required!")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format!")]
        public DateTime TaskDueDate { get; set; }


        [Required(ErrorMessage = "Task status is required!")]
        [StringLength(15)]
        public string TaskStatus { get; set; } = string.Empty;

        [Required(ErrorMessage = "Project ID is required!")]
        public int ProjectId { get; set; }
    }
}
