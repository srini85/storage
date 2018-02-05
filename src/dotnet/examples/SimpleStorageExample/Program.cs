using System;
using Metaparticle.Storage;
using System.Threading.Tasks;

namespace SimpleStorageExample
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Run();
            Console.WriteLine("Finished. Enter to exit.");
            Console.ReadLine();
        }

        private async static void Run()
        {
            await Task.WhenAll(IncrementCounter(1), IncrementCounter(2));
        }

        private static Task IncrementCounter(int taskId)
        {
            return Task.Run(async () => 
            {
                Console.WriteLine($"About to IncrementCounter from task id: {taskId}");
                var mpStorage = new MetaparticleStorage(new MetaparticleFileStorage(new MetaparticleFileStorageConfig{Directory = "c:\\temp"}));
                var result = await mpStorage.Scoped("globalscope", (scope) => {
                    if (scope.Val == null)
                        scope.Val = 0;

                    scope.Val++;
                    return scope.Val;
                });
                Console.WriteLine($"Result is: {(result as dynamic).Val}");
            });
        }
    }
}
