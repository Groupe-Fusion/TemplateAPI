using Microsoft.AspNetCore.Mvc;
using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Mapper;
using _5MI.BookManager.DTO.Requests;
using _5MI.BookManager.Applicatif.Exceptions;

namespace _5MI.BookManager.Presentation.Controllers
{
    [ApiController]
    [Route("api/members")]
    public class MemberController : ControllerBase
    {
        private readonly ILogger<MemberController> _logger;

        private readonly IGetAllMembersUseCase _getAllMembersUseCase;
        private readonly IGetMemberByIdUseCase _getMemberByIdUseCase;
        private readonly IAddMemberUseCase _addMemberUseCase;
        private readonly IUpdateMemberUseCase _updateMemberUseCase;
        private readonly IDeleteMemberUseCase _deleteMemberUseCase;

        public MemberController(
            ILogger<MemberController> logger,
            IGetAllMembersUseCase getAllMembersUseCase,
            IGetMemberByIdUseCase getMemberByIdUseCase,
            IAddMemberUseCase addMemberUseCase,
            IUpdateMemberUseCase updateMemberUseCase,
            IDeleteMemberUseCase deleteMemberUseCase)
        {
            _logger = logger;

            _getAllMembersUseCase = getAllMembersUseCase;
            _getMemberByIdUseCase = getMemberByIdUseCase;
            _addMemberUseCase = addMemberUseCase;
            _updateMemberUseCase = updateMemberUseCase;
            _deleteMemberUseCase = deleteMemberUseCase;
        }

        /// <summary>
        /// Ajouter un nouveau membre.
        /// </summary>
        [HttpPost("")]
        public async Task<IActionResult> AddMember([FromBody] MemberRequest memberRequest, CancellationToken ct = default)
        {
            try
            {
                var member = MemberMapper.ToEntity(memberRequest);
                var book = await _addMemberUseCase.ExecuteAsync(member, ct);
                _logger.LogInformation("New member \"{lastname} {firstname}\" created with id {id}",member.LastName, member.FirstName,  member.Id);
                return CreatedAtAction(nameof(GetMemberById), new { memberId = member.Id }, MemberMapper.ToResponse(member));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"errror {ex.Message}");
            }
        }

        /// <summary>
        /// Récupérer tous les membres.
        /// </summary>
        [HttpGet("")]
        public async Task<IActionResult> GetAllMembers(CancellationToken ct = default)
        {
            try
            {
                var members = await _getAllMembersUseCase.ExecuteAsync(ct);
                var membersResponses = members.Select(MemberMapper.ToResponse).ToList();
                return Ok(membersResponses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error {ex.Message}");
            }
        }

        /// <summary>
        /// Récupérer un membre par son ID.
        /// </summary>
        [HttpGet("{memberId}")]
        public async Task<IActionResult> GetMemberById(int memberId, CancellationToken ct = default)
        {
            try
            {
                var member = await _getMemberByIdUseCase.ExecuteAsync(memberId, ct);
                var memberResponse = MemberMapper.ToResponse(member);
                return Ok(memberResponse);
            }
            catch (ItemNotFoundException<Member> e)
            {
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error {ex.Message}");
            }
        }

        /// <summary>
        /// Modifie un membre par son ID.
        /// </summary>
        [HttpPut("{memberId}")]
        public async Task<IActionResult> UpdateMemberById(int memberId, [FromBody] MemberRequest memberRequest, CancellationToken ct = default)
        {
            try
            {
                var raw = MemberMapper.ToEntity(memberRequest);

                var member = await _updateMemberUseCase.ExecuteAsync(memberId, raw, ct);
                _logger.LogInformation("Update member \"{lastname} {firstname}\" with id {id}", member.LastName, member.FirstName, member.Id);
                return Ok(MemberMapper.ToResponse(member));
            }
            catch (ItemNotFoundException<Member> e)
            {
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error {ex.Message}");
            }
        }

        /// <summary>
        /// Supprime un membre par son ID.
        /// </summary>
        [HttpDelete("{memberId}")]
        public async Task<IActionResult> DeleteMemberById(int memberId, CancellationToken ct = default)
        {
            try
            {
                var member = await _deleteMemberUseCase.ExecuteAsync(memberId, ct);
                _logger.LogInformation("Remove member \"{lastname} {firstname}\" with id {id}", member.LastName, member.FirstName, member.Id);
                return Ok(MemberMapper.ToResponse(member));
            }
            catch (ItemNotFoundException<Member> e)
            {
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error {ex.Message}");
            }
        }
    }
}
