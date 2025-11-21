namespace VoeMais.Models;

public class VooEscala
{
    public int IdVoo { get; set; }
    public int IdEscala { get; set; }

    public Voo? Voo { get; set; }
    public Escala? Escala { get; set; }
}
