using System.Threading.Tasks;

namespace LongRunningTestClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var listening = new ListeningServer();
            //var manual = new ManualCheck();
            //await manual.CheckJobDone();
        }
    }
}
