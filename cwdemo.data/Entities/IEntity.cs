namespace cwdemo.data.Entities
{
    public interface IEntity
    {
        object Id { get; }

        DateTime CreatedUtc { get; set; }
        string CreatedUtcLocale { get; set; }

        DateTime ModifiedUtc { get; set; }

        string ModifiedUtcLocale { get; set; }

        long CreatedBy { get; set; }

        long ModifiedBy { get; set; }
    }

    public interface IEntity<T> : IEntity
    {
        new T Id { get; set; }
    }
}
