using Projeto.Infra.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Infra.Data.Dtos
{
    public class TotalTipoContaDto
    {
        public TipoConta Tipo { get; set; }
        public decimal Total { get; set; }
    }
}
