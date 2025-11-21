using VoeMais.Models;

namespace VoeMais.Repository;

public interface IAeronaveRepository
{
    Task<List<Aeronave>> GetAll();
    Task<Aeronave?> GetById(int id);
    Task Create(Aeronave item);
    Task Update(Aeronave item);
    Task Delete(Aeronave item);
}
