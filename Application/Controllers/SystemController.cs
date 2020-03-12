using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecuritySystem.Domain;
using SecuritySystem.Domain.DTO;
using SecuritySystem.Repositories.Interfaces;
using Systems = SecuritySystem.Domain.Entities.System;

namespace Application.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class SystemController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromServices]ISystemRepository systemRepository, [FromQuery(Name = "page")] int page = 1)
        {
            try
            {
                return StatusCode(200, systemRepository.GetAll(page));
            } catch
            {
                return BadRequest();
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromServices]ISystemRepository systemRepository, int id)
        {
            try
            {
                return StatusCode(200, systemRepository.GetById(id));
            }
            catch 
            {
                return BadRequest();
            }

            
        }

        [Route("filter")]
        [HttpGet]
        public IActionResult Get([FromServices]ISystemRepository systemRepository, 
                                        [FromQuery(Name = "description")] string description, 
                                        [FromQuery(Name = "initials")] string initials, 
                                        [FromQuery(Name = "email")] string email,
                                        [FromQuery(Name = "page")] int page = 1)
        {
            try
            {
                var result = systemRepository.GetAllWithFilter(description, initials, email, page);

                if (result.TotalResults > 0)
                    return StatusCode(200, result);

                var messageResult = new JsonResult(new { errorMessage = Messages.NotResult });

                return StatusCode(200, messageResult);
            }
            catch 
            {
                return BadRequest();
            }

            
        }

        [HttpPost]
        public IActionResult Create([FromServices]ISystemRepository systemRepository, Systems system)
        {
            try
            {
                systemRepository.Create(system);

                return StatusCode(201, new { successMessage = Messages.SuccessOperation });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Update([FromServices]ISystemRepository systemRepository, Systems system)
        {

            try
            {
                if (system.Id == 0)
                {
                    return BadRequest();
                }

                systemRepository.Update(system);

                return StatusCode(200, new { successMessage = Messages.SuccessOperation });
            }
            catch (Exception)
            {
                return BadRequest();
            }

           
        }
    }
}
