using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;

namespace BackendServiceDispatcher.Extensions
{
    /// <summary>
    /// Utilities to check client authentication within a given request context
    /// </summary>
    public static class ClientAuthExtensions
    {
        /// <summary>
        /// Get Collection of <see cref="AuthenticationDescription"/> for a given HttpContext's external auth providers
        /// </summary>
        /// <param name="context">Context to describe</param>
        /// <returns>Collection of info on external auth providers</returns>
        public static IEnumerable<AuthenticationDescription> GetExternalProviders(this HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return from description in context.Authentication.GetAuthenticationSchemes()
                   where !string.IsNullOrWhiteSpace(description.DisplayName)
                   select description;
        }

        /// <summary>
        /// Check whether a named provider is supported for a given context
        /// </summary>
        /// <param name="context">HttpContext to check</param>
        /// <param name="provider">Provider to check</param>
        /// <returns>True if allowed, else false</returns>
        public static bool IsProviderSupported(this HttpContext context, string provider)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return (from description in context.GetExternalProviders()
                    where string.Equals(description.AuthenticationScheme, provider, StringComparison.OrdinalIgnoreCase)
                    select description).Any();
        }
    }
}
