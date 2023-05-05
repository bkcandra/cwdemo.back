namespace cwdemo.core.Models
{
    public class Catalog : CreateCatalog
    {
        public long Id { get; set; }
        public bool Active { get; set; }
    }

    public class CreateCatalog
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Type { get; set; }
        public int StoreId { get; set; }
    }
}