using IPTreatment.Models;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPTreatment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentPlanController : ControllerBase

    {
        private static readonly ILog _log4net = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ITreatmentRepository _repo;
        public TreatmentPlanController(ITreatmentRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        [Route("FormulateTreatmentTimetable")]
        public async Task<ActionResult<TreatmentPlan>> FormulateTreatmentTimetable([FromQuery]PatientDetail _details)
        {
            _log4net.Info("Generating plan...");
            try
            {
                TreatmentPlan plan = await _repo.GeneratePlan(_details);
                if (plan == null)
                {
                    _log4net.Info("Plan generation failed!");
                    return NotFound();
                }
                _log4net.Info("Plan generated and returned.");
                return Ok(plan);
            }
            catch (Exception ex)
            {
                _log4net.Error($"Some error occurred while generating plan for {_details.Name}!\n {ex.Message}");
                return null;
            }
         }

        [HttpGet]
        [Authorize]
        [Route("UpdateCost")]
        public async Task<ActionResult<string>> UpdateDatabaseCost([FromQuery] int id, int cost, string insurer)
        {
            try
            {
                string result = await _repo.UpdateCost(id,cost,insurer);
                if (result != "False")
                {
                    return Ok(result);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        [HttpGet]
        [Route("GetNotCompletedMembers")]
        public async Task<ActionResult<List<TreatmentPlan>>> GetNotCompleted()
        {
            try
            {
                List<TreatmentPlan> result =await _repo.GetPaymentInProgress();
                return result;
            }
            catch
            {
                return null;
            }
        }

        [HttpGet]
        [Route("GetMembers")]
        public async Task<ActionResult<List<TreatmentPlan>>> GetAll()
        {
            try
            {
                List<TreatmentPlan> result = await _repo.GetAllMembers();
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
