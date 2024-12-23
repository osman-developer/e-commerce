using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Shopping.webAPI.Errors;

namespace Shopping.webAPI.Extensions {
  public static class ApplicationServicesExtensions {
    public static IServiceCollection AddApplicationServices (this IServiceCollection services) {
      services.AddScoped<IProductRepository, ProductRepository> ();
      services.AddScoped (typeof (IGenericRepository<>), typeof (GenericRepository<>));

      //Adjust the validation (400) error so it returns an array of errors
      services.Configure<ApiBehaviorOptions> (options => {
        options.InvalidModelStateResponseFactory = actionContext => {
          var errors = actionContext.ModelState.Where (err => err.Value.Errors.Count > 0).SelectMany (x => x.Value.Errors).Select (x => x.ErrorMessage).ToArray ();
          var errorResponse = new ApiValidationErrorResponse {
            Errors = errors
          };
          return new BadRequestObjectResult (errorResponse);
        };
      });

      return services;
    }

  }
}