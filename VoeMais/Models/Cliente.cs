namespace VoeMais.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }

        public ICollection<Passagem> Passagens { get; set; } = new List<Passagem>();
    }
}
