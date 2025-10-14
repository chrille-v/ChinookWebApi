namespace ChinookWebApi.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open ChinookWebApi
open ChinookWebApi.Models
open ChinookWebApi.Services

[<ApiController>]
[<Route("[controller]")>]
type ArtistController (logger : ILogger<ArtistController>, service : ArtistFunctions) =
    inherit ControllerBase()

    [<HttpPost>]
    member this.Create(model : ArtistModel) =
        task {
            let! result = service.Create(model)
            match result with
            | Some artist-> this.Ok(artist)
            | None -> this.NotFound() |> ignore
        }

    [<HttpGet>]
    member this.Get() =
        task {
            let! result = service.GetAll()
            // Log how many artists were returned
            return this.Ok(result)
        }
        
    [<HttpGet("{id}")>]
    member this.GetById(id) =
        task {
            let! result = service.GetById(id)
            match result with
            | Some artist -> this.Ok(artist)
            | None -> this.NotFound() |> ignore 
        } 
        
            
        
        
        

