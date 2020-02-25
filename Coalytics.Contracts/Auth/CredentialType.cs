using System;

namespace Coalytics.Contracts.Auth
{
    /// <summary>
    /// Flags to describe credential type
    /// </summary>
    [Flags]
    public enum CredentialType
    {
        /// <summary>
        /// Internal Coalytics auth
        /// </summary>
        INTERNAL = 0x0,
        /// <summary>
        /// Facebook OAuth
        /// </summary>
        FACEBOOK = 0x1,
        /// <summary>
        /// Microsoft OAuth
        /// </summary>
        MICROSOFT = 0x2,
        /// <summary>
        /// Google OAuth
        /// </summary>
        GOOGLE = 0x4,
        /// <summary>
        /// Twitter OAuth
        /// </summary>
        TWITTER = 0x8
    }
}
