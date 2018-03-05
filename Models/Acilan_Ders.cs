namespace Mobil_Ogrenci_Yoklama_Uygulamasi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("aliumutc_Blog.Acilan_Ders")]
    public partial class Acilan_Ders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Acilan_Ders()
        {
            Disardan_Baglanan = new HashSet<Disardan_Baglanan>();
            Yoklamadakiler = new HashSet<Yoklamadakiler>();
        }

        public int id { get; set; }

        public int? ders_id { get; set; }

        public int? ogretmen_id { get; set; }

        public DateTime? acilma_tarihi { get; set; }

        public DateTime? kapanma_tarihi { get; set; }

        public bool? durum { get; set; }

        public virtual Dersler Dersler { get; set; }

        public virtual Ogretmenler Ogretmenler { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Disardan_Baglanan> Disardan_Baglanan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yoklamadakiler> Yoklamadakiler { get; set; }
    }
}
