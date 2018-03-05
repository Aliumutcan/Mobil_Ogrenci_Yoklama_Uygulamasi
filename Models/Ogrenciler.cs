namespace Mobil_Ogrenci_Yoklama_Uygulamasi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("aliumutc_Blog.Ogrenciler")]
    public partial class Ogrenciler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ogrenciler()
        {
            Disardan_Baglanan = new HashSet<Disardan_Baglanan>();
            Donemlik_Yoklama = new HashSet<Donemlik_Yoklama>();
            Yoklamadakiler = new HashSet<Yoklamadakiler>();
            Dersler = new HashSet<Dersler>();
        }

        public int id { get; set; }

        [StringLength(150)]
        public string adi_soyadi { get; set; }

        [StringLength(15)]
        public string no { get; set; }

        [StringLength(50)]
        public string kullanici_adi { get; set; }

        [StringLength(30)]
        public string sifre { get; set; }

        [StringLength(100)]
        public string fakulte { get; set; }

        [StringLength(100)]
        public string bolum { get; set; }

        public byte? sinif { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Disardan_Baglanan> Disardan_Baglanan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donemlik_Yoklama> Donemlik_Yoklama { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yoklamadakiler> Yoklamadakiler { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dersler> Dersler { get; set; }
    }
}
