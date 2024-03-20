using System.Collections.Generic;
using System.Threading.Tasks;
using CamApi.Data.Entities;
using CamApi.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CamaraController : ControllerBase
    {
        private readonly IMongoCollection<Camara> _camaras;

        public CamaraController(MongoDbService mongoDbService)
        {
            _camaras = mongoDbService.Database?.GetCollection<Camara>("Camera");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Camara>>> Get()
        {
            var camaras = await _camaras.Find(_ => true).ToListAsync();
            return Ok(camaras);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Camara>> GetById(string id)
        {
            var camara = await _camaras.Find(camara => camara.Id == id).FirstOrDefaultAsync();
            if (camara == null)
            {
                return NotFound();
            }
            return Ok(camara);
        }

        [HttpPost]
        public async Task<ActionResult<Camara>> Post(Camara camara)
        {
            await _camaras.InsertOneAsync(camara);
            return CreatedAtAction(nameof(GetById), new { id = camara.Id }, camara);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, Camara camara)
        {
            if (id != camara.Id)
            {
                return BadRequest("ID de la cámara no coincide");
            }

            var filter = Builders<Camara>.Filter.Eq(x => x.Id, id);
            var result = await _camaras.ReplaceOneAsync(filter, camara);

            if (result.ModifiedCount == 0)
            {
                return NotFound("Cámara no encontrada o no se ha modificado");
            }

            return Ok("Cámara actualizada exitosamente");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _camaras.DeleteOneAsync(camara => camara.Id == id);

            if (result.DeletedCount == 0)
            {
                return NotFound("Cámara no encontrada");
            }

            return Ok("Cámara eliminada exitosamente");
        }
    }
}
