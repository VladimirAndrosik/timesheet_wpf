using System;
using System.Windows.Input;
using Prism.Regions;
using timesheet.services.Constants;
using timesheet.wpf.Views;

namespace timesheet.wpf.Commands
{
    public class ReturnToEmployeeListCommand : ICommand
    {
        private readonly IRegionManager _regionManager;

        public ReturnToEmployeeListCommand(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _regionManager.RequestNavigate(Regions.Employee, nameof(EmployeeList));
        }

        public event EventHandler CanExecuteChanged;
    }
}