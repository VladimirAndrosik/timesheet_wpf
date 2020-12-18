using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace timesheet.model
{
    public class Timesheet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        
        [ForeignKey(nameof(Task))]
        public int TaskId { get; set; }
        public Task Task { get; set; }
        
        public int SundayEffort { get; set; }
        public int MondayEffort { get; set; }
        public int TuesdayEffort { get; set; }
        public int WednesdayEffort { get; set; }
        public int ThursdayEffort { get; set; }
        public int FridayEffort { get; set; }
        public int SaturdayEffort { get; set; }

        public DateTime StartDay { get; set; }
    }
}