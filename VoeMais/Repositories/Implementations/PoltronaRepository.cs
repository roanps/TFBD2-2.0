using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Models;
using VoeMais.Repositories.Interfaces;

namespace VoeMais.Repositories.Implementations
{
    public class PoltronaRepository : IPoltronaRepository
    {
        private readonly VoeMaisContext _context;

        public PoltronaRepository(VoeMaisContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Poltrona>> GetAllAsync()
        {
            return await _context.Poltronas.ToListAsync();
        }

        public async Task<Poltrona> GetByIdAsync(int id)
        {
            return await _context.Poltronas.FindAsync(id);
        }

        public async Task AddAsync(Poltrona poltrona)
        {
            _context.Poltronas.Add(poltrona);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Poltrona poltrona)
        {
            _context.Poltronas.Update(poltrona);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Poltronas.FindAsync(id);
            if (entity != null)
            {
                _context.Poltronas.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
