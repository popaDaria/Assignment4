﻿using System;
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
    public class UsersController : ControllerBase
    {
        private IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        /*[HttpGet]
        public async Task<ActionResult<IList<Family>>> GetAllUsers()
        {
            try
            {
                IList<User> users = await userService.GetUsers();
                return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }*/
        
        
        [HttpGet]
        public async Task<ActionResult<User>> GetUser([FromQuery] string userName)
        {
            try
            {
                User user = await userService.ValidateUser(userName);
                return Ok(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}