using System;

namespace Domain.Questions.ValueObject
{
    public class Audit
    {
        public string CreatedBy { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public string LastModifiedBy { get; private set; }
        public DateTime LastModifiedOn { get; private set; }

        public Audit(string createdBy, DateTime createdOn, string lastModifiedBy, DateTime lastModifedOn)
        {
            CreatedBy = createdBy;
            CreatedOn = createdOn;
            LastModifiedBy = lastModifiedBy;
            LastModifiedOn = lastModifedOn;
        }

        public static Audit EntityCreated(string createdBy)
        {
            var currentDate = DateTime.UtcNow;
            return new Audit(createdBy, currentDate, createdBy, currentDate);
        }

        public static Audit EntityModified(string modifiedBy)
        {
            var currentDate = DateTime.UtcNow;
            return new Audit(string.Empty, default(DateTime), modifiedBy, currentDate);
        }

        public static Audit EntityModified(string createdBy, DateTime createdOn, string modifiedBy)
        {
            var currentDate = DateTime.UtcNow;
            return new Audit(createdBy, createdOn, modifiedBy, currentDate);
        }
    }
}