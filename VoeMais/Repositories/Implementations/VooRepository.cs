using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Models;
using VoeMais.Repositories.Interfaces;

namespace VoeMais.Repositories.Implementations
{
    public class VooRepository : IVooRepository
    {
        private readonly VoeMaisContext _context;

        public VooRepository(VoeMaisContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Voo>> GetAllAsync()
        {
            return await _context.Voos
                .Include(v => v.Origem)
                .Include(v => v.Destino)
                .Include(v => v.Aviao)
                .ToListAsync();
        }

        public async Task<Voo> GetByIdAsync(int id)
        {
            return await _context.Voos
                .Include(v => v.Aviao)
                .Include(v => v.Origem)
                .Include(v => v.Destino)
                .Include(v => v.Escalas)
                .Include(v => v.Poltronas)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task AddAsync(Voo voo)
        {
            _context.Voos.Add(voo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Voo voo)
        {
            _context.Voos.Update(voo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Voos.FindAsync(id);
            if (entity != null)
            {
                _context.Voos.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
