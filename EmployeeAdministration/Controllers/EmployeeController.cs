using ClassLibrary.Model.Models;
using EmployeeAdministration.DbContexts;
using EmployeeAdministration.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KREATX.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly AdministrationContext _dbContext;

        public EmployeeController(AdministrationContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        //UPDATE:
        [HttpPut("UpdateProfileData")]
        public async Task<IActionResult> UpdateEmployee(EmployeeForUpdateModel employee)
        {
            var employeeFromDb = await _dbContext.Employees.FindAsync(employee.EmployeeId);
            if (employeeFromDb == null)
            {
                return NotFound();
            }

            //To perform the full update on the entire collection of employee:
            employeeFromDb.EmployeeId = employee.EmployeeId;
            employeeFromDb.EmployeeFirstName = employee.EmployeeFirstName;
            employeeFromDb.EmployeeLastName = employee.EmployeeLastName;
            employeeFromDb.EmployeeEmail = employee.EmployeeEmail;
            employeeFromDb.EmployeePassword = employee.EmployeePassword;
            employeeFromDb.EmployeeProfilePicture = employee.EmployeeProfilePicture;
            employeeFromDb.ProjectId = employee.ProjectId;
            employeeFromDb.TaskId = employee.TaskId;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        //CREATE:
        [HttpPost("CreateTasks")]
        public async Task<IActionResult> CreateTask(TaskModel task)
        {
            //To validate the task model:
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //To create a new task, based on task model:
            var newTask = new EmployeeAdministration.Entities.Task
            {
                TaskId = task.TaskId,
                TaskTitle = task.TaskTitle,
                TaskDescription = task.TaskDescription,
                TaskDueDate = task.TaskDueDate,
                TaskStatus = task.TaskStatus,
                ProjectId = task.ProjectId,
            };

            //To add this newly created task to the database:
            _dbContext.Tasks.Add(newTask);
            await _dbContext.SaveChangesAsync();

            //To return the newly created task or a success message as needed:
            return CreatedAtAction("GetTaskById", new { id = newTask.TaskId }, newTask);
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

        //VIEW:
        [HttpGet("ViewTasks")]
        public async Task<IActionResult> GetMyTasks(int employeeId)
        {
            // To find the employee by their ID:
            var employee = await _dbContext.Employees.FindAsync(employeeId);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            // To get the project IDs that the employee is a part of:
            var projectIds = await _dbContext.Projects
                .Where(p => p.TaskId == employee.TaskId)
                .Select(p => p.ProjectId)
                .ToListAsync();

            // To get all tasks that have the same ProjectId as the projects the employee is a part of:
            var tasks = await _dbContext.Tasks
                .Where(t => projectIds.Contains(t.ProjectId))
                .ToListAsync();

            return Ok(tasks);
        }

    }
}
