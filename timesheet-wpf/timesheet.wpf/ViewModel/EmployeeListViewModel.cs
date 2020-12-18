using CommonServiceLocator;
using Prism.Regions;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using timesheet.core.Base;
using timesheet.core.Singleton;
using timesheet.data.Contracts;
using timesheet.data.Models;
using timesheet.services.Services;
using timesheet.wpf.Commands;
using Task = System.Threading.Tasks.Task;

namespace timesheet.wpf.ViewModel
{
    public class EmployeeListViewModel : BaseViewModel, INavigationAware
    {
        private readonly IEmployeeService _employeeService;

        private static IRegionManager RegionManger => ServiceLocator.Current.GetInstance<IRegionManager>();
        public ICommand OpenEmployeeTimesheetCommand { get; set; }

        private Employee _selectedEmployee;

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                NotifyPropertyChanged(nameof(SelectedEmployee));
            }
        }

        public ICollectionView Employees { get; set; }

        public EmployeeListViewModel()
        {
            _employeeService = (EmployeeService)SingletonInstances.GetEmployeeService(typeof(EmployeeService));
            OpenEmployeeTimesheetCommand = new OpenEmployeeTimesheetCommand(RegionManger);
            Task.Run(OnLoaded);
        }

        private async void OnLoaded()
        {
            await Load();
        }

        private async Task Load()
        {
            Employees = CollectionViewSource.GetDefaultView(await _employeeService.GetEmployees());
            NotifyPropertyChanged(nameof(Employees));
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await Load();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}