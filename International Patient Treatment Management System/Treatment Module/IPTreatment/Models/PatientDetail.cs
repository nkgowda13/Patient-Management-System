using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IPTreatment.Models
{
    public class PatientDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Ailment { get; set; }
        public string TreatmentPackageName { get; set; }
        public DateTime TreatmentCommencementDate { get; set; }
        
    }
}
