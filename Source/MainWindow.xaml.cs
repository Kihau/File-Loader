using Microsoft.Win32;
using System;
using System.IO;
using System.Media;
using System.Windows;

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
            LoadData();
        }

        private DataManager data;

        private readonly string dsfile = "dataset.xml";

        #region BUTTONS
        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                try
                {
                    string scrFile = data.dataSet.FileDataList[listBox.SelectedIndex].FileDirectory;
                    string destFile = textBoxDestination.Text + Path.GetFileName(data.dataSet.FileDataList[listBox.SelectedIndex].FileDirectory);

                    File.Copy(scrFile, destFile, true);
                    SystemSounds.Beep.Play();
                }
                catch
                {
                    MessageBox.Show("Destintion copy path or file directory is incorrect", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("None of the itemes were selected", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            FileData fd = new FileData(null, null);

            if (fileDialog.ShowDialog() == true)
            {
                fd.FileDirectory = fileDialog.FileName;
            }

            if (fd.FileDirectory != null)
            {
                FileEdit editwindow = new FileEdit(fd) { Owner = this };
                editwindow.ShowDialog();

                if (editwindow.DialogSaved)
                {
                    listBox.Items.Add(fd.FileName);
                    data.dataSet.FileDataList.Add(fd);
                }
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                FileEdit editwindow = new FileEdit(data.dataSet.FileDataList[listBox.SelectedIndex]) { Owner = this };
                editwindow.ShowDialog();

                listBox.Items[listBox.SelectedIndex] = data.dataSet.FileDataList[listBox.SelectedIndex].FileName;
            }
            else
            {
                MessageBox.Show("None of the itemes were selected", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                data.dataSet.FileDataList.RemoveAt(listBox.SelectedIndex);
                listBox.Items.RemoveAt(listBox.SelectedIndex);
            }
            else
            {
                MessageBox.Show("None of the itemes were selected", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region MENU ITEMS
        private void MenuItemFileSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Data Files (*.xml)|*.xml";
            fileDialog.FileName = "dataset";

            if (fileDialog.ShowDialog() == true)
            {
                data.Save(fileDialog.FileName, textBoxDestination.Text);
            }
        }

        private void MenuItemFileLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Data Files (*.xml)|*.xml";

            if (fileDialog.ShowDialog() == true)
            {
                data = new DataManager();
                LoadData();
            }
        }
        private void MenuItemFileClear_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear all saves?", "WARNING", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                listBox.Items.Clear();
                data.dataSet.FileDataList.Clear();
            }
        }

        private void MenuItemDestinationClear_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear destination copy path?", "WARNING", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                textBoxDestination.Text = "";
            }
        }

        private void MenuItemDestinationSet_Click(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.FolderBrowserDialog folderBrowser = new System.Windows.Forms.FolderBrowserDialog())
            {
                folderBrowser.ShowDialog();
                if (folderBrowser.SelectedPath != "")
                {
                    textBoxDestination.Text = folderBrowser.SelectedPath + "\\";
                }
            }
        }

        private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Program created by Kihau\n\n           ¯\\_( ツ )_/¯        ", "About program", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region OTHER
        private void Window_Closed(object sender, EventArgs e)
        {
            data.Save(dsfile, textBoxDestination.Text);
        }

        private void LoadData()
        {
            try
            {
                data.Load(dsfile);
                listBox.Items.Clear();

                data.dataSet.FileDataList.ForEach(x => listBox.Items.Add(x.FileName));
                textBoxDestination.Text = data.dataSet.Destination;
            }
            catch
            {
                MessageBox.Show("Could not load data from data file", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
