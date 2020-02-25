using System;
using Coalytics.Contracts.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace BackendServiceDispatcher.Extensions
{
    /// <summary>
    /// Extensions for auth configuration
    /// </summary>
    public static class AuthConfig
    {
        /// <summary>
        /// Add external auth provider, if valid
        /// </summary>
        /// <param name="ab"><see cref="AuthenticationBuilder"/> instance for fluent config</param>
        /// <param name="cred"><see cref="IExternalAuthCredential"/> valid external auth credential</param>
        /// <returns><see cref="AuthenticationBuilder"/> for further fluent config</returns>
        public static AuthenticationBuilder AddExternalAuth(this AuthenticationBuilder ab, IExternalAuthCredential cred)
        {
            switch (cred.credentialType)
            {
                case CredentialType.FACEBOOK:
                {
                    return ab.AddFacebook(opts =>
                    {
                        opts.AppId = cred.clientId;
                        opts.AppSecret = cred.clientSecret;
                    });
                }
                case CredentialType.GOOGLE:
                {
                    return ab.AddGoogle(opts =>
                    {
                        opts.ClientId = cred.clientId;
                        opts.ClientSecret = cred.clientSecret;
                    });
                }
                case CredentialType.MICROSOFT:
                {
                    return ab.AddMicrosoftAccount(opts =>
                    {
                        opts.ClientId = cred.clientId;
                        opts.ClientSecret = cred.clientSecret;
                    });
                }
                case CredentialType.TWITTER:
                {
                    return ab.AddTwitter(opts =>
                    {
                        opts.ConsumerKey = cred.clientId;
                        opts.ConsumerSecret = cred.clientSecret;
                    });
                }
                default:
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
