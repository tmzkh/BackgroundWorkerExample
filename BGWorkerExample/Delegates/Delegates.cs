using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGWorkerExample.Delegates
{
    public delegate void ApiRequestCallback(JObject response);
}
