namespace FundevServer.Models
{
    public class UserModel
    {
        public string FullName { get; set; } = null!;
        public List<ProductModel>? ProductModels { get; set; }
    }
}
