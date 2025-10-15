namespace ChinookWebApi.Services

open ChinookWebApi
open ChinookWebApi.Models
open Microsoft.EntityFrameworkCore
open System.Collections.Generic
open Microsoft.Extensions.Logging

type ArtistFunctions = {
    GetAll: unit -> Async<ArtistModel list>
    GetById: int  ->Async<ArtistModel option>
    // Update: int -> ArtistModel -> Async<ArtistModel> 
    Delete: int -> Async<bool>
}

module ArtistService =
    let create (context: ChinookContext) : ArtistFunctions =
        {
            GetAll =
                fun () ->
                    async {
                    // Fetch all artists
                    let! (artists: List<ArtistEntity>) = context.Artist.ToListAsync() |> Async.AwaitTask

                    // Map to your model type
                    let models: ArtistModel list =
                        artists
                        |> Seq.map (fun a -> { ArtistId = a.ArtistId; Name = a.Name })
                        |> Seq.toList

                    return models 
                }
                
            GetById = 
                fun (id: int) -> 
                    async {
                        let! (artists: List<ArtistEntity>) = context.Artist.ToListAsync() |> Async.AwaitTask

                        let model = 
                            artists
                            |> Seq.toList
                            |> List.map (fun (x: ArtistEntity) -> { ArtistId = x.ArtistId; Name = x.Name })
                            |> List.tryFind (fun (x: ArtistModel) -> x.ArtistId = id)

                        return model
                    }

            Delete = 
                fun (id : int ) ->
                    async {
                        let! artists = 
                            context.Artist
                                .ToListAsync()
                            |> Async.AwaitTask

                        let entity =
                            artists
                            |> Seq.toList
                            |> List.tryFind (fun x -> x.ArtistId = id)

                        match entity with
                        | None -> return false
                        | Some (entity: ArtistEntity) ->
                            context.Artist.Remove(entity) |> ignore
                            let! _ = context.SaveChangesAsync() |> Async.AwaitTask
                            return true
                    }
        }
