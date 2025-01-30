using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.gateway.Interfaces
{
    public interface IGatewayService
    {
        public HttpResponseMessage ProcessRequest(HttpRequest request);
        public HttpResponseMessage RouteRequest(HttpResponseMessage response);
    }
}