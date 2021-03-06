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
        #region GLOBAL VARIABLES
        private readonly ApplicationContext _context;
        private List<MoedasCotacao> MoedasCotacaoList { get; set; } = new List<MoedasCotacao>();
        private string PathRead = @"C:\Wipro\TesteWipro";
        private readonly CsvConfiguration Config = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            Delimiter = ";",
            Encoding = Encoding.UTF8,
            HeaderValidated = null,
            MissingFieldFound = null
        };
        public CotacaosController(ApplicationContext context)
        {
            _context = context;
        }
        #endregion

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
            List<Cotacao> listCotacao = await _context.Cotacao.ToListAsync();
            List<DadosMoeda> MoedaList = GetDadosMoeda();
            List<DadosCotacao> dadosCotacaoList = GetDadosCotacao();

            if (listCotacao.Count > 0 && MoedaList.Count > 0)
            {
                foreach (var item in listCotacao)
                {
                    DadosMoeda DadosMoeda = MoedaList.FirstOrDefault(m => m.DATA_REF >= item.Data_Inicio && m.DATA_REF <= item.Data_Fim);
                    if (DadosMoeda != null)
                    {
                        MoedasCotacao MoedasCotacao = new MoedasCotacao
                        {
                            Moeda = DadosMoeda.ID_MOEDA,
                            Periodo = DadosMoeda.DATA_REF.ToShortDateString()
                        };
                        MoedasCotacao.Cod_Cotacao = new MoedasCotacao().tabelaDeParaList.FirstOrDefault(x => x.Key == DadosMoeda.ID_MOEDA).Value;
                        MoedasCotacao.VlrCotacao = dadosCotacaoList.FirstOrDefault(d => d.cod_cotacao == MoedasCotacao.Cod_Cotacao).vlr_cotacao;
                        MoedasCotacaoList.Add(MoedasCotacao);
                    }
                }
                ConvertListToCSV(MoedasCotacaoList);
                return listCotacao.LastOrDefault();
            }
            return NotFound();
        }
        #endregion 

        #region GET AND CONVERT DADOSMOEDA.CSV FILE TO LIST
        public List<DadosMoeda> GetDadosMoeda()
        {
            using (StreamReader reader = new StreamReader(PathRead + @"\DadosMoeda.CSV"))
            {
                if (reader != null)
                {
                    using CsvReader csv = new CsvReader(reader, Config);
                    {
                        if (csv != null)
                            return csv.GetRecords<DadosMoeda>().ToList();
                    }
                }

            }
            return new List<DadosMoeda>();
        }
        #endregion

        #region GET AND CONVERT DADOSCOTACAO.CSV FILE TO LIST
        public List<DadosCotacao> GetDadosCotacao()
        {
            using (StreamReader reader = new StreamReader(PathRead + @"\DadosCotacao.CSV"))
            {
                if (reader != null)
                {
                    using CsvReader csv = new CsvReader(reader, Config);
                    {
                        if (csv != null)
                            return csv.GetRecords<DadosCotacao>().ToList();
                    }
                }
            }
            return new List<DadosCotacao>();
        }
        #endregion

        #region  CONVERT LIST TO MOEDASCOTACAO.CSV FILE 
        public void ConvertListToCSV(List<MoedasCotacao> moedasCotacaos)
        {
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\MoedaCotacao";
            bool exists = Directory.Exists(Path);
            if (!exists)
                Directory.CreateDirectory(Path);

            using (StreamWriter file = new StreamWriter(Path + @"\MoedaDataCotacao.CSV"))
            {
                foreach (MoedasCotacao item in moedasCotacaos)
                    file.WriteLine(string.Format("{0},{1},{2}", item.Moeda, item.Periodo, item.VlrCotacao));
            }
        }
        #endregion
    }
}
