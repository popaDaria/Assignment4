using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment2_3WebApi.Data;
using Assignment2_3WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2_3WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FamiliesController : ControllerBase
    {
        private IFamilyService familyService;

        public FamiliesController(IFamilyService familyService)
        {
            this.familyService = familyService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Family>>> GetFamilies()
        {
            try
            {
                IList<Family> families = await familyService.GetFamiliesAsync();
                return Ok(families);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> AddFamily([FromBody] Family family)
        {
            try
            {
                await familyService.AddFamilyAsync(family);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveAdultFromFamily([FromQuery] int adultId)
        {
            try
            {
                await familyService.RemoveAdultAsync(adultId);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }   
        }
    }
}