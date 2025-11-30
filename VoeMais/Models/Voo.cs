namespace VoeMais.Models
{
    public class Voo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }

        public int AviaoId { get; set; }
        public Aviao Aviao { get; set; }

        public int OrigemId { get; set; }
        public Aeroporto Origem { get; set; }

        public int DestinoId { get; set; }
        public Aeroporto Destino { get; set; }

        public DateTime Partida { get; set; }
        public DateTime Chegada { get; set; }

        public ICollection<Escala> Escalas { get; set; } = new List<Escala>();
        public ICollection<VooPoltrona> Poltronas { get; set; } = new List<VooPoltrona>();

    }
}
