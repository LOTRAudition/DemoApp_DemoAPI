using AutoMapper;
using DemoApp_DemoAPI.Data;
using DemoApp_DemoAPI.Models;
using DemoApp_DemoAPI.Models.Dto;
using DemoApp_DemoAPI.Repository.IRepostiory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace DemoApp_DemoAPI.Controllers.v1
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/DemoAPI")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DemoAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IDemoRepository _dbDemo;
        private readonly IMapper _mapper;
        public DemoAPIController(IDemoRepository dbDemo, IMapper mapper)
        {
            _dbDemo = dbDemo;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetDemos([FromQuery(Name ="filterOccupancy")]int? occupancy,
            [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
        {
            try
            {

                IEnumerable<Demo> demoList;

                if (occupancy > 0)
                {
                    demoList = await _dbDemo.GetAllAsync(u => u.Occupancy == occupancy, pageSize:pageSize,
                        pageNumber:pageNumber);
                }
                else
                {
                    demoList = await _dbDemo.GetAllAsync(pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    demoList = demoList.Where(u=>u.Name.ToLower().Contains(search));
                }
                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<DemoDTO>>(demoList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [HttpGet("{id:int}", Name = "GetDemo")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(DemoDTO))]
//        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetDemo(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var demo = await _dbDemo.GetAsync(u => u.Id == id);
                if (demo == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<DemoDTO>(demo);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateDemo([FromBody] DemoCreateDTO createDTO)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                if (await _dbDemo.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Demo already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }
                //if (demoDTO.Id > 0)
                //{
                //    return StatusCode(StatusCodes.Status500InternalServerError);
                //}
                Demo demo = _mapper.Map<Demo>(createDTO);

                //Demo model = new()
                //{
                //    Amenity = createDTO.Amenity,
                //    Details = createDTO.Details,
                //    ImageUrl = createDTO.ImageUrl,
                //    Name = createDTO.Name,
                //    Occupancy = createDTO.Occupancy,
                //    Rate = createDTO.Rate,
                //    Sqft = createDTO.Sqft
                //};
                await _dbDemo.CreateAsync(demo);
                _response.Result = _mapper.Map<DemoDTO>(demo);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetDemo", new { id = demo.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteDemo")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteDemo(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var demo = await _dbDemo.GetAsync(u => u.Id == id);
                if (demo == null)
                {
                    return NotFound();
                }
                await _dbDemo.RemoveAsync(demo);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "UpdateDemo")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateDemo(int id, [FromBody] DemoUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }

                Demo model = _mapper.Map<Demo>(updateDTO);

                await _dbDemo.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialDemo")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialDemo(int id, JsonPatchDocument<DemoUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var demo = await _dbDemo.GetAsync(u => u.Id == id, tracked: false);

            DemoUpdateDTO demoDTO = _mapper.Map<DemoUpdateDTO>(demo);


            if (demo == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(demoDTO, ModelState);
            Demo model = _mapper.Map<Demo>(demoDTO);

            await _dbDemo.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }


    }
}
