using Microsoft.Graph;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Api.Services
{
    public interface IGraphApiService
    {
        Task<User> GetUserProfileAsync();
        Task<List<User>> SearchUsersAsync(string search, int limit);
    }

    public class GraphApiService : IGraphApiService
    {
        private readonly IAuthenticationProvider _msGraphAuthenticationProvider;

        public GraphApiService(IAuthenticationProvider authenticationProvider)
        {
            _msGraphAuthenticationProvider = authenticationProvider;
        }

        public async Task<User> GetUserProfileAsync()
        {
            var client = new GraphServiceClient(_msGraphAuthenticationProvider);
            return await client.Me.Request().GetAsync();
        }

        public async Task<List<User>> SearchUsersAsync(string search, int limit)
        {
            var client = new GraphServiceClient(_msGraphAuthenticationProvider);
            var users = new List<User>();

            var currentReferencesPage = await client.Users
                .Request()
                .Top(limit)
                .Filter($"startsWith(displayName, '{search}') or startswith(mail, '{search}')")
                .GetAsync();

            users.AddRange(currentReferencesPage);

            return users;
        }
    }
}
