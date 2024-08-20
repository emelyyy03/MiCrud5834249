using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MiCrud5834249
{
    [Table("distancia")]
    public class Distancia
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }
        [Column("x1")]
        public string? X1 { get; set; }
        [Column("x2")]
        public string? X2 { get; set; }
        [Column("y1")]
        public string? Y1 { get; set; }
        [Column("y2")]
        public string? Y2 { get; set; }

       

        [Column("distanciaa")]

        public string? Distanciaa { get; set; }

    }
}
