using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceClaim.Models
{
    public class InitiateClaim
    {
        [Key]
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string Ailment { get; set; }
        public string TreatmentPackageName { get; set; }
        public string InsurerName { get; set; }
        public int TotalInsuranceCost { get; set; }

    }
}
