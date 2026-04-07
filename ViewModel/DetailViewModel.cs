using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace MauiApp1.ViewModel;

[QueryProperty("Text", "Text")]
public partial class DetailViewModel : ObservableObject
{
    private readonly ICameraProvider _cameraProvider;

    // ============================================================
    // 產生公開屬性 (get/set), 類似 public string Text { get; set; }
    [ObservableProperty]
    private string text = string.Empty;

    [ObservableProperty]
    private ImageSource? capturedImage;

    [ObservableProperty]
    private string locationText = string.Empty;

    [ObservableProperty]
    private string flashButtonText = "Flash";

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    // ============================================================
    public DetailViewModel(ICameraProvider cameraProvider)
    {
        _cameraProvider = cameraProvider;
    }
    // ============================================================
    public void OnMediaCaptured(Stream media)
    {
        using var memoryStream = new MemoryStream();
        media.CopyTo(memoryStream);
        var bytes = memoryStream.ToArray();

        CapturedImage = ImageSource.FromStream(() => new MemoryStream(bytes));
    }
    // ============================================================
    public async Task CaptureAndLoadLocationAsync(CameraView camera)
    {
        await camera.CaptureImage(CancellationToken.None);

        try
        {
            // 取得定位資訊
            GeolocationRequest request = new(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(request);

            LocationText = location is null
                ? "無法取得定位資訊"
                : $"緯度(Latitude): {location.Latitude}, \n經度(Longitude): {location.Longitude}, \n高度(Altitude):{location.Altitude}, \n速度(Speed):{location.Speed}";
        }
        catch
        {
            LocationText = "無法取得定位資訊";
        }
    }
    // ============================================================
    public void ToggleFlash(CameraView camera)
    {
        camera.CameraFlashMode = camera.CameraFlashMode == CameraFlashMode.Off
            ? CameraFlashMode.On
            : CameraFlashMode.Off;

        FlashButtonText = camera.CameraFlashMode == CameraFlashMode.Off
            ? "Flash On"
            : "Flash Off";
    }
    // ============================================================
    public void ZoomIn(CameraView camera)
    {
        camera.ZoomFactor += 0.1f;
    }
    // ============================================================
    public void ZoomOut(CameraView camera)
    {
        if (camera.ZoomFactor > 0.0f)
        {
            camera.ZoomFactor -= 0.1f;
        }
    }
    // ============================================================
    public async Task SwitchCameraAsync(CameraView camera)
    {
        await _cameraProvider.RefreshAvailableCameras(CancellationToken.None);

        if (camera.SelectedCamera is null)
        {
            return;
        }

        var targetPosition = camera.SelectedCamera.Position == CameraPosition.Front
            ? CameraPosition.Rear
            : CameraPosition.Front;

        var targetCamera = _cameraProvider.AvailableCameras
            .FirstOrDefault(c => c.Position == targetPosition);

        if (targetCamera is not null)
        {
            camera.SelectedCamera = targetCamera;
        }
    }
}