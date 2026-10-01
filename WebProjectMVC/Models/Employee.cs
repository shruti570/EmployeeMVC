using System.ComponentModel.DataAnnotations;



namespace WebProjectMVC.Models
{
    public class Employee
    {
        public int SlNo { get; set; }

        [Required(ErrorMessage = "Employee name is required")]
        [StringLength(100)]
        [Display(Name = "Employee Name")]
        public string Empname { get; set; }

        [Required(ErrorMessage = "Employee code is required")]
        [StringLength(20)]
        [Display(Name = "Employee Code")]
        public string EmpCode { get; set; }

        [Display(Name = "Reporting Person")]
        public int ReportingPersonId { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public int CreateuId { get; set; }

        public DateTime Createdt { get; set; }

        public string CreatedBY { get; set; }

        public string ModifiedBY { get; set; }

       
        public string ReportingPerson { get; set; }
       

        public string Deptname { get; set; }

        public string lmodifyby { get; set; }

        public string PhoneNumber { get; set; }

        public string  Email { get; set; }

        public DateTime lmodifydt { get; set; }

        public int deluid { get; set; }

        public DateTime deldt { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]  
        [Display(Name = "Salary")]
        public decimal Salary { get; set; }


        //[Required(ErrorMessage = "StateName is required")]
        //[Display(Name = "StateName")]
        public int StateId { get; set; }

        public int DistrictId { get; set; }

        public int CityId { get; set; }

        public int PincodeId { get; set; }

        public int AreaId { get; set; }


        public string StateName { get; set; }

        public string DistrictName { get; set; }

        public string CityName { get; set; }

        public string Pincode { get; set; }


        public string AreaName { get; set; }

      




    }
}