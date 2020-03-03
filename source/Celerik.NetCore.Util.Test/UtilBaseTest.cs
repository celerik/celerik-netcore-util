using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Celerik.NetCore.Util.Test
{
    public class UtilBaseTest : BaseTest
    {
        protected override void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IValidator<PaginationRequest>, PaginationRequestValidator<PaginationRequest>>();
            services.AddSingleton<IValidator<PaginationRequest<Cat>>, PaginationRequestValidator<PaginationRequest, Cat>>();
        }
    }
}
