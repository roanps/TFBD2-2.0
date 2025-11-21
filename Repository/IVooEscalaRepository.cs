using VoeMais.Models;

namespace VoeMais.Repository;
public interface IVooEscalaRepository
{
    Task<List<VooEscala>> GetAll();
    Task<VooEscala?> GetById(int idVoo, int idEscala);
    Task Create(VooEscala item);
    Task Delete(VooEscala item);
    Task DeleteByVoo(int idVoo);

}
