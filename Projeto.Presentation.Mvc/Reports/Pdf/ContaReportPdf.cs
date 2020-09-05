using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Projeto.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Presentation.Mvc.Reports.Pdf
{
    public class ContaReportPdf
    {
        public static byte[] GenerateReport(List<Conta> contas)
        {
            MemoryStream ms = new MemoryStream();
            var pdf = new PdfDocument(new PdfWriter(ms));

            using (var doc = new Document(pdf))
            {
                var titulo = "Relatório de Contas - C# WebDeveloper";
                var subtitulo = "Data de geração: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                var formatacaoTitulo = new Style();
                formatacaoTitulo.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
                formatacaoTitulo.SetFontSize(18);
                formatacaoTitulo.SetTextAlignment(TextAlignment.CENTER);
                formatacaoTitulo.SetFontColor(Color.ConvertRgbToCmyk(new DeviceRgb(72, 61, 139)));

                var formatacaoSubTitulo = new Style();
                formatacaoSubTitulo.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
                formatacaoSubTitulo.SetFontSize(11);
                formatacaoSubTitulo.SetTextAlignment(TextAlignment.CENTER);
                formatacaoSubTitulo.SetFontColor(Color.ConvertRgbToCmyk(new DeviceRgb(72, 61, 139)));

                doc.Add(new Paragraph(new Text(titulo).AddStyle(formatacaoTitulo)));
                doc.Add(new Paragraph(new Text(subtitulo).AddStyle(formatacaoSubTitulo)));

                var table = new Table(6); //6 colunas

                table.AddHeaderCell("Nome da Conta").SetFontSize(10);
                table.AddHeaderCell("Data").SetFontSize(10);
                table.AddHeaderCell("Valor").SetFontSize(10);
                table.AddHeaderCell("Tipo").SetFontSize(10);
                table.AddHeaderCell("Categoria").SetFontSize(10);
                table.AddHeaderCell("Forma de Pagamento").SetFontSize(10);

                foreach (var item in contas)
                {
                    table.AddCell(item.NomeConta).SetFontSize(9);
                    table.AddCell(item.DataConta.ToString("dd/MM/yyyy")).SetFontSize(9);
                    table.AddCell(item.ValorConta.ToString("c")).SetFontSize(9);
                    table.AddCell(item.Tipo.ToString()).SetFontSize(9);
                    table.AddCell(item.Categoria.ToString()).SetFontSize(9);
                    table.AddCell(item.FormaDePagamento.ToString()).SetFontSize(9);
                }

                doc.Add(table);
            }

            return ms.ToArray();
        }
    }
}
