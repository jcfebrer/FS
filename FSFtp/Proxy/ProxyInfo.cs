using System.Net;

namespace FSFtp.Proxy
{
    /// <summary> POCO holding proxy informations </summary>
	public class ProxyInfo
    {
        /// <summary> Proxy host name </summary>
        public string Host { get; set; }

        /// <summary> Proxy port </summary>
        public int Port { get; set; }

        /// <summary> Proxy login credentials </summary>
        public NetworkCredential Credentials { get; set; }
    }
}