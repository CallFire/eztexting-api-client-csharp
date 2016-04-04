using System;

namespace EzTextingApiClient
{
    /// <summary>
    /// Client constants
    ///</summary>
    public static class ClientConstants
    {
        public const string EzBasePathProperty = "EzBasePath";
        public const string CtBasePathProperty = "CtBasePath";
        public const string GtBasePathProperty = "GtBasePath";
        public const string TmcBasePathProperty = "TmcBasePath";

        public const string ProxyAddressProperty = "ProxyAddress";
        public const string ProxyCredentialsProperty = "ProxyCredentials";
        public const int DefaultProxyPort = 8080;

        public const string LogTraceSourceName = "EzTextingApiClient";
        public const string LogFileListenerName = "EzTextingLogFile";
        public const string LogDatetimePattern = "yyyy-MM-dd HH:mm:ss.fff";

        public const string Placeholder = "{}";
        public const string DateFormatPattern = "MM-dd-yyyy h:mm tt";

        public const string FormEncodedContentType = "application/x-www-form-urlencoded";
        public const string JsonContentType = "application/json";

        public static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
}