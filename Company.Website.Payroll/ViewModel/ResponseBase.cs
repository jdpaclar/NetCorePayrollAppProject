using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Website.Payroll.ViewModel
{
    public class ResponseBase
    {
        public string[] ListMessage { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
