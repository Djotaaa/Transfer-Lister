using Microsoft.AspNetCore.Mvc;
using Transfer_ListerWebApp.Models.DTO;
using Transfer_ListerWebApp.Services;

namespace Transfer_ListerWebApp.Controllers
{
    public class TransferListerWebAppController : Controller
    {
        private readonly IPlayerService _playerService;

        public TransferListerWebAppController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        public async Task<IActionResult> Index()
        {
            var players = await _playerService.GetPlayersAsync();
            List<PlayerDTO> list = players.ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePlayerDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.LastName) || string.IsNullOrWhiteSpace(dto.PositionsInput))
            {
                TempData["ErrorDTO"] = "You must enter every value!!!";
                return View(dto);
            }

            dto.PositionIds = dto.PositionsInput.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();

            var result = await _playerService.CreatePlayerAsync(dto);

            if (result.IsSuccess)
            {
                TempData["Success"] = "Player has been created!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorDTO"] = result.ErrorMessage ?? "Unknown error with DTO model!!!";
                return View(dto);
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            var player = await _playerService.GetSingleAsync(id);

            if (player == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var dto = new UpdatePlayerDTO
            {
                FirstName = player.FirstName,
                LastName = player.LastName,
                PositionsInput = string.Join(", ", player.Positions.Select(p => p.Id).ToList()),
                BornDate = player.BornDate,
                Price = player.Price,
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdatePlayerDTO dto)
        {
            var player = _playerService.GetSingleAsync(id);

            if (player == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.LastName) || string.IsNullOrWhiteSpace(dto.PositionsInput))
            {
                TempData["ErrorDTO"] = "You must enter every value!!!";
                return View(dto);
            }                    

            dto.PositionIds = dto.PositionsInput.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();

            var result = await _playerService.UpdatePlayerAsync(id, dto);

            if (result.IsSuccess)
            {
                TempData["Success"] = "Player has been updated!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorDTO"] = result.ErrorMessage ?? "Unknown error with DTO model!!!";
                return View(dto);
            } 
                

            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var player = await _playerService.GetSingleAsync(id);

            if (player == null)
            {
                return RedirectToAction(nameof(Index));
            }

            bool isDeleted = await _playerService.DeletePlayerAsync(id);

            if (isDeleted)
            {
                TempData["Success"] = "You deleted player successfully!";
            }
            else
            {
                TempData["Error"] = "Error! Can't delete player!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
