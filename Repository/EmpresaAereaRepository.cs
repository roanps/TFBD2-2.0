using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Models;

namespace VoeMais.Repository;

    public class EmpresaAereaRepository : IEmpresaAereaRepository
    {
        private readonly VoeMaisDbContext _context;

        public EmpresaAereaRepository(VoeMaisDbContext context)
        {
            _context = context;
        }

        public async Task Create(EmpresaAerea empresaAerea)
        {
            _context.EmpresasAereas.Add(empresaAerea);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(EmpresaAerea empresaAerea)
        {
            _context.EmpresasAereas.Remove(empresaAerea);
            await _context.SaveChangesAsync();
        }

        public async Task Update(EmpresaAerea empresaAerea)
        {
            _context.EmpresasAereas.Update(empresaAerea);
            await _context.SaveChangesAsync();
        }

        public async Task<EmpresaAerea?> GetById(int id)
        {
            return await _context.EmpresasAereas.FirstOrDefaultAsync(e => e.IdEmpresa == id);
        }

        public async Task<List<EmpresaAerea>> GetAll()
        {
            return await _context.EmpresasAereas.ToListAsync();
        }

        public async Task<List<EmpresaAerea>> GetByNome(string nome)
        {
            return await _context.EmpresasAereas
                .Where(e => e.Nome == nome)
                .ToListAsync();
        }

        public async Task<List<EmpresaAerea>> GetByCnpj(string cnpj)
        {
            return await _context.EmpresasAereas
                .Where(e => e.Cnpj == cnpj)
                .ToListAsync();
        }
    }

