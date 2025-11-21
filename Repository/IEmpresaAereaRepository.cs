using VoeMais.Models;

namespace VoeMais.Repository;
public interface IEmpresaAereaRepository
{
    Task<List<EmpresaAerea>> GetAll();
    Task<EmpresaAerea?> GetById(int id);
    Task Create(EmpresaAerea empresa);
    Task Update(EmpresaAerea empresa);
    Task Delete(EmpresaAerea empresa);
}
