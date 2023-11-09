using AutoMapper;
using DemoApp_Utility;
using DemoApp_Web.Models;
using DemoApp_Web.Models.Dto;
using DemoApp_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DemoApp_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDemoService _demoService;
        private readonly IMapper _mapper;
        public HomeController(IDemoService demoService, IMapper mapper)
        {
            _demoService = demoService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<DemoDTO> list = new();

            var response = await _demoService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<DemoDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
       
    }
}