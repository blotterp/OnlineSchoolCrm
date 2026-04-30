using OnlineSchoolCrm.Domain.Common;

namespace OnlineSchoolCrm.Domain.Crm
{
    public sealed class Lead : Entity
    {
        private Lead()
        {

        }
        public Lead(
            Guid tenantId,
            string parentName,
            string phone,
            string? email,
            string? childName,
            int? childAge,
            string? courseInterest)
        {
            if(tenantId == Guid.Empty)
                throw new ArgumentNullException("Tenant id is required",nameof(tenantId));
            if (string.IsNullOrWhiteSpace(parentName))
                throw new ArgumentNullException("Parent name is required", nameof(parentName));
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentNullException("Phone is required", nameof(phone));
            if (childAge is <= 0 or > 18)
                throw new ArgumentException("Child are must between 1 and 18",nameof(childAge));
            TenantId = tenantId;
            ParentName = parentName.Trim();
            Phone = phone.Trim();
            Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
            ChildName = string.IsNullOrWhiteSpace(childName) ? null : childName.Trim();
            ChildAge = childAge;
            CourseInterest = string.IsNullOrWhiteSpace(courseInterest) ? null : courseInterest.Trim();
            Status = LeadStatus.New;
        }

        public Guid TenantId { get; private set; }
        public string ParentName { get; private set; } = null!;

        public string Phone { get; private set; } = null!;

        public string? Email { get; private set; } = null!;
        public string? ChildName { get; private set; }

        public int? ChildAge { get; private set; }
        public string? CourseInterest { get; private set; }
        public LeadStatus Status { get; private set; }


        public void MarkAsContacted()
        {
            EnsureNotConverted();

            Status = LeadStatus.Contacted;
            MarkAsUpdated();
        }

        public void MarkTrialScheduled()
        {
            EnsureNotConverted();

            Status = LeadStatus.TrialScheduled;
            MarkAsUpdated();
        }

        public void MarkTrialCompleted()
        {
            EnsureNotConverted();

            if (Status != LeadStatus.TrialScheduled)
                throw new InvalidOperationException("Only scheduled trial lead can be marked as trial completed");

            Status = LeadStatus.TrialCompleted;
            MarkAsUpdated();
        }

        public void ConvertToStudent()
        {
            if (Status == LeadStatus.Lost)
                throw new InvalidOperationException("Lost lead can`t be converted to student");

            Status = LeadStatus.ConvertedToStudent;
            MarkAsUpdated();
        }

        public void MarkAsLost()
        {
            if (Status == LeadStatus.ConvertedToStudent)
                throw new InvalidOperationException("Converted lead can`t be marked as lost");
            Status = LeadStatus.Lost;
            MarkAsUpdated();
        }

        private void EnsureNotConverted()
        {
            if (Status == LeadStatus.ConvertedToStudent)
                throw new InvalidOperationException("Converted lead can`t be changed");
        }

    }
}
