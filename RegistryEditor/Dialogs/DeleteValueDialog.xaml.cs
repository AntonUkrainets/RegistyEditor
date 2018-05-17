using Microsoft.Win32;
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

namespace RegistryEditor
{
    /// <summary>
    /// Interaction logic for DeleteValueDialog.xaml
    /// </summary>
    public partial class DeleteValueDialog : Window
    {
        private RegistryKey registryKey = null;
        public DeleteValueDialog(RegistryKey registryKey)
        {
            InitializeComponent();

            this.registryKey = registryKey;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string value = this.textBox.Text;

                registryKey.DeleteSubKeyTree(value, true);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}