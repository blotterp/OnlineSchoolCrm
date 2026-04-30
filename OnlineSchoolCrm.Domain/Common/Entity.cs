using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolCrm.Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();

        public DateTimeOffset CreatedAt { get; protected set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; protected set; }

        protected void MarkAsUpdated()
        {
            UpdatedAt = DateTimeOffset.UtcNow;
        }

    }
}
