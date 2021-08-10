using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreatmentMicroservice.Models
{
    public class PackageDetail
    {
       
        public int PackageId { get; set; }
        public string Ailment { get; set; }
        public string TreatmentPackageName { get; set; }
        public string TestDetails { get; set; }
        public int Cost { get; set; }
        public int TreatmentDuration { get; set; }
    }
}
