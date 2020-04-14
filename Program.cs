using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Multithreading
{
    class Program
    {
        private int SharedInteger { get; set; }
        private Mutex Mutex = new Mutex();
        public List<int> Winers { get; set; } = new List<int>();

        static void Main(string[] args)
        {
            Console.WriteLine("3, 2, 1, Goooooooooooooooooo !");

            Program program = new Program();
            program.Run();

            program.Winers.ForEach(i => Console.WriteLine(i));

            Console.WriteLine("Wineeeeeeeeeeeeeeeeeeeeeer is {0}", program.Winers.First());
        }

        public void Run()
        {
            var threadStartDelegate = new ThreadStart(OnThreadStart);

            var threads = new List<Thread> {
            new Thread(threadStartDelegate),
            new Thread(threadStartDelegate),
            new Thread(threadStartDelegate) };

            foreach (var t in threads)
            {
                t.Start();
                
            }

            foreach (var t in threads)
            {
                t.Join();
            }
        }

        private void OnThreadStart()
        {
            var random = new System.Random();
            var executionTime = random.Next(2, 5);
            var timeSpan = TimeSpan.FromSeconds(executionTime);

            Mutex.WaitOne();
            SharedInteger += timeSpan.Milliseconds;
            Thread.Sleep(timeSpan);
            Mutex.ReleaseMutex();
            Winers.Add(Thread.CurrentThread.ManagedThreadId);
        }        
    }
}