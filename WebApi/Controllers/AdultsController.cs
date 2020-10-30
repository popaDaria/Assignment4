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
    public class AdultsController
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
                return Ok
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}