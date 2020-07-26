using BGWorkerExample.APIClient;
using BGWorkerExample.Interfaces;
using BGWorkerExample.Queue;
using BGWorkerExample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGWorkerExample.IoC
{
    public class Container {
        #region Properties

        public IApiClient ApiClient { 
            get { 
                return new ApiClient(); 
            } 
        }

        public ApiRequestQueue ApiRequestQueue { get; }

        private QueueProcessor QueueProcessor { get; }

        public FakeClassService FakeClassService { 
            get { 
                return new FakeClassService(this.ApiRequestQueue); 
            } 
        }

        #endregion

        public Container() {
            this.ApiRequestQueue = new ApiRequestQueue();
            this.QueueProcessor = new QueueProcessor(this.ApiRequestQueue, this.ApiClient);
            this.QueueProcessor.StartProcessingQueue();
        }

        public void StartQueueProcessor() {
            this.QueueProcessor.StartProcessingQueue();
        }

        public void StopQueueProcessor() {
            this.QueueProcessor.StopProcessingQueue();
        }
    }
}
