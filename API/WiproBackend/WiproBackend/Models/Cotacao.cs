using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WiproBackend.Models
{
    public class Cotacao
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idcotacao", TypeName = "bigint")]
        public long IDCotacao { get; set; }

        [Required(ErrorMessage = "Campo   obrigátorio")]
        [Column("moeda", TypeName = "vachar(10)")]
        public string Moeda { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formatoinválido")]
        [Column("data_Inicio")]
        public DateTime Data_Inicio { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [Column("data_fim")]
        public DateTime Data_Fim { get; set; }
    }
}
