using System.Collections.Concurrent;
using System.Threading.Tasks;
using ArchiveFiles;
using Log;
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("запустился архиватор...");
        Archiver archiver = new Archiver(new ConsoleLogger());
        archiver.ArchiveFolder(@"C:\Users\User\Documents\mveuC#");

        Console.WriteLine("хотите закончить архивирование? нажмите esc");

        if (Console.ReadKey().Key == ConsoleKey.Escape)
        {
            archiver.CancelArchive();
        }
    }

}
