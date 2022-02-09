using catalog.api.Entities;
using MongoDB.Driver;

namespace catalog.api.Data
{
    public interface ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }
    }
}
