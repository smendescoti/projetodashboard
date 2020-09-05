using Projeto.Infra.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Infra.Data.Entities
{
    public class Conta
    {
        public Guid Id { get; set; }
        public string NomeConta { get; set; }        
        public DateTime DataConta { get; set; }
        public decimal ValorConta { get; set; }
        public string Observacoes { get; set; }

        public CategoriaConta Categoria { get; set; }
        public TipoConta Tipo { get; set; }
        public FormaDePagamento FormaDePagamento { get; set; }
    }
}
