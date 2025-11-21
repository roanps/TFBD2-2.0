namespace VoeMais.Models;

public class Poltrona
{
    public int IdVoo { get; set; }
    public string NumeroPoltrona { get; set; } = string.Empty;
    public string TipoPoltrona { get; set; } = string.Empty;
    public string Lado { get; set; } = string.Empty;
    public bool Ocupada { get; set; }

    public Voo? Voo { get; set; }
}
