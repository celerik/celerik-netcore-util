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
    public abstract class BaseTest
    {
        /// <summary>
        /// Reference to the current HttpContext instance.
        /// </summary>
        private DummyIHttpContextAccessor _httpContext;

        /// <summary>
        /// Provider to resolve service objects.
        /// </summary>
        private IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="userClaimKey">The key used to store the user claim.</param>
        /// <param name="userClaimValue">The value of the user claim.</param>
        protected BaseTest(string userClaimKey = null, object userClaimValue = null)
        {
            _httpContext = new DummyIHttpContextAccessor(userClaimKey, userClaimValue);

            var services = new ServiceCollection();
            var stringLocalizerFactory = new DummyIStringLocalizerFactory();
            var config = new DummyIConfiguration();

            services.AddSingleton<IStringLocalizerFactory>(stringLocalizerFactory);
            services.AddSingleton<IConfiguration>(config);
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

            // The analyzer is not suppressing the CA1801 warning by any way.
            // We need to infer the type based on the parameter to make the
            // code cleaner.
            return payload != null ? validator : validator;
        }

        /// <summary>
        /// Sets the current User Claims.
        /// </summary>
        /// <param name="userClaimKey">The key used to store the user claim.</param>
        /// <param name="userClaimValue">The value of the user claim.</param>
        protected void SetUserClaims(string userClaimKey, object userClaimValue)
        {
            _httpContext = new DummyIHttpContextAccessor(userClaimKey, userClaimValue);
        }
    }
}
