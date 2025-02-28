using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.DTO.Requests;
using _5MI.BookManager.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace _5MI.ReservationManager.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly IAddReservationUseCase _addReservationUseCase;
        private readonly IGetAllReservationsUseCase _getAllReservationsUseCase;
        private readonly IGetReservationByIdUseCase _getReservationByIdUseCase;
        private readonly IDeleteReservationUseCase _deleteReservationUseCase;

        public ReservationController(
            IAddReservationUseCase addReservationUseCase,
            IGetAllReservationsUseCase getAllReservationsUseCase,
            IGetReservationByIdUseCase getReservationByIdUseCase,
            IDeleteReservationUseCase deleteReservationUseCase)
        {
            _addReservationUseCase = addReservationUseCase;
            _getAllReservationsUseCase = getAllReservationsUseCase;
            _getReservationByIdUseCase = getReservationByIdUseCase;
            _deleteReservationUseCase = deleteReservationUseCase;
        }

        /// <summary>
        /// Ajouter une nouvelle réservation.
        /// </summary>
        [HttpPost()]
        public async Task<IActionResult> AddReservation([FromBody] ReservationRequest reservationRequest, CancellationToken ct)
        {
            try
            {
                var reservation = await _addReservationUseCase.ExecuteAsync(ReservationMapper.ToEntity(reservationRequest), ct);
                return Ok(ReservationMapper.ToResponse(reservation));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Récupérer toutes les réservations.
        /// </summary>
        [HttpGet()]
        public async Task<IActionResult> GetAllReservations(CancellationToken ct)
        {
            try
            {
                var reservations = await _getAllReservationsUseCase.ExecuteAsync(ct);
                return Ok(reservations.Select(ReservationMapper.ToResponse));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error {ex.Message}");
            }
        }

        /// <summary>
        /// Récupérer une réservation par son id.
        /// </summary>
        [HttpGet("{bookId}/{memberId}")]
        public async Task<IActionResult> GetReservationById(int bookId, int memberId, CancellationToken ct)
        {
            try
            {
                var reservation = await _getReservationByIdUseCase.ExecuteAsync(bookId, memberId, ct);
                return Ok(ReservationMapper.ToResponse(reservation));
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"reservation Not found");
            }
        }

        /// <summary>
        /// Supprime une réservation par son id.
        /// </summary>
        [HttpDelete("{bookId}/{memberId}")]
        public async Task<IActionResult> DeleteReservation(int bookId, int memberId, CancellationToken ct)
        {
            try
            {
                var reservation = await _deleteReservationUseCase.ExecuteAsync(bookId, memberId, ct);
                if (reservation is null)
                    return NotFound("Reservation not found");

                return Ok(ReservationMapper.ToResponse(reservation));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
