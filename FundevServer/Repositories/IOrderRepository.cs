using FundevServer.Data;
using FundevServer.Models;

namespace FundevServer.Repositories
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<OrderModel>> GetAllOrdersAsync(string id);
        public Task<IEnumerable<OrderModel>> GetAllOrderingAsync(string id);
        public Task UploadOrderAsync(UploadOrderModel model);
        public Task UpdateOrderAsync(string userId ,int id);
        public Task CancelOrderAsync(string userId, int id);
    }
}
