using System.ComponentModel;
using System.Runtime.CompilerServices;
using timesheet.services.Annotations;

namespace timesheet.data.Models
{
    public class Task: INotifyPropertyChanged
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}