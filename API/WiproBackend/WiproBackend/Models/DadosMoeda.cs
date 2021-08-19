using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WiproBackend.Models
{
    public class DadosMoeda
    {
        [Column("id_moeda", TypeName = "vachar(10)")]
        public string ID_MOEDA { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Column("data_ref")]
        public DateTime DATA_REF { get; set; }
    }
}
