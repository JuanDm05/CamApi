using CamApi.Data.Entities;
using CamApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;


namespace User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMongoCollection<Users> _users;
        public UsersController(MongoDbService mongoDbService)
        {
            _users = mongoDbService.Database?.GetCollection<Users>("Users");
        }
        [HttpGet]
        public async Task<IEnumerable<Users>> get()
        {
            return await _users.Find(FilterDefinition<Users>.Empty).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetById(string id)
        {
            var filter = Builders<Users>.Filter.Eq(x => x.Id, id);
            var customer = _users.Find(filter).FirstOrDefault();
            return customer is not null ? Ok(customer) : NotFound();

        }
        [HttpPost]

        public async Task<ActionResult> Post(Users customer)
        {
            await _users.InsertOneAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }
        [HttpPut]
        public async Task<ActionResult> Create(Users customer)
        {
            var filter = Builders<Users>.Filter.Eq(x => x.Id, customer.Id);

            await _users.ReplaceOneAsync(filter, customer);
            return Ok();

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var filter = Builders<Users>.Filter.Eq(x => x.Id, id);
            await _users.DeleteOneAsync(filter);
            return Ok();
        }

    }
}
