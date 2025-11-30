using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Models;
using VoeMais.Repositories.Interfaces;

namespace VoeMais.Repositories.Implementations
{
    public class AviaoRepository : IAviaoRepository
    {
        private readonly VoeMaisContext _context;

        public AviaoRepository(VoeMaisContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Aviao>> GetAllAsync()
        {
            return await _context.Avioes
                .Include(a => a.EmpresaAerea)
                .Include(a => a.Poltronas)
                .ToListAsync();
        }

        public async Task<Aviao> GetByIdAsync(int id)
        {
            return await _context.Avioes
                .Include(a => a.EmpresaAerea)
                .Include(a => a.Poltronas)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Aviao aviao)
        {
            _context.Avioes.Add(aviao);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Aviao aviao)
        {
            _context.Avioes.Update(aviao);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Avioes.FindAsync(id);
            if (entity != null)
            {
                _context.Avioes.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
