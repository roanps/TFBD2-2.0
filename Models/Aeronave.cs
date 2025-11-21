namespace VoeMais.Models;

public class Aeronave
{
    public int IdAeronave { get; set; }
    public int IdEmpresa { get; set; }

    public string ModeloAeronave { get; set; } = string.Empty;
    public DateTime DataFabricacao { get; set; }
    public int NumeroPoltronas { get; set; }
    public int NumeroMaximoTripulantes { get; set; }
    public int CapacidadeMaximaCombustivel { get; set; }
    public int CapacidadeMaximaVoo { get; set; }

    public EmpresaAerea? EmpresaAerea { get; set; }
    public ICollection<Voo> Voos { get; set; } = new List<Voo>();
}
