using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Accord.Video.FFMPEG;
using AForge.Video;
using AForge.Video.DirectShow;
using FDR.DependencyInjection;
using FDR.Repositories.Implement;
using FDR.Repositories.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using Prism.Commands;

namespace FDR.ViewModel
{
    internal class CameraViewModel : ObservableObject, IDisposable,INotifyPropertyChanged
    {
        #region Private fields

        private IBase<NgayHoc> ngayhoc_repo;
        private IBase<SinhVien> sv_repo;
        private IBase<LopHocPhan> lhp_repo;
        private FilterInfo _currentDevice;

        private BitmapImage _image;
        private BitmapImage _detachImage;
        private string _ipCameraUrl;

        private bool _isDesktopSource;
        private bool _isIpCameraSource;
        private bool _isWebcamSource;

        private IVideoSource _videoSource;
        private VideoFileWriter _writer;
        private bool _recording;
        private DateTime? _firstFrameTime;

        #endregion

        #region Constructor

        public CameraViewModel()
        {
            VideoDevices = new ObservableCollection<FilterInfo>();


            GetVideoDevices();
            IsDesktopSource = true;
            StartSourceCommand = new RelayCommand(StartCamera);
            StopSourceCommand = new RelayCommand(StopCamera);
            StartRecordingCommand = new RelayCommand(StartRecording);
            StopRecordingCommand = new RelayCommand(StopRecording);
            SaveSnapshotCommand = new RelayCommand(SaveSnapshot);
            DetachSourceCommand = new RelayCommand(DetachImages);
            IpCameraUrl = "http://88.53.197.250/axis-cgi/mjpg/video.cgi?resolution=320×240";

            ngayhoc_repo = new BaseRepositories<NgayHoc>();

            ClosedCommand = new DelegateCommand(OpenOld);

            sv_repo = new BaseRepositories<SinhVien>();
            lhp_repo = new BaseRepositories<LopHocPhan>();

        }

        private void OpenOld()
        {
            var view = new MainWindow();
            view.Show();
        }

        #endregion

        #region Properties

        public ObservableCollection<FilterInfo> VideoDevices { get; set; }

        public BitmapImage Image
        {
            get { return _image; }
            set { Set(ref _image, value); }
        }
        public BitmapImage DetachImage
        {
            get { return _detachImage; }
            set { Set(ref _detachImage, value); }
        }

        public bool IsDesktopSource
        {
            get { return _isDesktopSource; }
            set { Set(ref _isDesktopSource, value); }
        }

        public bool IsWebcamSource
        {
            get { return _isWebcamSource; }
            set { Set(ref _isWebcamSource, value); }
        }

        public bool IsIpCameraSource
        {
            get { return _isIpCameraSource; }
            set { Set(ref _isIpCameraSource, value); }
        }

        public string IpCameraUrl
        {
            get { return _ipCameraUrl; }
            set { Set(ref _ipCameraUrl, value); }
        }

        public FilterInfo CurrentDevice
        {
            get { return _currentDevice; }
            set { Set(ref _currentDevice, value); }
        }

        public ICommand StartRecordingCommand { get; private set; }

        public ICommand StopRecordingCommand { get; private set; }

        public ICommand StartSourceCommand { get; private set; }

        public ICommand StopSourceCommand { get; private set; }

        public ICommand SaveSnapshotCommand { get; private set; }
        public ICommand DetachSourceCommand { get; private set; }

        public DelegateCommand ClosedCommand
        {
            get;
            set;
        }

        #endregion

        private void GetVideoDevices()
        {
            var devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in devices)
            {
                VideoDevices.Add(device);
            }
            if (VideoDevices.Any())
            {
                CurrentDevice = VideoDevices[0];
            }
            else
            {
                MessageBox.Show("No webcam found");
            }
        }

