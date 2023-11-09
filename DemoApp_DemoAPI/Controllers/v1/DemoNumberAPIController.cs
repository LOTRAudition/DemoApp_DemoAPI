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
using System.Data;
using System.Net;

namespace DemoApp_DemoAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/DemoNumberAPI")]
    [ApiController]
    [ApiVersion("1.0")]

    public class DemoNumberAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IDemoNumberRepository _dbDemoNumber;
        private readonly IDemoRepository _dbDemo;
        private readonly IMapper _mapper;
        public DemoNumberAPIController(IDemoNumberRepository dbDemoNumber, IMapper mapper,
            IDemoRepository dbDemo)
        {
            _dbDemoNumber = dbDemoNumber;
            _mapper = mapper;
            _response = new();
            _dbDemo = dbDemo;
        }


        [HttpGet("GetString")]
        public IEnumerable<string> Get()
        {
            return new string[] { "String1", "string2" };
        }

        [HttpGet]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetDemoNumbers()
        {
            try
            {

                IEnumerable<DemoNumber> demoNumberList = await _dbDemoNumber.GetAllAsync(includeProperties: "Demo");
                _response.Result = _mapper.Map<List<DemoNumberDTO>>(demoNumberList);
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


        [HttpGet("{id:int}", Name = "GetDemoNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetDemoNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var demoNumber = await _dbDemoNumber.GetAsync(u => u.DemoNo == id);
                if (demoNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<DemoNumberDTO>(demoNumber);
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
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateDemoNumber([FromBody] DemoNumberCreateDTO createDTO)
        {
            try
            {

                if (await _dbDemoNumber.GetAsync(u => u.DemoNo == createDTO.DemoNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Demo Number already Exists!");
                    return BadRequest(ModelState);
                }
                if (await _dbDemo.GetAsync(u => u.Id == createDTO.DemoID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Demo ID is Invalid!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                DemoNumber demoNumber = _mapper.Map<DemoNumber>(createDTO);


                await _dbDemoNumber.CreateAsync(demoNumber);
                _response.Result = _mapper.Map<DemoNumberDTO>(demoNumber);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetDemo", new { id = demoNumber.DemoNo }, _response);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteDemoNumber")]
        public async Task<ActionResult<APIResponse>> DeleteDemoNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var demoNumber = await _dbDemoNumber.GetAsync(u => u.DemoNo == id);
                if (demoNumber == null)
                {
                    return NotFound();
                }
                await _dbDemoNumber.RemoveAsync(demoNumber);
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
        [HttpPut("{id:int}", Name = "UpdateDemoNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateDemoNumber(int id, [FromBody] DemoNumberUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.DemoNo)
                {
                    return BadRequest();
                }
                if (await _dbDemo.GetAsync(u => u.Id == updateDTO.DemoID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Demo ID is Invalid!");
                    return BadRequest(ModelState);
                }
                DemoNumber model = _mapper.Map<DemoNumber>(updateDTO);

                await _dbDemoNumber.UpdateAsync(model);
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


    }
}
