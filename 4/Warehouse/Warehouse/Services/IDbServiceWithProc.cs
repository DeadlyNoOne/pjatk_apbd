using System.Threading.Tasks;
using Warehouse.Models;

namespace Warehouse.Services
{
    public interface IDbServiceWithProc
    {
        Task<int> addToWarehouseAsync(ProductWarehouse warehouseProduct);
    }
}
