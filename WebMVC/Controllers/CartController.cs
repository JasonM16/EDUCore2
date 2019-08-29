using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICoursePlannerService _plannerSvc;
        private readonly ICourseService _courseSvc;
        private readonly IIdentityParser<Student> _appUserParser;


        public CartController(
            ICoursePlannerService plannerSvc,
            ICourseService courseSvc,
            IIdentityParser<Student> appUserParser
            )
        {
            _plannerSvc = plannerSvc;
            _courseSvc = courseSvc;
            _appUserParser = appUserParser;
        }



        public async Task<IActionResult> Index()
        {
            try
            {
                var student = _appUserParser.Parse(HttpContext.User);
                var vm = await _plannerSvc.GetCoursePlanner(student);
            }
            catch (BrokenCircuitException)
            {
                // Catch error when Basket.api is in circuit-opened mode                 
                HandleBrokenCircuitException();
            }
            return View();
        }

        private void HandleBrokenCircuitException()
        {
            throw new NotImplementedException();
        }
    }
}