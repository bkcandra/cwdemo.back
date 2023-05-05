namespace cwdemo.data.Entities
{
    public class CatalogEntity : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public int Type { get; set; }
        public int StoreId { get; set; }
    }
    public class CatalogStoreEntity : CatalogEntity
    {
        public string StoreName { get; set; }
    }
}