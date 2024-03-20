using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CamApi.Data;
using CamApi.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly IMongoCollection<Log> _logCollection;

        public LogController(MongoDbService mongoDbService)
        {
            _logCollection = mongoDbService.Database?.GetCollection<Log>("Log");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log>>> Get()
        {
            try
            {
                var logs = await _logCollection.Find(_ => true).ToListAsync();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener los logs: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Log>> GetById(string id)
        {
            try
            {
                var log = await _logCollection.Find(log => log.Id == id).FirstOrDefaultAsync();
                if (log == null)
                {
                    return NotFound();
                }
                return Ok(log);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener el log: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Log>> Post(Log log)
        {
            try
            {
                await _logCollection.InsertOneAsync(log);
                return CreatedAtAction(nameof(GetById), new { id = log.Id }, log);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al crear el log: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, Log log)
        {
            try
            {
                if (id != log.Id)
                {
                    return BadRequest("ID del log no coincide");
                }

                var filter = Builders<Log>.Filter.Eq(x => x.Id, id);
                var result = await _logCollection.ReplaceOneAsync(filter, log);

                if (result.ModifiedCount == 0)
                {
                    return NotFound("Log no encontrado o no se ha modificado");
                }

                return Ok("Log actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar el log: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _logCollection.DeleteOneAsync(log => log.Id == id);

                if (result.DeletedCount == 0)
                {
                    return NotFound("Log no encontrado");
                }

                return Ok("Log eliminado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al eliminar el log: {ex.Message}");
            }
        }
    }
}
