using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceClaim.Models
{
    public class InsurerDetail
    {
        [Key]
        public string InsurerName { get; set; }
        public string InsurerPackageName { get; set; }
        public int InsuranceAmountLimit { get; set; }
        public string DisbursementDuration { get; set; }

    }
}
