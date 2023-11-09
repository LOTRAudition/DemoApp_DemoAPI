using AutoMapper;
using DemoApp_Utility;
using DemoApp_Web.Models;
using DemoApp_Web.Models.Dto;
using DemoApp_Web.Models.VM;
using DemoApp_Web.Services;
using DemoApp_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;

namespace DemoApp_Web.Controllers
{
    public class DemoNumberController : Controller
    {
        private readonly IDemoNumberService _demoNumberService;
        private readonly IDemoService _demoService;
        private readonly IMapper _mapper;
        public DemoNumberController(IDemoNumberService demoNumberService, IMapper mapper, IDemoService demoService)
        {
            _demoNumberService = demoNumberService;
            _mapper = mapper;
            _demoService = demoService;
        }

        public async Task<IActionResult> IndexDemoNumber()
        {
            List<DemoNumberDTO> list = new();

            var response = await _demoNumberService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<DemoNumberDTO>>(Convert.ToString(response.Result));
}
            return View(list);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateDemoNumber()
        {
            DemoNumberCreateVM demoNumberVM = new();
            var response = await _demoService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
{
                demoNumberVM.DemoList = JsonConvert.DeserializeObject<List<DemoDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); ;
            }
            return View(demoNumberVM);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDemoNumber(DemoNumberCreateVM model)
        {
            if (ModelState.IsValid)
            {

                var response = await _demoNumberService.CreateAsync<APIResponse>(model.DemoNumber, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexDemoNumber));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _demoService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.DemoList = JsonConvert.DeserializeObject<List<DemoDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); ;
            }
            return View(model);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateDemoNumber(int demoNo)
        {
            DemoNumberUpdateVM demoNumberVM = new();
            var response = await _demoNumberService.GetAsync<APIResponse>(demoNo, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                DemoNumberDTO model = JsonConvert.DeserializeObject<DemoNumberDTO>(Convert.ToString(response.Result));
                demoNumberVM.DemoNumber =  _mapper.Map<DemoNumberUpdateDTO>(model);
            }

            response = await _demoService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                demoNumberVM.DemoList = JsonConvert.DeserializeObject<List<DemoDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); 
                return View(demoNumberVM);
            }


            return NotFound();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDemoNumber(DemoNumberUpdateVM model)
        {
            if (ModelState.IsValid)
            {

                var response = await _demoNumberService.UpdateAsync<APIResponse>(model.DemoNumber, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexDemoNumber));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _demoService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.DemoList = JsonConvert.DeserializeObject<List<DemoDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); ;
            }
            return View(model);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteDemoNumber(int demoNo)
        {
            DemoNumberDeleteVM demoNumberVM = new();
            var response = await _demoNumberService.GetAsync<APIResponse>(demoNo, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                DemoNumberDTO model = JsonConvert.DeserializeObject<DemoNumberDTO>(Convert.ToString(response.Result));
                demoNumberVM.DemoNumber = model;
            }

            response = await _demoService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                demoNumberVM.DemoList = JsonConvert.DeserializeObject<List<DemoDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(demoNumberVM);
            }


            return NotFound();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDemoNumber(DemoNumberDeleteVM model)
        {

            var response = await _demoNumberService.DeleteAsync<APIResponse>(model.DemoNumber.DemoNo, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexDemoNumber));
            }

            return View(model);
        }



    }
}
