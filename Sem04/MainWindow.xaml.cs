using System.Windows;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Sem04
{
    public partial class MainWindow : Window
    {
        SqlConnection connection = new SqlConnection(
            "Data Source=192.168.18.65;Initial Catalog=Neptuno;User ID=sa;Password=VeryStr0ngP@ssw0rd;TrustServerCertificate=True;"
        );

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnProductos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("ListarProductos", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dgProductos.ItemsSource = table.DefaultView;
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnCategorias_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("ListarCategorias", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dgCategorias.ItemsSource = table.DefaultView;
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnFiltrarFechas_Click(object sender, RoutedEventArgs e)
        {
            if (dpFechaInicio.SelectedDate == null || dpFechaFin.SelectedDate == null)
            {
                MessageBox.Show("Selecciona ambas fechas.");
                return;
            }

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("ListarDetallesPorFechas", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FechaInicio", dpFechaInicio.SelectedDate.Value);
                command.Parameters.AddWithValue("@FechaFin", dpFechaFin.SelectedDate.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dgDetalles.ItemsSource = table.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnBuscarProveedores_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("BuscarProveedores", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@NombreContacto", txtContacto.Text.Trim());
                command.Parameters.AddWithValue("@Ciudad", txtCiudad.Text.Trim());

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dgProveedores.ItemsSource = table.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar proveedores: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
