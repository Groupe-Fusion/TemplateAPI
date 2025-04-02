using _5MI.BookManager.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure; // Add this using directive

namespace _5MI.BookManager.Persistence.Seeding
{
    public class DbSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            Console.WriteLine("Initializing database");

            try
            {
                using var scope = serviceProvider.CreateScope();
                var scopedServices = scope.ServiceProvider;
                using UserManagerContext context = new UserManagerContext(scopedServices.GetRequiredService<DbContextOptions<UserManagerContext>>());
                Seed(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Seed(UserManagerContext context)
        {
            Console.WriteLine("trying to seed");
            if (context == null)
            {
                return;
            }

            if (!context.Database.IsInMemory())
            {
                Console.WriteLine("Using local database");
            }
            else
            {
                Console.WriteLine("Using in-memory database");
            }

            context.GenerateFakeUserData();
            Console.WriteLine("Seeded");
        }
    }

    public static class DatabaseFacadeExtensions
    {
        public static bool IsInMemory(this DatabaseFacade databaseFacade)
        {
            return databaseFacade.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        }
    }
}
