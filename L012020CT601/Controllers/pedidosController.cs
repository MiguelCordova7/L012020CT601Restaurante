using L012020CT601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L012020CT601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedidosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContexto;

        public pedidosController (restauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {

            List<pedidos> listadoPedidos = (from e in _restauranteContexto.pedidos
                                           select e).ToList();

            if (listadoPedidos.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoPedidos);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarPedido([FromBody] pedidos pedido)
        {

            try
            {
                _restauranteContexto.pedidos.Add(pedido);
                _restauranteContexto.SaveChanges();
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Actualizar/(id)")]

        public IActionResult ActualizarPedido(int id, [FromBody] pedidos pedidoModificar)
        {

            

            pedidos? pedidoActual = (from e in _restauranteContexto.pedidos
                                     where e.pedidoId == id
                                     select e).FirstOrDefault();


            if (pedidoActual == null)
            { return NotFound(); }


            pedidoActual.pedidoId = pedidoModificar.pedidoId;
            pedidoActual.motoristaId = pedidoModificar.motoristaId;
            pedidoActual.clienteId = pedidoModificar.clienteId;
            pedidoActual.platoId = pedidoModificar.platoId;
            pedidoActual.cantidad = pedidoModificar.cantidad;
            pedidoActual.precio = pedidoModificar.precio;

            _restauranteContexto.Entry(pedidoActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(pedidoModificar);
        }


        [HttpDelete]
        [Route("eliminar/(id)")]

        public IActionResult EliminarPedido(int id)
        {

            pedidos? pedido = (from e in _restauranteContexto.pedidos
                               where e.pedidoId == id
                      select e).FirstOrDefault();

            if (pedido == null)
                return NotFound();

            _restauranteContexto.pedidos.Attach(pedido);
            _restauranteContexto.pedidos.Remove(pedido);
            _restauranteContexto.SaveChanges();
            return Ok(pedido);

        }

        [HttpGet]
        [Route("Find/(id)")]

        public IActionResult FindByclienteId(int clienteId)
        {

            pedidos? pedido = (from e in _restauranteContexto.pedidos
                               where e.pedidoId == clienteId
                               select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpGet]
        [Route("Find/(id)")]

        public IActionResult FindBymotoristaId(int motoristaId)
        {

            pedidos? pedido = (from e in _restauranteContexto.pedidos
                               where e.pedidoId == motoristaId
                               select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }
    }
}