        private void StartCamera()
        {
            if (IsDesktopSource)
            {
                var rectangle = new Rectangle();
                foreach (var screen in System.Windows.Forms.Screen.AllScreens)
                {
                    rectangle = Rectangle.Union(rectangle, screen.Bounds);
                }
                _videoSource = new ScreenCaptureStream(rectangle);
                _videoSource.NewFrame += video_NewFrame;
                _videoSource.Start();
            }
            else if (IsWebcamSource)
            {
                if (CurrentDevice != null)
                {
                    _videoSource = new VideoCaptureDevice(CurrentDevice.MonikerString);
                    _videoSource.NewFrame += video_NewFrame;
                    _videoSource.Start();
                }
                else
                {
                    MessageBox.Show("Current device can't be null");
                }
            }
            else if (IsIpCameraSource)
            {
                _videoSource = new MJPEGStream(IpCameraUrl);
                _videoSource.NewFrame += video_NewFrame;
                _videoSource.Start();
            }
        }
        private void DetachImages()
        {
            BitmapImage tmp = Image;
            Bitmap bi = BitmapImageToBitmap(Image);
            string B64 = ConvertBitmapToBase64String(bi);

           // Console.WriteLine(B64);

            DetachImage = tmp;

            String server_ip = "localhost";
            String server_path = "http://" + server_ip + ":8000/recog";
            String retStr = sendPOST(server_path, B64);

            CheckStudent(retStr);
        }
        private void CheckStudent(String maSinhVien)
        {
            try
            {
                if (!maSinhVien.Equals("Unknow"))
                {
                    if (!string.IsNullOrEmpty(StateSubject.MaHP))
                    {
                        var data = new NgayHoc()
                        {
                            MaSinhVien = maSinhVien,
                            MaLopHocPhan = StateSubject.MaHP,

                        };
                        data.NgayHoc1 = DateTime.Now;

                        var result = ngayhoc_repo.Insert(data);
                        if (result != null)
                        {
                            var stu = sv_repo.GetById(result.MaSinhVien);
                            var sub = lhp_repo.GetById(result.MaLopHocPhan);
                            MessageBox.Show($"Sinh viên : " + stu.TenSinhVien + "Đã Điểm Danh Ngày : " +
                                result.NgayHoc1.ToShortDateString() + " Cho Môn " + sub.MonHoc.TenMonHoc);
                        }
                        else
                        {
                            MessageBox.Show("Điểm Danh Thất Bại",
                                            "Thông Báo",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error);
                        }
                    }
                    else
                        MessageBox.Show("Chưa Có Mã Lớp Học Phần Được Chọn",
                                            "Thông Báo",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error);
                }
                else
                    MessageBox.Show("Không Nhận Dạng Được Sinh Viên",
                                            "Thông Báo",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error);
            }
            catch
            {
                MessageBox.Show("Có Lỗi");
            }
        }
        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                if (_recording)
                {
                    using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                    {
                        if (_firstFrameTime != null)
                        {
                            _writer.WriteVideoFrame(bitmap, DateTime.Now - _firstFrameTime.Value);
                        }
                        else
                        {
                            _writer.WriteVideoFrame(bitmap);
                            _firstFrameTime = DateTime.Now;
                        }
                    }
                }
                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                {
                    var bi = bitmap.ToBitmapImage();
                    bi.Freeze();
                    Dispatcher.CurrentDispatcher.Invoke(() => Image = bi);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error on _videoSource_NewFrame:\n" + exc.Message, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                StopCamera();
            }
        }

        private void StopCamera()
        {
            if (_videoSource != null && _videoSource.IsRunning)
            {
                _videoSource.SignalToStop();
                _videoSource.NewFrame -= video_NewFrame;
            }
            Image = null;
        }

        private void StopRecording()
        {
            _recording = false;
            _writer.Close();
            _writer.Dispose();
        }


        private void StartRecording()
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = "Video1";
            dialog.DefaultExt = ".avi";
            dialog.AddExtension = true;
            var dialogresult = dialog.ShowDialog();
            if (dialogresult != true)
            {
                return;
            }
            _firstFrameTime = null;
            _writer = new Accord.Video.FFMPEG.VideoFileWriter();
            _writer.Open(dialog.FileName, (int)Math.Round(Image.Width, 0), (int)Math.Round(Image.Height, 0));
            _recording = true;
        }

        private void SaveSnapshot()
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = "Snapshot1";
            dialog.DefaultExt = ".png";
            var dialogresult = dialog.ShowDialog();
            if (dialogresult != true)
            {
                return;
            }
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(Image));
            using (var filestream = new FileStream(dialog.FileName, FileMode.Create))
            {
                encoder.Save(filestream);
            }
        }

        public void Dispose()
        {
            if (_videoSource != null && _videoSource.IsRunning)
            {
                _videoSource.SignalToStop();
            }
            _writer?.Dispose();
        }

        //--------------------------------------------------------------------------------------------------------------------------------


        // Ham chuyen Image thanh Base 64
        private Bitmap BitmapImageToBitmap(BitmapImage bitmapImage)
        {
            // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }
        public static string ConvertBitmapToBase64String(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);

                return Convert.ToBase64String(ms.ToArray());
            }
        }


        // Ham goi HTTP Get len server
        public string sendGet(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }


        // Ham convert B64 de gui len server
        private String EscapeData(String B64)
        {
            int B64_length = B64.Length;
            if (B64_length <= 32000)
            {
                return Uri.EscapeDataString(B64);
            }


            int idx = 0;
            StringBuilder builder = new StringBuilder();
            String substr = B64.Substring(idx, 32000);
            while (idx < B64_length)
            {
                builder.Append(Uri.EscapeDataString(substr));
                idx += 32000;

                if (idx < B64_length)
                {

                    substr = B64.Substring(idx, Math.Min(32000, B64_length - idx));
                }

            }
            return builder.ToString();

        }

        // Ham goi HTTP POST len server de detect
        private String sendPOST(String url, String B64)
        {
            try
            {

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 5000;
                var postData = "image=" + EscapeData(B64);

                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return responseString;
            }
            catch (Exception ex)
            {
                return "Exception" + ex.ToString();
            }
        }

    }
}
