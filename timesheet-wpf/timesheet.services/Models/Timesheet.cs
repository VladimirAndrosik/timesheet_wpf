using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using timesheet.services.Annotations;

namespace timesheet.data.Models
{
    public class Timesheet : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int TaskId { get; set; }
        public DateTime StartDay { get; set; }

        public int SundayEffort { get; set; }
        public int MondayEffort { get; set; }
        public int TuesdayEffort { get; set; }
        public int WednesdayEffort { get; set; }
        public int ThursdayEffort { get; set; }
        public int FridayEffort { get; set; }
        public int SaturdayEffort { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override bool Equals(object obj)
        {
            return obj is Timesheet anotherTimesheet && anotherTimesheet.Id == Id;
        }
    }
}