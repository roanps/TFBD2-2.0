namespace VoeMais.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class Escala
{
    public int IdEscala { get; set; }
    public int IdAeroportoEscala { get; set; }
    public DateTime ChegadaEscala { get; set; }
    public DateTime SaidaEscala { get; set; }

    [ForeignKey("IdAeroportoEscala")]
    public Aeroporto AeroportoEscala { get; set; }

    public ICollection<VooEscala> VoosEscalas { get; set; }
}
