using Projeto.Infra.Data.Dtos;
using Projeto.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Infra.Data.Contracts
{
    public interface IContaRepository : IBaseRepository<Conta>
    {
        List<Conta> GetByDatas(DateTime dataMin, DateTime dataMax);

        List<TotalCategoriaDto> GroupBySumCategoria();
        List<TotalFormaDePagamentoDto> GroupBySumFormaDePagamento();
        List<TotalTipoContaDto> GroupBySumTipo();
    }
}
