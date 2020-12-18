using CommonServiceLocator;
using Prism.Regions;
using timesheet.services.Constants;
using timesheet.wpf.ViewModel;
using timesheet.wpf.Views;

namespace timesheet.wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new EmployeeListViewModel();
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            regionManager.RegisterViewWithRegion(Regions.Employee, typeof(EmployeeList));
        }
    }
}
