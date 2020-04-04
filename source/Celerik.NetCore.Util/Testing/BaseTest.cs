using System;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Base class for all tests.
    /// </summary>
    public class BaseTest
    {
        /// <summary>
        /// Reference to the current HttpContext instance.
        /// </summary>
        private DummyHttpContextAccessor _httpContext;

        /// <summary>
        /// Provider to resolve service objects.
        /// </summary>
        private IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="userClaimKey">The user claim key.</param>
        /// <param name="userClaimValue">The user claim value.</param>
        protected BaseTest(string userClaimKey = null, object userClaimValue = null)
        {
            _httpContext = new DummyHttpContextAccessor(userClaimKey, userClaimValue);

            var services = new ServiceCollection();
            var stringLocalizerFactory = new DummyStringLocalizerFactory();

            services.AddSingleton<IStringLocalizerFactory>(stringLocalizerFactory);
            services.AddSingleton<IConfiguration>(svcProvider => new DummyConfiguration());
            services.AddTransient<IHttpContextAccessor>(svcProvider => _httpContext);

            InitializeServiceProvier(services);
            UtilResources.Initialize(stringLocalizerFactory);
        }

        /// <summary>
        /// Initializes the service provider.
        /// </summary>
        /// <param name="services">List where we add the services to.</param>
        private void InitializeServiceProvier(ServiceCollection services)
        {
            _serviceProvider = services.BuildServiceProvider();
            AddServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Adds services to the current service provider.
        /// </summary>
        /// <param name="services">List where we add the services to.</param>
        protected virtual void AddServices(IServiceCollection services)
        {
        }

        /// <summary>
        /// Gets a service of type TService from the IServiceProvider.
        /// </summary>
        /// <returns>A service object of type TService.</returns>
        protected TService GetService<TService>()
        {
            return _serviceProvider.GetRequiredService<TService>();
        }

        /// <summary>
        /// Gets the IValidator that validates the passed-in payload object.
        /// </summary>
        /// <param name="payload">The payload object to get its IValidator.</param>
        /// <returns>IValidator that validates the passed-in payload object.</returns>
        protected IValidator<TPayload> GetValidator<TPayload>(TPayload payload)
        {
            var validator = _serviceProvider.GetRequiredService<IValidator<TPayload>>();

            // This useless call is beacuse The analyzer is not suppressing the CA1801
            // warning by any way. We need to infer the type based on the payload to
            // make the code cleaner.
            payload.GetType();

            return validator;
        }

        /// <summary>
        /// Sets the current User Claims.
        /// </summary>
        /// <param name="userClaimKey">The user claim key.</param>
        /// <param name="userClaimValue">The user claim value.</param>
        protected void SetUserClaims(string userClaimKey, object userClaimValue)
        {
            _httpContext = new DummyHttpContextAccessor(userClaimKey, userClaimValue);
        }
    }
}
