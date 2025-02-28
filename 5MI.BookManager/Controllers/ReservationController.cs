using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

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
        public async Task<IActionResult> AddReservation([FromBody] Reservation reservation, CancellationToken ct)
        {
            try
            {
                // Vérifie que le modèle n'est pas null
                if (reservation == null)
                {
                    return BadRequest("Reservation is null.");
                }

                var newReservation = await _addReservationUseCase.ExecuteAsync(reservation, ct);
                return Ok(newReservation);
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
        [HttpGet("{bookId}/{memberId}")]
        public async Task<IActionResult> GetReservationById(int bookId, int memberId, CancellationToken ct)
        {
            try
            {
                var reservation = await _getReservationByIdUseCase.ExecuteAsync(bookId, memberId, ct);
                return Ok(reservation);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"reservation Not found");
            }
        }

        [HttpDelete("{bookId}/{memberId}")]
        public async Task<IActionResult> DeleteReservation(int bookId, int memberId, CancellationToken ct)
        {
            try
            {
                var reservation = await _deleteReservationUseCase.ExecuteAsync(bookId, memberId, ct);
                if (reservation is null)
                    return NotFound("Reservation not found");

                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
