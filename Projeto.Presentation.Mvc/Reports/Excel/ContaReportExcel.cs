using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using Projeto.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Presentation.Mvc.Reports.Excel
{
    public class ContaReportExcel
    {
        //método estático para gerar um relatório em formato EXCEL
        //A saida do método é um arquivo para download
        public static byte[] GenerateReport(List<Conta> contas)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            Color white = ColorTranslator.FromHtml("#FFFFFF");
            Color darkGrey = ColorTranslator.FromHtml("#363636");
            Color lightGrey = ColorTranslator.FromHtml("#DCDCDC");

            //criando um arquivo excel atraves do EPPLUS
            using (var excelPackage = new ExcelPackage())
            {
                //criando uma worksheet (planilha)
                var sheet = excelPackage.Workbook.Worksheets.Add("Contas");

                //adicionando um título na planilha
                sheet.Cells["A1"].Value = "Relatório de Contas";

                var titulo = sheet.Cells["A1:F1"];
                titulo.Merge = true;
                titulo.Style.Font.Size = 16;
                titulo.Style.Font.Bold = true;
                titulo.Style.Font.Color.SetColor(white);
                titulo.Style.Fill.PatternType = ExcelFillStyle.Solid;
                titulo.Style.Fill.BackgroundColor.SetColor(darkGrey);
                titulo.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                sheet.Cells["A3"].Value = "Nome da Conta";
                sheet.Cells["B3"].Value = "Data";
                sheet.Cells["C3"].Value = "Valor";
                sheet.Cells["D3"].Value = "Tipo";
                sheet.Cells["E3"].Value = "Categoria";
                sheet.Cells["F3"].Value = "Forma de Pagamento";

                var cabecalho = sheet.Cells["A3:F3"];
                cabecalho.Style.Font.Size = 12;
                cabecalho.Style.Font.Bold = true;
                cabecalho.Style.Font.Color.SetColor(white);
                cabecalho.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cabecalho.Style.Fill.BackgroundColor.SetColor(darkGrey);
                cabecalho.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                int linha = 4;
                foreach (var item in contas)
                {
                    sheet.Cells[$"A{linha}"].Value = item.NomeConta;
                    sheet.Cells[$"B{linha}"].Value = item.DataConta.ToString("dd/MM/yyyy");
                    sheet.Cells[$"C{linha}"].Value = item.ValorConta;                    
                    sheet.Cells[$"D{linha}"].Value = item.Tipo.ToString();
                    sheet.Cells[$"E{linha}"].Value = item.Categoria.ToString();
                    sheet.Cells[$"F{linha}"].Value = item.FormaDePagamento.ToString();

                    var conteudo = sheet.Cells[$"A{linha}:F{linha}"];
                    if(linha % 2 == 0)
                    {
                        conteudo.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        conteudo.Style.Fill.BackgroundColor.SetColor(lightGrey);
                    }                       

                    linha++;
                }

                sheet.Cells["C:C"].Style.Numberformat.Format = "R$#,##0.00";

                var dados = sheet.Cells[$"A3:F{linha - 1}"];
                dados.Style.Border.BorderAround(ExcelBorderStyle.Medium);

                sheet.Cells["A:AZ"].AutoFitColumns();

                //retornando o conteudo em byte[] (arquivo)
                return excelPackage.GetAsByteArray();
            }
        }
    }
}
