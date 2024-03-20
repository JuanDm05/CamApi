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
    public class ServoLRController : ControllerBase
    {
        private readonly IMongoCollection<ServoLR> _servoLRs;

        public ServoLRController(MongoDbService mongoDbService)
        {
            _servoLRs = mongoDbService.Database?.GetCollection<ServoLR>("ServoLR");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServoLR>>> Get()
        {
            var servoLRs = await _servoLRs.Find(_ => true).ToListAsync();
            return Ok(servoLRs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServoLR>> GetById(string id)
        {
            var servoLR = await _servoLRs.Find(servoLR => servoLR.Id == id).FirstOrDefaultAsync();
            if (servoLR == null)
            {
                return NotFound();
            }
            return Ok(servoLR);
        }

        [HttpPost]
        public async Task<ActionResult<ServoLR>> Post(ServoLR servoLR)
        {
            await _servoLRs.InsertOneAsync(servoLR);
            return CreatedAtAction(nameof(GetById), new { id = servoLR.Id }, servoLR);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, ServoLR servoLR)
        {
            if (id != servoLR.Id)
            {
                return BadRequest("ID del ServoLR no coincide");
            }

            var filter = Builders<ServoLR>.Filter.Eq(x => x.Id, id);
            var result = await _servoLRs.ReplaceOneAsync(filter, servoLR);

            if (result.ModifiedCount == 0)
            {
                return NotFound("ServoLR no encontrado o no se ha modificado");
            }

            return Ok("ServoLR actualizado exitosamente");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _servoLRs.DeleteOneAsync(servoLR => servoLR.Id == id);

            if (result.DeletedCount == 0)
            {
                return NotFound("ServoLR no encontrado");
            }

            return Ok("ServoLR eliminado exitosamente");
        }
    }
}
