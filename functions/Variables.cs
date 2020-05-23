using EasyHttp.Http;
using System.Collections.Generic;
using System.Net;

namespace MILG0IR_home_windows.functions {
    public class Pages {
        public IList<Page> Page { get; set; }
    }
    public class Page {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public object Uri_local { get; set; }
        public string Uri_remote { get; set; }
        public string Icon { get; set; }
        public string Location { get; set; }
    }
    public class HttpResponse {
        public string URI { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string StatusDescription{ get; set; }
        public string Body { get; set; }
    }
    public class ErrorCode {
        public string Code { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
    }
}
