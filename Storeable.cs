using System;

namespace Interview
{
    public class Storeable : IStoreable
    {
        public Storeable() : this(Guid.NewGuid().ToString())
        {
        }

        public Storeable(IComparable id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            this.Id = id;
        }

        public IComparable Id { get; set; }
    }
}