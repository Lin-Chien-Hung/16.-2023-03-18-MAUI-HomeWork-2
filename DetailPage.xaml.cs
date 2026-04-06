using CommunityToolkit.Maui.Core;
using MauiApp1.ViewModel;


namespace MauiApp1;

public partial class DetailPage : ContentPage
{
    private readonly CommunityToolkit.Maui.Core.ICameraProvider _cameraProvider;
    public DetailPage(DetailViewModel vm, CommunityToolkit.Maui.Core.ICameraProvider cameraProvider)
    {
        // 初始化頁面元件
        InitializeComponent();
        // 設定資料繫結來源（BindingContext = vm;），讓 XAML 可以綁定 ViewModel
        BindingContext = vm;
        // 接收並儲存相機服務（_cameraProvider = cameraProvider;），讓頁面可以操作相機功能
        _cameraProvider = cameraProvider;
    }
    // ==============================================================================
    private void myCamera_MediaCaptured(object sender, MediaCapturedEventArgs e)
    {
        Dispatcher.Dispatch(() => { ncameraimage.Source = ImageSource.FromStream(() => e.Media); });
    }
    // ==============================================================================
    private async void click_Clicked(object sender, EventArgs e)
    {
        await myCamera.CaptureImage(CancellationToken.None);

        try
        {
            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best);

            var location = await Geolocation.GetLocationAsync(request);
            if (location != null)
            {
                string locationString = $"緯度(Latitude): {location.Latitude}, \n經度(Longitude): {location.Longitude}, \n高度(Altitude):{location.Altitude}, \n速度(Speed):{location.Speed}";
                //await App.Current.MainPage.DisplayAlert("Location", locationString, "OK");
                locationLabel.Text = locationString;
            }
        }
        catch
        {
            locationLabel.Text = "無法取得定位資訊";
        }
    }
    // ==============================================================================
    private void flash_Clicked(object sender, EventArgs e)
    {
        myCamera.CameraFlashMode = myCamera.CameraFlashMode == CameraFlashMode.Off ? CameraFlashMode.On : CameraFlashMode.Off;
        flash.Text = myCamera.CameraFlashMode == CameraFlashMode.Off ? "Flash On" : "Flash Off";
    }
    // ==============================================================================
    private void zoomin_Clicked(object sender, EventArgs e)
    {
        myCamera.ZoomFactor += 0.1f;
    }
    // ==============================================================================
    private void zoomout_Clicked(object sender, EventArgs e)
    {
        if (myCamera.ZoomFactor != 0.0f)
        {
            myCamera.ZoomFactor -= 0.1f;
        }
    }
    // ==============================================================================
    private async void switch_Clicked(object sender, EventArgs e)
    {
        await _cameraProvider.RefreshAvailableCameras(CancellationToken.None);
        if (myCamera.SelectedCamera.Position == CameraPosition.Front)
        {
            myCamera.SelectedCamera = _cameraProvider.AvailableCameras.FirstOrDefault(c => c.Position == CameraPosition.Rear);
        }
        else {
            myCamera.SelectedCamera = _cameraProvider.AvailableCameras.FirstOrDefault(c => c.Position == CameraPosition.Front);
        }
    }
    // ==============================================================================
}