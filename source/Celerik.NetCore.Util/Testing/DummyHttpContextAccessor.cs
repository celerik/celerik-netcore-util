using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Implementation of the IHttpContextAccessor interface for testing purposes. With
    /// this implementation we get a DefaultHttpContext.
    /// </summary>
    public class DummyHttpContextAccessor : IHttpContextAccessor
    {
        /// <summary>
        /// Reference to the current HttpContext instance.
        /// </summary>
        private HttpContext _httpContext;

        /// <summary>
        /// The claims related to the current UserIdentity.
        /// </summary>
        private readonly ClaimsIdentity _claimsIdentity;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="userClaimKey">The user claim key.</param>
        /// <param name="userClaimValue">The user claim value.</param>
        public DummyHttpContextAccessor(string userClaimKey = null, object userClaimValue = null)
        {
            if (userClaimKey != null)
            {
                var userJson = JsonConvert.SerializeObject(userClaimValue);
                var userClaims = new List<Claim> { new Claim(userClaimKey, userJson) };

                _claimsIdentity = new ClaimsIdentity(userClaims);
            }
        }

        /// <summary>
        /// Gets or sets a reference to the current HttpContext.
        /// </summary>
        public HttpContext HttpContext
        {
            get
            {
                if (_httpContext == null)
                {
                    _httpContext = new DefaultHttpContext();
                    _httpContext.User.AddIdentity(_claimsIdentity);
                }

                return _httpContext;
            }
            set
            {
                _httpContext = value;
            }
        }
    }
}
