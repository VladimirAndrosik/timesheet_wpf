using CommonServiceLocator;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Prism.Commands;
using timesheet.core.Base;
using timesheet.core.Helpers;
using timesheet.core.Singleton;
using timesheet.data.Contracts;
using timesheet.data.Models;
using timesheet.services.Services;
using timesheet.wpf.Commands;
using Task = System.Threading.Tasks.Task;
using TimeTask = timesheet.data.Models.Task;

namespace timesheet.wpf.ViewModel
{
    public class EmployeeTimesheetViewModel : BaseViewModel, INavigationAware
    {
        private List<Timesheet> _initialTimesheets;
        private readonly IEmployeeService _employeeService;

        private static IRegionManager RegionManger => ServiceLocator.Current.GetInstance<IRegionManager>();
        public ICommand ReturnToEmployeeListCommand { get; set; }
        public ICollectionView Employees { get; set; }

        public ObservableCollection<TimeTask> Tasks { get; set; }

        public ObservableCollection<Timesheet> Timesheets
        {
            get;
            set;
        }

        public DateTime StartWeekDate { get; set; }
        public DateTime EndWeekDate { get; set; }

        public ObservableCollection<Timesheet> TotalRow { get; set; }

        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand NextCommand { get; private set; }
        public DelegateCommand PrevCommand { get; private set; }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                NotifyPropertyChanged(nameof(SelectedEmployee));
                UpdateTimesheets(DateTime.Today).RunSynchronously();
            }
        }

        private Timesheet _currentTimesheet;

        public Timesheet CurrentTimesheet
        {
            get => _currentTimesheet;
            set
            {
                UpdateTotalRow();
                _currentTimesheet = value;
            }
        }
        
        public EmployeeTimesheetViewModel()
        {
            _employeeService = (EmployeeService)SingletonInstances.GetEmployeeService(typeof(EmployeeService));
            ReturnToEmployeeListCommand = new ReturnToEmployeeListCommand(RegionManger);
            SaveCommand = new DelegateCommand(Save, CanSubmit);
            PrevCommand = new DelegateCommand(Prev, CanSubmit);
            NextCommand = new DelegateCommand(Next, CanSubmit);

            Timesheets = new ObservableCollection<Timesheet>();

            TotalRow = new ObservableCollection<Timesheet>(new List<Timesheet> { new Timesheet() });

            StartWeekDate = DateTimeHelpers.StartOfWeek(DateTime.Today);
            EndWeekDate = StartWeekDate.AddDays(6);
            NotifyWeekDates();
            Task.Run(OnLoaded);
        }

        private async void Save()
        {
            List<Timesheet> current = Timesheets.ToList();
            List<Timesheet> deleted = _initialTimesheets.Except(current).ToList();
            List<Timesheet> inserted = Timesheets.Except(_initialTimesheets).ToList();
            inserted.ForEach(t =>
            {
                t.EmployeeId = SelectedEmployee.Id;
                t.StartDay = StartWeekDate;
            });
            List<Timesheet> update = _initialTimesheets.Except(deleted).ToList();
            if (update.Any())
            {
                await _employeeService.UpdateTimesheets(update);
            }

            if (inserted.Any())
            {
                await _employeeService.InsertTimesheets(inserted);
            }

            if (deleted.Any())
            {
                await _employeeService.DeleteTimesheets(deleted);
            }
            UpdateTotalRow();
        }

        private void Prev()
        {
            StartWeekDate = StartWeekDate.AddDays(-7);
            EndWeekDate = EndWeekDate.AddDays(-7);
            NotifyWeekDates();
            UpdateTimesheets(StartWeekDate).ConfigureAwait(false);
        }

        private void Next()
        {
            StartWeekDate = StartWeekDate.AddDays(7);
            EndWeekDate = EndWeekDate.AddDays(7);
            NotifyWeekDates();
            UpdateTimesheets(StartWeekDate).ConfigureAwait(false);
        }

        private static bool CanSubmit()
        {
            return true;
        }

        private async void OnLoaded()
        {
            await Load();
        }

        private async Task Load()
        {
            Employees = CollectionViewSource.GetDefaultView(await _employeeService.GetEmployees());
            NotifyPropertyChanged(nameof(Employees));
            Tasks = new ObservableCollection<TimeTask>(await _employeeService.GetTasks());
            NotifyPropertyChanged(nameof(Tasks));
            await UpdateTimesheets(DateTime.Today);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.TryGetValue("employee", out Employee employee))
            {
                SelectedEmployee = employee;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        private async Task UpdateTimesheets(DateTime date)
        {
            if (SelectedEmployee != null)
            {
                await System.Windows.Application.Current.Dispatcher.Invoke(async delegate
                {
                    Timesheets = new ObservableCollection<Timesheet>(await _employeeService.GetEmployeeWeekTimesheets(SelectedEmployee.Id, DateTimeHelpers.StartOfWeek(date)));
                    _initialTimesheets = Timesheets.ToList();
                    NotifyPropertyChanged(nameof(Timesheets));
                    UpdateTotalRow();
                });
            }
        }

        private void NotifyWeekDates()
        {
            NotifyPropertyChanged(nameof(StartWeekDate));
            NotifyPropertyChanged(nameof(EndWeekDate));
        }

        private void UpdateTotalRow()
        {
            List<Timesheet> current = Timesheets.ToList();
            Timesheet totalRow = TotalRow.Single();
            totalRow.SundayEffort = current.Sum(t => t.SundayEffort);
            totalRow.MondayEffort = current.Sum(t => t.MondayEffort);
            totalRow.TuesdayEffort = current.Sum(t => t.TuesdayEffort);
            totalRow.WednesdayEffort = current.Sum(t => t.WednesdayEffort);
            totalRow.ThursdayEffort = current.Sum(t => t.ThursdayEffort);
            totalRow.FridayEffort = current.Sum(t => t.FridayEffort);
            totalRow.SaturdayEffort = current.Sum(t => t.SaturdayEffort);
            TotalRow.Remove(totalRow);
            TotalRow.Add(totalRow);
        }
    }
}