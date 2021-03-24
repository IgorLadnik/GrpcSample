using System;
using System.Collections.Generic;
using System.Diagnostics;
using RemoteInterfaces;
using RemoteImplementations;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            List<Arg1> lstArg1 = new()
            {
                new() { Id = "0", Arg2Props = new() { new() { Id = "0.0" }, new() { Id = "0.1" }, } },
                new() { Id = "1", Arg2Props = new() { new() { Id = "1.0" }, new() { Id = "1.1" }, } },
            };

            RemoteCall ob = new();

            List<long> ticks = new();

            int factor = 1_000_000;

            var parameters = new object[] { "qq", lstArg1.ToArray() };

            {
                Stopwatch sw = new();
                int ret;
                sw.Start();
                for (var  i = 0; i < factor; i++)
                    ret = ob.Foo("qq", lstArg1.ToArray());
                sw.Stop();
                ticks.Add(sw.ElapsedTicks);
            }

            var method = ob.GetType().GetMethod("Foo");

            {
                Stopwatch sw = new();
                object ret;
                sw.Start();
                for (var i = 0; i < factor; i++)
                    ret = method.Invoke(ob, parameters);
                sw.Stop();
                ticks.Add(sw.ElapsedTicks);
            }

            Dictionary<string, object> dctMethod = new();
            Func<string, Arg1[], int> func = ob.Foo;
            dctMethod["Foo"] = (Func<string, Arg1[], int>)ob.Foo;

            var func1 = (Func<string, Arg1[], int>)dctMethod["Foo"];
            
            {
                Stopwatch sw = new();
                int ret;
                sw.Start();
                for (var i = 0; i < factor; i++)
                    ret = func1("qq", lstArg1.ToArray());
                sw.Stop();
                ticks.Add(sw.ElapsedTicks);
            }


            //public int Foo(string name, Arg1[] arg1s)

            Aa(ob.Foo);
        }

        static void Aa(Func<string, Arg1[], int> func) 
        {
        }
    }

    public class Parser
    {
        private Dictionary<Type, object> _dctTypes = new Dictionary<Type, object>();


        public void ParseType(Type type)
        {
            foreach (var method in type.GetMethods())
            {
                var methodName = method.Name;
                var methodParameters = method.GetParameters();
            }
        }
    }
}
