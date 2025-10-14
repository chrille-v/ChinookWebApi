namespace ChinookWebApi
#nowarn "20"
open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.EntityFrameworkCore
open Microsoft.Extensions.Logging
open ChinookWebApi.Services

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        builder.Services.AddDbContext<ChinookContext>(fun options ->
            options.UseSqlServer(builder.Configuration.GetConnectionString("LocalSql")) |> ignore
        )

        builder.Services.AddScoped<ArtistFunctions>(fun sp ->
            let context = sp.GetRequiredService<ChinookContext>()
            ArtistService.create context
        ) |> ignore

        builder.Services.AddControllers()
        
        builder.Services.AddEndpointsApiExplorer()    // Needed for Swagger
        builder.Services.AddSwaggerGen()              // Adds Swagger generator

        let app = builder.Build()

        if app.Environment.IsDevelopment() then
            app.UseSwagger() |> ignore
            app.UseSwaggerUI() |> ignore // <-- Enables the Swagger UI

        

        app.UseHttpsRedirection()

        app.UseAuthorization()
        app.MapControllers()

        app.Run()

        exitCode
