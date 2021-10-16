using RegistroDetalle2.BLL;
using RegistroDetalle2.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RegistroDetalle2.UI.Registros
{
    /// <summary>
    /// Interaction logic for rRoles.xaml
    /// </summary>
    public partial class rRoles : Window
    {
        private Roles Rol = new Roles();
        public rRoles()
        {
            InitializeComponent();
            this.DataContext = Rol;

            PermisoComboBox.ItemsSource = PermisosBLL.GetPermisos();
            PermisoComboBox.SelectedValuePath = "PermisoId";
            PermisoComboBox.DisplayMemberPath = "Descripcion";

            
        }

        Permisos permisos = new();

        

        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = Rol;
        }
        private void Limpiar()
        {
            this.Rol = new Roles();
            this.DataContext = Rol;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Roles encontrado = RolesBLL.Buscar(Rol.RolId);

            if (encontrado != null)
            {
                Rol = encontrado;
                Cargar();
            }
            else
            {
                Limpiar();
                MessageBox.Show("Rol no existe en la base de datos", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        int aumento = 0;
        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
           

            Rol.RolesDetalle.Add(new RolesDetalle
            {
                RolId = Rol.RolId,
                PermisoId = (int)PermisoComboBox.SelectedValue,
                esAsignado = (bool)esActivoCheckBox.IsChecked
            });

            permisos.VecesAsignado = aumento++;

            Cargar();
        }

        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
            {
                Rol.RolesDetalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                Cargar();
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Roles esValido = RolesBLL.Buscar(Rol.RolId);

            return (esValido != null);
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (Rol.RolId == 0)
            {
                paso = RolesBLL.Guardar(Rol);
            }
            else
            {
                if (ExisteEnLaBaseDeDatos())
                {
                    paso = RolesBLL.Guardar(Rol);
                }
                else
                {
                    MessageBox.Show("No existe en la base de datos", "ERROR");
                }
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Fallo al guardar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            Roles existe = RolesBLL.Buscar(Rol.RolId);

            if (existe == null)
            {
                MessageBox.Show("No existe el grupo en la base de datos", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                RolesBLL.Eliminar(Rol.RolId);
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
        }
    }
}
