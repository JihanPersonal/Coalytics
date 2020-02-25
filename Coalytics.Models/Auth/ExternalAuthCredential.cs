using System;
using Coalytics.Contracts.Auth;

namespace Coalytics.Models.Auth
{
    /// <summary>
    /// Describe credential object
    /// </summary>
    public sealed class ExternalAuthCredential : IExternalAuthCredential
    {
        // TODO (CL) implement InternalsVisibleTo since MS removed appdomains in .net core and this should be internal

        /// <summary>
        /// API client ID
        /// </summary>
        public string clientId { get; set; } = string.Empty;
        /// <summary>
        /// API client secret
        /// </summary>
        public string clientSecret { get; private set; } = string.Empty;

        /// <summary>
        /// Type of credential (must be a valid external credential type)
        /// </summary>
        public CredentialType credentialType { get; private set; }

        /// <summary>
        /// Construct new ExternalAuthCredential
        /// </summary>
        /// <param name="_credentialType">Type of credential (must be a valid external provider)</param>
        /// <param name="_clientId">API client ID (will be set to an empty string if null is passed)</param>
        /// <param name="_clientSecret">API client secret (will be set to an empty string if null is passed)</param>
        public ExternalAuthCredential(CredentialType _credentialType, string _clientId, string _clientSecret)
        {
            if (_credentialType == CredentialType.FACEBOOK || _credentialType == CredentialType.MICROSOFT || _credentialType == CredentialType.GOOGLE || _credentialType == CredentialType.TWITTER)
            {
                credentialType = _credentialType;
            }
            else
            {
                throw new ArgumentNullException("credentialType", "Null or invalid credential type");
            }
            clientId = _clientId??string.Empty;
            clientSecret = _clientSecret ?? string.Empty;
        }
    }
}
