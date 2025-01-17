using _5MI.BookManager.Applicatif.UseCases;
using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Domain.Repositories.Core;
using AutoFixture;
using Moq;

namespace _5MI.BookManager.Test.UseCases
{
    public class BorrowBookUnitTest
    {
        [Fact]
        public async void Success()
        {
            var fixture = new Fixture();
            var book = fixture.Build<Book>()
                              .With(b => b.IsBorrowed, true)
                              .Create();

            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock
                .Setup(x => x.GetByIdAsync(book.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);

            var borrowBookUseCase = new BorrowBookUseCase(bookRepositoryMock.Object);

            var book = await borrowBookUseCase.ExecuteAsync(book.Id);

            Assert.False(book.IsBorrowed);
        }


        [Fact]
        public async void Book_Not_Available()
        {
            var fixture = new Fixture();
            var book = fixture.Build<Book>()
                              .With(b => b.IsBorrowed, false) 
                              .Create();

            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock
                .Setup(x => x.GetByIdAsync(book.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);

            var borrowBookUseCase = new BorrowBookUseCase(bookRepositoryMock.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await borrowBookUseCase.ExecuteAsync(book.Id);
            });
        }
    }
}
