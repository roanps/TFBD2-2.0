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

        public async Task<Passagem> GetByIdAsync(int id)
        {
            return await _context.Passagens
                .Include(p => p.Cliente)
                .Include(p => p.VooPoltrona)
                .ThenInclude(vp => vp.Poltrona)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Passagem>> GetByClienteAsync(int clienteId)
        {
            return await _context.Passagens
                .Where(p => p.ClienteId == clienteId)
                .Include(p => p.VooPoltrona)
                .ToListAsync();
        }

        public async Task AddAsync(Passagem passagem)
        {
            _context.Passagens.Add(passagem);
            await _context.SaveChangesAsync();
        }
    }
}
