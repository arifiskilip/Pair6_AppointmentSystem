namespace Core.Domain
{
    public class Entity<TId> : IEntity
    {
        public TId Id { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Entity()
        {

        }

        public Entity(TId id)
        {
            Id = id;
        }
    }
}
