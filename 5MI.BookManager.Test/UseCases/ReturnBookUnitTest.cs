using _5MI.BookManager.Applicatif.UseCases;
using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Domain.Repositories.Core;
using AutoFixture;
using Moq;

namespace _5MI.BookManager.Test.UseCases
{
    public class ReturnBookUnitTest
    {
        [Fact]
        public async void Success()
        {
            var fixture = new Fixture();
            var book = fixture.Create<Book>();
            book.IsBorrowed = true; 

            var bookrepositoryMoq = new Mock<IBookRepository>();
            bookrepositoryMoq
                .Setup(x => x.GetBookByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);

            bookrepositoryMoq
                .Setup(x => x.UpdateBookAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Book b, CancellationToken ct) =>
                {
                    b.IsBorrowed = false; 
                    return b;
                });

            var returnBookUseCase = new ReturnBookUseCase(bookrepositoryMoq.Object);
            var returnedBook = await returnBookUseCase.ExecuteAsync(book.Id);

            Assert.Equal(book.Id, returnedBook.Id);
            Assert.False(returnedBook.IsBorrowed); 
        }

        [Fact]
        public async void Book_Already_In_Stock()
        {
            var fixture = new Fixture();
            var book = fixture.Create<Book>();
            book.IsBorrowed = false; 

            var bookrepositoryMoq = new Mock<IBookRepository>();
            bookrepositoryMoq
                .Setup(x => x.GetBookByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);

            var returnBookUseCase = new ReturnBookUseCase(bookrepositoryMoq.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await returnBookUseCase.ExecuteAsync(book.Id);
            });
        }
    }
}
