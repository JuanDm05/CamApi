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
    public class ServoUDController : ControllerBase
    {
        private readonly IMongoCollection<ServoUD> _servoUDs;

        public ServoUDController(MongoDbService mongoDbService)
        {
            _servoUDs = mongoDbService.Database?.GetCollection<ServoUD>("ServoUD");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServoUD>>> Get()
        {
            var servoUDs = await _servoUDs.Find(_ => true).ToListAsync();
            return Ok(servoUDs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServoUD>> GetById(string id)
        {
            var servoUD = await _servoUDs.Find(servoUD => servoUD.Id == id).FirstOrDefaultAsync();
            if (servoUD == null)
            {
                return NotFound();
            }
            return Ok(servoUD);
        }

        [HttpPost]
        public async Task<ActionResult<ServoUD>> Post(ServoUD servoUD)
        {
            await _servoUDs.InsertOneAsync(servoUD);
            return CreatedAtAction(nameof(GetById), new { id = servoUD.Id }, servoUD);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, ServoUD servoUD)
        {
            if (id != servoUD.Id)
            {
                return BadRequest("ID del ServoUD no coincide");
            }

            var filter = Builders<ServoUD>.Filter.Eq(x => x.Id, id);
            var result = await _servoUDs.ReplaceOneAsync(filter, servoUD);

            if (result.ModifiedCount == 0)
            {
                return NotFound("ServoUD no encontrado o no se ha modificado");
            }

            return Ok("ServoUD actualizado exitosamente");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _servoUDs.DeleteOneAsync(servoUD => servoUD.Id == id);

            if (result.DeletedCount == 0)
            {
                return NotFound("ServoUD no encontrado");
            }

            return Ok("ServoUD eliminado exitosamente");
        }
    }
}
