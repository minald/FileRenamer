using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace FileRenamer
{
    public partial class MainWindow : Window
    {
        #region Initialization

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

        #endregion

        #region Handlers

        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Multiselect = true };
            openFileDialog.ShowDialog();
            openFileDialog.SafeFileNames.ToList().ForEach(x => Modifications.Add(new Modification(x)));
            Modification.FolderName = Path.GetDirectoryName(openFileDialog.FileName);
            FolderName.Content = "Path to the folder : " + Modification.FolderName;
        }

        private void ConvertExtensionToLowercase_Click(object sender, RoutedEventArgs e) => GetPerfomingModifications().ForEach(x => MakeExtensionLow(x.OldName));

        private void Clean_Click(object sender, RoutedEventArgs e) => GetPerfomingModifications().ForEach(x => Modifications.Remove(x));

        private void Rename_Click(object sender, RoutedEventArgs e) => Modifications.ToList().ForEach(x => x.TryApply());

        #endregion

        #region Helping functions

        private List<Modification> GetPerfomingModifications()
        {
            var isSomethingSelected = Filelist.SelectedItems.Count != 0;
            var perfomingModifications = isSomethingSelected ? Filelist.SelectedItems.Cast<Modification>() : Modifications;
            return perfomingModifications.ToList();
        }

        private void MakeExtensionLow(string modificationOldName)
        {
            string newLowExtension = Path.GetExtension(modificationOldName).ToLower();
            string newNameWithLowExtension = Path.ChangeExtension(modificationOldName, newLowExtension);
            Modifications.First(x => x.OldName == modificationOldName).NewName = newNameWithLowExtension;
        }

        #endregion
    }
}
