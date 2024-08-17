using FundevServer.Models;

namespace FundevServer.Repositories
{
    public interface ICartItemRepository
    {
        public Task<IEnumerable<CartItemModel>> GetAllCartItemOfCustomAsync (string  customId);
        public Task AddCartItemAsync (AddCartItemModel model);
        public Task RemvoveCartItemAsync (string customerId, int id);
        public Task<int> IncreaseCartItemQuantityAsync(string customerId, int id);
        public Task<int> DecreaseCartItemQuantityAsync(string customerId, int id);
    }
}
