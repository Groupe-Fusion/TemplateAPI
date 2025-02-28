using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace _5MI.ReservationManager.Controllers
{
    public class ReservationController : ControllerBase
    {
        private readonly IAddReservationUseCase _addReservationUseCase;
        private readonly IGetAllReservationsUseCase _getAllReservationsUseCase;
        private readonly IGetReservationByIdUseCase _getReservationByIdUseCase;

        public ReservationController(
            IAddReservationUseCase addReservationUseCase,
            IGetAllReservationsUseCase getAllReservationsUseCase,
            IGetReservationByIdUseCase getReservationByIdUseCase)
        {
            _addReservationUseCase = addReservationUseCase;
            _getAllReservationsUseCase = getAllReservationsUseCase;
            _getReservationByIdUseCase = getReservationByIdUseCase;
        }

        /// <summary>
        /// Ajouter une nouvelle réservation.
        /// </summary>
        [HttpPost("add")]
        public async Task<IActionResult> AddReservation([FromBody] Reservation reservationn, CancellationToken ct)
        {
            try
            {
                var reservation = await _addReservationUseCase.ExecuteAsync(reservationn, ct);
                return CreatedAtAction(nameof(GetReservationById), new { reservationId = new Guid() }, reservation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"errror {ex.Message}");
            }
        }

        /// <summary>
        /// Récupérer toutes les réservations.
        /// </summary>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllReservations(CancellationToken ct)
        {
            try
            {
                var reservations = await _getAllReservationsUseCase.ExecuteAsync(ct);
                var reservationResponses = reservations.ToList();
                return Ok(reservationResponses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error {ex.Message}");
            }
        }

        /// <summary>
        /// Récupérer une réservation par son id.
        /// </summary>
        [HttpGet("{reservationId}")]
        public async Task<IActionResult> GetReservationById(int reservationId, CancellationToken ct)
        {
            try
            {
                var reservation = await _getReservationByIdUseCase.ExecuteAsync(reservationId, ct);
                return Ok(reservation);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"{reservationId}Not found");
            }
        }
    }
}
