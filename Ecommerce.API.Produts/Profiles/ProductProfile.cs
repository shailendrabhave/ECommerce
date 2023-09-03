namespace Ecommerce.API.Produts.Profiles
{
    public class ProductProfile:AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<DB.Product, Models.Product>();
        }
    }
}
