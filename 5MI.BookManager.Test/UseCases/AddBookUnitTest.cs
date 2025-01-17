using _5MI.BookManager.Applicatif.UseCases;
using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Domain.Repositories.Core;
using AutoFixture;
using Moq;

namespace _5MI.BookManager.Test.UseCases
{
    public class AddBookUnitTest
    {
        [Fact]
        public async void Success()
        {
            // créer les fictures
            var fixture = new Fixture();
            var book = fixture.Create<Book>();

            // setup le mock pour simuler le repository
            var bookrepositoryMoq = new Mock<IBookRepository>();
            bookrepositoryMoq
                .Setup(x => x.AddBookAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);

            // Test du usecase
            var addBookUseCase = new AddBookUseCase(bookrepositoryMoq.Object);
            var newBook = await addBookUseCase.ExecuteAsync(book);

            // Verification
            Assert.Equal(book.Id, newBook.Id);
        }

        [Fact]
        public async void No_Title_Set()
        {

            var fixture = new Fixture();
            var book = fixture.Build<Book>()
                              .With(b => b.Title, string.Empty)
                              .Create();

            var bookrepositoryMoq = new Mock<IBookRepository>();

            var addBookUseCase = new AddBookUseCase(bookrepositoryMoq.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await addBookUseCase.ExecuteAsync(book);
            });
        }
    }
}
