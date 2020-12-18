using Prism.Ioc;
using System.Windows;
using timesheet.wpf.ViewModel;
using timesheet.wpf.Views;

namespace timesheet.wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<EmployeeList, EmployeeListViewModel>();
            containerRegistry.RegisterForNavigation<EmployeeTimesheet, EmployeeTimesheetViewModel>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}
