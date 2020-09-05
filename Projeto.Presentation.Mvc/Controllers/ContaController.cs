using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Infra.Data.Contracts;
using Projeto.Infra.Data.Entities;
using Projeto.Infra.Data.Enums;
using Projeto.Presentation.Mvc.Models;
using Projeto.Presentation.Mvc.Reports.Excel;
using Projeto.Presentation.Mvc.Reports.Pdf;

namespace Projeto.Presentation.Mvc.Controllers
{
    public class ContaController : Controller
    {
        private readonly IContaRepository contaRepository;

        public ContaController(IContaRepository contaRepository)
        {
            this.contaRepository = contaRepository;
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ContaCadastroModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var conta = new Conta
                    {
                        Id = Guid.NewGuid(),
                        NomeConta = model.NomeConta,
                        DataConta = DateTime.Parse(model.DataConta),
                        ValorConta = decimal.Parse(model.ValorConta),
                        Observacoes = model.Observacoes,
                        Categoria = (CategoriaConta) model.Categoria,
                        FormaDePagamento = (FormaDePagamento) model.FormaDePagamento,
                        Tipo = (TipoConta) model.Tipo
                    };

                    contaRepository.Create(conta);

                    TempData["MensagemSucesso"] = $"Conta {conta.NomeConta}, cadastrada com sucesso.";
                    ModelState.Clear();
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        public IActionResult Consulta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Consulta(ContaConsultaModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var dataInicio = DateTime.Parse(model.DataInicio);
                    var dataTermino = DateTime.Parse(model.DataTermino);

                    //executar a consulta..
                    model.ListagemContas = contaRepository.GetByDatas(dataInicio, dataTermino);

                    //verificando se algum resultado foi obtido
                    if (model.ListagemContas.Count == 0)
                    {
                        throw new Exception("Nenhum resultado foi obtido para o período selecionado.");
                    }
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            //enviando a model para a página
            return View(model);
        }

        //método para realizar a exclusão de uma conta
        public IActionResult Exclusao(string id)
        {
            try
            {
                //buscar a conta pelo id
                var conta = contaRepository.GetById(Guid.Parse(id));
                //excluindo a conta
                contaRepository.Delete(conta);

                TempData["MensagemSucesso"] = "Conta excluída com sucesso.";
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return RedirectToAction("Consulta");
        }

        //método para abrir a página de edição
        public IActionResult Edicao(string id)
        {
            var model = new ContaEdicaoModel();

            try
            {
                //buscar a conta pelo id..
                var conta = contaRepository.GetById(Guid.Parse(id));

                //transferir os dados da conta para model
                model.Id = conta.Id.ToString();
                model.NomeConta = conta.NomeConta;
                model.Observacoes = conta.Observacoes;
                model.ValorConta = conta.ValorConta.ToString();
                model.DataConta = conta.DataConta.ToString("dd/MM/yyyy");
                model.Categoria = conta.Categoria;
                model.Tipo = conta.Tipo;
                model.FormaDePagamento = conta.FormaDePagamento;
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edicao(ContaEdicaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var conta = new Conta
                    {
                        Id = Guid.Parse(model.Id),
                        NomeConta = model.NomeConta,
                        DataConta = DateTime.Parse(model.DataConta),
                        ValorConta = decimal.Parse(model.ValorConta),
                        Observacoes = model.Observacoes,
                        Categoria = (CategoriaConta)model.Categoria,
                        FormaDePagamento = (FormaDePagamento)model.FormaDePagamento,
                        Tipo = (TipoConta)model.Tipo
                    };

                    contaRepository.Update(conta);

                    TempData["MensagemSucesso"] = $"Conta {conta.NomeConta}, atualizada com sucesso.";
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        public IActionResult Relatorios()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Relatorios(string dataMin, string dataFim, string tipo)
        {
            try
            {
                if (string.IsNullOrEmpty(dataMin) || string.IsNullOrEmpty(dataFim) || string.IsNullOrEmpty(tipo))
                    throw new Exception("Erro. Todos os campos devem estar preenchidos.");

                switch(int.Parse(tipo))
                {
                    case 1: //PDF
                        var pdf = ContaReportPdf.GenerateReport
                           (contaRepository.GetByDatas(DateTime.Parse(dataMin), DateTime.Parse(dataFim)));

                        //DOWNLOAD DO DOCUMENTO PDF..
                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.Headers.Add("content-disposition", "attachment; filename=contas.pdf");
                        Response.Body.WriteAsync(pdf, 0, pdf.Length);
                        Response.Body.Flush();

                        break;

                    case 2: //EXCEL

                        var excel = ContaReportExcel.GenerateReport
                            (contaRepository.GetByDatas(DateTime.Parse(dataMin), DateTime.Parse(dataFim)));

                        //DOWNLOAD DO DOCUMENTO EXCEL..
                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.Headers.Add("content-disposition", "attachment; filename=contas.xlsx");
                        Response.Body.WriteAsync(excel, 0, excel.Length);
                        Response.Body.Flush();

                        break;
                }
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View();
        }

        //método executado por uma função javascript (AJAX)
        public JsonResult ObterTotalPorTipoConta()
        {
            var model = new List<HighChartsModel>();

            //percorrendo a consulta do banco de dados
            foreach (var item in contaRepository.GroupBySumTipo())
            {
                model.Add(
                    new HighChartsModel
                    {
                        Name = item.Tipo.ToString(),
                        Data = new List<decimal>() { item.Total }
                    });
            }

            return Json(model);
        }

        //método executado por uma função javascript (AJAX)
        public JsonResult ObterTotalPorCategoriaConta()
        {
            var model = new List<HighChartsModel>();

            //percorrendo a consulta do banco de dados
            foreach (var item in contaRepository.GroupBySumCategoria())
            {
                model.Add(
                    new HighChartsModel
                    {
                        Name = item.Categoria.ToString(),
                        Data = new List<decimal>() { item.Total }
                    });
            }

            return Json(model);
        }

        //método executado por uma função javascript (AJAX)
        public JsonResult ObterTotalPorFormaDePagamento()
        {
            var model = new List<HighChartsModel>();

            //percorrendo a consulta do banco de dados
            foreach (var item in contaRepository.GroupBySumFormaDePagamento())
            {
                model.Add(
                    new HighChartsModel
                    {
                        Name = item.FormaDePagamento.ToString(),
                        Data = new List<decimal>() { item.Total }
                    });
            }

            return Json(model);
        }

    }
}
