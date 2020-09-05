using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //validações
using Projeto.Infra.Data.Entities;

namespace Projeto.Presentation.Mvc.Models
{
    public class ContaConsultaModel
    {
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public string DataInicio { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        public string DataTermino { get; set; }

        //propriedade de saida de dados
        public List<Conta> ListagemContas { get; set; }
    }
}
