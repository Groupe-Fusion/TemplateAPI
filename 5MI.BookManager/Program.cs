using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Applicatif.UseCases;
using _5MI.BookManager.Domain.Repositories.Core;
using _5MI.BookManager.Persistence;
using _5MI.BookManager.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookManagerContext>(options =>
{
    var sqlConnectionString = builder.Configuration.GetConnectionString("Default");
    if(sqlConnectionString is null)
        throw new ArgumentNullException("No connection string found");

    options.UseSqlServer(
        sqlConnectionString,
        b => b.MigrationsAssembly("5MI.BookManager.Persistence"));
});
// inject repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

// inject use cases
builder.Services.AddTransient<IGetAllReservationsUseCase, GetAllReservationsUseCase>();
builder.Services.AddTransient<IGetReservationByIdUseCase, GetReservationByIdUseCase>();
builder.Services.AddTransient<IAddReservationUseCase, AddReservationUseCase>();
builder.Services.AddTransient<IDeleteReservationUseCase, DeleteReservationUseCase>();

// Add HttpClient service
builder.Services.AddHttpClient("BookService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ExternalServices:BookService:Url"]);
});

builder.Services.AddHttpClient("MemberService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ExternalServices:MemberService:Url"]);
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// auto migrate database
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetService<BookManagerContext>()!;
context.Database.Migrate();

app.Run();
