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
    public class BidImageController : ControllerBase
    {
        private IBidImageService _bidImageService;

        public BidImageController(IBidImageService bidImageService)
        {
            _bidImageService = bidImageService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _bidImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _bidImageService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbybidid")]
        public IActionResult GetByBidId(int id)
        {
            var result = _bidImageService.GetByBidId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = ("Image"))] IFormFile file, [FromForm] BidImage bidImage)
        {
            var result = _bidImageService.Add(file,bidImage);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update([FromForm(Name = ("Image"))] IFormFile file, [FromForm] BidImage bidImage)
        {
            var result = _bidImageService.Update(file,bidImage);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromForm(Name = ("ImageId"))] int id)
        {
            var image = _bidImageService.GetById(id).Data;
            var result = _bidImageService.Delete(image);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
