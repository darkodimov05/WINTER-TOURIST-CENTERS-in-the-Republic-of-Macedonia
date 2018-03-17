using System;
using System.Collections.Generic;
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
using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime.Portal;
using Esri.ArcGISRuntime.WebMap;

namespace PortalApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ArcGISPortal ArcGISOnline;
        private ArcGISPortalItem SelectedPortalItem;
        private const int PageLimit = 10;
        private int PageStart = 1;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            PageStart = 1;
            PerformSearch();
        }

        private async void PerformSearch()
        {
            try
            {
                // if first run, get a ref to the portal (ArcGIS Online)
                if (this.ArcGISOnline == null)
                {
                  
                   // create the uri for the portal 
                    var portalUri = new Uri("https://www.arcgis.com/sharing/rest");
                    // create the portal 
                    this.ArcGISOnline = await ArcGISPortal.CreateAsync(portalUri);
                }

                // create a variable to store search results (collection of portal items)
                IEnumerable<ArcGISPortalItem> results = null;
                if (this.ItemTypeComboBox.SelectedValue.ToString() == "Basemap")
                {
                    // basemap search returns web maps that contain the basemap layer
                    var basemapSearch = await this.ArcGISOnline.ArcGISPortalInfo.SearchBasemapGalleryAsync();
                    results = basemapSearch.Results;
                }
                else
                {
                    // get the search term and item type provided in the UI
                    var searchTerm = this.SearchTextBox.Text.Trim();
                    var searchItem = this.ItemTypeComboBox.SelectedValue.ToString();

                    // build a query that searches for the specified type
                    // ('web mapping application' is excluded from the search since 'web map' will match those item types too)
                    var queryString = string.Format("\"{0}\" type:(\"{1}\" NOT \"web mapping application\")", searchTerm, searchItem);
                    // create a SearchParameters object, set options
                    var searchParameters = new SearchParameters()
                    {
                        QueryString = queryString,
                        SortField = "avgrating",
                        SortOrder = QuerySortOrder.Descending,
                        Limit = PageLimit,
                        StartIndex = (PageStart-1)*PageLimit + 1
                    };
                    // execute the search
                    var itemSearch = await this.ArcGISOnline.SearchItemsAsync(searchParameters);
                    results = itemSearch.Results;
                }

                // show the results in the list box
                this.ResultListBox.ItemsSource = results;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error searching portal");
            } finally
            {
                this.CurrentPageNumber.Text = String.Format("Page {0}", this.PageStart);
            }
        }
        private void ResetUI()
        {
            // clear UI controls
            this.SnippetTextBlock.Text = "";
            this.ThumbnailImage.Source = null;
            this.ShowMapButton.IsEnabled = false;
        }
        public void ResultList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // clear UI controls
            this.ResetUI();
            // store the currently selected portal item 
            this.SelectedPortalItem = this.ResultListBox.SelectedItem as ArcGISPortalItem;
            if (this.SelectedPortalItem == null) { return; }

            // show the portal item snippet (brief description) in the UI
            if (!string.IsNullOrEmpty(this.SelectedPortalItem.Snippet))
            {
                this.SnippetTextBlock.Text = this.SelectedPortalItem.Snippet;
            }
            // show a thumbnail for the selected portal item (if there is one)
            if (this.SelectedPortalItem.ThumbnailUri != null)
            {
                var src = new BitmapImage(this.SelectedPortalItem.ThumbnailUri);
                this.ThumbnailImage.Source = src;
            }
            // enable the show map button when a web map portal item is chosen
            this.ShowMapButton.IsEnabled = (this.SelectedPortalItem.Type == ItemType.WebMap);
        }

        private async void ShowMapButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // create a web map from the selected portal item
                var webMap = await WebMap.FromPortalItemAsync(this.SelectedPortalItem);
                // load the web map into a web map view model
                var webMapVM = await WebMapViewModel.LoadAsync(webMap, this.ArcGISOnline);
                // show the web map view model's map in the page's map view control
                this.MyMapView.Map = webMapVM.Map;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PrevPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (PageStart > 1)
            {
                PageStart--;
                PerformSearch();
            }

            if (PageStart == 1)
                PrevPageButton.IsEnabled = false;
            else
                PrevPageButton.IsEnabled = true;
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            PageStart++;
            PerformSearch();
        }    
    }
}
