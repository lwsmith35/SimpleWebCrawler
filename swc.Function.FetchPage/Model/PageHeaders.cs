using System.Collections.Generic;

namespace swc.Function.FetchPage.Model
{
    public class PageHeaders
    {
        public IList<(string HeaderKey, IEnumerable<string> HeaderValues)> headers;
    }
}
