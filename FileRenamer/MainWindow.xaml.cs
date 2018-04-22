using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace FileRenamer
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Modification> Modifications { get; set; } = new ObservableCollection<Modification>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            FixMainWindowRightAndBottomPaddings();
        }

        private void FixMainWindowRightAndBottomPaddings()
        {
            FileRenamerMainWindow.Height += 8;
            FileRenamerMainWindow.Width += 8;
        }

        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();
            openFileDialog.FileNames.ToList().ForEach(x => Modifications.Add(new Modification(x)));
        }

        private void ConvertExtensionToLowercase_Click(object sender, RoutedEventArgs e)
        {
            var isSomethingSelected = Filelist.SelectedItems.Count != 0;
            var selectedModifications = isSomethingSelected ? Filelist.SelectedItems.Cast<Modification>() : Modifications;
            selectedModifications.ToList().ForEach(x => MakeExtensionLow(x.OldName));
        }

        private void MakeExtensionLow(string modificationOldName)
        {
            string newLowExtension = Path.GetExtension(modificationOldName).ToLower();
            string newNameWithLowExtension = Path.ChangeExtension(modificationOldName, newLowExtension);
            Modifications.First(x => x.OldName == modificationOldName).NewName = newNameWithLowExtension;
        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            Modifications.ToList().ForEach(x => RenameFile(x));
            Modifications.Clear();
        }

        private void RenameFile(Modification modification)
        {
            if (!String.IsNullOrEmpty(modification.NewName))
            {
                File.Move(modification.OldName, modification.NewName);
            }
        }
    }
}
