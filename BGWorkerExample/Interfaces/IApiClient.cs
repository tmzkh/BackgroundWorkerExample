using BGWorkerExample.APIClient;
using BGWorkerExample.Delegates;
using BGWorkerExample.Enums;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGWorkerExample.Interfaces
{
    public interface IApiClient
    {
        void Request(HTTPRequestType requestType, string url, ApiRequestCallback callback, JObject data = null);
    }
}
