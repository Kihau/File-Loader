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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Media;

namespace FileLoader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            data = new DataManager();

            data.Load();
            data.filesData.ForEach(x => listBox.Items.Add(x.fileName));
            textBoxDestination.Text = data.CopyDestination;
        }

        private DataManager data;

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                string scrFile = data.filesData[listBox.SelectedIndex].fileDirectory;
                string destFile = textBoxDestination.Text + System.IO.Path.GetFileName(data.filesData[listBox.SelectedIndex].fileDirectory);
                File.Copy(scrFile, destFile, true);
                SystemSounds.Beep.Play();
            }
            else MessageBox.Show("None of the itemes were selected", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            FileData fd = new FileData(null, null);

            if (fileDialog.ShowDialog() == true)
                fd.fileDirectory = fileDialog.FileName;

            if (fd.fileDirectory != null)
            {
                FileEdit editwindow = new FileEdit(fd);
                editwindow.ShowDialog();

                if (editwindow.DialogSaved)
                {
                    listBox.Items.Add(fd.fileName);
                    data.filesData.Add(fd);
                }
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                FileEdit editwindow = new FileEdit(data.filesData[listBox.SelectedIndex]);
                editwindow.ShowDialog();

                listBox.Items[listBox.SelectedIndex] = data.filesData[listBox.SelectedIndex].fileName;
            }
            else MessageBox.Show("None of the itemes were selected", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                data.filesData.RemoveAt(listBox.SelectedIndex);
                listBox.Items.RemoveAt(listBox.SelectedIndex);
            }
            else MessageBox.Show("None of the itemes were selected", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            data.Save(textBoxDestination.Text);
        }

        private void MenuItemDestination_Click(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.FolderBrowserDialog folderBrowser = new System.Windows.Forms.FolderBrowserDialog())
            {
                folderBrowser.ShowDialog();
                if (folderBrowser.SelectedPath != "")
                    textBoxDestination.Text = folderBrowser.SelectedPath + "\\";
            }
        }

        private void MenuItemClear_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear all saves?", "WARNING", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                listBox.Items.Clear();
                data.filesData.Clear();
            }
        }

        private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("        ¯\\_( ツ )_/¯        ", "About program", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
