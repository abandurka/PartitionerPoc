using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace PartitionerPoc
{
    public class QueuePartitioner<T>
    {
        private PartitionerPart<T>[] _parts;
        public ConcurrentQueue<T> q { get; }

        public bool IsClosed => _parts.All(x => x.IsClosed);

        public QueuePartitioner(ConcurrentQueue<T> q)
        {
            this.q = q;
        }

        public IEnumerable<PartitionerPart<T>> GetPartitions(int partitions = 1)
        {
            _parts = new PartitionerPart<T>[partitions];
            for (int i = 0; i < partitions; i++)
            {
                _parts[i] = new PartitionerPart<T>(this);
                yield return _parts[i];
            }
        }
    }
}