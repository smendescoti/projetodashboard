using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //validações
using Projeto.Infra.Data.Enums;

namespace Projeto.Presentation.Mvc.Models
{
    public class ContaEdicaoModel
    {
        public string Id { get; set; } //campo oculto

        [MinLength(3, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe o nome da conta.")]
        public string NomeConta { get; set; }

        [Required(ErrorMessage = "Informe a data da conta.")]
        public string DataConta { get; set; }

        [Required(ErrorMessage = "Informe o valor da conta.")]
        public string ValorConta { get; set; }

        [MinLength(6, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(500, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe as observações sobre a conta.")]
        public string Observacoes { get; set; }

        [Required(ErrorMessage = "Selecione a categoria da conta.")]
        public CategoriaConta? Categoria { get; set; }

        [Required(ErrorMessage = "Selecione o tipo da conta.")]
        public TipoConta? Tipo { get; set; }

        [Required(ErrorMessage = "Selecione a forma de pagamento.")]
        public FormaDePagamento? FormaDePagamento { get; set; }
    }
}
