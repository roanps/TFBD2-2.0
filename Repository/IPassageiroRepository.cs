using VoeMais.Models;

namespace VoeMais.Repository;
public interface IPassageiroRepository
{
    Task<List<Passageiro>> GetAll();
    Task<Passageiro?> GetById(int id);
    Task Create(Passageiro item);
    Task Update(Passageiro item);
    Task Delete(Passageiro item);
}
