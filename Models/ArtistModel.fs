namespace ChinookWebApi.Models

open System.Text.Json;
open System.Text.Json.Serialization;

type ArtistModel = {
    // [<JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)>]
    ArtistId: int option
    Name: string
}