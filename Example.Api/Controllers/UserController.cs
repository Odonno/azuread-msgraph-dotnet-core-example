using Example.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Example.Api.Controllers
{
    public class CurrentUserInfo
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public bool IsAuthenticated { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly IIdentityService _identityService;
        private readonly IGraphApiService _graphApiService;

        public UserController(IIdentityService identityService, IGraphApiService graphApiService)
        {
            _identityService = identityService;
            _graphApiService = graphApiService;
        }

        [HttpGet("current")]
        public CurrentUserInfo CurrentUser()
        {
            return new CurrentUserInfo
            {
                Id = _identityService.GetId(),
                Login = _identityService.GetMail(),
                IsAuthenticated = _identityService.IsAuthenticated()
            };
        }

        [HttpGet("search/{term}")]
        public async Task<IEnumerable<User>> Search(string term)
        {
            return await _graphApiService.SearchUsersAsync(term, 10);
        }
    }
}
