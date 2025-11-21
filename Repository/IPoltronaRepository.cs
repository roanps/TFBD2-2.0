using VoeMais.Models;

namespace VoeMais.Repository;
public interface IPoltronaRepository
{
    Task<List<Poltrona>> GetAll();
    Task<Poltrona?> GetById(int idVoo, string numeroPoltrona);
    Task Create(Poltrona item);
    Task Update(Poltrona item);
    Task Delete(Poltrona item);
}
