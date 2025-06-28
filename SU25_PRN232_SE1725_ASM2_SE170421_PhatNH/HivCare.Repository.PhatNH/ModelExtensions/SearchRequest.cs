using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HivCare.Repository.PhatNH.ModelExtensions
{
    public class SearchRequest
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 1;
    }
    public class ClassSearchDoctorRequest : SearchRequest
    {
        public string? DayOfWeek { get; set; }
        public string? Notes { get; set; }
        public string? Status { get; set; }
    }
}
