using System;

namespace ProductCatalog.Common
{
    public interface IAuditableEntity
    {
        public DateTime? LastUpdated { get; set; }
    }
}