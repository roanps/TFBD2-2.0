using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Models;
using VoeMais.Repositories.Interfaces;

namespace VoeMais.Repositories.Implementations
{
    public class EscalaRepository : IEscalaRepository
    {
        private readonly VoeMaisContext _context;

        public EscalaRepository(VoeMaisContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Escala>> GetAllAsync()
        {
            return await _context.Escalas
                .Include(e => e.Aeroporto)
                .Include(e => e.Voo)
                .ToListAsync();
        }

        public async Task<Escala> GetByIdAsync(int id)
        {
            return await _context.Escalas
                .Include(e => e.Aeroporto)
                .Include(e => e.Voo)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Escala escala)
        {
            _context.Escalas.Add(escala);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Escala escala)
        {
            _context.Escalas.Update(escala);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Escalas.FindAsync(id);
            if (entity != null)
            {
                _context.Escalas.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
