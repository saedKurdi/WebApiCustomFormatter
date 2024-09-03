using aspLesson10WebApi.DTO;
using aspLesson10WebApi.Entities;
using Microsoft.AspNetCore.Mvc;
namespace WbApiDemo3_22_5.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    public static List<Player> Players { get; set; } = new List<Player>
    {
        new Player
        {
            Id = 1,
             City="Baku",
              PlayerName="Elvin",
               Score=100
        },
        new Player
        {
            Id = 2,
             City="Sumqayit",
              PlayerName="Arif",
               Score=75
        },
        new Player
        {
            Id = 3,
             City="Gence",
              PlayerName="Aysel",
               Score=83
        }
    };

    //GET
    //POST
    //PUT
    //DELETE
    //PATCH ?

    //https://localhost:7215/api/Player
    // GET: api/<PlayerController>
    [HttpGet]
    public IEnumerable<PlayerDTO> Get(string key = "")
    {
        IEnumerable<Player> items = Players;
        var keyWord = key.Trim();
        if (keyWord != "") items = Players.Where(p => p.PlayerName.ToLower().Contains(keyWord.ToLower()));
        var dtos = items.Select(p => new PlayerDTO
        {
            PlayerName = p.PlayerName,
            Score = p.Score,
            City = p.City
        });
        return dtos;
    }

    // GET: api/<PlayerController>
    [HttpGet("Bests")]
    public IEnumerable<PlayerDTO> BestPlayers()
    {
        var dtos = Players.Where(p => p.Score >= 80).Select(p => new PlayerDTO
        {
            PlayerName = p.PlayerName,
            Score = p.Score,
            City = p.City
        });
        return dtos;
    }

    // GET api/<PlayerController>/5
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var player = Players.FirstOrDefault(x => x.Id == id);
        if (player != null)
        {
            var dto = new PlayerDTO
            {
                City = player.City,
                Score = player.Score,
                PlayerName = player.PlayerName
            };
            return Ok(dto);
        }
        return NotFound();
    }

    // POST api/<PlayerController>
    [HttpPost]
    public IActionResult Post([FromBody] PlayerDTO dto) {
        if(dto.Score > 0)
        {
            var player = new Player
            {
                Id = (new Random()).Next(10,1000),
                PlayerName = dto.PlayerName,
                Score = dto.Score,
                City = dto.City,
            };
            Players.Add(player);
            return Ok(player);
        }
        return BadRequest("Score is not valid !");
    }

    // PUT api/<PlayerController>/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] PlayerDTO dto)
    {
        var player = Players.FirstOrDefault(p => p.Id == id);
        if (player != null)
        {
            player.PlayerName = dto.PlayerName;
            player.Score = dto.Score;
            player.City = dto.City;
            return Ok(player);
        }
        return NotFound();
    }

    // DELETE api/<PlayerController>/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var player = Players.FirstOrDefault(p => p.Id == id);
        if (player == null) return NotFound();
        Players.Remove(player);
        return NoContent();
    }
}