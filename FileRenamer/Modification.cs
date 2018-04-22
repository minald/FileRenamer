using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FileRenamer
{
    public class Modification : INotifyPropertyChanged
    {
        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        private string oldName;
        public string OldName
        {
            get { return oldName; }
            set
            {
                oldName = value;
                OnPropertyChanged();
            }
        }

        private string newName;
        public string NewName
        {
            get { return newName; }
            set
            {
                newName = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public Modification(string oldName)
        {
            OldName = oldName;
        }

        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
