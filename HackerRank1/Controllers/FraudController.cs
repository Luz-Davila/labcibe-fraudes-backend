using System;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.WebAPI.Data;
using LibraryService.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FraudController : ControllerBase
    {
        private readonly IFraudService _fraudService;

        public FraudController(IFraudService fraudService)
        {
            _fraudService = fraudService;
        }

        // GET: api/Fraud
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var frauds = await _fraudService.Get(null);
            return Ok(frauds);
        }

        // POST: api/Fraud
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Fraud fraud)
        {
            if (fraud == null
                || string.IsNullOrWhiteSpace(fraud.ImpostorDetails)
                || string.IsNullOrWhiteSpace(fraud.ContactInfo))
            {
                return BadRequest(new
                {
                    message = "Los campos ImpostorDetails y ContactInfo son obligatorios."
                });
            }

            var created = await _fraudService.Add(fraud);
            return StatusCode(201, created);
        }
    }
}
