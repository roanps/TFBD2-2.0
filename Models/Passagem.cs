namespace VoeMais.Models;

public class Passagem
{
    public int IdPassageiro { get; set; }
    public int IdVoo { get; set; }
    public string? NumeroPoltrona { get; set; }
    public bool CheckinRealizado { get; set; }

    public Passageiro? Passageiro { get; set; }
    public Voo? Voo { get; set; }
    public Poltrona? Poltrona { get; set; }

}
