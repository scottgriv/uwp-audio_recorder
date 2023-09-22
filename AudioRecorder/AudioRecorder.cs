using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace AudioRecorder
{
    public class AudioRecorder
    {
        private const string DEFAULT_AUDIO_FILENAME = "audio_clip.mp3";
        private string _fileName;
        private MediaCapture _mediaCapture;
        private InMemoryRandomAccessStream _memoryBuffer;

        public bool IsRecording { get; set; }

        public async void Record()
        {
            if (IsRecording)
            {
                throw new InvalidOperationException("Recording already in progress!");
            }

            try
            {
                Initialize();
                // await DeleteExistingFile();

                MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings
                {
                    StreamingCaptureMode = StreamingCaptureMode.Audio
                };

                _mediaCapture = new MediaCapture();
                await _mediaCapture.InitializeAsync(settings);
                await _mediaCapture.StartRecordToStreamAsync(MediaEncodingProfile.CreateMp3(AudioEncodingQuality.Auto), _memoryBuffer);
            }
            catch (Exception e)
            {
                await new MessageDialog("You must give this application access to your microphone to enable audio recording.", "No Microphone Access").ShowAsync();
                Console.Out.WriteLine(e.StackTrace);
            }

            IsRecording = true;
        }

        public async void StopRecording()
        {
            try
            {
                await _mediaCapture.StopRecordAsync();

                SaveAudioToFile();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.StackTrace);
            }
            IsRecording = false;

        }

        public async Task PlayFromDisk(CoreDispatcher dispatcher)
        {

            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                MediaElement playbackMediaElement = new MediaElement();
                StorageFolder storageFolder = Package.Current.InstalledLocation;
                StorageFile storageFile = await storageFolder.GetFileAsync(this._fileName);
                IRandomAccessStream stream = await storageFile.OpenAsync(FileAccessMode.Read);

                playbackMediaElement.SetSource(stream, storageFile.FileType);
                playbackMediaElement.AreTransportControlsEnabled = true;
                playbackMediaElement.Play();

            });
        }

        public async Task<StorageFile> GetStorageFile(CoreDispatcher dispatcher)
        {
            StorageFolder storageFolder = Package.Current.InstalledLocation;
            StorageFile storageFile = await storageFolder.GetFileAsync(this._fileName);
            return storageFile;
        }

        public void Play()
        {
            MediaElement playbackMediaElement = new MediaElement();
            playbackMediaElement.AreTransportControlsEnabled = true;
            playbackMediaElement.SetSource(_memoryBuffer, "MP3");
            playbackMediaElement.Play();
        }


        private void Initialize()
        {
            if (_memoryBuffer != null)
            {
                _memoryBuffer.Dispose();
            }

            _memoryBuffer = new InMemoryRandomAccessStream();

            if (_mediaCapture != null)
            {
                _mediaCapture.Dispose();
            }

            this._fileName = DEFAULT_AUDIO_FILENAME;

        }


        private async void SaveAudioToFile()
        {
            try
            {

                IRandomAccessStream audioStream = _memoryBuffer.CloneStream();
                StorageFolder storageFolder = Package.Current.InstalledLocation;

                StorageFile storageFile = await storageFolder.CreateFileAsync(DEFAULT_AUDIO_FILENAME, CreationCollisionOption.GenerateUniqueName);

                this._fileName = storageFile.Name;

                using (IRandomAccessStream fileStream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    await RandomAccessStream.CopyAndCloseAsync(audioStream.GetInputStreamAt(0), fileStream.GetOutputStreamAt(0));
                    await audioStream.FlushAsync();
                    audioStream.Dispose();

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.StackTrace);
                throw;
            }
        }


        private async Task DeleteExistingFile()
        {
            try
            {
                StorageFolder storageFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                StorageFile existingFile = await storageFolder.GetFileAsync(this._fileName);
                await existingFile.DeleteAsync();
            }
            catch (FileNotFoundException)
            {

            }

        }

    }
}
