using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiproBackend.DataService;
using WiproBackend.Models;

namespace WiproBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotacaosController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private static List<DadosMoeda> moedaList { get; set; }
        private static List<DadosCotacao> cotacaoList { get; set; }

        private string PathFile = @"C:\Wipro\TesteWipro\";

        private CsvConfiguration config = new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";", Encoding = Encoding.UTF8 };
        public CotacaosController(ApplicationContext context)
        {
            _context = context;
        }

        #region POST: api/Cotacaos
        [HttpPost]
        public async Task AddItemFila(List<Cotacao> cotacao)
        {
            try
            {
                if (cotacao.Count > 0)
                {
                    _context.Cotacao.AddRange(cotacao);
                    await _context.SaveChangesAsync();
                }
                else
                    new Exception("Nenhum dado foi informado!");
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        #endregion POST: api/Cotacaos

        #region GET: api/Cotacaos
        [HttpGet]
        public async Task<ActionResult<Cotacao>> GetItemFila()
        {
            DadosMoeda();
            List<Cotacao> listCotacao = await _context.Cotacao.ToListAsync();
            if (listCotacao.Count > 0)
                return listCotacao.LastOrDefault();
            else
                return NotFound();
        }
        #endregion GET: api/Cotacaos
        public void DadosMoeda()
        {
            using (StreamReader reader = new StreamReader(PathFile + "DadosMoeda.CSV"))
            {
                if (reader != null)
                {
                    using CsvReader csv = new CsvReader(reader, config);
                    {
                        if (csv != null)
                            moedaList = csv.GetRecords<DadosMoeda>().ToList();
                    }
                }

            }
            CreateCSVFile();
        }
        public void DadosCotacao()
        {
            using (StreamReader reader = new StreamReader(PathFile + "DadosCotacao.CSV"))
            {
                if (reader != null)
                {
                    using CsvReader csv = new CsvReader(reader, config);
                    {
                        if (csv != null)
                            cotacaoList = csv.GetRecords<DadosCotacao>().ToList();
                    }
                }
            }

        }
        public void CreateCSVFile()
        {
            //System.IO.File.WriteAllLines(@"C:\Wipro\TesteWipro\cotacaoList.CSV", moedaList.Select(x => string.Join(", ", x)));
            //foreach (var obj in moedaList)
            //{
            //    var sb = new StringBuilder();
            //    var line = "";
            //    foreach (var prop in info)
            //    {
            //        line += prop.GetValue(obj, null) + "; ";
            //    }
            //    line = line.Substring(0, line.Length - 2);
            //    sb.AppendLine(line);
            //    TextWriter sw = new StreamWriter(finalPath, true);
            //    sw.Write(sb.ToString());
            //    sw.Close();
            //}
            //byte[] result;
            //using (var memoryStream = new MemoryStream())
            //{
            //    using (var streamWriter = new StreamWriter(memoryStream))
            //    {
            //        using (var csvWriter = new CsvWriter(streamWriter, config))
            //        {
            //            csvWriter.WriteRecords(cotacaoList);
            //            streamWriter.Flush();
            //            result = memoryStream.ToArray();
            //        }
            //    }
            //}

        }
    }
}
