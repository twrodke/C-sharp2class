using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Collector_TWR
{
    public class FixedSizedQueue<T>
    { 
        //code to create custom ConcurrentQueue using generics and limiting queue to Limit
        public ConcurrentQueue<T> q = new ConcurrentQueue<T>();
        private object lockObject = new object();
        public int Limit { get; set; }
        public void Enqueue(T obj)
        {
            q.Enqueue(obj);
            lock (lockObject)
            {
                T overflow;
                while (q.Count > Limit && q.TryDequeue(out overflow)) ;
            }
        }
    }
}
