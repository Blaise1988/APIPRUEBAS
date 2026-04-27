using APIPRUEBAS.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace APIPRUEBAS.Controllers
{

    [EnableCors("ReglasCors")]
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductoController : Controller
    {

        public readonly DbapiContext _dbcontext;

        public ProductoController(DbapiContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("api/Producto/Lista")]
        public IActionResult Listar()
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                lista = _dbcontext.Productos.Include(c=>c.oCategoria).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response= lista });
            
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }

        }


        [HttpGet]
        [Route("api/Producto/Obtener/{idProducto:int}")]
        public IActionResult Obtener(int idProducto)
        {
            Producto oProducto = _dbcontext.Productos.Find(idProducto);

            List<Producto> lista = new List<Producto>();
            try
            {

                if (oProducto == null) return BadRequest("Producto no encontrado.");

                oProducto = _dbcontext.Productos.Include(c => c.oCategoria).Where(p=>p.IdProducto== idProducto).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oProducto });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = oProducto });
            }

        }


        [HttpPut]
        [Route("api/Producto/Editar")]
        public IActionResult Editar([FromBody] Producto objeto)
        {


            Producto oProducto = _dbcontext.Productos.Find(objeto.IdProducto);


            if (oProducto == null) return BadRequest("Producto no encontrado.");

            oProducto.CodigoBarra = objeto.CodigoBarra is null? oProducto.CodigoBarra :objeto.CodigoBarra;
            oProducto.Descripcion = objeto.Descripcion is null ? oProducto.Descripcion : objeto.Descripcion;
            oProducto.Marca = objeto.Marca is null ? oProducto.Marca : objeto.Marca;
            oProducto.IdCategoria = objeto.IdCategoria is null ? oProducto.IdCategoria : objeto.IdCategoria;
            oProducto.Precio = objeto.Precio is null ? oProducto.Precio : objeto.Precio;


            _dbcontext.Productos.Update(oProducto);
            _dbcontext.SaveChanges();

            try
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok"});

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message});
            }

        }

        [HttpDelete]
        [Route("api/Producto/Eliminar/{idProducto:int}")]
        public IActionResult Eliminar(int idProducto)
        {
             Producto oProducto = _dbcontext.Productos.Find(idProducto);

            _dbcontext.Productos.Remove(oProducto);
            _dbcontext.SaveChanges();

            try
            {

                if (oProducto == null) return BadRequest("Producto no encontrado.");

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message});
            }

        }
    }
}
