namespace VoeMais.Models
{
    public class Passagem
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int VooPoltronaId { get; set; }
        public VooPoltrona VooPoltrona { get; set; }

        public DateTime DataCompra { get; set; }
    }
}
