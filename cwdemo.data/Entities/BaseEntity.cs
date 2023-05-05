using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cwdemo.data.Entities
{
    public abstract class Entity<T> : BaseEntity, IEntity<T>
    {
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }

        object IEntity.Id
        {
            get { return this.Id; }
        }
    }

    public abstract class Entity : BaseEntity, IEntity
    {
        public Entity()
        {
        }

        public Entity(int UserId) : base(UserId)
        {
        }

        [System.ComponentModel.DataAnnotations.Key]
        [Column(TypeName = "bigint")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        object IEntity.Id => this.Id;
    }

    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            this.CreatedUtc = DateTime.UtcNow;
            this.ModifiedUtc = DateTime.UtcNow;
        }

        public BaseEntity(int UserId) : this()
        {
            this.CreatedBy = UserId;
            this.ModifiedBy = UserId;
        }

        /// <summary>
        /// UTC created datetime
        /// </summary>
        [Required]
        public DateTime CreatedUtc { get; set; }

        [MaxLength(40)]
        [Column(TypeName = "varchar")]
        public string CreatedUtcLocale { get; set; }

        /// <summary>
        /// UTC modified datetime
        /// </summary>
        [Required]
        public DateTime ModifiedUtc { get; set; }

        [MaxLength(40)]
        [Column(TypeName = "varchar")]
        public string ModifiedUtcLocale { get; set; }

        [Column(TypeName = "bigint")]
        [Required]
        public long CreatedBy { get; set; }

        [Column(TypeName = "bigint")]
        [Required]
        public long ModifiedBy { get; set; }
    }
}
