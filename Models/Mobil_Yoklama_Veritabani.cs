namespace Mobil_Ogrenci_Yoklama_Uygulamasi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Mobil_Yoklama_Veritabani : DbContext
    {
        public Mobil_Yoklama_Veritabani()
            : base("name=Mobil_Yoklama_Veritabani2")
        {
        }

        public virtual DbSet<Acilan_Ders> Acilan_Ders { get; set; }
        public virtual DbSet<Dersler> Dersler { get; set; }
        public virtual DbSet<Disardan_Baglanan> Disardan_Baglanan { get; set; }
        public virtual DbSet<Donemlik_Yoklama> Donemlik_Yoklama { get; set; }
        public virtual DbSet<Ogrenciler> Ogrenciler { get; set; }
        public virtual DbSet<Ogretmenler> Ogretmenler { get; set; }
        public virtual DbSet<Yoklamadakiler> Yoklamadakiler { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Acilan_Ders>()
                .HasMany(e => e.Disardan_Baglanan)
                .WithOptional(e => e.Acilan_Ders)
                .HasForeignKey(e => e.acilan_ders_id);

            modelBuilder.Entity<Acilan_Ders>()
                .HasMany(e => e.Yoklamadakiler)
                .WithOptional(e => e.Acilan_Ders)
                .HasForeignKey(e => e.acilan_ders_id);

            modelBuilder.Entity<Dersler>()
                .HasMany(e => e.Acilan_Ders)
                .WithOptional(e => e.Dersler)
                .HasForeignKey(e => e.ders_id);

            modelBuilder.Entity<Dersler>()
                .HasMany(e => e.Donemlik_Yoklama)
                .WithOptional(e => e.Dersler)
                .HasForeignKey(e => e.ders_id);

            modelBuilder.Entity<Dersler>()
                .HasMany(e => e.Ogrenciler)
                .WithMany(e => e.Dersler)
                .Map(m => m.ToTable("ogrenci_dersler", "aliumutc_Blog").MapLeftKey("ders_id").MapRightKey("ogrenci_id"));

            modelBuilder.Entity<Dersler>()
                .HasMany(e => e.Ogretmenler)
                .WithMany(e => e.Dersler)
                .Map(m => m.ToTable("ogretmen_dersler", "aliumutc_Blog").MapLeftKey("ders_id").MapRightKey("ogretmen_id"));

            modelBuilder.Entity<Ogrenciler>()
                .HasMany(e => e.Disardan_Baglanan)
                .WithOptional(e => e.Ogrenciler)
                .HasForeignKey(e => e.ogrenci_id);

            modelBuilder.Entity<Ogrenciler>()
                .HasMany(e => e.Donemlik_Yoklama)
                .WithOptional(e => e.Ogrenciler)
                .HasForeignKey(e => e.ogrenci_id);

            modelBuilder.Entity<Ogrenciler>()
                .HasMany(e => e.Yoklamadakiler)
                .WithOptional(e => e.Ogrenciler)
                .HasForeignKey(e => e.ogrenci_id);

            modelBuilder.Entity<Ogretmenler>()
                .HasMany(e => e.Acilan_Ders)
                .WithOptional(e => e.Ogretmenler)
                .HasForeignKey(e => e.ogretmen_id);
        }
    }
}
