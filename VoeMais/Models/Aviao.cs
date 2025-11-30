namespace VoeMais.Models
{
    public class Aviao
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Prefixo { get; set; }
        public int Capacidade { get; set; }

        public int EmpresaAereaId { get; set; }
        public EmpresaAerea EmpresaAerea { get; set; }

        public ICollection<Poltrona> Poltronas { get; set; } = new List<Poltrona>();
        public ICollection<Voo> Voos { get; set; } = new List<Voo>();

    }
}
