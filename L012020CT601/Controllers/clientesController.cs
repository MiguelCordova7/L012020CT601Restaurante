using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L012020CT601.Models;
using Microsoft.EntityFrameworkCore;


namespace L012020CT601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class clientesController : ControllerBase
    {
        private readonly clientesController _clientesController;

        public clientesController(clientesController clientesController)
        {
            _clientesController = clientesController;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List <clientes> listadoClientes= (from e in _clientesContexto._clientesController
                                              select e).ToList();
            if (listadoClientes.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoClientes);
        }
    }
}
