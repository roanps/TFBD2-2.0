using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Models;
using VoeMais.Repositories.Interfaces;

namespace VoeMais.Repositories.Implementations
{
    public class AeroportoRepository : IAeroportoRepository
    {
        private readonly VoeMaisContext _context;

        public AeroportoRepository(VoeMaisContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Aeroporto>> GetAllAsync()
        {
            return await _context.Aeroportos.ToListAsync();
        }

        public async Task<Aeroporto> GetByIdAsync(int id)
        {
            return await _context.Aeroportos.FindAsync(id);
        }

        public async Task AddAsync(Aeroporto aeroporto)
        {
            _context.Aeroportos.Add(aeroporto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Aeroporto aeroporto)
        {
            _context.Aeroportos.Update(aeroporto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Aeroportos.FindAsync(id);
            if (entity != null)
            {
                _context.Aeroportos.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
