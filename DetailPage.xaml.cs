using CommunityToolkit.Maui.Core;
using MauiApp1.ViewModel;

namespace MauiApp1;

public partial class DetailPage : ContentPage
{
    private readonly DetailViewModel _viewModel; // 導入 DetailViewModel

    public DetailPage(DetailViewModel vm)
    {
        InitializeComponent(); // 初始化 DetailPage 一個空間
        BindingContext = vm; // 資料來源是 ViewModel
        _viewModel = vm;
    }
    // ============================================================
    // 拍照事件
    private void myCamera_MediaCaptured(object sender, MediaCapturedEventArgs e)
    {
        Dispatcher.Dispatch(() => _viewModel.OnMediaCaptured(e.Media)); // e.Media 是拍照後的資料流 Stream
    }
    // ============================================================
    // 拍照事件 => 先拍照，然後載入定位資訊
    private async void click_Clicked(object sender, EventArgs e)
    {
        await _viewModel.CaptureAndLoadLocationAsync(myCamera);
    }
    // ============================================================
    private void flash_Clicked(object sender, EventArgs e)
    {
        _viewModel.ToggleFlash(myCamera);
    }
    // ============================================================
    private void zoomin_Clicked(object sender, EventArgs e)
    {
        _viewModel.ZoomIn(myCamera);
    }
    // ============================================================
    private void zoomout_Clicked(object sender, EventArgs e)
    {
        _viewModel.ZoomOut(myCamera);
    }
    // ============================================================
    private async void switch_Clicked(object sender, EventArgs e)
    {
        await _viewModel.SwitchCameraAsync(myCamera);
    }
}