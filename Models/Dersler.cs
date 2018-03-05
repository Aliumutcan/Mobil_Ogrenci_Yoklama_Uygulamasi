namespace Mobil_Ogrenci_Yoklama_Uygulamasi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("aliumutc_Blog.Dersler")]
    public partial class Dersler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dersler()
        {
            Acilan_Ders = new HashSet<Acilan_Ders>();
            Donemlik_Yoklama = new HashSet<Donemlik_Yoklama>();
            Ogrenciler = new HashSet<Ogrenciler>();
            Ogretmenler = new HashSet<Ogretmenler>();
        }

        public int id { get; set; }

        [StringLength(100)]
        public string ders_adi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Acilan_Ders> Acilan_Ders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donemlik_Yoklama> Donemlik_Yoklama { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ogrenciler> Ogrenciler { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ogretmenler> Ogretmenler { get; set; }
    }
}
