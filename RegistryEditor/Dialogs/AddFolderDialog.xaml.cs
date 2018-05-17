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
using Microsoft.Win32;

namespace RegistryEditor
{
    /// <summary>
    /// Interaction logic for Add_.xaml
    /// </summary>
    public partial class AddFolderDialog : Window
    {
        private RegistryKey registryKey = null;

        public AddFolderDialog(RegistryKey registryKey)
        {
            InitializeComponent();

            this.registryKey = registryKey;
        }        

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string value = this.textBox.Text;

            registryKey.CreateSubKey(value);

            this.Close();
        }
    }
}