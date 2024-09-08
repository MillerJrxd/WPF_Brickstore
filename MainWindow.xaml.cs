using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using System.Xml.Linq;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;

namespace LegoDolog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Lego> Legos { get; set; }
        private ICollectionView LegoCollectionView;

        public MainWindow()
        {
            Legos = new ObservableCollection<Lego>();
            InitializeComponent();
            DataContext = this;

            // Set up CollectionView for filtering
            LegoCollectionView = CollectionViewSource.GetDefaultView(Legos);
            LegoCollectionView.Filter = FilterLegoItems;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "BSX Files (*.bsx)|*.bsx|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                LoadLegoItemsFromXaml(openFileDialog.FileName);
            }
        }

        private void LoadLegoItemsFromXaml(string filename)
        {
            Legos.Clear();

            try
            {
                XDocument xaml = XDocument.Load(filename);
                foreach (var elem in xaml.Descendants("Item"))
                {
                    try
                    {
                        Legos.Add(new Lego(
                            elem.Element("ItemID")?.Value ?? string.Empty,
                            elem.Element("ItemName")?.Value ?? string.Empty,
                            elem.Element("CategoryName")?.Value ?? string.Empty,
                            elem.Element("ColorName")?.Value ?? string.Empty,
                            int.Parse(elem.Element("Qty")?.Value ?? "0")
                        ));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error parsing item: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load file: {ex.Message}");
            }
        }

    }
}