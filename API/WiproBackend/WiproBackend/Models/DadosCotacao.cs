using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WiproBackend.Models
{
    public class DadosCotacao
    {
        [Column("Vlr_Cotacao", TypeName = "integer)")]
        public string vlr_cotacao { get; set; }

        [Column("cod_cotacao", TypeName = "vachar(10)")]
        public string cod_cotacao { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Column("dat_cotacao")]
        public DateTime dat_cotacao { get; set; }
    }
}
