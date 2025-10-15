namespace ChinookWebApi

open Microsoft.EntityFrameworkCore

type ChinookContext(options: DbContextOptions<ChinookContext>) =
    inherit DbContext(options)

    [<DefaultValue>]
    val mutable artist : DbSet<ArtistEntity>

    member this.Artist
        with get() = this.artist
        and set v = this.artist <- v