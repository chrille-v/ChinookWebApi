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
            let! (result: ArtistModel option) = service.GetById(id)

            match result with
            | Some artist -> return this.Ok(artist) :> IActionResult
            | None -> return this.NotFound() :> IActionResult
        }

    [<HttpDelete("{id}")>]
    member this.Delete(id: int) =
        task {
            let! result = service.Delete(id) |> Async.StartAsTask

            match result with
            | true -> this.Ok($"Artist with id {id} was deleted.") |> ignore
            | false -> this.NotFound() |> ignore
        }