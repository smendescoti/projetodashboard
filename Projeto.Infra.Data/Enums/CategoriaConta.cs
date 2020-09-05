using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Projeto.Infra.Data.Enums
{
    public enum CategoriaConta
    {
        [Display(Name = "Alimentação")]
        Alimentação = 1,

        [Display(Name = "Casa")]
        Casa = 2,

        [Display(Name = "Família")]
        Família = 3,

        [Display(Name = "Farmácia")]
        Farmácia = 4,

        [Display(Name = "Lazer")]
        Lazer = 5,

        [Display(Name = "Trabalho")]
        Trabalho = 6
    }
}
