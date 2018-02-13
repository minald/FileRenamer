using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace Renamer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FixRightAndBottomMargins();
        }

        private void FixRightAndBottomMargins()
        {
            RenamerMainWindow.Height += 8;
            RenamerMainWindow.Width += 8;
        }

        private void chooseFile_Click(object sender, RoutedEventArgs e)
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
                    //file.Extension;
                    
                    filelist.Items.Add(new Modification(pathWithFilename, ""));
                }
            }
        }

        private void change_Click(object sender, RoutedEventArgs e)
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
