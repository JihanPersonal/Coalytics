using Pipl.APIs.Data.Containers;
using System.Threading.Tasks;

namespace BackendServiceDispatcher.Services
{
    /// <summary>
    /// SocialMedia Searching Service 
    /// </summary>
    public interface ISocialMediaScanner
    {
        /// <summary>
        /// Search SocialMedia profile by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>
        /// Return Person object which contains Gender, Jobs, Address...
        /// </returns>
        Task<Person> SearchSocailMedia(string email);
    }
}
