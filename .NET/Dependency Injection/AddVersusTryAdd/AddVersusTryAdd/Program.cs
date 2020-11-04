using Microsoft.Extensions.DependencyInjection;
using System;

namespace AddVersusTryAdd
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ServiceCollection serviceCollection = new ServiceCollection();
            // Add...

            // Mix AddSingleton und AddScoped -> selbes Interface registrieren und schauen, was bei GetService(s) kommt

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            IDisposable test = serviceProvider.GetService<IDisposable>();

            using (IServiceScope serviceScope = serviceProvider.CreateScope())
            {
                // Scoped Services
            }
        }
    }
}
