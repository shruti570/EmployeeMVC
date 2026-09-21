namespace WebProjectMVC.Models
{
    public class Employee
    {
        public int SlNo { get; set; }

        public string Empname { get; set; }

        public string EmpCode { get; set; }

        public int ReportingPersonId { get; set; }

        public int DepartmentId { get; set; }

        public int CreateuId { get; set; }

        public DateTime Createdt { get; set; }

        public string CreatedBY { get; set; }

        public string ModifiedBY { get; set; }

        public string ReportingPersonName { get; set; }

        public string DepartmentName { get; set; }

        public string lmodifyby { get; set; }

        public DateTime lmodifydt { get; set; }

        public int deluid { get; set; }

        public DateTime deldt { get; set; }

        public decimal Salary {  get; set; }

    }
}
