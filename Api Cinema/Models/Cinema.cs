using System.ComponentModel.DataAnnotations;

namespace CinemaApi.Models
{
    public class Cinema
    {
        [Key]
        public int CinemaId { get; set; }
        public string? Nome { get; set; }
        public string? cnpj { get; set; } 
        public List<Filme> Filmes { get; set; } = new();
        
    }
}
