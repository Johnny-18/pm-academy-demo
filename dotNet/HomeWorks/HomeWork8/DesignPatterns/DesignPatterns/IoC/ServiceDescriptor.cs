using System;

namespace DesignPatterns.IoC
{
    public class ServiceDescriptor
    {
        public ServiceDescriptor(Type serviceType, 
            Lifetime lifetime)
        {
            ServiceType = serviceType;
            Lifetime = lifetime;
        }

        public ServiceDescriptor(Type serviceType,
            Lifetime lifetime,
            Func<IServiceProvider, object> factory)
            : this(serviceType, lifetime)
        {
            Factory = factory;
        }

        public ServiceDescriptor(Type serviceType,
            object instance,
            Lifetime lifetime)
            : this(serviceType, lifetime)
        {
            Instance = instance;
        }

        public Type ServiceType { get; }

        public object Instance { get; set; }

        public Lifetime Lifetime { get; set; }
        
        public Func<IServiceProvider, object> Factory { get; set; }
    }
}