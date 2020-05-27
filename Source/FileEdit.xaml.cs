using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;

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

            textBoxName.Text = data.FileName;
            textBoxDirectory.Text = data.FileDirectory;
        }

        FileData data;
        public bool DialogSaved { get; private set; }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogSaved = false;
            this.Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxName.Text != "" && textBoxDirectory.Text != "")
            {
                data.FileName = textBoxName.Text;
                data.FileDirectory = textBoxDirectory.Text;

                DialogSaved = true;
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
                textBoxDirectory.Text = data.FileDirectory = fileDialog.FileName;
        }
    }
}
