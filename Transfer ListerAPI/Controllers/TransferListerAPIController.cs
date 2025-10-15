using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using Transfer_ListerAPI.Data;
using Transfer_ListerAPI.Models;
using Transfer_ListerAPI.Models.DTOs;
using Transfer_ListerAPI.Repository.IRepository;

namespace Transfer_ListerAPI.Controllers
{
    [Route("/api/TransferListerAPI")]
    [ApiController]
    public class TransferListerAPIController : ControllerBase
    {
        private readonly IPlayerRepository _dbPlayer;
        private readonly IPositionRepository _dbPosition;
        private readonly IMapper _mapper;

        public TransferListerAPIController(IPlayerRepository dbPlayer, IPositionRepository dbPosition, IMapper mapper) 
        {
            _dbPlayer = dbPlayer;
            _dbPosition = dbPosition;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerDTO>>> GetPlayers()
        {

            var players = await _dbPlayer.GetAllAsync(includeEntities: "Positions");

            if (players.Count == 0)
            {
                return NoContent();
            }

            return Ok(_mapper.Map<List<PlayerDTO>>(players));
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PlayerDTO>> GetPlayer(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var player = await _dbPlayer.GetSingleAsync(filter: p => p.Id == id, includeEntities: "Positions");

            if (player == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PlayerDTO>(player));         
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dto.Price < 0)
            {
                ModelState.AddModelError(nameof(dto.Price), "Price can't be less than zero!");
                return BadRequest(ModelState);
            }

            if (dto.PositionIds == null || !dto.PositionIds.Any())
            {
                ModelState.AddModelError(nameof(dto.PositionIds), "Positions are reqired");
                return BadRequest(ModelState);
            }

            if (await _dbPlayer.GetSingleAsync(filter: p => p.FirstName == dto.FirstName && p.LastName == dto.LastName && p.BornDate == dto.BornDate) != null)
            {
                ModelState.AddModelError("PlayerExists", "This player is already on Transfer List!");
                return BadRequest(ModelState);
            }

            var player = _mapper.Map<Player>(dto);
            player.ListedDate = DateOnly.FromDateTime(DateTime.Now);

            var positions = await _dbPosition.GetAllAsync(filter: p => dto.PositionIds.Distinct().Contains(p.Id));


            if (!positions.Any())
            {
                ModelState.AddModelError(nameof(dto.PositionIds), "You must enter correct position ID");
                return BadRequest(ModelState);
            }

            player.Positions = positions;
            

            await _dbPlayer.Create(player);

            return CreatedAtAction(nameof(GetPlayer), new {id = player.Id}, dto);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var player = await _dbPlayer.GetSingleAsync(filter: p => p.Id == id);

            if (player == null)
            {
                return NotFound();
            }

            await _dbPlayer.Delete(player);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePlayer(int id, [FromBody] UpdatePlayerDTO dto)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dto.Price < 0)
            {
                ModelState.AddModelError(nameof(dto.Price), "Price can't be less than zero!");
                return BadRequest(ModelState);
            }    

            if (dto.PositionIds == null || !dto.PositionIds.Any())
            {
                ModelState.AddModelError(nameof(dto.PositionIds), "Positions are reqired");
                return BadRequest(ModelState);
            }

            var player = await _dbPlayer.GetSingleAsync(filter: p => p.Id == id, includeEntities: "Positions");

            if (player == null)
            {
                return NotFound();
            }

            var positions = await _dbPosition.GetAllAsync(filter: p => dto.PositionIds.Distinct().Contains(p.Id));

            if (!positions.Any())
            {
                ModelState.AddModelError(nameof(dto.PositionIds), "You must enter correct position ID");
                return BadRequest(ModelState);
            }

            player.FirstName = dto.FirstName;
            player.LastName = dto.LastName;
            player.BornDate = dto.BornDate;
            player.Price = dto.Price;
            player.Positions = positions;

            await _dbPlayer.SaveAsync();

            return Ok(dto);
        }
    }
}
