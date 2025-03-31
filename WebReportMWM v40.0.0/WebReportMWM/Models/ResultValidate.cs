using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebReportMWM.Models
{
    public class ResultValidate
    {
        public bool Validated
        {
            get
            {
                return ErrorMessages.Count == 0;
            }
        }

        public List<string> ErrorMessages { get; set; } = new List<string>();
        
        public override string ToString()
        {
            return String.Join(". ",ErrorMessages);
        }
    }
}