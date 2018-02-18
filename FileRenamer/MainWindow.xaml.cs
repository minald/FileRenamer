using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace FileRenamer
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
            FileRenamerMainWindow.Height += 8;
            FileRenamerMainWindow.Width += 8;
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

        private void ConvertExtensionToLowercase_Click(object sender, RoutedEventArgs e) //TODO: Refactor
        {
            var selectedIndex = Filelist.SelectedIndex;
            if(selectedIndex == -1)
            {
                InitializeNewNames(Filelist.Items.Cast<Modification>());
            }
            else
            {
                InitializeNewNames(Filelist.SelectedItems as IEnumerable<Modification>);
            }
        }

        private void InitializeNewNames(IEnumerable<Modification> modifications)
        {
            foreach(Modification modification in modifications)
            {
                modification.NewName = Path.ChangeExtension(modification.OldName, ".jpg"); //TODO: Add supporting of all types
                Filelist.Items.Refresh();
            }
        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            foreach(Modification modification in Filelist.Items)
            {
                if(!String.IsNullOrEmpty(modification.NewName))
                {
                    File.Move(modification.OldName, modification.NewName);
                }
            }
            Filelist.Items.Clear();
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
