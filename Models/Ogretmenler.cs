namespace Mobil_Ogrenci_Yoklama_Uygulamasi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("aliumutc_Blog.Ogretmenler")]
    public partial class Ogretmenler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ogretmenler()
        {
            Acilan_Ders = new HashSet<Acilan_Ders>();
            Dersler = new HashSet<Dersler>();
        }

        public int id { get; set; }

        [StringLength(150)]
        public string adi_soyadi { get; set; }

        [StringLength(50)]
        public string kullanici_adi { get; set; }

        [StringLength(30)]
        public string sifre { get; set; }

        [StringLength(100)]
        public string fakulte { get; set; }

        [StringLength(100)]
        public string bolum { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Acilan_Ders> Acilan_Ders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dersler> Dersler { get; set; }
    }
}
