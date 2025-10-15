namespace ChinookWebApi

open Microsoft.EntityFrameworkCore
open System.ComponentModel.DataAnnotations
open System.ComponentModel.DataAnnotations.Schema

[<CLIMutable>]
type ArtistEntity = {
    [<Key>]
    ArtistId: int
    Name: string
}