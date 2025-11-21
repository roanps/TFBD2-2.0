using VoeMais.Models;

namespace VoeMais.Repository;
public interface IPassagemRepository
{
    Task<List<Passagem>> GetAll();
    Task<Passagem?> GetById(int idPassageiro, int idVoo);
    Task Create(Passagem item);
    Task Update(Passagem item);
    Task Delete(Passagem item);
}
