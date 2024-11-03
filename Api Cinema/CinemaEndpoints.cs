using CinemaApi.Data;
using CinemaApi.Models;
using Microsoft.EntityFrameworkCore;

public static class CinemaEndpoints
{
    public static void MapCinemaEndpoints(this WebApplication app)
    {
        // Obter todos os cinemas
        app.MapGet("/cinemas", async (CinemaContext db) => 
            await db.Cinema.Include(c => c.Filmes).ToListAsync());

        // Obter um cinema específico pelo ID
        app.MapGet("/cinemas/{id}", async (int id, CinemaContext db) =>
            await db.Cinema.Include(c => c.Filmes).FirstOrDefaultAsync(c => c.CinemaId == id)
            is Cinema cinema ? Results.Ok(cinema) : Results.NotFound());

        // Adicionar um novo cinema
        app.MapPost("/cinemas", async (Cinema cinema, CinemaContext db) =>
        {
            db.Cinema.Add(cinema);
            await db.SaveChangesAsync();
            return Results.Created($"/cinemas/{cinema.CinemaId}", cinema);
        });

        // Atualizar um cinema existente
        app.MapPut("/cinemas/{id}", async (int id, Cinema cinemaUpdate, CinemaContext db) =>
        {
            var cinema = await db.Cinema.FindAsync(id);
            if (cinema is null) return Results.NotFound();

            cinema.Nome = cinemaUpdate.Nome;
            cinema.cnpj = cinemaUpdate.cnpj;
            await db.SaveChangesAsync();

            return Results.Ok(cinema);
        });

        // Deletar um cinema pelo ID
        app.MapDelete("/cinemas/{id}", async (int id, CinemaContext db) =>
        {
            var cinema = await db.Cinema.FindAsync(id);
            if (cinema is null) return Results.NotFound();

            db.Cinema.Remove(cinema);
            await db.SaveChangesAsync();

            return Results.Ok();
        });
    }
}
