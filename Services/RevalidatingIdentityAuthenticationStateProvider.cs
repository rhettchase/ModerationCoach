using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ModerationCrudApp.Services
{
    public class RevalidatingIdentityAuthenticationStateProvider<TUser> : AuthenticationStateProvider where TUser : class
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IdentityOptions _options;
        private readonly Timer _timer;
        private readonly ILogger<RevalidatingIdentityAuthenticationStateProvider<TUser>> _logger;

        public RevalidatingIdentityAuthenticationStateProvider(
            IServiceProvider serviceProvider,
            IOptions<IdentityOptions> options,
            ILogger<RevalidatingIdentityAuthenticationStateProvider<TUser>> logger)
        {
            _serviceProvider = serviceProvider;
            _options = options.Value;
            _logger = logger;
            _timer = new Timer(TimerCallback, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        }

        private async void TimerCallback(object state)
        {
            try
            {
                var isAuthenticated = await CheckIfUserIsAuthenticatedAsync();
                if (!isAuthenticated)
                {
                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking user authentication state.");
            }
        }

        private async Task<bool> CheckIfUserIsAuthenticatedAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<TUser>>();
            var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<TUser>>();
            var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            var httpContext = httpContextAccessor.HttpContext;

            if (httpContext == null || httpContext.User == null)
            {
                _logger.LogWarning("HttpContext or HttpContext.User is null.");
                return false;
            }

            var user = await userManager.GetUserAsync(httpContext.User);
            return user != null;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<TUser>>();
            var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            var httpContext = httpContextAccessor.HttpContext;

            if (httpContext == null || httpContext.User == null)
            {
                _logger.LogWarning("HttpContext or HttpContext.User is null.");
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var user = await signInManager.UserManager.GetUserAsync(httpContext.User);

            if (user == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var claimsPrincipal = await signInManager.CreateUserPrincipalAsync(user);
            return new AuthenticationState(claimsPrincipal);
        }
    }
}
