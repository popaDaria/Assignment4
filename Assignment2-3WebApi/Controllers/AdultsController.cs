using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment2_3WebApi.Data;
using Assignment2_3WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2_3WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdultsController : ControllerBase
    {
        private IAdultService adultService;

        public AdultsController(IAdultService adultService)
        {
            this.adultService = adultService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Adult>>> GetAdults()
        {
            try
            {
                IList<Adult> adults = await adultService.GetAdultsAsync();
                return Ok(adults);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPut]
        public async Task<ActionResult> AddAdult([FromBody] Adult adult)
        {
            try
            {
                await adultService.AddAdultAsync(adult);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateAdult([FromBody] Adult adult)
        {
            try
            {
                await adultService.UpdateAdultInfoAsync(adult);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteAdult([FromRoute] int id)
        {
            try
            {
                await adultService.RemoveAdultAsync(id);
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