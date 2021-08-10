using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreatmentMicroservice.Models;

namespace IPTreatment.Models
{
    public interface ITreatmentRepository
    {
        public Task<List<PackageDetail>> GetPackageList();
        public Task<List<SpecialistDetail>> GetSpecialists();
        public Task<bool> SaveAll(PatientDetail _detail, TreatmentPlan _plan);
        public Task<TreatmentPlan> GeneratePlan(PatientDetail _detail);
        public Task<string> UpdateCost(int id, int cost, string insurer);
        public Task<List<TreatmentPlan>> GetPaymentInProgress();
        public Task<List<TreatmentPlan>> GetAllMembers();
    }
}
