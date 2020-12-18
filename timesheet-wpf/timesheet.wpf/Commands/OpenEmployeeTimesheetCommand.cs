using System;
using System.Windows.Input;
using Prism.Regions;
using timesheet.data.Models;
using timesheet.services.Constants;
using timesheet.wpf.Views;

namespace timesheet.wpf.Commands
{
    public class OpenEmployeeTimesheetCommand : ICommand
    {
        private readonly IRegionManager _regionManager;

        public OpenEmployeeTimesheetCommand(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public bool CanExecute(object parameter)
        {
            return parameter is Employee;
        }

        public void Execute(object parameter)
        {
            var parameters = new NavigationParameters {{"employee", parameter as Employee}};
            _regionManager.RequestNavigate(Regions.Employee, nameof(EmployeeTimesheet), parameters);
        }

        public event EventHandler CanExecuteChanged;
    }
}