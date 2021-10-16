using Microsoft.EntityFrameworkCore;
using RegistroDetalle2.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroDetalle2.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Permisos> Permisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permisos>().HasData(new Permisos
            {
                PermisoId = 1,
                Nombre = "Administrador",
                Descripcion = "Permiso para ser administrador del sistema"
            }) ;

            modelBuilder.Entity<Permisos>().HasData(new Permisos
            {
                PermisoId = 2,
                Nombre = "Usuario",
                Descripcion = "Permiso para entrar como usuario al sistema"
            });

            modelBuilder.Entity<Permisos>().HasData(new Permisos
            {
                PermisoId = 3,
                Nombre = "Invitado",
                Descripcion = "Permiso para entrar como invitado del sistema"
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = DATA\RolesDetalle2.db");
        }
    }
}
