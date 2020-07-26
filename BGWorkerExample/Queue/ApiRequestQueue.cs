using BGWorkerExample.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BGWorkerExample.Queue
{
    public class ApiRequestQueue
    {
        private ConcurrentQueue<FakeClass> _queue;
        private SemaphoreSlim _signal;

        public ApiRequestQueue() {
            this._queue = new ConcurrentQueue<FakeClass>();
            this._signal = new SemaphoreSlim(0);
        }

        public void EnqueueItem(FakeClass item) {
            _queue.Enqueue(item);
            _signal.Release();
        }

        public FakeClass PopQueue() {
            _signal.Wait();
            _queue.TryDequeue(out FakeClass item);
            return item;
        }
    }
}
