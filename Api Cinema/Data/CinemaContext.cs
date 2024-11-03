using Microsoft.EntityFrameworkCore;
using CinemaApi.Models;

namespace CinemaApi.Data
{
    public class CinemaContext : DbContext
    {
    public CinemaContext (DbContextOptions<CinemaContext> options)
        : base(options)
    {
    }

    public DbSet<CinemaApi.Models.Cinema> Cinema { get; set; } = default!;
    public DbSet<CinemaApi.Models.Filme> Filme { get; set; } = default!;



    }
}