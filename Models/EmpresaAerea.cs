namespace VoeMais.Models;

public class EmpresaAerea
{
    public int IdEmpresa { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;

    public ICollection<Aeronave> Aeronaves { get; set; } = new List<Aeronave>();
}
