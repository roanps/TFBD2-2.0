namespace VoeMais.Models
{
    public class Poltrona
    {
        public int Id { get; set; }
        public string Numero { get; set; }

        public int AviaoId { get; set; }
        public Aviao Aviao { get; set; }
    }
}
