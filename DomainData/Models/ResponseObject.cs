using System.Net;

namespace DomainData.Models
{
    public class ResponseObject
    {
        public string Status { get; set; }

        public string Message { get; set; }

        public string Message2 { get; set; }

        public object Data { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

    }

}
