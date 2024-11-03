using CinemaApi.Data;
using CinemaApi.Models;
using Microsoft.EntityFrameworkCore;

public static class FilmeEndpoints
{
    public static void MapFilmeEndpoints(this WebApplication app)
    {
        // Obter todos os filmes
        app.MapGet("/filmes", async (CinemaContext db) => await db.Filme.ToListAsync());

        // Obter um filme específico pelo ID
        app.MapGet("/filmes/{id}", async (int id, CinemaContext db) =>
            await db.Filme.FindAsync(id) is Filme filme ? Results.Ok(filme) : Results.NotFound());

        // Adicionar um novo filme
        app.MapPost("/filmes", async (Filme filme, CinemaContext db) =>
        {
            db.Filme.Add(filme);
            await db.SaveChangesAsync();
            return Results.Created($"/filmes/{filme.FilmeId}", filme);
        });

        // Atualizar um filme existente
        app.MapPut("/filmes/{id}", async (int id, Filme filmeUpdate, CinemaContext db) =>
        {
            var filme = await db.Filme.FindAsync(id);
            if (filme is null) return Results.NotFound();

            filme.Nome = filmeUpdate.Nome;
            filme.Genero = filmeUpdate.Genero;
            filme.Ano = filmeUpdate.Ano;
            filme.CinemaId = filmeUpdate.CinemaId;

            await db.SaveChangesAsync();
            return Results.Ok(filme);
        });

        // Deletar um filme pelo ID
        app.MapDelete("/filmes/{id}", async (int id, CinemaContext db) =>
        {
            var filme = await db.Filme.FindAsync(id);
            if (filme is null) return Results.NotFound();

            db.Filme.Remove(filme);
            await db.SaveChangesAsync();

            return Results.Ok();
        });
    }
}
