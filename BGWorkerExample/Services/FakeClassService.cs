using BGWorkerExample.Data;
using BGWorkerExample.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGWorkerExample.Services
{
    public class FakeClassService
    {
        private ApiRequestQueue _queue;

        public FakeClassService(ApiRequestQueue queue) {
            this._queue = queue;
        }

        public void AddItem(FakeClass item) {
            this._queue.EnqueueItem(item);
        }
    }
}
