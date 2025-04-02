using _5MI.BookManager.Applicatif.Core.fusion;
using _5MI.BookManager.Domain.Models.fusion;
using Microsoft.AspNetCore.Mvc;

namespace _5MI.BookManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAddUserUseCase _addUserUseCase;
        private readonly IGetAllUsersUseCase _getAllUsersUseCase;
        private readonly IGetUserByIdUseCase _getUserByIdUseCase;

        public UserController(IAddUserUseCase addUserUseCase, IGetAllUsersUseCase getAllUsersUseCase, IGetUserByIdUseCase getUserByIdUseCase)
        {
            _addUserUseCase = addUserUseCase;
            _getAllUsersUseCase = getAllUsersUseCase;
            _getUserByIdUseCase = getUserByIdUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync(User newUser, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _addUserUseCase.ExecuteAsync(newUser, cancellationToken);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync(CancellationToken ct)
        {
            try
            {
                var users = await _getAllUsersUseCase.ExecuteAsync(ct);
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id, CancellationToken ct)
        {
            try
            {
                var user = await _getUserByIdUseCase.ExecuteAsync(id, ct);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
