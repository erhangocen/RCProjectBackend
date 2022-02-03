using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenOperationController : ControllerBase
    {
        private ITokenOperationService _tokenOperationService;

        public TokenOperationController(ITokenOperationService tokenOperationService)
        {
            _tokenOperationService = tokenOperationService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _tokenOperationService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _tokenOperationService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyuserid")]
        public IActionResult GetByUserId(int id)
        {
            var result = _tokenOperationService.GetByUserId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(TokenOperation tokenOperation)
        {
            var result = _tokenOperationService.Add(tokenOperation);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
