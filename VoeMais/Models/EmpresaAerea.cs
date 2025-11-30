namespace VoeMais.Models
{
    public class EmpresaAerea
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<Aviao> Avioes { get; set; } = new List<Aviao>();
    }
}
