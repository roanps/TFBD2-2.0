using VoeMais.Models;

namespace VoeMais.Repository;
public interface IEscalaRepository
{
    Task<List<Escala>> GetAll();
    Task<Escala?> GetById(int id);
    Task Create(Escala item);
    Task Update(Escala item);
    Task Delete(Escala item);
    Task Delete(int id);

}
