# 整合相機與地理定位之人員基本資料維護系統設計與實作
## Design and Implementation of a Personnel Information Management System with Camera and Geolocation Integration

* Title      : Design and Implementation of a Personnel Information Management System with Camera and Geolocation Integration
* Author     : Chien-Hung Lin
* Conference : Homework_2
* Date       : 2026-04-05

<p align="center">
  <img src="./Images/MainPage.png" width="300"/>
  <img src="./Images/DetailPage.png" width="300"/>
</p>

## 摘要 (Abstract)
建立一個「人員基本資料維護」畫面（延續 HW1 架構）。
當新增人員後，點選人員項目可進入明細頁，並具備返回功能。

*明細頁設計如下：

上方置中顯示人員姓名
提供「開啟相機」按鈕
下方顯示照片區（畫面占比需超過 40%）

*功能說明：
1. 點擊開啟相機後，於照片區顯示相機畫面
2. 提供拍攝按鈕，拍照後顯示照片於畫面中
3. 照片下方顯示拍攝當下的地理位置（Geolocation）資訊
4. 最後需將應用程式部署至手機進行測試。

## 環境 (Requirements)
* .Net Core 10
* Android Emulator - Pixcel_7_api_35_0

## NuGet 安裝套件
* Microsoft.Maui.Controls (10.0.51)
* CommunityToolkit.Maui (using CommunityToolkit.Maui;)
* CommunityToolkit.Maui.Camera (6.0.0), Request Microsoft.Maui.Controls >= 10.0.30
  
## 操作流程：
* dotnet workload restore

## 注意事項
* 中文輸入法 - 系統設定變更語言
* Plateform - Android - Resources - AndroidManifest.xml - Camera 加入權限 (看是要用打勾，還是編輯的都可以)

* Plateform - Android - Resources - Access_Coarse_Location 地理權限
* Plateform - Android - Resources - Access_Fine_Location 地理權限
* Windows - Package.appxmanifest - Capabilities - Location 地理權限 (功能 - 位置)

## 流程圖
<p align="center">
  <img src="./Images/flow_chart.png" width="400"/>
</p>

## 參考連結
1. https://learn.microsoft.com/zh-tw/dotnet/communitytoolkit/maui/get-started?tabs=CommunityToolkitMauiCamera
2. https://learn.microsoft.com/zh-tw/dotnet/communitytoolkit/maui/views/camera-view?tabs=android
3. https://youtu.be/PhBa263M8vg?si=lWmK2tiD9jix8GOl

## Tools
1. ScrollView - https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/scrollview?view=net-maui-10.0

## Answer Code
1. https://youtu.be/ZmZb12hNTWk?si=BuxSt0LH8jCsf1Ud

## 引文(Citation)：
Please cite the following if you make use of the code.

>@inproceedings{kye2026meta,
  title={Design and Implementation of a Personnel Information Management System with Camera and Geolocation Integration},
  author={Chien-Hung Lin},
  year={2026-04-05}
}
