using Microsoft.Win32;
using System;
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

        private void ChooseFiles_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Multiselect = true };
            openFileDialog.ShowDialog();
            openFileDialog.SafeFileNames.ToList().ForEach(x => Modifications.Add(new Modification(x)));
            Modification.FolderName = Path.GetDirectoryName(openFileDialog.FileName);
            FolderName.Content = "Path to the folder : " + Modification.FolderName;
        }

        private void ConvertExtensionToLowercase_Click(object sender, RoutedEventArgs e) => GetPerfomingModifications().ForEach(x => x.MakeExtensionLow());

        private void ChangeNames_Click(object sender, RoutedEventArgs e)
        {
            int counter = 1;
            foreach (var modification in GetPerfomingModifications())
            {
                var number = IsNumerate.IsChecked == true ? (counter++).ToString() : String.Empty;
                modification.NewName = NameTemplate.Text + number + Path.GetExtension(modification.OldName);
            }
        }

        private void Clean_Click(object sender, RoutedEventArgs e) => GetPerfomingModifications().ForEach(x => Modifications.Remove(x));

        private void Rename_Click(object sender, RoutedEventArgs e) => Modifications.ToList().ForEach(x => x.TryApply());

        #endregion

        #region Helping functions

        private List<Modification> GetPerfomingModifications()
        {
            var isSomethingSelected = ModificationsListView.SelectedItems.Count != 0;
            var perfomingModifications = isSomethingSelected ? ModificationsListView.SelectedItems.Cast<Modification>() : Modifications;
            return perfomingModifications.ToList();
        }

        #endregion
    }
}
