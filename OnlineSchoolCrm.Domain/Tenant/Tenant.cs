using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineSchoolCrm.Domain.Common;
namespace OnlineSchoolCrm.Domain.Tenant
{
    public sealed class Tenant: Entity
    {
        private Tenant()
        {

        }

        public Tenant(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Tenant name can`t be empty", nameof(name));

            Name = name;
            IsActive = true;
        }

        public string Name { get; private set; } = null!;
        public bool IsActive { get; private set; }

        public void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tenant name can`t be empty", nameof(name));
            Name = name;
            MarkAsUpdated();
        }
        public void Deactivated()
        {
            if(!IsActive)
                return;
            IsActive = false;
            MarkAsUpdated();
        }

    }
}
