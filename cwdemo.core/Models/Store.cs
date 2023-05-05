namespace cwdemo.core.Models
{
    public class Store : CreateStore
    {
        public long Id { get; set; }
    }
    public class CreateStore
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public bool Active { get; set; }
    }
}
