using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _5MI.BookManager.Domain.Models.fusion;
using Bogus;

namespace _5MI.BookManager.Persistence.Seeding.Fakers
{
    public sealed class UserFaker : Faker<User>
    {
        public UserFaker()
        {
            //RuleFor(u => u.Id, f => f.UniqueIndex);
            RuleFor(u => u.FirstName, f => f.Person.FirstName);
            RuleFor(u => u.LastName, f => f.Person.LastName);
            RuleFor(u => u.Email, f => f.Person.Email);
            RuleFor(u => u.Password, f => f.Internet.Password());
        }
    }
}
