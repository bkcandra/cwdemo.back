namespace cwdemo.data.Entities
{
    public class StoreEntity : Entity
    {
        public string Name { get; set; }
        public string Location { get; set; }
    }

    public class CatalogEntity : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public int Type { get; set; }
    }

}