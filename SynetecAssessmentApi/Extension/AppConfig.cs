using Microsoft.AspNetCore.Builder;

namespace SynetecAssessmentApi.Extension
{
    public static class AppConfig
    {
        public static void Cors(this IApplicationBuilder app)
        {
            app.UseCors(
                options => options.AllowAnyOrigin()
            );
        }

        public static IApplicationBuilder ExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
