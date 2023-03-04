using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L012020CT601.Models;

namespace L012020CT601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContexto;

        public platosController (restauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto;
        }


        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {

            List<platos> listadoPlatos = (from e in _restauranteContexto.platos
                                           select e).ToList();

            if (listadoPlatos.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoPlatos);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarPlatos([FromBody] platos plato)
        {

            try
            {
                _restauranteContexto.platos.Add(plato);
                _restauranteContexto.SaveChanges();
                return Ok(plato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Actualizar/(id)")]

        public IActionResult ActualizarPlatos(int id, [FromBody] platos platoModificar)
        {

            platos? platoActual = (from e in _restauranteContexto.platos
                                     where e.platoId == id
                                     select e).FirstOrDefault();

            if (platoActual == null)
            { return NotFound(); }

            platoActual.platoId = platoModificar.platoId;
            platoActual.nombrePlato = platoModificar.nombrePlato;
            platoActual.precio = platoModificar.precio;


            _restauranteContexto.Entry(platoActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(platoModificar);
        }


        [HttpDelete]
        [Route("eliminar/(id)")]

        public IActionResult EliminarPlato(int id)
        {

            platos? plato = (from e in _restauranteContexto.platos
                               where e.platoId == id
                      select e).FirstOrDefault();

            

            if (plato == null)
                return NotFound();

            _restauranteContexto.platos.Attach(plato);
            _restauranteContexto.platos.Remove(plato);
            _restauranteContexto.SaveChanges();
            return Ok(plato);

        }

        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult FinByDescription(string filtro)
        {

            platos? plato = (from e in _restauranteContexto.platos
                                 where e.nombrePlato.Contains(filtro)
                                 select e).FirstOrDefault();

            if (plato == null)
            {
                return NotFound();
            }
            return Ok(plato);
        }
    }
}
