using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppChamaGas.Models
{
    public class Foto_MD
    {
        [PrimaryKey,AutoIncrement,NotNull]
        public int id { get; set; }

        [NotNull]
        public byte[] fotoArray { get; set; }

        [NotNull]
        public string PathGaleria { get; set; }

        [NotNull]
        public string PathInterno{ get; set; }

    }
}
