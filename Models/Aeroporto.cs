namespace VoeMais.Models;

public class Aeroporto
{
    public int IdAeroporto { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string CodigoIATA { get; set; } = string.Empty;

    public ICollection<Voo> VoosOrigem { get; set; } = new List<Voo>();
    public ICollection<Voo> VoosDestino { get; set; } = new List<Voo>();
    public ICollection<Escala> Escalas { get; set; } = new List<Escala>();
}
