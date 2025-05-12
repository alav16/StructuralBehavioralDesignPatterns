/*
You are simulating how a computer boots up.
 Normally, starting a computer involves many steps at different levels:
Starting the CPU
Loading Memory
Reading data from the HardDrive
Activating the GPU for graphics
✅ Your goal is to hide the complexity behind a simple Facade:
 Just call startComputer() and everything starts correctly.
✅ Apply Facade Design Pattern.
🛠 Tasks:
Create 4 subsystem classes:
CPU
freeze()
execute()
Memory
load(address: number, data: string)
HardDrive
read(sector: number, size: number): string
GPU
initialize()
Create a ComputerFacade class that:
Takes instances of CPU, Memory, HardDrive, GPU.
Has a method startComputer() that:
Freezes the CPU
Loads the program into memory
Reads data from hard drive
Initializes the GPU
Executes the CPU
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prob5Facade
{
    public class CPU
    {
        public void Freeze() => Console.WriteLine("CPU freezing  ");
        public void Execute() => Console.WriteLine("CPU executing  ");
        public void Shutdown() => Console.WriteLine("CPU shuting down  ");  
    }

    public class Memory
    {
        public void Load(int address, string data) => Console.WriteLine($"{address}");
        public void Clear() => Console.WriteLine("Memory clearing  ");
    }

    public class HardDrive
    {
        public string Read(int sector, int size) => $"{sector}  {size}";
        public void ParkHead() => Console.WriteLine("HardDrive parking  ");
    }

    public class GPU
    {
        public void Initialize() => Console.WriteLine("GPU initializing  ");
        public void ShutDown() => Console.WriteLine("GPU shutting down  ");

    }

    public class ComputerFacade
    {
        private readonly CPU _cpu;
        private readonly Memory _memory;
        private readonly HardDrive _hardDrive;
        private readonly GPU _gpu;

        public ComputerFacade(CPU cpu, Memory memory, HardDrive harddrive, GPU gpu)
        {
            _cpu = cpu;
            _memory = memory;
            _hardDrive = harddrive;
            _gpu = gpu;
        }

        public void StartComputer()
        {
            _cpu.Freeze();
            _memory.Load(0, _hardDrive.Read(0, 1024));
            _gpu.Initialize();
            _cpu.Execute();
        }

        public void ShutDownComputer()
        {
            _cpu.Shutdown();
            _gpu.ShutDown();
            _memory.Clear();
            _hardDrive.ParkHead();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var cpu = new CPU();
            var memory = new Memory();
            var hardDrive = new HardDrive();
            var gpu = new GPU();

            
            var computer = new ComputerFacade(cpu, memory, hardDrive, gpu);

            
            computer.StartComputer();
            computer.ShutDownComputer();
        }
    }
}
