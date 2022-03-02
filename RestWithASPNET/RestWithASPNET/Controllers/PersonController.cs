using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNET.Model;
using RestWithASPNET.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;


        // Declaração do serviço utilizado
        private IPersonService _personService;

        // Injeção de uma instância de IPersonService
        // ao criar uma instância de PersonController
        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        // Mapeia solicitações GET para https://localhost:{port}/api/person
        // Não obtém parâmetros para FindAll -> Search All
        [HttpGet]
        public IActionResult Get()
        {

            return Ok(_personService.FindAll());
        }

        // Mapeia solicitações GET para https://localhost:{port}/api/person/{id}
        // recebendo um ID como no Request Path
        // Obter com parâmetros para FindById -> Pesquisar por ID
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personService.FindByID(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        // Mapeia solicitações POST para https://localhost:{port}/api/person/
        // [FromBody] consome o objeto JSON enviado no corpo da solicitação
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {

            if (person == null)
            {
                return BadRequest();
            }
            return Ok(_personService.Create(person));
        }

        // Mapeia solicitações PUT para https://localhost:{port}/api/person/
        // [FromBody] consome o objeto JSON enviado no corpo da solicitação
        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {

            if (person == null)
            {
                return BadRequest();
            }
            return Ok(_personService.Update(person));
        }


        // Mapeia solicitações DELETE para https://localhost:{port}/api/person/{id}
        // recebendo um ID como no Request Path
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);
            return NoContent();
        }


    }
}
