using System.Collections;
using System.Collections.Generic;

namespace PartitionerPoc
{
    public class PartitionerPart<T>: IEnumerable<T>, IEnumerator<T>
    {
        private readonly QueuePartitioner<T> _partitioner;
        private bool _isClosed;
        private T _current;

        public bool IsClosed => _isClosed;

        public PartitionerPart(QueuePartitioner<T> partitioner)
        {
            _partitioner = partitioner;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool MoveNext()
        {
            return _partitioner.q.TryDequeue(out _current) && !_isClosed;
        }

        public void Reset()
        {
            // forwardonly
        }

        public T Current => _current;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            // nope
        }

        public void Close() => _isClosed = true;
    }
}