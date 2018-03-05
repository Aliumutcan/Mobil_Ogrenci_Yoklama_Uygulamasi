namespace Mobil_Ogrenci_Yoklama_Uygulamasi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("aliumutc_Blog.Yoklamadakiler")]
    public partial class Yoklamadakiler
    {
        public int id { get; set; }

        public int? ogrenci_id { get; set; }

        public int? acilan_ders_id { get; set; }

        [StringLength(30)]
        public string mac_adres { get; set; }

        public virtual Acilan_Ders Acilan_Ders { get; set; }

        public virtual Ogrenciler Ogrenciler { get; set; }
    }
}
