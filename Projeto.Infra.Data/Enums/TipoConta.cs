using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Projeto.Infra.Data.Enums
{
    public enum TipoConta
    {
        [Display(Name = "Receita (Contas a Receber)")]
        Receita = 1,

        [Display(Name = "Despesa (Contas a Pagar)")]
        Despesa = 2
    }
}
