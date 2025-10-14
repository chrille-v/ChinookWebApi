namespace ChinookWebApi.Services

open ChinookWebApi
open ChinookWebApi.Models
open Microsoft.EntityFrameworkCore
open System.Collections.Generic
open Microsoft.Extensions.Logging

type ArtistFunctions = {
    Create: ArtistModel -> Async<ArtistModel option>
    GetAll: unit -> Async<ArtistModel list>
    GetById: int option ->Async<ArtistModel option>
    // Update: int -> ArtistModel -> Async<ArtistModel> 
    Delete: int -> Async<bool>
}

module ArtistService =

    let private toEntity (model: ArtistModel) : ArtistEntity =
        { ArtistId = defaultArg model.ArtistId 0
          Name = model.Name }

    let private toModel (entity: ArtistEntity) : ArtistModel =
        { ArtistId = Some entity.ArtistId
          Name = entity.Name }
    
    let create (context: ChinookContext) : ArtistFunctions =
        {
            Create =
                fun (artist: ArtistModel) ->
                    async {
                        let entity = toEntity artist
                        let! _ = context.Artist.AddAsync(entity).AskTask() |> Async.AwaitTask
                        let! _ = context.SaveChangesAsync() |> Async.AwaitTask
                        return Some (toModel entity)
                    }

            GetAll =
                fun () ->
                    async {
                    // Fetch all artists
                    let! artists = context.Artist.ToListAsync() |> Async.AwaitTask

                    // Map to your model type
                    let models =
                        artists
                        |> Seq.map (fun a -> { ArtistId = a.ArtistId; Name = a.Name })
                        |> Seq.toList

                    return models 
                }

            GetById = 
                fun (id: int option) -> 
                    async {
                        let! artists = 
                            context.Artist
                                .ToListAsync()
                            |> Async.AwaitTask

                        let model = 
                            artists
                            |> Seq.toList
                            |> List.map (fun x -> { ArtistId = x.ArtistId; Name = x.Name })
                            |> List.tryFind (fun x -> x.ArtistId = id)

                        return model
                    }

            Delete = 
                fun (id : int ) ->
                    async {
                        let! entity = context.Artist.FindAsync(id) |> Async.AwaitTask
                        if isNull entity then
                            return false // Nothing to delete
                        else
                            context.Artist.Remove(entity) |> ignore
                            let! _ = context.SaveChangesAsync() |> Async.AwaitTask
                            return true
                    }
                    


        }
