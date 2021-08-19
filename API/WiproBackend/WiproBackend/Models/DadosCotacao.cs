using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WiproBackend.Models
{
    public class DadosCotacao
    {
        [Column("Vlr_Cotacao", TypeName = "integer)")]
        public int Vlr_Cotacao { get; set; }

        [Column("cod_Cotacao", TypeName = "vachar(10)")]
        public string cod_Cotacao { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Column("dat_cotacao")]
        public DateTime dat_cotacao { get; set; }
    }
}
