using CamApi.Data.Entities;
using CamApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowerController : ControllerBase
    {
        private readonly IMongoCollection<Power> _powers;

        public PowerController(MongoDbService mongoDbService)
        {
            _powers = mongoDbService.Database?.GetCollection<Power>("Power");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Power>>> Get()
        {
            var powers = await _powers.Find(_ => true).ToListAsync();
            return Ok(powers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Power>> GetById(string id)
        {
            var power = await _powers.Find(power => power.Id == id).FirstOrDefaultAsync();
            if (power == null)
            {
                return NotFound();
            }
            return Ok(power);
        }

        [HttpPost]
        public async Task<ActionResult<Power>> Post(Power power)
        {
            await _powers.InsertOneAsync(power);
            return CreatedAtAction(nameof(GetById), new { id = power.Id }, power);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, Power power)
        {
            if (id != power.Id)
            {
                return BadRequest("ID del estado de energía no coincide");
            }

            var filter = Builders<Power>.Filter.Eq(x => x.Id, id);
            var result = await _powers.ReplaceOneAsync(filter, power);

            if (result.ModifiedCount == 0)
            {
                return NotFound("Estado de energía no encontrado o no se ha modificado");
            }

            return Ok("Estado de energía actualizado exitosamente");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _powers.DeleteOneAsync(power => power.Id == id);

            if (result.DeletedCount == 0)
            {
                return NotFound("Estado de energía no encontrado");
            }

            return Ok("Estado de energía eliminado exitosamente");
        }
    }
}