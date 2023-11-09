using AutoMapper;
using DemoApp_Utility;
using DemoApp_Web.Models;
using DemoApp_Web.Models.Dto;
using DemoApp_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

namespace DemoApp_Web.Controllers
{
    public class DemoController : Controller
    {
        private readonly IDemoService _demoService;
        private readonly IMapper _mapper;
        public DemoController(IDemoService demoService, IMapper mapper)
        {
            _demoService = demoService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexDemo()
        {
            List<DemoDTO> list = new();

            var response = await _demoService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<DemoDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> CreateDemo()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDemo(DemoCreateDTO model)
        {
            if (ModelState.IsValid)
            {

                var response = await _demoService.CreateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
{
                    TempData["success"] = "Demo created successfully";
                    return RedirectToAction(nameof(IndexDemo));
                }
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateDemo(int demoId)
{
            var response = await _demoService.GetAsync<APIResponse>(demoId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                
                DemoDTO model = JsonConvert.DeserializeObject<DemoDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<DemoUpdateDTO>(model));
            }
            return NotFound();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDemo(DemoUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Demo updated successfully";
                var response = await _demoService.UpdateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexDemo));
                }
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteDemo(int demoId)
        {
            var response = await _demoService.GetAsync<APIResponse>(demoId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                DemoDTO model = JsonConvert.DeserializeObject<DemoDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDemo(DemoDTO model)
        {
            
                var response = await _demoService.DeleteAsync<APIResponse>(model.Id, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                TempData["success"] = "Demo deleted successfully";
                return RedirectToAction(nameof(IndexDemo));
                }
            TempData["error"] = "Error encountered.";
            return View(model);
        }

    }
}
