namespace ChinookWebApi.Services

open System.Collections.Generic
open ChinookWebApi.Models

type ArtistFunctions = {
    GetAll: Async<ICollection<ArtistModel>>
    GetById: int -> Async<Option<ArtistModel>>
    Create: ArtistModel -> Async<ArtistModel>
    Update: int -> ArtistModel -> Async<ArtistModel> 
    Delete: int -> bool
}