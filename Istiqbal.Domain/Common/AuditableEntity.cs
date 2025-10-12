using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Domain.Common
{
    public class AuditableEntity : Entity
    {
        protected AuditableEntity()
        { }

        protected AuditableEntity(Guid id)
            : base(id)
        {
        }

        public DateTimeOffset CreatedAtUtc { get; set; }

        public string? CreatedBy { get; set; }

        public DateTimeOffset LastModifiedUtc { get; set; }

        public string? LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTimeOffset? DeletedUtc { get; set; }
        public string? DeletedBy { get; set; }
    }
}
