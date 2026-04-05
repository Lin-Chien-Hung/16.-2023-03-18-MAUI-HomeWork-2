using CommunityToolkit.Maui.Core;
using MauiApp1.ViewModel;


namespace MauiApp1;

public partial class DetailPage : ContentPage
{
    private readonly CommunityToolkit.Maui.Core.ICameraProvider _cameraProvider;
    public DetailPage(DetailViewModel vm, CommunityToolkit.Maui.Core.ICameraProvider cameraProvider)
    {
        InitializeComponent();
        BindingContext = vm;
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
}