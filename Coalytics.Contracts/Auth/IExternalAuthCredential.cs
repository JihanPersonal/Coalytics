namespace Coalytics.Contracts.Auth
{
    /// <summary>
    /// Describe credential object
    /// </summary>
    public interface IExternalAuthCredential
    {
        /// <summary>
        /// API client ID
        /// </summary>
        string clientId { get; }

        /// <summary>
        /// API client secret
        /// </summary>
        string clientSecret { get; }

        /// <summary>
        /// Type of credential (must be a valid external provider)
        /// </summary>
        CredentialType credentialType { get; }
    }
}
