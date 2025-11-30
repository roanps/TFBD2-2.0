namespace VoeMais.Models
{
    public class Escala
    {
        public int Id { get; set; }

        public int VooId { get; set; }
        public Voo Voo { get; set; }

        public int AeroportoId { get; set; }
        public Aeroporto Aeroporto { get; set; }

        public DateTime Chegada { get; set; }
        public DateTime Partida { get; set; }
    }
}
