using Microsoft.EntityFrameworkCore;
using RegistroDetalle2.DAL;
using RegistroDetalle2.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RegistroDetalle2.BLL
{
    public class RolesBLL
    {
        public static bool Guardar(Roles rol)
        {
            if (!Existe(rol.RolId))
                return Insertar(rol);
            else
                return Modificar(rol);

        }

        private static bool Insertar(Roles rol)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            Permisos permiso = new();
            try
            {
                contexto.Roles.Add(rol);
                foreach(var Item in rol.RolesDetalle)
                {
                    permiso = contexto.Permisos.Find(Item.PermisoId);
                    permiso.VecesAsignado += 1;
                }


                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;

        }

        private static bool Modificar(Roles rol)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete from RolesDetalle where RolId={rol.RolId}");
                foreach (var item in rol.RolesDetalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }
                contexto.Entry(rol).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;

        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var rol = contexto.Roles.Find(id);

                if (rol != null)
                {
                    contexto.Roles.Remove(rol);
                    paso = contexto.SaveChanges() > 0;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static Roles Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Roles rol = new Roles();

            try
            {
                rol = contexto.Roles.Include(x => x.RolesDetalle).Where(p => p.RolId == id).SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return rol;
        }

        public static List<Roles> GetList(Expression<Func<Roles, bool>> criterio)
        {
            List<Roles> lista = new List<Roles>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Roles.Where(criterio).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }


        public static List<Roles> GetRoles()
        {
            List<Roles> lista = new List<Roles>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Roles.ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }


        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Roles.Any(e => e.RolId == id);
            }

            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }
    }
}
