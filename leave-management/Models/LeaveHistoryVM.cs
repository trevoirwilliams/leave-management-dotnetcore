using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class LeaveHistoryVM
    {
      
        public int Id { get; set; }

        
        public EmployeeVM RequestingEmployee { get; set; }
        public string RequestingEmployeeId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        
        public DetailsLeaveTypeVM LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateActioned { get; set; }
        public bool? Approved { get; set; }
        
        public EmployeeVM ApprovedBy { get; set; }
        public string ApprovedById { get; set; }
    }
}
