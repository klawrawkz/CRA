﻿namespace CRA.ClientLibrary
{
    /// <summary>
    /// Describes a connection between two process/endpoint pairs
    /// </summary>
    public class ConnectionInfo
    {
        /// <summary>
        /// Connection is from this process
        /// </summary>
        public string FromProcess { get; set; }
        /// <summary>
        /// Connection is from this output endpoint
        /// </summary>
        public string FromEndpoint { get; set; }
        /// <summary>
        /// Connection is to this process
        /// </summary>
        public string ToProcess { get; set; }
        /// <summary>
        /// Connection is to this input endpoint
        /// </summary>
        public string ToEndpoint { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fromProcess"></param>
        /// <param name="fromEndpoint"></param>
        /// <param name="toProcess"></param>
        /// <param name="toEndpoint"></param>
        public ConnectionInfo(string fromProcess, string fromEndpoint, string toProcess, string toEndpoint)
        {
            this.FromProcess = fromProcess;
            this.FromEndpoint = fromEndpoint;
            this.ToProcess = toProcess;
            this.ToEndpoint = toEndpoint;
        }

        /// <summary>
        /// String representation of a CRA conection
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return new { FromProcess = FromProcess, FromEndpoint = FromEndpoint, ToProcess = ToProcess, ToEndpoint = ToEndpoint }.ToString();
        }

        /// <summary>
        /// Check if two instances are equal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var otherConnectionInfo = (ConnectionInfo)obj;
            if (otherConnectionInfo == null) return false;

            return
                (FromProcess == otherConnectionInfo.FromProcess) &&
                (ToProcess == otherConnectionInfo.ToProcess) &&
                (FromEndpoint == otherConnectionInfo.FromEndpoint) &&
                (ToEndpoint == otherConnectionInfo.ToEndpoint);
        }

        public override int GetHashCode()
        {
            return FromProcess.GetHashCode() ^ FromEndpoint.GetHashCode() ^ ToProcess.GetHashCode() ^ ToEndpoint.GetHashCode();
        }
    }
}