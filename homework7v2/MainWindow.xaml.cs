using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace homework7v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString;
        SqlDataAdapter adapter_e;
        SqlDataAdapter adapter_d;
        DataTable employeesTable;
        DataTable departmentsTable;

        public MainWindow()
        {
            InitializeComponent();

            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            employeesGrid.RowEditEnding += employeesGrid_RowEditEnding;
            departmentsGrid.RowEditEnding += departmentsGrid_RowEditEnding;
        }
    

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Employees";
            employeesTable = new DataTable();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter_e = new SqlDataAdapter(command);

                // установка команды на добавление для вызова хранимой процедуры
                adapter_e.InsertCommand = new SqlCommand("sp_InsertEmployee", connection);
                adapter_e.InsertCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = adapter_e.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "ID");
                adapter_e.InsertCommand.Parameters.Add(new SqlParameter("@fullName", SqlDbType.NVarChar, 50, "FullName"));
                adapter_e.InsertCommand.Parameters.Add(new SqlParameter("@department", SqlDbType.NVarChar, 50, "Department"));

                parameter.Direction = ParameterDirection.Output;

                connection.Open();
                adapter_e.Fill(employeesTable);
                employeesGrid.ItemsSource = employeesTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            sql = "SELECT * FROM Departments";
            departmentsTable = new DataTable();
            connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter_d = new SqlDataAdapter(command);

                // установка команды на добавление для вызова хранимой процедуры
                adapter_d.InsertCommand = new SqlCommand("sp_InsertDepartment", connection);
                adapter_d.InsertCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = adapter_d.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "ID");
                adapter_d.InsertCommand.Parameters.Add(new SqlParameter("@nameOfDepartment", SqlDbType.NVarChar, 50, "NameOfDepartment"));
                parameter.Direction = ParameterDirection.Output;
                connection.Open();
                adapter_d.Fill(departmentsTable);
                departmentsGrid.ItemsSource = departmentsTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        private void UpdateDB()
        {
            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter_e);
            adapter_e.Update( employeesTable );
        }

        private void deleteDepBtn_Click(object sender, RoutedEventArgs e)
        {
            if (departmentsGrid.SelectedItems != null)
            {
                for (int i = 0; i < departmentsGrid.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = departmentsGrid.SelectedItems[i] as DataRowView;
                    if (datarowView != null)
                    {
                        DataRow dataRow = (DataRow)datarowView.Row;
                        dataRow.Delete();
                    }
                }
            }
            UpdateDBDep();
        }

        private void deleteEmpButton_Click(object sender, RoutedEventArgs e)
        {
            if (employeesGrid.SelectedItems != null)
            {
                for (int i = 0; i < employeesGrid.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = employeesGrid.SelectedItems[i] as DataRowView;
                    if (datarowView != null)
                    {
                        DataRow dataRow = (DataRow)datarowView.Row;
                        dataRow.Delete();
                    }
                }
            }
            UpdateDB();
        }

        private void updateEmpButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateDB();
        }

        private void updateDepButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateDBDep();
        }

        private void UpdateDBDep()
        {
            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter_d);
            adapter_d.Update(departmentsTable);
        }

        private void employeesGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            UpdateDB();
        }

        private void departmentsGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            UpdateDBDep();
        }
    }
}
