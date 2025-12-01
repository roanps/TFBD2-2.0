using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Models;
using VoeMais.Repositories.Interfaces;

namespace VoeMais.Repositories.Implementations
{
    public class PassagemRepository : IPassagemRepository
    {
        private readonly VoeMaisContext _context;

        public PassagemRepository(VoeMaisContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Passagem>> GetAllAsync()
        {
            return await _context.Passagens
                .Include(p => p.Cliente)
                .Include(p => p.VooPoltrona)
                    .ThenInclude(vp => vp.Voo)
                .ToListAsync();
        }

        public async Task<Passagem?> GetByIdAsync(int id)
        {
            return await _context.Passagens
                .Include(p => p.Cliente)
                .Include(p => p.VooPoltrona)
                    .ThenInclude(vp => vp.Voo)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Passagem>> GetByClienteAsync(int clienteId)
        {
            return await _context.Passagens
                .Include(p => p.VooPoltrona)
                    .ThenInclude(vp => vp.Voo)
                .Where(p => p.ClienteId == clienteId)
                .ToListAsync();
        }

        public async Task AddAsync(Passagem passagem)
        {
            _context.Passagens.Add(passagem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Passagem passagem)
        {
            _context.Passagens.Update(passagem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var passagem = await _context.Passagens.FindAsync(id);
            if (passagem != null)
            {
                _context.Passagens.Remove(passagem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
