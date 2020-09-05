using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Projeto.Infra.Data.Enums
{
    public enum FormaDePagamento
    {
        [Display(Name = "Dinheiro")]
        Dinheiro = 1,

        [Display(Name = "Cartão de Débito")]
        CartãoDeDebito = 2,

        [Display(Name = "Cartão de Crédito")]
        CartãoDeCredito = 3,

        [Display(Name = "Cheque")]
        Cheque = 4,

        [Display(Name = "Carnê")]
        Carnê = 5
    }
}
