using System.Collections.Specialized;
using System.Web;

namespace EcoRoute.Common.Http
{
    public class QueryStringBuilder
    {
        private readonly NameValueCollection _nameValueCollection = HttpUtility.ParseQueryString("");

        public QueryStringBuilder AddParameter(string name, string value, bool addIfNull = false)
        {
            if (value != null || addIfNull)
            {
                _nameValueCollection.Add(name, value);
            }

            return this;
        }
        
        public string Build()
        {
            return _nameValueCollection.ToString();
        }
    }
}