using System;
using Unity;
using Unity.Injection;

namespace UnityContainerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("UnityContainerDemo");

            IUnityContainer container = new UnityContainer();
            container.RegisterType<ICar, Ford>();// Map ICar with BMW 
            container.RegisterType<ICar, Audi>("LuxuryCar");

            // Registers Driver type
            container.RegisterType<Driver>("LuxuryCarDriver",
                new InjectionConstructor(container.Resolve<ICar>("LuxuryCar")));

            //Resolves dependencies and returns the Driver object 
            Driver drv1 = container.Resolve<Driver>();

            drv1.RunCar();
            drv1.RunCar();

            Driver drv2 = container.Resolve<Driver>("LuxuryCarDriver");
            drv2.RunCar();
        }
    }

    public interface ICar
    {
        int Run();
    }

    public class BMW : ICar
    {
        private int _miles = 0;

        public int Run()
        {
            return ++_miles;
        }
    }

    public class Ford : ICar
    {
        private int _miles = 0;

        public int Run()
        {
            return ++_miles;
        }
    }

    public class Audi : ICar
    {
        private int _miles = 0;

        public int Run()
        {
            return ++_miles;
        }

    }
    public class Driver
    {
        private ICar _car = null;

        public Driver(ICar car)
        {
            _car = car;
        }

        public void RunCar()
        {
            Console.WriteLine("Running {0} - {1} mile ", _car.GetType().Name, _car.Run());
        }
    }
}
