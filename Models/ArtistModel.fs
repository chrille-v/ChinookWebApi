namespace ChinookWebApi.Models

open System.Text.Json;
open System.Text.Json.Serialization;

type ArtistModel = {
    ArtistId: int 
    Name: string
}