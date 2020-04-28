using eShop.Common.Commands;
using System.Threading.Tasks;

namespace eShop.Common.Mediator
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : ICommand;
    }
}