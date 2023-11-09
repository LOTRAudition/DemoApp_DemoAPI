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

namespace DemoApp_DemoAPI.Controllers.v2
{
    [Route("api/v{version:apiVersion}/DemoNumberAPI")]
    [ApiController]
    [ApiVersion("2.0")]
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


        //[MapToApiVersion("2.0")]
        [HttpGet("GetString")]
        public IEnumerable<string> Get()
        {
            return new string[] { "Alperen", "Büğdüz" };
        }


    }
}
