using _5MI.BookManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5MI.BookManager.Applicatif.Core
{
    public interface IDeleteReservationUseCase
    {
        Task<Reservation?> ExecuteAsync(int bookId, int memberId, CancellationToken ct = default);
    }
}
