using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coelsa.Admin.Services;
using Coelsa.Infrastructure.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICoelsa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetAll()
        {
            try
            {
                var result = await _contactService.GetAll();
                if (result == null) return NotFound("No data found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllPaged")]
        public async Task<ActionResult<ContactPagedDto>> GetAllPaged(int page = 1, int size = 10)
        {
            try
            {
                var result = await _contactService.GetAllPaged(page, size);
                if (result == null) return NotFound("No data found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Create")]        
        public async Task<ActionResult<ContactDto>> Create([FromBody] ContactDto dto)
        {
            try
            {
                var result = await _contactService.Create(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Update/{id}")]
        public async Task<ActionResult<ContactDto>> Update([FromRoute] int id, [FromBody] ContactDto dto)
        {
            try
            {
                var result = await _contactService.Update(dto, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [HttpPost("Delete/{id}")]
        public async Task<ActionResult<ContactDto>> Delete([FromRoute] int id)
        {
            try
            {
                if (id == 0) return BadRequest("Please enter an id to delete");

                var result = await _contactService.Delete(id);

                if (result)
                    return Ok(result);

                return NotFound($"Contact Not Found with ID : {id}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactDto))]
        public async Task<ActionResult<ContactDto>> GetById(int id)
        {
            try
            {
                if (id.Equals(0)) return BadRequest("Please enter an id is a required data");
                return Ok( await _contactService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }        
    }
}
