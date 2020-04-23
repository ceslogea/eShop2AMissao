using System.Threading.Tasks;

namespace eShop.Common.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}