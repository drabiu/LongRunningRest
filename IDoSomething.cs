using System;
using System.Threading.Tasks;

namespace LongRunningRest
{
    public interface IDoSomething
    {
        Guid Idle();
        string CheckStatus(Guid taskId);
        Task Notify(string url);
    }
}