using System;

namespace Common.Domain
{
    public abstract class Command
    {
        public Command()
        {
            Id = new Guid();
            CreatedOn = DateTime.UtcNow;
        }

        public Command(Guid customId)
        {
            Id = customId;
            CreatedOn = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}