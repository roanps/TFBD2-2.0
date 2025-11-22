namespace VoeMais.Models;

public class Voo
{
    public int IdVoo { get; set; }
    public int IdAeronave { get; set; }
    public int IdAeroportoOrigem { get; set; }
    public DateTime Partida { get; set; }
    public int IdAeroportoDestino { get; set; }
    public DateTime Chegada { get; set; }

    public Aeronave? Aeronave { get; set; }
    public Aeroporto? Origem { get; set; }
    public Aeroporto? Destino { get; set; }

    public ICollection<VooEscala>? VoosEscalas { get; set; }
    public ICollection<Poltrona> Poltronas { get; set; } = new List<Poltrona>();
    public ICollection<Passagem> Passagens { get; set; } = new List<Passagem>();
}
