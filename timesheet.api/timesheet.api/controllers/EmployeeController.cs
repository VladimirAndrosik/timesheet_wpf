using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using timesheet.business;
using timesheet.model;
using Task = timesheet.model.Task;

namespace timesheet.api.controllers
{
    [Route("api/v1/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            IQueryable<Employee> items = _employeeService.GetEmployees();
            return new ObjectResult(items);
        }

        [HttpGet("getweektimesheets")]
        public IActionResult GetEmployeeWeekTimesheets([FromQuery(Name = "employeeId")] int employeeId,
            [FromQuery(Name = "firstDayOfTheWeek")] DateTime firstDayOfTheWeek)
        {
            IQueryable<Timesheet> items = _employeeService.GetEmployeeWeekTimesheets(employeeId, firstDayOfTheWeek);
            return new ObjectResult(items);
        }

        [HttpGet("gettasks")]
        public IActionResult GetEmployeeWeekTimesheets()
        {
            IQueryable<Task> items = _employeeService.GetEmployeeTasks();
            return new ObjectResult(items);
        }

        [HttpPut("updatetimesheets")]
        public async Task<IActionResult> UpdateTimesheets([FromBody] List<Timesheet> content)
        {
            await _employeeService.UpdateTimesheets(content);
            return Ok();
        }

        [HttpPost("inserttimesheets")]
        public async Task<IActionResult> InsertTimesheets([FromBody] List<Timesheet> content)
        {
            await _employeeService.InsertTimesheets(content);
            return Ok();
        }

        [HttpDelete("deletetimesheets")]
        public async Task<IActionResult> DeleteTimesheets(string content)
        {
            var items = JsonConvert.DeserializeObject<List<Timesheet>>(content);
            await _employeeService.DeleteTimesheets(items);
            return Ok();
        }
    }
}