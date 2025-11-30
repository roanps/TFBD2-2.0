using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Models;
using VoeMais.Repositories.Interfaces;

namespace VoeMais.Repositories.Implementations
{
    public class VooPoltronaRepository : IVooPoltronaRepository
    {
        private readonly VoeMaisContext _context;

        public VooPoltronaRepository(VoeMaisContext context)
        {
            _context = context;
        }

        public async Task<VooPoltrona> GetByIdAsync(int id)
        {
            return await _context.VoosPoltronas
                .Include(vp => vp.Poltrona)
                .Include(vp => vp.Voo)
                .FirstOrDefaultAsync(vp => vp.Id == id);
        }

        public async Task<IEnumerable<VooPoltrona>> GetByVooAsync(int vooId)
        {
            return await _context.VoosPoltronas
                .Where(vp => vp.VooId == vooId)
                .Include(vp => vp.Poltrona)
                .ToListAsync();
        }

        public async Task UpdateAsync(VooPoltrona vooPoltrona)
        {
            _context.VoosPoltronas.Update(vooPoltrona);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(VooPoltrona vooPoltrona)
        {
            _context.VoosPoltronas.Add(vooPoltrona);
            await _context.SaveChangesAsync();
        }

    }
}
