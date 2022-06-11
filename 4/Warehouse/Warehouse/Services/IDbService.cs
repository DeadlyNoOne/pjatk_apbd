using System.Threading.Tasks;
using Warehouse.Models;

namespace Warehouse.Services
{
    public interface IDbService
    {
        Task<int> addToWarehouseAsync(ProductWarehouse warehouseProduct);
    }
}
