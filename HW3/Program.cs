using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCreation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Create();
        }

        private static async Task Create()
        {
            using (var context= new MoviesContext()) {
                bool created = await context.Database.EnsureCreatedAsync();
                Console.WriteLine(created);
                Console.ReadLine();
            }
        }
    }
}
