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
        /// <summary>
        /// Retorna uma lista de sistemas.
        /// </summary>
        /// <param name="page"></param>
        /// <returns>Retorna uma lista de sistemas.</returns>
        /// <response code="200">Retorna um objeto json contendo o número resultados, a página solicitada e a lista de sistemas.</response>
        /// <response code="500">Se houver algum problema interno.</response>          
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpGet]
        public IActionResult Get([FromServices]ISystemRepository systemRepository, [FromQuery(Name = "page")] int page = 1)
        {
            try
            {
                return StatusCode(200, systemRepository.GetAll(page));
            } catch
            {
                return StatusCode(500);
            }
            
        }

        /// <summary>
        /// Retorna um sistema.
        /// </summary>
        /// <param name="id">Id do sistema</param>
        /// <returns>Retorna um sistema cujo id corresponde ao que foi informado.</returns>
        /// <response code="200">Retorna um sistema.</response>
        /// <response code="400">Caso o sistema não seja encontrado.</response>          
        /// <response code="500">Se houver algum problema interno.</response>     
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("{id}")]
        public IActionResult GetById([FromServices]ISystemRepository systemRepository, int id)
        {
            try
            {
                var sys = systemRepository.GetById(id);

                if (sys != null)
                {
                    return StatusCode(200, sys);
                }

                return BadRequest(new { ErrorMessage = "Não foi possivel encontrar nenhum sistema com o id informado." });
            }
            catch 
            {
                return StatusCode(500);
            }

            
        }

        /// <summary>
        /// Retorna uma lista de sistemas.
        /// </summary>
        /// <param name="description"></param>
        /// <param name="initials"></param>
        /// <param name="email"></param>
        /// <param name="page"></param>
        /// <returns>Retorna uma lista de sistemas.</returns>
        /// <response code="200">Retorna um objeto json contendo o número resultados, a página solicitada e a lista de sistemas.</response>
        /// <response code="500">Se houver algum problema interno.</response>          
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
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
                return StatusCode(500);
            }

            
        }

        /// <summary>
        /// Cadastra um novo sistema.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     POST v1/system
        ///     {        
        ///       "description": "Sim Multi",
        ///       "initials": "SIM",
        ///       "status": 0, 
        ///     }
        /// </remarks>
        /// <param name="system"></param>
        /// <returns>Retorna uma mensagem informando o resultado.</returns>
        /// <response code="201">Retorna uma mensagem informando que a operação foi realizada com sucesso.</response>
        /// <response code="500">Se houver algum problema interno.</response>          
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
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
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Altera um sistema cadastrado.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     POST v1/system
        ///     { 
        ///       "id": 1,
        ///       "description": "Sim Multi Alterado",
        ///       "initials": "SIM",
        ///       "email": "contato@infopharma.com.br",
        ///       "status": 0, 
        ///     }
        /// </remarks>
        /// <param name="system"></param>
        /// <returns>Retorna uma mensagem informando o resultado.</returns>
        /// <response code="200">Retorna uma mensagem informando que a operação foi realizada com sucesso.</response>
        /// <response code="400">Se o parametro informado for inválido</response>     
        /// <response code="500">Se houver algum problema interno.</response>   
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
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
                return StatusCode(500);
            }

           
        }
    }
}
