﻿using FlightPlanner.Data;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.API.Controllers
{


    public abstract class BaseApiController : ControllerBase
    {
        protected IFlightPlannerDbContext _context;
        public BaseApiController(IFlightPlannerDbContext context)
        {
            _context = context;
        }
    }
}