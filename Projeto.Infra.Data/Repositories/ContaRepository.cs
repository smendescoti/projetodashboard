using Projeto.Infra.Data.Contexts;
using Projeto.Infra.Data.Contracts;
using Projeto.Infra.Data.Dtos;
using Projeto.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto.Infra.Data.Repositories
{
    public class ContaRepository : BaseRepository<Conta>, IContaRepository
    {
        private readonly DataContext dataContext;

        public ContaRepository(DataContext dataContext)
            : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public override List<Conta> GetAll()
        {
            return dataContext.Contas
                    .OrderByDescending(c => c.DataConta)
                    .ToList();
        }

        public List<Conta> GetByDatas(DateTime dataMin, DateTime dataMax)
        {
            return dataContext.Contas
                    .Where(c => c.DataConta >= dataMin && c.DataConta <= dataMax)
                    .OrderByDescending(c => c.DataConta)
                    .ToList();
        }

        public List<TotalCategoriaDto> GroupBySumCategoria()
        {
            return dataContext
                    .Contas
                    .GroupBy(c => c.Categoria)
                    .Select(
                        c => new TotalCategoriaDto
                        {
                            Categoria = c.Key,
                            Total = c.Sum(c => c.ValorConta)
                        }
                    ).ToList();
        }

        public List<TotalFormaDePagamentoDto> GroupBySumFormaDePagamento()
        {
            return dataContext
                   .Contas
                   .GroupBy(c => c.FormaDePagamento)
                   .Select(
                       c => new TotalFormaDePagamentoDto
                       {
                           FormaDePagamento = c.Key,
                           Total = c.Sum(c => c.ValorConta)
                       }
                   ).ToList();
        }

        public List<TotalTipoContaDto> GroupBySumTipo()
        {
            return dataContext
                   .Contas
                   .GroupBy(c => c.Tipo)
                   .Select(
                       c => new TotalTipoContaDto
                       {
                           Tipo = c.Key,
                           Total = c.Sum(c => c.ValorConta)
                       }
                   ).ToList();
        }
    }
}
