using System;

namespace WooliesX.Utility
{
    public static class Extension
    {
        public static Uri CombineUri(this Uri baseUri, string relativeOrAbsoluteUri)
        {
            return new Uri(baseUri, relativeOrAbsoluteUri);
        }
    }
}
