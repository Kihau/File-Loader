using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FileLoader
{
    /// <summary>
    /// Interaction logic for FileEdit.xaml
    /// </summary>
    public partial class FileEdit : Window
    {
        public FileEdit(FileData newdata)
        {
            InitializeComponent();
            data = newdata;

            textBoxName.Text = data.fileName;
            textBoxDirectory.Text = data.fileDirectory;
        }

        FileData data;
        public System.Windows.Forms.DialogResult Result { get; private set; }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = System.Windows.Forms.DialogResult.No;
            this.Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxName.Text != "" && textBoxDirectory.Text != "")
            {
                data.fileName = textBoxName.Text;
                data.fileDirectory = textBoxDirectory.Text;

                Result = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }
            else MessageBox.Show("TextBox fields cannot be empty", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ButtonSave_Click(sender, e);
        }

        private void ButtonBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() == true)
                textBoxDirectory.Text = data.fileDirectory = fileDialog.FileName;
        }
    }
}
