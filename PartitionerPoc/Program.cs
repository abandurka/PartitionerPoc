using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PartitionerPoc
{
    static class Program
    {
        static void Main(string[] args)
        {
            var q = new ConcurrentQueue<int>();
            q.Enqueue(1);

            var partitioner = new QueuePartitioner<int>(q);


            var parts = partitioner.GetPartitions(4);
            foreach (var part in parts)
            {
                foreach (var i in part)
                {
                    if (i > 100)
                    {
                        part.Close();
                        continue;
                    }
                    q.Enqueue(i*2);
                    Console.WriteLine(i);
                }
            }

            Console.WriteLine("done");
        }
    }
}