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

namespace RegistryEditor.Dialogs
{
    /// <summary>
    /// Interaction logic for EditValueDialog.xaml
    /// </summary>
    public partial class EditValueDialog : Window
    {
        private RegistryKey registryKey = null;
        private string name = null;

        public EditValueDialog(RegistryKey registryKey, string name)
        {
            InitializeComponent();

            this.registryKey = registryKey;
            this.name = name;
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string value = this.tbValue.Text;

                registryKey.SetValue(name, value);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }
    }
}