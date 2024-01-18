using ClassLibrary.Model.Models;
using EmployeeAdministration.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace KREATX.Controllers
{
    [ApiController]
    [Route("api/administrator")]
    public class AdministratorController : Controller
    {
        private readonly AdministrationContext _dbContext;

        public AdministratorController(AdministrationContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        private bool HasOpenTasks(int projectId)
        {
            return _dbContext.Tasks.Any(task => task.ProjectId == projectId && task.TaskStatus == "Open");
        }

        //***FOR EMPLOYEES:
        //Add employees to projects:

        [HttpPost("AddEmployeeToProject")]
        public async Task<IActionResult> AddEmployeeToProject([FromBody] EmployeeForCreationModel employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //To create a new employee that is associated to a project:
            var newEmployee = new EmployeeAdministration.Entities.Employee
            {
                EmployeeId = employee.EmployeeId,
                EmployeeFirstName = employee.EmployeeFirstName,
                EmployeeLastName = employee.EmployeeLastName,
                EmployeeEmail = employee.EmployeeEmail,
                EmployeePassword = employee.EmployeePassword,
                EmployeeProfilePicture = employee.EmployeeProfilePicture,
                ProjectId = employee.ProjectId,
                TaskId = employee.TaskId
            };

            _dbContext.Employees.Add(newEmployee);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = newEmployee.EmployeeId }, newEmployee);
        }



        //DELETE employees from projects:
        [HttpDelete("RemoveEmployeeFromProject/{employeeId}")]
        public async Task<IActionResult> RemoveEmployeeFromProject(int employeeId)
        {
            //To find the employee:
            var selectedEmployee = await _dbContext.Employees.FindAsync(employeeId);

            if (selectedEmployee == null)
            {
                return NotFound();
            }

            //To remove the employee from the project:
            selectedEmployee.ProjectId = null;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }


        //***FOR PROJECTS:
        [HttpDelete("RemoveProject/{projectId}")]
        public async Task<IActionResult> RemoveProject(int projectId)
        {
            var project = await _dbContext.Projects.FindAsync(projectId);

            if (project == null)
            {
                return NotFound();
            }

            // To check if the project has open tasks:
            if (HasOpenTasks(projectId))
            {
                return BadRequest("Cannot remove the project as it has open tasks.");
            }

            // To remove the project:
            _dbContext.Projects.Remove(project);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }


        //***FOR TASKS:

        //CREATE:
        [HttpPost("CreateNewTask")]
        public async Task<IActionResult> CreateTask([FromBody] TaskForCreationModel taskForCreationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //To create a new task based on TaskForCreationModel.cs data:
            var newTask = new EmployeeAdministration.Entities.Task
            {
                TaskId = taskForCreationModel.TaskId,
                TaskTitle = taskForCreationModel.TaskTitle,
                TaskDescription = taskForCreationModel.TaskDescription,
                TaskDueDate = taskForCreationModel.TaskDueDate,
                TaskStatus = taskForCreationModel.TaskStatus,
                ProjectId = taskForCreationModel.ProjectId
            };

            _dbContext.Tasks.Add(newTask);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { id = newTask.TaskId }, newTask);
        }

        //UPDATE (the task status as completed):
        [HttpPut("MarkAsCompleted/{taskId}")]
        public async Task<IActionResult> MarkTaskAsCompleted(int taskId)
        {
            var task = await _dbContext.Tasks.FindAsync(taskId);

            if (task == null)
            {
                return NotFound();
            }

            // To set the task status to "Completed" or another appropriate status:
            task.TaskStatus = "Completed";

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        //DELETE:
        [HttpDelete("RemoveTask/{taskId}")]
        public async Task<IActionResult> RemoveTask(int taskId)
        {
            var task = await _dbContext.Tasks.FindAsync(taskId);
            if (task == null)
            {
                return NotFound();
            }

            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
