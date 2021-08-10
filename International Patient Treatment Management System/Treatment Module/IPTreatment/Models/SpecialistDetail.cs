using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreatmentMicroservice.Models
{
    public class SpecialistDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ExperienceInYears { get; set; }
        public long ContactNumber { get; set; }
        public string AreaOfExpertise { get; set; }
    }
}
