using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WiproBackend.Models
{
    public class MoedasCotacao
    {
        [Column("moeda")]
        public string Moeda { get; set; }
        [Column("Vlr_Cotacao", TypeName = "integer)")]
        public string VlrCotacao { get; set; }

        [Column("periodo")]
        public string Periodo { get; set; }

        [Column("cod_cotacao")]
        public string Cod_Cotacao { get; set; }

        public Dictionary<string, string> tabelaDeParaList =
         new Dictionary<string, string>()
         {
             { "KES", "1" },
             { "USD", "2" },
             { "JPY", "9" },
             { "BOB", "5" },
             { "EUR", "20" },
             { "CAD", "25" },
         };
    }
}
