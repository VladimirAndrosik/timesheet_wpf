namespace timesheet.data.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
        
        public double Average { get; set; }
        
        public double Sum { get; set; }
        
        public override bool Equals(object obj)
        {
            if (!(obj is Employee))
                return false;

            return ((Employee)obj).Id == Id;
        }
    }
}