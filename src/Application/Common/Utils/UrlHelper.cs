using CarsManager.Application.Common.Interfaces;

namespace CarsManager.Application.Common.Utils
{
    public class UrlHelper : IUrlHelper
    {
        public string UrlCombine(string uri1, string uri2)
        {
            uri1 = uri1.TrimEnd('/');
            uri2 = uri2.TrimStart('/');
            return string.Format("{0}/{1}", uri1, uri2);
        }
    }
}
