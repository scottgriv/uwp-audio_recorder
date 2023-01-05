using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Speak_Out
{
    /// <summary>
    // Created by: Scott Grivner
    // Website: https://www.scottgrivner.com
    /// </summary>
    public sealed partial class MainPage : Page
    {

        MediaPlayer mediaPlayer;
    
        AudioRecorder _audioRecorder;

        DispatcherTimer dispatcherTimer;
        int timesTicked = 1;

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += new RoutedEventHandler(GetFilesAndFoldersButton_Click);
            mediaPlayer = new MediaPlayer();
            
            this._audioRecorder = new AudioRecorder();
           Application.Current.Resources["PointerOverForeground"] = new SolidColorBrush(Colors.WhiteSmoke);
            //   Application.Current.Resources["PointerOverBackground"] = new SolidColorBrush(Colors.Red);
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        /// <summary>
        /// Retrieves all of the recorded audio in the Music Library folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GetFilesAndFoldersButton_Click(object sender, RoutedEventArgs e)
        {
            var queryOptions = new QueryOptions(CommonFileQuery.DefaultQuery, new[] { ".mp3" });
            queryOptions.FolderDepth = FolderDepth.Deep;

            // add descending sort by date created
            queryOptions.SortOrder.Clear();
            SortEntry se = new SortEntry();
            se.PropertyName = "System.DateModified";
            se.AscendingOrder = false;
            queryOptions.SortOrder.Add(se);

            StorageFileQueryResult query = Windows.ApplicationModel.Package.Current.InstalledLocation.CreateFileQueryWithOptions(queryOptions);
            IReadOnlyList<StorageFile> allFiles = await query.GetFilesAsync();

            ViewAudioModel audioModel = new ViewAudioModel();

            //audioModel.audioModelList
            List<ViewAudioModel> audioModelList = new List<ViewAudioModel>();

            int i = 0;
            foreach (var f in allFiles)
            {
                i++;
                MusicProperties properties = await f.Properties.GetMusicPropertiesAsync();
                TimeSpan myTrackDuration = properties.Duration;
                // ListView1.Items.Add( new ViewAudioModel(f.Name, f.DateCreated.ToString()));
                audioModelList.Add(new ViewAudioModel { name = f.Name.Substring(0, f.Name.Length - 4), date = f.DateCreated.ToString(), duration = myTrackDuration.ToString("h'h 'm'm 's's'"), path = f.Path, ID = i, storageFile = f });
            }

            ListView1.ItemsSource = audioModelList;

        }

        /// <summary>
        /// Gets the item that is clicked and plays the audio.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void itemclicked(object sender, ItemClickEventArgs e)
        {
            //Highlight row, get id of row
            try
            {
                Object c = e.ClickedItem;
                ViewAudioModel f = (ViewAudioModel)c;

                var file = f.storageFile;


                if (file != null)
                {
                    var stream = await file.OpenReadAsync();
                    MediaPlayerEle.Source = MediaSource.CreateFromStream(stream, file.ContentType);
                }
            }
            catch (Exception b)
            {
                string a = b.StackTrace;
            }
        }

        /// <summary>
        /// Record button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnRecord_Click(object sender, RoutedEventArgs e)
        {
       
            appBar.IsEnabled = true;
           // appBar.Visibility = Visibility.Visible;

            if (this._audioRecorder.IsRecording)
            {
                this._audioRecorder.StopRecording();
                this.btnRecordImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Record.png"));
                //Stops timer
                dispatcherTimer.Stop();
                //Gets all MP3s for listview
                GetFilesAndFoldersButton_Click(sender, e);

                //Moves to Archive page
                await Task.Delay(TimeSpan.FromSeconds(1));
                Pivot1.SelectedItem = Archive;

                //Resets timer
                dispatcherTimer = null;
                dispatcherTimer = new DispatcherTimer();
                TimerLog.Text = "";
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                timesTicked = 1;
                
            }
            else
            {
                this._audioRecorder.Record();
                this.btnRecordImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Stop.png"));

                dispatcherTimer.Start();
                dispatcherTimer.Tick += dispatcherTimer_Tick;

                appBar.IsEnabled = false;
            }
        }

        void dispatcherTimer_Tick(object sender, object e)
        {
            TimerLog.Text = timesTicked.ToString();
            timesTicked++;
        }

        int lastIndex = 0;

        /// <summary>
        /// Whenever the pivot is changed this is called.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            {
                switch (Pivot1.SelectedIndex)
                {
                    case 0:
                        GetFilesAndFoldersButton_Click(sender, e);
                        break;
                    case 1:
                        GetFilesAndFoldersButton_Click(sender, e);
                        break;

                }
                if (Pivot1.SelectedIndex + 1 == lastIndex)
                {

                    await Task.Delay(50);
                    Pivot1.SelectedIndex = 0;
                }
                lastIndex = Pivot1.SelectedIndex;
            }
        }

        private async void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            this._audioRecorder.Play();
            await this._audioRecorder.PlayFromDisk(Dispatcher);
        }

        /// <summary>
        /// Renames the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TextBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            var a = e.OriginalSource;
            TextBox c = (TextBox)a;

            ViewAudioModel m = (ViewAudioModel)c.DataContext;
            try
            {
                await m.storageFile.RenameAsync(c.Text + ".mp3");
            }
            catch (Exception y)
            {
                Console.Out.WriteLine(y.StackTrace);
            }
        }

        /// <summary>
        /// Deletes the audio file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Object a = e.OriginalSource;
            Button c = (Button)a;

            ViewAudioModel m = (ViewAudioModel)c.DataContext;
     
            if (ListView1.SelectedItems.Count > 0)
            {
                StorageFile filed = m.storageFile;
                if (filed != null)
                {
                    await filed.DeleteAsync();
                }
                int index = ListView1.Items.IndexOf(ListView1.SelectedItem);
           
                ListView1.SelectedItems.Remove(index);
           }

            GetFilesAndFoldersButton_Click(sender, e);
        }

        private void ListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = e.AddedItems?.FirstOrDefault();
            // edit: also get container
            var container = ((ListViewItem)(ListView1.ContainerFromItem(item)));
        }

        private void btnRecord_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            btnRecord.UseSystemFocusVisuals = false;
            btnRecord.Background = new SolidColorBrush(Colors.White);
            if (btnRecord.IsPointerOver)
            {
                btnRecord.Background = new SolidColorBrush(Colors.White);
            }
           // btnRecord.

        }

        private async void ExportButton_Click_1(object sender, RoutedEventArgs e)
        {
            Object a = e.OriginalSource;
            Button c = (Button)a;

            ViewAudioModel m = (ViewAudioModel)c.DataContext;

            try
            {
                var savePicker = new Windows.Storage.Pickers.FileSavePicker();
                savePicker.SuggestedStartLocation =
                    Windows.Storage.Pickers.PickerLocationId.MusicLibrary;
                // Dropdown of file types the user can save the file as
                savePicker.FileTypeChoices.Add("MP3", new List<string>() { ".mp3" });
                savePicker.FileTypeChoices.Add("WAV", new List<string>() { ".wav" });
                // Default file name if the user does not type one in or select a file to replace
                savePicker.SuggestedFileName = "New Audio";
                ///

                var curItem = m.storageFile;
                StorageFile currentMedia = await StorageFile.GetFileFromPathAsync(curItem.Path);
                byte[] buffer;
                Stream stream = await currentMedia.OpenStreamForReadAsync();
                buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, (int)stream.Length);
                savePicker.SuggestedSaveFile = currentMedia;
                savePicker.SuggestedFileName = "New Audio";
                var file = await savePicker.PickSaveFileAsync();
                if (file != null)
                {
                    CachedFileManager.DeferUpdates(file);
                    await FileIO.WriteBytesAsync(file, buffer);
                    await CachedFileManager.CompleteUpdatesAsync(file);
                }

                GetFilesAndFoldersButton_Click(sender, e);
                await new MessageDialog("Export successful", "File Saved Successfully").ShowAsync();

            }
            catch (Exception ac)
            {
                Console.Out.WriteLine(ac.StackTrace);
            }
        }

    }

    public class ViewAudioModel
    {

        public string name { get; set; }
        public string date { get; set; }
        public string duration { get; set; }
        public string path { get; set; }
        public int ID { get; set; }
        public StorageFile storageFile { get; set; }

        public List<ViewAudioModel> audioModelList { get; set; }

        public ViewAudioModel() { }

        public ViewAudioModel(string name, string date, string duration, string path, int ID, StorageFile storageFile)
        {
            this.name = name;
            this.date = date;
            this.duration = duration;
            this.path = path;
            this.ID = ID;
            this.storageFile = storageFile;
        }

        public Windows.Foundation.IAsyncOperation<IReadOnlyList<StorageFile>> GetThis()
        {
            var queryOptions = new QueryOptions(CommonFileQuery.DefaultQuery, new[] { ".mp3" });
            queryOptions.FolderDepth = FolderDepth.Deep;
            var query = Windows.ApplicationModel.Package.Current.InstalledLocation.CreateFileQueryWithOptions(queryOptions);
            var allFiles = query.GetFilesAsync();

            return allFiles;
        }


        public StorageFile GetSpecific(string name)
        {
            var d = GetThis();
            try
            {
                var g = d.GetResults();

                Object obj = new Object();
                List<StorageFile> strlist = new List<StorageFile>();
                for (int i = 0; i < g.Count; i++)
                {

                    obj = g[i];
                    strlist.Add((StorageFile)obj);
                }

                for (int i = 0; i < strlist.Count; i++)
                {
                    if (name == strlist[i].DisplayName)
                    {
                        return strlist[i];
                    }
                }
            }
            catch (Exception v)
            {
                Console.Out.WriteLine(v.StackTrace);
            }
            return null;

        }

    }


}





