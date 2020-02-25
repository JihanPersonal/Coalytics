using Pipl.APIs.Data.Containers;
using Pipl.APIs.Data.Fields;
using Pipl.APIs.Search;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendServiceDispatcher.Services
{
    /// <summary>
    /// Service to suppot searching User's Socialmedia Profile by using User's Email
    /// </summary>
    public class SocialMediaScanner : ISocialMediaScanner
    {
        private readonly SearchConfiguration _searchConfiguration;
        /// <summary>
        /// Constructer
        /// </summary>
        /// <param name="searchConfiguration"></param>
        public SocialMediaScanner(SearchConfiguration searchConfiguration)
        {
            var apiKey = Environment.GetEnvironmentVariable("PIPL_KEY");
            _searchConfiguration = new SearchConfiguration(apiKey);
        }

        /// <summary>
        /// Search User's Public SocialMedia profile
        /// </summary>
        /// <param name="email"></param>
        /// <returns>
        /// Return Person object which contains Gender, Jobs, Address...
        /// </returns>
        public async Task<Person> SearchSocailMedia(string email)
        {
            List<Field> fields = new List<Field>()
            {
                new Email(email)
            };
            Person person = new Person(fields);
            SearchAPIRequest request = new SearchAPIRequest(person: person, requestConfiguration: _searchConfiguration);
            SearchAPIResponse response =await request.SendAsync();
            if (response != null && response.PersonsCount > 0)
            {
                return response.Person;
            }
            else
            {
                return null;
            }

        }
    }
}
