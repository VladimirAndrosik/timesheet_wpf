using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using timesheet.data.Contracts;
using timesheet.data.Models;
using TimeTask = timesheet.data.Models.Task;
using Task = System.Threading.Tasks.Task;

namespace timesheet.services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly string _baseUrl;

        public EmployeeService()
        {
            _baseUrl = ConfigurationManager.AppSettings.Get("baseUrl");
        }

        public async Task<List<Employee>> GetEmployees()
        {
            using (var client = new HttpClient())
            {
                return await GetResponseData<Employee>(client, _baseUrl + "/employee/getall");
            }
        }

        public async Task<List<Timesheet>> GetEmployeeWeekTimesheets(int employeeId, DateTime firstDayOfTheWeek)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format(_baseUrl + "/employee/getweektimesheets?employeeId={0}&firstDayOfTheWeek={1}",
                    employeeId, firstDayOfTheWeek.ToString("yyyy-MM-ddTHH:mm:ss"));
                return await GetResponseData<Timesheet>(client, url);
            }
        }

        public async Task<List<TimeTask>> GetTasks()
        {
            using (var client = new HttpClient())
            {
                var url = _baseUrl + "/employee/gettasks";
                return await GetResponseData<TimeTask>(client, url);
            }
        }

        public async Task UpdateTimesheets(List<Timesheet> timesheets)
        {
            using (var client = new HttpClient())
            {
                var url = _baseUrl + "/employee/updatetimesheets";
                var content = JsonConvert.SerializeObject(timesheets);
                await client.PutAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));
            }
        }

        public async Task InsertTimesheets(List<Timesheet> timesheets)
        {
            using (var client = new HttpClient())
            {
                var url = _baseUrl + "/employee/inserttimesheets";
                var content = JsonConvert.SerializeObject(timesheets);
                await client.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));
            }
        }

        public async Task DeleteTimesheets(List<Timesheet> timesheets)
        {
            using (var client = new HttpClient())
            {
                var content = JsonConvert.SerializeObject(timesheets);
                var url = string.Format(_baseUrl + "/employee/deletetimesheets?content={0}", content);
                await client.DeleteAsync(url);
            }
        }

        private static async Task<List<T>> GetResponseData<T>(HttpClient client, string url)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            var data = new List<T>();
            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<T>>(json);
            }
            return data;
        }
    }
}
