using System;

namespace AddVersusTryAdd
{
    /// <summary>
    /// This is the common implementation of the examples.
    /// </summary>
    public abstract class AbstractExampleImplementation : IExample
    {
        private readonly string implementationName;

        /// <summary>
        /// Constructs the instance by storing the passed in name.
        /// </summary>
        /// <param name="implementationName">[in] Name of the derived implementation.</param>
        public AbstractExampleImplementation(string implementationName)
        {
            // Build a unique instance id
            Guid instanceId = Guid.NewGuid();
            this.implementationName = $"[{instanceId}] {implementationName}";
        }

        /// <inheritdoc cref="IExample.ImplementationName"/>
        public string ImplementationName { get => implementationName; }
    }
}
