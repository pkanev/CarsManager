using System;
using Microsoft.AspNetCore.Http;

namespace CarsManager.Server.Utils
{
    public static class ExtensionMethods
    {
        public static UriBuilder WithScheme(this UriBuilder builder, string scheme)
        {
            builder.Scheme = scheme;
            return builder;
        }

        public static UriBuilder WithHost(this UriBuilder builder, string host)
        {
            builder.Host = host;
            return builder;
        }

        public static UriBuilder WithPort(this UriBuilder builder, int? port)
        {
            if (port.HasValue)
                builder.Port = port.Value;

            return builder;
        }

        public static UriBuilder WithPath(this UriBuilder builder, string path)
        {
            builder.Path = path;
            return builder;
        }

        public static string GetAbsoluteUri(this HttpRequest request, string path)
        {
            var b = new UriBuilder()
                .WithScheme(request.Scheme)
                .WithHost(request.Host.Host)
                .WithPort(request.Host.Port)
                .WithPath(path);

            return b.Uri.AbsoluteUri;
        }
    }
}
