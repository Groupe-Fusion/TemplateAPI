using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Applicatif.Core.fusion;
using _5MI.BookManager.Applicatif.UseCases;
using _5MI.BookManager.Applicatif.UseCases.fusion;
using _5MI.BookManager.Domain.Repositories.Core;
using _5MI.BookManager.Domain.Repositories.Core.fusion;
using _5MI.BookManager.Persistence;
using _5MI.BookManager.Persistence.Repositories;
using _5MI.BookManager.Persistence.Repositories.fusion;
using _5MI.BookManager.Persistence.Seeding;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

void ConfigureDbContext<TContext>(IServiceCollection services, IConfiguration configuration) where TContext : DbContext
{
    var sqlConnectionString = configuration.GetConnectionString("Default");
    if (sqlConnectionString is null)
        throw new ArgumentNullException("No connection string found");

    services.AddDbContext<TContext>(options =>
    {
        options.UseSqlServer(
            sqlConnectionString,
            b => b.MigrationsAssembly("5MI.BookManager.Persistence"));
    });
}

ConfigureDbContext<BookManagerContext>(builder.Services, builder.Configuration);
ConfigureDbContext<UserManagerContext>(builder.Services, builder.Configuration);

// inject repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

// fusion inject repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// inject use cases
builder.Services.AddTransient<IGetAllBooksUseCase, GetAllBooksUseCase>();
builder.Services.AddTransient<IGetBookByIdUseCase, GetBookByIdUseCase>();
builder.Services.AddTransient<IAddBookUseCase, AddBookUseCase>();

// fusion inject use cases
builder.Services.AddTransient<IAddUserUseCase, AddUserUseCase>();
builder.Services.AddTransient<IGetAllUsersUseCase, GetAllUsersUseCase>();
builder.Services.AddTransient<IGetUserByIdUseCase, GetUserByIdUseCase>();

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
//context.Database.Migrate();

void MigrateAndSeedDatabase<TContext>(IServiceProvider services) where TContext : DbContext
{
    using var scope = services.CreateScope();
    var context = scope.ServiceProvider.GetService<TContext>()!;
    context.Database.Migrate();

    if (typeof(TContext) == typeof(UserManagerContext))
    {
        Console.WriteLine("Pre seeding database");
        DbSeeder.Initialize(scope.ServiceProvider);
    }
}

// auto migrate and seed database
//MigrateAndSeedDatabase<BookManagerContext>(app.Services);
MigrateAndSeedDatabase<UserManagerContext>(app.Services);

//connection string
//Console.WriteLine(app.Configuration.GetConnectionString("Default"));

app.Run();
