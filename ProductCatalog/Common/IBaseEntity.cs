namespace ProductCatalog.Common
{
    public interface IBaseEntity<T>
    {
        public T Id { get; set; }
    }
}