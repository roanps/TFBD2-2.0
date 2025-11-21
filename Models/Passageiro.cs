namespace VoeMais.Models;

public class Passageiro
{
    public int IdPassageiro { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string Sexo { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Rg { get; set; } = string.Empty;
    public string EstadoCivil { get; set; } = string.Empty;
    public string Nacionalidade { get; set; } = string.Empty;
    public bool MalaDeMao { get; set; }
    public bool MalaParaDespache { get; set; }
    public bool Preferencial { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }

    public ICollection<Passagem> Passagens { get; set; } = new List<Passagem>();
}
