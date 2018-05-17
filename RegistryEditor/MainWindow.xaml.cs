using Microsoft.Win32;
using RegistryEditor.Dialogs;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RegistryEditor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            treeView.AddHandler(TreeViewItem.ExpandedEvent, new RoutedEventHandler(ItemExpanded));
            treeView.AddHandler(TreeViewItem.SelectedEvent, new RoutedEventHandler(ItemSelected));

            InitRoot();
        }

        private void InitRoot()
        {
            try
            {
                RegistryKey[] keys = new RegistryKey[]
            {
                Registry.ClassesRoot,
                Registry.CurrentConfig,
                Registry.CurrentUser,
                Registry.LocalMachine,
                Registry.Users
            };

                foreach (var key in keys)
                {
                    TreeViewItem item = new TreeViewItem()
                    {
                        Header = key.Name,
                        Tag = key 
                    };

                    item.Items.Add(null);

                    treeView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ItemSelected(object sender, RoutedEventArgs e)
        {
            try
            {
                TreeViewItem treeViewItem = e.OriginalSource as TreeViewItem;
                RegistryKey selectedRegistryKey = treeViewItem.Tag as RegistryKey;

                listView.Items.Clear();

                foreach (var valueName in selectedRegistryKey.GetValueNames())
                {
                    var registryValue = new RegistryValue()
                    {
                        Name = valueName,
                        Type = selectedRegistryKey.GetValueKind(valueName).ToString(),
                        Value = selectedRegistryKey.GetValue(valueName).ToString(),
                    };

                    listView.Items.Add(registryValue);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ItemExpanded(object sender, RoutedEventArgs e)
        {
            try
            {
                TreeViewItem treeViewItem = e.OriginalSource as TreeViewItem;
                RegistryKey parentKey = treeViewItem.Tag as RegistryKey;

                treeViewItem.Items.Clear();

                foreach (var subKey in parentKey.GetSubKeyNames())
                {
                    RegistryKey childKey = parentKey.OpenSubKey(subKey, true);

                    TreeViewItem item = new TreeViewItem()
                    {
                        Header = childKey.Name.Split('\\').Last(),
                        Tag = childKey,
                    };

                    item.Items.Add(null);

                    treeViewItem.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }

        private RegistryKey GetCurrentRegistryKey()
        {
            TreeViewItem treeViewItem = treeView.SelectedItem as TreeViewItem;

            return treeViewItem == null ? null : treeViewItem.Tag as RegistryKey;
        }

        private void CreateFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistryKey currentRegistryKey = GetCurrentRegistryKey();

                if (currentRegistryKey == null)
                    return;

                AddFolderDialog addValueWindow = new AddFolderDialog(currentRegistryKey);
                addValueWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateStringValue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistryKey currentRegistryKey = GetCurrentRegistryKey();

                if (currentRegistryKey == null)
                    return;

                AddValueDialog dialog = new AddValueDialog(currentRegistryKey, TypeAction.CreateString);
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateBinaryValue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistryKey currentRegistryKey = GetCurrentRegistryKey();

                if (currentRegistryKey == null)
                    return;

                AddValueDialog dialog = new AddValueDialog(currentRegistryKey, TypeAction.CreateBinary);
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateDWORD32Value_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistryKey currentRegistryKey = GetCurrentRegistryKey();

                if (currentRegistryKey == null)
                    return;

                AddValueDialog dialog = new AddValueDialog(currentRegistryKey, TypeAction.CreateDWord);
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateDWORD64Value_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistryKey currentRegistryKey = GetCurrentRegistryKey();

                if (currentRegistryKey == null)
                    return;

                AddValueDialog dialog = new AddValueDialog(currentRegistryKey, TypeAction.CreateQWord);
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateMultiString_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistryKey currentRegistryKey = GetCurrentRegistryKey();

                if (currentRegistryKey == null)
                    return;

                AddValueDialog dialog = new AddValueDialog(currentRegistryKey, TypeAction.CreateMultiString);
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateExpandString_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistryKey currentRegistryKey = GetCurrentRegistryKey();

                if (currentRegistryKey == null)
                    return;

                AddValueDialog dialog = new AddValueDialog(currentRegistryKey, TypeAction.CreateExpandString);
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem treeViewItem = e.OriginalSource as TreeViewItem;
            RegistryKey currentRegistryKey = GetCurrentRegistryKey();

            DeleteValueDialog dialog = new DeleteValueDialog(currentRegistryKey);
            dialog.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var lvi = listView.SelectedItems;
                if (lvi[0] == null)
                {
                    MessageBox.Show("Select at least one item");
                    return;
                }
                
                RegistryValue value = lvi[0] as RegistryValue;
                RegistryKey currentRegistryKey = GetCurrentRegistryKey();

                if (currentRegistryKey == null)
                    return;

                EditValueDialog dialog = new EditValueDialog(currentRegistryKey, value.Name);
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
/*Написать редактор реестра, который аналогичен regedit по интерфейсу
с помощью которого пользователь должен уметь:
1) Создавать/изменять/удалять ключи(разделы)
2) Создавать/изменять/удалять значения


Кто сделал 1е дз - Диспетчер Задач, тогда его дополнить нужно этим решением. 
Кто не сделал(очень плохо) делайте отдельным приложением.

Обязательно соблюдать все правила кодирования.*/