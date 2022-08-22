using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace XwaSalvageYardScoreEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public string ScoreTableFileName { get; set; } = string.Empty;

        public ObservableCollection<SalvageYardCraftScore> ScoreTable { get; set; } = new ObservableCollection<SalvageYardCraftScore>();

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Open XWAHS.TBL file",
                CheckFileExists = true,
                AddExtension = true,
                DefaultExt = ".tbl",
                Filter = "TBL files|*.tbl"
            };

            if (dialog.ShowDialog(this) == true)
            {
                try
                {
                    this.ScoreTable = SalvageYardScoresTable.Read(dialog.FileName);
                    this.ScoreTableFileName = dialog.FileName;
                    this.DataContext = null;
                    this.DataContext = this;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.ToString(), ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Title = "Save XWAHS.TBL file",
                AddExtension = true,
                DefaultExt = ".tbl",
                Filter = "TBL files|*.tbl",
                FileName = System.IO.Path.GetFileName(this.ScoreTableFileName),
                InitialDirectory = System.IO.Path.GetDirectoryName(this.ScoreTableFileName)
            };

            if (dialog.ShowDialog(this) == true)
            {
                try
                {
                    SalvageYardScoresTable.Write(this.ScoreTable, dialog.FileName);
                    this.ScoreTableFileName = dialog.FileName;
                    this.DataContext = null;
                    this.DataContext = this;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.ToString(), ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SalvageYardScoresTable.SortByModelIndex(this.ScoreTable);
                SalvageYardScoresTable.SortByTime(this.ScoreTable);
                this.DataContext = null;
                this.DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NewCraftButton_Click(object sender, RoutedEventArgs e)
        {
            SalvageYardScoresTable.AddNew(this.ScoreTable);
        }

        private void DeleteCraftButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button.Tag as SalvageYardCraftScore;

            if (item != null)
            {
                this.ScoreTable.Remove(item);
            }
        }
    }
}
