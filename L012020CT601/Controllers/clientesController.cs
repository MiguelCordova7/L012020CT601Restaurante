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
        private readonly restauranteContext _restauranteContexto;

        public clientesController (restauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List <clientes> listadoClientes= (from e in _restauranteContexto.clientes
                                              select e).ToList();
            if (listadoClientes.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoClientes);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarCliente([FromBody] clientes cliente)
        {

            try
            {
                _restauranteContexto.clientes.Add(cliente);
                _restauranteContexto.SaveChanges();
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Actualizar/(id)")]

        public IActionResult Actualizarcliente(int id, [FromBody] clientes clienteModificar)
        {

            clientes? clienteActual = (from e in _restauranteContexto.clientes
                                     where e.clienteId == id
                                     select e).FirstOrDefault();


            if (clienteActual == null)
            { return NotFound(); }


            clienteActual.clienteId = clienteModificar.clienteId;
            clienteActual.nombreCliente = clienteModificar.nombreCliente;
            clienteActual.direccion = clienteModificar.direccion;
           
            _restauranteContexto.Entry(clienteActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(clienteModificar);
        }

        [HttpDelete]
        [Route("eliminar/(id)")]

        public IActionResult EliminarCliente(int id)
        {

            clientes? cliente = (from e in _restauranteContexto.clientes
                               where e.clienteId == id
                      select e).FirstOrDefault();

            

            if (cliente == null)
                return NotFound();

            _restauranteContexto.clientes.Attach(cliente);
            _restauranteContexto.clientes.Remove(cliente);
            _restauranteContexto.SaveChanges();
            return Ok(cliente);

        }

        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult FinByDescription(string filtro)
        {

            clientes? cliente = (from e in _restauranteContexto.clientes
                                 where e.direccion.Contains(filtro)
                                 select e).FirstOrDefault();

            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }
    }
}
