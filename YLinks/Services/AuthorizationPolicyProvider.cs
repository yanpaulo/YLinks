using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YLinks.Services
{
    public class AuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly IHttpContextAccessor _context;
        private readonly IHostingEnvironment _environment;
        private readonly AuthorizationPolicy
            basicPolicy = new AuthorizationPolicyBuilder().RequireAssertion(c => true).Build(),
            adminPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

        public AuthorizationPolicyProvider(IHttpContextAccessor context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            AuthorizationPolicy policy;
            if (_context.HttpContext.Request.Path.StartsWithSegments("/Links") && !_environment.IsDevelopment())
            {
                policy = adminPolicy;
            }
            else
            {
                policy = basicPolicy;
            }
            return Task.FromResult(policy);
        }

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            throw new NotImplementedException();
        }
    }
}
