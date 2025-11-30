using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Models;
using VoeMais.Repositories.Interfaces;

namespace VoeMais.Repositories.Implementations
{
    public class EmpresaAereaRepository : IEmpresaAereaRepository
    {
        private readonly VoeMaisContext _context;

        public EmpresaAereaRepository(VoeMaisContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmpresaAerea>> GetAllAsync()
        {
            return await _context.EmpresasAereas.ToListAsync();
        }

        public async Task<EmpresaAerea> GetByIdAsync(int id)
        {
            return await _context.EmpresasAereas.FindAsync(id);
        }

        public async Task AddAsync(EmpresaAerea empresa)
        {
            _context.EmpresasAereas.Add(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmpresaAerea empresa)
        {
            _context.EmpresasAereas.Update(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.EmpresasAereas.FindAsync(id);
            if (entity != null)
            {
                _context.EmpresasAereas.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
