using System;
using System.Diagnostics;
using System.Linq;
using VoeMais.Data;
using VoeMais.Models;

public static class DbInitializer
{
    public static void Initialize(VoeMaisDbContext context)
    {
        Debug.WriteLine("===> SEED: Inicializando o banco...");

        // Garante que o banco existe
        context.Database.EnsureCreated();

        // =========================
        // EMPRESA_AEREA
        // =========================
        if (!context.EmpresasAereas.Any())
        {
            var empresas = new[]
            {
                new EmpresaAerea { Nome = "Azul Linhas Aéreas", Telefone = "0800123456", Cnpj = "12345678000190" },
                new EmpresaAerea { Nome = "Latam Airlines",      Telefone = "0800987654", Cnpj = "98765432000112" },
                new EmpresaAerea { Nome = "Gol Linhas Aéreas",   Telefone = "0800555333", Cnpj = "11222333000144" }
            };

            context.EmpresasAereas.AddRange(empresas);
            context.SaveChanges();
        }

        // =========================
        // AEROPORTO
        // =========================
        if (!context.Aeroportos.Any())
        {
            var aeroportos = new[]
            {
                new Aeroporto
                {
                    Nome = "Aeroporto Internacional de São Paulo",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    CodigoIATA = "GRU"
                },
                new Aeroporto
                {
                    Nome = "Aeroporto Internacional do Rio de Janeiro",
                    Cidade = "Rio de Janeiro",
                    Estado = "RJ",
                    CodigoIATA = "GIG"
                },
                new Aeroporto
                {
                    Nome = "Aeroporto Internacional de Brasília",
                    Cidade = "Brasília",
                    Estado = "DF",
                    CodigoIATA = "BSB"
                }
            };

            context.Aeroportos.AddRange(aeroportos);
            context.SaveChanges();
        }

        // =========================
        // AERONAVE
        // =========================
        if (!context.Aeronaves.Any())
        {
            var aeronaves = new[]
            {
                new Aeronave
                {
                    IdEmpresa = 1,
                    ModeloAeronave = "Boeing 737",
                    DataFabricacao = new DateTime(2015, 5, 20),
                    NumeroPoltronas = 180,
                    NumeroMaximoTripulantes = 6,
                    CapacidadeMaximaCombustivel = 26000,
                    CapacidadeMaximaVoo = 5600
                },
                new Aeronave
                {
                    IdEmpresa = 2,
                    ModeloAeronave = "Airbus A320",
                    DataFabricacao = new DateTime(2018, 3, 15),
                    NumeroPoltronas = 150,
                    NumeroMaximoTripulantes = 5,
                    CapacidadeMaximaCombustivel = 24000,
                    CapacidadeMaximaVoo = 6100
                },
                new Aeronave
                {
                    IdEmpresa = 1,
                    ModeloAeronave = "Boeing 777",
                    DataFabricacao = new DateTime(2012, 11, 10),
                    NumeroPoltronas = 396,
                    NumeroMaximoTripulantes = 10,
                    CapacidadeMaximaCombustivel = 181000,
                    CapacidadeMaximaVoo = 9700
                }
            };

            context.Aeronaves.AddRange(aeronaves);
            context.SaveChanges();
        }

        // =========================
        // VOO
        // =========================
        if (!context.Voos.Any())
        {
            var voos = new[]
            {
                new Voo
                {
                    IdAeronave = 1,
                    IdAeroportoOrigem = 1,
                    IdAeroportoDestino = 2,
                    Partida = new DateTime(2024, 7, 1, 10, 0, 0),
                    Chegada = new DateTime(2024, 7, 1, 12, 0, 0)
                },
                new Voo
                {
                    IdAeronave = 2,
                    IdAeroportoOrigem = 2,
                    IdAeroportoDestino = 3,
                    Partida = new DateTime(2024, 7, 2, 14, 0, 0),
                    Chegada = new DateTime(2024, 7, 2, 16, 30, 0)
                },
                new Voo
                {
                    IdAeronave = 3,
                    IdAeroportoOrigem = 3,
                    IdAeroportoDestino = 1,
                    Partida = new DateTime(2024, 7, 3, 9, 0, 0),
                    Chegada = new DateTime(2024, 7, 3, 11, 45, 0)
                }
            };

            context.Voos.AddRange(voos);
            context.SaveChanges();
        }

        // =========================
        // ESCALA
        // =========================
        if (!context.Escalas.Any())
        {
            var escalas = new[]
            {
                new Escala
                {
                    IdAeroportoEscala = 2, // Rio
                    ChegadaEscala = new DateTime(2024, 7, 1, 11, 0, 0),
                    SaidaEscala = new DateTime(2024, 7, 1, 15, 0, 0)
                },
                new Escala
                {
                    IdAeroportoEscala = 3, // Brasília
                    ChegadaEscala = new DateTime(2024, 7, 2, 17, 0, 0),
                    SaidaEscala = new DateTime(2024, 7, 2, 18, 30, 0)
                },
                new Escala
                {
                    IdAeroportoEscala = 1, // São Paulo
                    ChegadaEscala = new DateTime(2024, 7, 3, 10, 0, 0),
                    SaidaEscala = new DateTime(2024, 7, 3, 12, 0, 0)
                }
            };

            context.Escalas.AddRange(escalas);
            context.SaveChanges();
        }

        // =========================
        // VOO_ESCALA (junção)
        // =========================
        if (!context.VooEscalas.Any())
        {
            var vooEscalas = new[]
            {
                new VoeMais.Models.VooEscala { IdVoo = 1, IdEscala = 1 },
                new VoeMais.Models.VooEscala { IdVoo = 2, IdEscala = 2 },
                new VoeMais.Models.VooEscala { IdVoo = 3, IdEscala = 3 }
            };

            context.VooEscalas.AddRange(vooEscalas);
            context.SaveChanges();
        }

        // =========================
        // PASSAGEIRO
        // =========================
        if (!context.Passageiros.Any())
        {
            var passageiros = new[]
            {
                new Passageiro
                {
                    Nome = "João Silva",
                    DataNascimento = new DateTime(1990, 5, 15),
                    Sexo = "Masculino",
                    Cpf = "12345678900",
                    Rg = "12345678901",
                    EstadoCivil = "Solteiro",
                    Nacionalidade = "Brasileiro",
                    MalaDeMao = false,
                    MalaParaDespache = false,
                    Preferencial = false,
                    Email = "josesilva@email.com",
                    Telefone = "11999999999"
                },
                new Passageiro
                {
                    Nome = "Maria Oliveira",
                    DataNascimento = new DateTime(1985, 8, 22),
                    Sexo = "Feminino",
                    Cpf = "98765432100",
                    Rg = "98765432101",
                    EstadoCivil = "Casada",
                    Nacionalidade = "Brasileira",
                    MalaDeMao = true,
                    MalaParaDespache = true,
                    Preferencial = false,
                    Email = "mariaoliveira@email.com",
                    Telefone = "11988888888"
                },
                new Passageiro
                {
                    Nome = "Carlos Souza",
                    DataNascimento = new DateTime(1958, 3, 10),
                    Sexo = "Masculino",
                    Cpf = "45678912300",
                    Rg = "45678912301",
                    EstadoCivil = "Divorciado",
                    Nacionalidade = "Brasileiro",
                    MalaDeMao = false,
                    MalaParaDespache = true,
                    Preferencial = true,
                    Email = "carlosouza@email.com",
                    Telefone = "11977777777"
                }
            };

            context.Passageiros.AddRange(passageiros);
            context.SaveChanges();
        }

        // =========================
        // POLTRONA
        // =========================
        if (!context.Poltronas.Any())
        {
            var poltronas = new[]
            {
                new Poltrona { IdVoo = 1, NumeroPoltrona = "1A", TipoPoltrona = "JANELA",    Lado = "DIREITA",  Ocupada = true  },
                new Poltrona { IdVoo = 1, NumeroPoltrona = "1B", TipoPoltrona = "CORREDOR", Lado = "ESQUERDA", Ocupada = false },
                new Poltrona { IdVoo = 2, NumeroPoltrona = "2A", TipoPoltrona = "MEIO",     Lado = "DIREITA",  Ocupada = true  }
            };

            context.Poltronas.AddRange(poltronas);
            context.SaveChanges();
        }

        // =========================
        // PASSAGEM
        // =========================
        if (!context.Passagens.Any())
        {
            var passagens = new[]
            {
                new Passagem { IdPassageiro = 1, IdVoo = 1, NumeroPoltrona = "1A", CheckinRealizado = true  },
                new Passagem { IdPassageiro = 2, IdVoo = 2, NumeroPoltrona = "2A", CheckinRealizado = false },
                new Passagem { IdPassageiro = 3, IdVoo = 1, NumeroPoltrona = "1B", CheckinRealizado = true  }
            };

            context.Passagens.AddRange(passagens);
            context.SaveChanges();
        }

        Debug.WriteLine("===> SEED FINALIZADO COM SUCESSO!");
    }
}
