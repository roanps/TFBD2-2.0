namespace VoeMais.Models
{
    public class VooPoltrona
    {
        public int Id { get; set; }

        public int VooId { get; set; }
        public Voo Voo { get; set; }

        public int PoltronaId { get; set; }
        public Poltrona Poltrona { get; set; }

        public PoltronaStatus Status { get; set; } = PoltronaStatus.Livre;
    }
}
