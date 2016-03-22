using System.Collections.Generic;
using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Common.Model
{
    /// Base response object from EzTexting servic
    public class EzTextingResponse<T> : EzTextingModel
    {
        [JsonProperty("Response")] private readonly ResponseHolder<T> _holder;

        public string Status => _holder.Status;
        public int Code => _holder.Code;
        public T Entry => _holder.Entry;
        public IList<T> Entries => _holder.Entries;
        public IList<string> Errors => _holder.Errors;

        public EzTextingResponse()
        {
        }

        public EzTextingResponse(string status, int code, T entry)
        {
            _holder = new ResponseHolder<T> {Status = status, Code = code, Entry = entry};
        }

        public EzTextingResponse(string status, int code, IList<T> entries)
        {
            _holder = new ResponseHolder<T> {Status = status, Code = code, Entries = entries};
        }

        public string GetStatus()
        {
            return _holder.Status;
        }

        private class ResponseHolder<T>
        {
            public string Status { get; set; }
            public int Code { get; set; }
            public T Entry { get; set; }
            public IList<T> Entries { get; set; }
            public IList<string> Errors { get; set; }
//            private InputStream content;
        }
    }
}