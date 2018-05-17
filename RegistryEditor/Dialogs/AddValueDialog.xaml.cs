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
    /// Логика взаимодействия для EditValueDialog.xaml
    /// </summary>
    public partial class AddValueDialog : Window
    {
        private RegistryKey registryKey = null;
        private TypeAction typeAction;

        public AddValueDialog(RegistryKey registryKey, TypeAction valueType)
        {
            InitializeComponent();

            this.registryKey = registryKey;
            this.typeAction = valueType;
        }

        //public AddValueDialog(RegistryKey registryKey, string valueName, object value, TypeAction valueType)
        //{
            //InitializeComponent();

            //this.registryKey = registryKey;
            //this.typeAction = valueType;

            //this.tbValueName.Text = valueName;
            //this.tbValue.Text = value.ToString();
        //}

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistryValueKind valueKind = GetRegistryValueKind();

                object value = null;

                string valueName = this.tbValueName.Text;

                if (valueKind == RegistryValueKind.String
                    || valueKind == RegistryValueKind.ExpandString)
                {
                    value = this.tbValue.Text;
                }
                else if (valueKind == RegistryValueKind.MultiString)
                {
                    value = new string[] { this.tbValue.Text };
                }
                else if (valueKind == RegistryValueKind.Binary)
                {
                    string hex = this.tbValue.Text;

                    value = Enumerable.Range(0, hex.Length)
                                        .Where(x => x % 2 == 0)
                                        .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                                        .ToArray();
                }
                else if (valueKind == RegistryValueKind.DWord
                    || valueKind == RegistryValueKind.QWord)
                {
                    value = decimal.Parse(this.tbValue.Text);
                }

                registryKey.SetValue(valueName, value, valueKind);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private RegistryValueKind GetRegistryValueKind()
        {
            if (typeAction == TypeAction.CreateString)
                return RegistryValueKind.String;

            else if (typeAction == TypeAction.CreateBinary)
                return RegistryValueKind.Binary;

            else if (typeAction == TypeAction.CreateDWord)
                return RegistryValueKind.DWord;

            else if (typeAction == TypeAction.CreateQWord)
                return RegistryValueKind.QWord;

            else if (typeAction == TypeAction.CreateMultiString)
                return RegistryValueKind.MultiString;

            else if (typeAction == TypeAction.CreateExpandString)
                return RegistryValueKind.ExpandString;

            return RegistryValueKind.None;
        }
    }
}