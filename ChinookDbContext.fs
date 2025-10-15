namespace ChinookWebApi

open Microsoft.EntityFrameworkCore

type ChinookContext(options: DbContextOptions<ChinookContext>) =
    inherit DbContext(options)

    [<DefaultValue>]
    val mutable Artist : DbSet<ArtistEntity>

    member this.artist
        with get() = this.Artist
        and set v = this.Artist <- v