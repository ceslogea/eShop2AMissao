using System.Threading.Tasks;

namespace eShop.Common.Mongo
{
    public interface IDatabaseSeeder
    {
         Task SeedAsync();
    }
}