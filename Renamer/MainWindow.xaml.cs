using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Renamer
{
    public partial class MainWindow : Window
    {
        public List<Modification> Modifications = new List<Modification>();

        public MainWindow()
        {
            InitializeComponent();
            FixMainWindowRightAndBottomMargins();
        }

        private void FixMainWindowRightAndBottomMargins()
        {
            RenamerMainWindow.Height += 8;
            RenamerMainWindow.Width += 8;
        }

        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true
            };

            if(openFileDialog.ShowDialog() ?? false)
            {
                foreach(var pathWithFilename in openFileDialog.FileNames)
                {
                    FileInfo file = new FileInfo(pathWithFilename);
                    Filelist.Items.Add(new Modification(pathWithFilename, ""));
                }
            }
        }

        private void ConvertExtensionToLowercase_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = Filelist.SelectedIndex;
            if(selectedIndex == -1)
            {
                MessageBox.Show("Please, choose file.", "File isn't chosen", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var modification = Filelist.Items.GetItemAt(selectedIndex) as Modification;
                modification.NewName = Path.ChangeExtension(modification.OldName, ".jpg");

                Filelist.Items.Refresh();
            }
        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class Modification
    {
        public string OldName { get; set; }

        public string NewName { get; set; }

        public Modification(string oldName, string newName)
        {
            OldName = oldName;
            NewName = newName;
        }
    }
}
