namespace CustompgAdmin.Extensions;

public static class CorsAppBuilderExtentions
{
    public static void ConfigureCors(this IApplicationBuilder app)
    {
        app.UseCors("AllowAll");
    }
}
