using BGWorkerExample.APIClient;
using BGWorkerExample.Data;
using BGWorkerExample.Enums;
using BGWorkerExample.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BGWorkerExample.Queue
{
    public class QueueProcessor
    {
        private ApiRequestQueue _queue;
        private BackgroundWorker _worker;
        private IApiClient _apiClient;

        public QueueProcessor(ApiRequestQueue queue, IApiClient apiClient) {
            this._queue = queue;
            this.InitWorker();
            this._apiClient = apiClient;
        }

        private void InitWorker() {
            this._worker = new BackgroundWorker();
            this._worker.WorkerSupportsCancellation = true;
            this._worker.DoWork += this.worker_dowork;
        }

        private void worker_dowork(object sender, DoWorkEventArgs args) {

            BackgroundWorker worker = sender as BackgroundWorker;

            while (! worker.CancellationPending) {
                FakeClass item = _queue.PopQueue();

                _apiClient.Request(
                    HTTPRequestType.POST, 
                    "http://test.test/api/test", 
                    (JObject response) => { 
                        if (response != null) {

                        }             
                    }, 
                    JObject.FromObject(item));
            }
            args.Cancel = true;
        }

        public void StartProcessingQueue() {
            this._worker.RunWorkerAsync();
        }

        public void StopProcessingQueue() {
            this._worker.CancelAsync();
        }

        ~QueueProcessor() {
            this._worker.DoWork -= this.worker_dowork;
        }
    }
}
