using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;

namespace AddVersusTryAdd
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AddVersusTryAdd - Dependency Injection Sample");
            Console.WriteLine(string.Empty.PadRight(Console.WindowWidth, '-'));

            Console.WriteLine("Example 1: AddSingleton");
            Example1_AddSingleton();
            Console.WriteLine();

            Console.WriteLine("Example 2: TryAddSingleton");
            Example2_TryAddSingleton();
            Console.WriteLine();

            Console.WriteLine("Example 3: Replace");
            Example3_Replace();
            Console.WriteLine();

            Console.WriteLine("Example 4: Mixing AddSingleton and AddScoped");
            Example4_MixingAddSingletonAndAddScoped();
            Console.WriteLine();
        }

        /// <summary>
        /// Example #1: AddSingleton
        /// </summary>
        /// <remarks>
        /// This example adds several implementations of the same interface to the <see cref="ServiceCollection"/>
        /// using <see cref="ServiceCollectionServiceExtensions.AddSingleton{TService, TImplementation}(IServiceCollection)"/>.
        /// After that it first retrieves a single instance using <see cref="ServiceProviderServiceExtensions.GetService{T}(IServiceProvider)"/> and then
        /// an instance list using <see cref="ServiceProviderServiceExtensions.GetServices{T}(IServiceProvider)"/>.
        /// The name of the implementation of each instance is then printed to the console.
        /// </remarks>
        private static void Example1_AddSingleton()
        {
            // Create new ServiceCollection
            ServiceCollection serviceCollection = new ServiceCollection();

            // Add some implementations
            Console.WriteLine("\tAdding singletons using AddSingleton ...");
            Console.WriteLine($"\t\t- {nameof(IExample)} -> {nameof(ExampleImplementation1)}");
            serviceCollection.AddSingleton<IExample, ExampleImplementation1>();
            Console.WriteLine($"\t\t- {nameof(IExample)} -> {nameof(ExampleImplementation2)}");
            serviceCollection.AddSingleton<IExample, ExampleImplementation2>();
            Console.WriteLine($"\t\t- {nameof(IExample)} -> {nameof(ExampleImplementation2)}");
            serviceCollection.AddSingleton<IExample, ExampleImplementation2>();
            Console.WriteLine($"\t\t- {nameof(IExample)} -> {nameof(ExampleImplementation3)}");
            serviceCollection.AddSingleton<IExample, ExampleImplementation3>();

            // Build the ServiceProvider
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            // GetService<IExample> returns the last implementation added
            IExample example = serviceProvider.GetService<IExample>();
            Console.WriteLine($"\tGetService<{nameof(IExample)}> returned '{example.ImplementationName}'.");

            // GetServices<IExample> returns an instance of every implementation added
            // Note that two different instances of ExampleImplementation2 are returned.
            IEnumerable<IExample> examples = serviceProvider.GetServices<IExample>();
            Console.WriteLine($"\tGetServices<{nameof(IExample)}> returned");
            foreach (IExample examplesInstance in examples)
            {
                Console.WriteLine($"\t\t'{examplesInstance.ImplementationName}'");
            }
        }

        /// <summary>
        /// Example #2: TryAddSingleton
        /// </summary>
        /// <remarks>
        /// This example adds several implementations of the same interface to the <see cref="ServiceCollection"/>
        /// using <see cref="ServiceCollectionDescriptorExtensions.TryAddSingleton{TService, TImplementation}(IServiceCollection)"/>.
        /// After that it first retrieves a single instance using <see cref="ServiceProviderServiceExtensions.GetService{T}(IServiceProvider)"/> and then
        /// an instance list using <see cref="ServiceProviderServiceExtensions.GetServices{T}(IServiceProvider)"/>.
        /// The name of the implementation of each instance is then printed to the console.
        /// </remarks>
        private static void Example2_TryAddSingleton()
        {
            // Create new ServiceCollection
            ServiceCollection serviceCollection = new ServiceCollection();

            // Add some implementations
            Console.WriteLine("\tAdding singletons using TryAddSingleton ...");
            Console.WriteLine($"\t\t- {nameof(IExample)} -> {nameof(ExampleImplementation1)}");
            serviceCollection.TryAddSingleton<IExample, ExampleImplementation1>();
            Console.WriteLine($"\t\t- {nameof(IExample)} -> {nameof(ExampleImplementation2)}");
            serviceCollection.TryAddSingleton<IExample, ExampleImplementation2>();
            Console.WriteLine($"\t\t- {nameof(IExample)} -> {nameof(ExampleImplementation2)}");
            serviceCollection.TryAddSingleton<IExample, ExampleImplementation2>();
            Console.WriteLine($"\t\t- {nameof(IExample)} -> {nameof(ExampleImplementation3)}");
            serviceCollection.TryAddSingleton<IExample, ExampleImplementation3>();

            // Build the ServiceProvider
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            // GetService<IExample> returns the first implementation added
            IExample example = serviceProvider.GetService<IExample>();
            Console.WriteLine($"\tGetService<{nameof(IExample)}> returned '{example.ImplementationName}'.");

            // GetServices<IExample> returns an instance of only the first implementation added
            IEnumerable<IExample> examples = serviceProvider.GetServices<IExample>();
            Console.WriteLine($"\tGetServices<{nameof(IExample)}> returned");
            foreach (IExample examplesInstance in examples)
            {
                Console.WriteLine($"\t\t'{examplesInstance.ImplementationName}'");
            }
        }

        /// <summary>
        /// Example #3: Replace
        /// </summary>
        /// <remarks>
        /// This example adds several implementations of the same interface to the <see cref="ServiceCollection"/>
        /// using <see cref="ServiceCollectionDescriptorExtensions.Replace(IServiceCollection, ServiceDescriptor)"/>.
        /// After that it first retrieves a single instance using <see cref="ServiceProviderServiceExtensions.GetService{T}(IServiceProvider)"/> and then
        /// an instance list using <see cref="ServiceProviderServiceExtensions.GetServices{T}(IServiceProvider)"/>.
        /// The name of the implementation of each instance is then printed to the console.
        /// </remarks>
        private static void Example3_Replace()
        {
            // Create new ServiceCollection
            ServiceCollection serviceCollection = new ServiceCollection();

            // Add some implementations
            Console.WriteLine("\tAdding singletons using Replace ...");
            Console.WriteLine($"\t\t- {nameof(IExample)} -> {nameof(ExampleImplementation1)}");
            serviceCollection.Replace(new ServiceDescriptor(typeof(IExample), typeof(ExampleImplementation1), ServiceLifetime.Singleton));
            Console.WriteLine($"\t\t- {nameof(IExample)} -> {nameof(ExampleImplementation2)}");
            serviceCollection.Replace(new ServiceDescriptor(typeof(IExample), typeof(ExampleImplementation2), ServiceLifetime.Singleton));
            Console.WriteLine($"\t\t- {nameof(IExample)} -> {nameof(ExampleImplementation2)}");
            serviceCollection.Replace(new ServiceDescriptor(typeof(IExample), typeof(ExampleImplementation2), ServiceLifetime.Singleton));
            Console.WriteLine($"\t\t- {nameof(IExample)} -> {nameof(ExampleImplementation3)}");
            serviceCollection.Replace(new ServiceDescriptor(typeof(IExample), typeof(ExampleImplementation3), ServiceLifetime.Singleton));

            // Build the ServiceProvider
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            // GetService<IExample> returns the last implementation added
            IExample example = serviceProvider.GetService<IExample>();
            Console.WriteLine($"\tGetService<{nameof(IExample)}> returned '{example.ImplementationName}'.");

            // GetServices<IExample> returns an instance of only the last implementation added
            IEnumerable<IExample> examples = serviceProvider.GetServices<IExample>();
            Console.WriteLine($"\tGetServices<{nameof(IExample)}> returned");
            foreach (IExample examplesInstance in examples)
            {
                Console.WriteLine($"\t\t'{examplesInstance.ImplementationName}'");
            }
        }

        /// <summary>
        /// Example #4: Mixing AddSingleton and AddScoped on the same type
        /// </summary>
        /// <remarks>
        /// This example adds several implementations of the same interface to the <see cref="ServiceCollection"/>
        /// by either using <see cref="ServiceCollectionServiceExtensions.AddSingleton{TService, TImplementation}(IServiceCollection)"/>
        /// or <see cref="ServiceCollectionServiceExtensions.AddScoped{TService, TImplementation}(IServiceCollection)"/>.
        /// After that it first retrieves a single instance using <see cref="ServiceProviderServiceExtensions.GetService{T}(IServiceProvider)"/> and then
        /// an instance list using <see cref="ServiceProviderServiceExtensions.GetServices{T}(IServiceProvider)"/> once outside and once inside a scope.
        /// The name of the implementation of each instance is then printed to the console.
        /// </remarks>
        private static void Example4_MixingAddSingletonAndAddScoped()
        {
            // Create new ServiceCollection
            ServiceCollection serviceCollection = new ServiceCollection();

            // Add some implementations
            Console.WriteLine("\tAdding singletons using AddSingleton ...");
            Console.WriteLine($"\t\t- {nameof(IExample)} -> {nameof(ExampleImplementation1)}");
            serviceCollection.AddSingleton<IExample, ExampleImplementation1>();
            Console.WriteLine($"\t\t- {nameof(IExample)} -> {nameof(ExampleImplementation2)}");
            serviceCollection.AddSingleton<IExample, ExampleImplementation2>();
            Console.WriteLine($"\t\t- {nameof(IExample)} -> {nameof(ExampleImplementation3)}");
            serviceCollection.AddSingleton<IExample, ExampleImplementation3>();

            Console.WriteLine("\tAdding scoped instances using AddScoped ...");
            Console.WriteLine($"\t\t- {nameof(IExample)} -> {nameof(ExampleImplementation1)}");
            serviceCollection.AddScoped<IExample, ExampleImplementation1>();
            Console.WriteLine($"\t\t- {nameof(IExample)} -> {nameof(ExampleImplementation2)}");
            serviceCollection.AddScoped<IExample, ExampleImplementation2>();
            Console.WriteLine($"\t\t- {nameof(IExample)} -> {nameof(ExampleImplementation3)}");
            serviceCollection.AddScoped<IExample, ExampleImplementation3>();

            // Build the ServiceProvider
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            // GetService<IExample> returns the last implementation added, event it was added using AddScoped
            IExample example = serviceProvider.GetService<IExample>();
            Console.WriteLine($"\tGetService<{nameof(IExample)}> returned '{example.ImplementationName}'.");

            // GetServices<IExample> returns an instance of every implementation added, no matter if singleton or scoped
            IEnumerable<IExample> examples = serviceProvider.GetServices<IExample>();
            Console.WriteLine($"\tGetServices<{nameof(IExample)}> returned");
            foreach (IExample examplesInstance in examples)
            {
                Console.WriteLine($"\t\t'{examplesInstance.ImplementationName}'");
            }

            // Create a scope
            Console.WriteLine("\tEntering a scope ...");
            using (IServiceScope serviceScope = serviceProvider.CreateScope())
            {
                // GetService<IExample> returns the last implementation added
                IExample scopedExample = serviceScope.ServiceProvider.GetService<IExample>();
                Console.WriteLine($"\t\tGetService<{nameof(IExample)}> returned '{scopedExample.ImplementationName}'.");

                // GetServices<IExample> returns an instance of every implementation added, no matter if singleton or scoped
                IEnumerable<IExample> scopedExamples = serviceScope.ServiceProvider.GetServices<IExample>();
                Console.WriteLine($"\t\tGetServices<{nameof(IExample)}> returned");
                foreach (IExample scopedExamplesInstance in scopedExamples)
                {
                    Console.WriteLine($"\t\t\t'{scopedExamplesInstance.ImplementationName}'");
                }

                Console.WriteLine("\t\tLeaving the scope.");
            }
        }
    }
}
