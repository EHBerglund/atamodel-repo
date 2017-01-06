using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DatamodelBuild.Main;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using FileSystemLibrary;
using System.Threading;
using DatamodelBuild.Exceptions;

namespace DatamodelBuildClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DatamodelBuilder _dmb;
        private String prefix = "Sheet.";
        private String suffix = "xlsx";
        private FileSystemHelper fsl;
        private ObservableCollection<CheckedListItem<FilePath>> savePaths;
        public ObservableCollection<CheckedListItem<FilePath>> SavePaths
        {
            get { return savePaths; }
            set
            {
                savePaths = value;
                OnPropertyChanged("savePaths");
                SheetPaths = getSheetPaths();
            }
        }
        private ObservableCollection<CheckedListItem<FilePath>> sheetPaths;
        public ObservableCollection<CheckedListItem<FilePath>> SheetPaths
        {
            get { return sheetPaths; }
            set
            {
                sheetPaths = value;
                OnPropertyChanged("SheetPaths");
            }
        }

        private String sheetDir;
        public String SheetDir {
            get { return sheetDir; }
            set
            {
                sheetDir = value;
                OnPropertyChanged("SheetDir");
            }
        }

        private Boolean toggleSelectSheetsState;

        public MainWindow()
        {
            InitializeComponent();

            toggleSelectSheetsState = true;
            _dmb = new DatamodelBuilder();
            fsl = new FileSystemHelper();
            parseConfiguration();

            lblSheetDirectory.SetBinding(ContentProperty, new System.Windows.Data.Binding("SheetDir"));
            savePaths = getSavePaths();
            sheetPaths = getSheetPaths();

            DataContext = this;
        }

        private ObservableCollection<CheckedListItem<FilePath>> getSavePaths()
        {
            ObservableCollection<CheckedListItem<FilePath>> oc = new ObservableCollection<CheckedListItem<FilePath>>();
            foreach (String sp in _dmb.getSavePaths())
            {
                CheckedListItem<FilePath> item = new CheckedListItem<FilePath>(new FilePath() { Name = sp }, true);
                oc.Add(item);
            }
            return oc;
        }

        private ObservableCollection<CheckedListItem<FilePath>> getSheetPaths()
        {
            ObservableCollection<CheckedListItem<FilePath>> oc = new ObservableCollection<CheckedListItem<FilePath>>();
            foreach (String sp in fsl.getFileNames(sheetDir, prefix, suffix))
            {
                CheckedListItem<FilePath> item = new CheckedListItem<FilePath>(new FilePath() { Name = sp }, true);
                oc.Add(item);
            }
            return oc;
        }

        async void OnClick_btnBuild(object sender, RoutedEventArgs e)
        {
            bool success = true;
            btnBuild.IsEnabled = false;
            lblStatus.Content = "Building...";
 
            _dmb.Clear();
            foreach (CheckedListItem<FilePath> fp in savePaths)
            {
                if (fp.IsChecked) { _dmb.addSavePath(fp.Item.Name); }
            }
            foreach (CheckedListItem<FilePath> fp in sheetPaths)
            {
                if (fp.IsChecked) { _dmb.addSheet(sheetDir + fp.Item.Name);  }
            }
            try
            {
                await Task.Factory.StartNew(() => Second.LongWork(_dmb), TaskCreationOptions.LongRunning);
            }
            catch (CustomCOMException err)
            {
                success = false;
                ErrorWindow ew = new ErrorWindow();
                ew.setMessage(err);
                ew.Show();
            }
            btnBuild.IsEnabled = true;
            lblStatus.Content = success ? "Finished..." : "Build failed...";
        }

        public void updateBtnBuild(Boolean state)
        {
            btnBuild.IsEnabled = state;
        }
        public void updateLblStatus(String status)
        {
            lblStatus.Content = status;
        }

        void OnClick_btnSheetDirectoryPicker(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                SheetDir = dialog.SelectedPath + "\\";
                SheetPaths = getSheetPaths();
            }
        }
        void OnClick_btnToggleSelectAllSheets(object sender, RoutedEventArgs e)
        {
            if (toggleSelectSheetsState)
            {
                toggleSelectSheetsState = false;
                foreach (CheckedListItem<FilePath> fp in sheetPaths)
                {
                    fp.IsChecked = false; 
                }
            }
            else
            {
                toggleSelectSheetsState = true;
                foreach (CheckedListItem<FilePath> fp in sheetPaths)
                {
                     fp.IsChecked = true; 
                }
            }
            OnPropertyChanged("SheetPaths");
        }

        private void parseConfiguration()
        {
            string[] savePaths = ConfigurationManager.AppSettings["savePaths"].Split(';');

            for (int i = 0; i < savePaths.Length; i++)
            {
                _dmb.addSavePath(savePaths[i]);
            }

            sheetDir = ConfigurationManager.AppSettings["sheetDirectory"];
            _dmb.backupDirectory = ConfigurationManager.AppSettings["backupDirectory"];
        }

        protected virtual void OnPropertyChanged(string name)
        {
            var handler = System.Threading.Interlocked.CompareExchange(ref PropertyChanged, null, null);
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    class Second
    {
        public static void LongWork(DatamodelBuilder dmb)
        {
            try
            {
                dmb.buildModel();
            }
            catch (CustomCOMException e)
            {
                throw e;
            }
        }
    }

    public class CheckedListItem<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isChecked;
        private T item;

        public CheckedListItem()
        {

        }

        public CheckedListItem(T item, bool isChecked=false)
        {
            this.item = item;
            this.isChecked = isChecked;
        }

        public T Item
        {
            get { return item; }
            set
            {
                item = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("item"));
            }
        }

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
            }
        }
    }

    public class FilePath
    {
        public String Name { get; set; }
    }
}
