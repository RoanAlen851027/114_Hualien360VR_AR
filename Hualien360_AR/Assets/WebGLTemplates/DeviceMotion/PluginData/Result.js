function ResultUnity(actionName) {
    switch (actionName) {
        case "LINELink":
            window.location.href = "https://lin.ee/GU8ZdoG";
            break;
        case "Alert_CopySerial":
            window.alert("拷貝完成");
            break;
        case "ShowPicture":
            captureScreen();
            //
            document.getElementById('screenshot-area').style.display = 'flex ';
            document.getElementById('video').style.display = 'none';
            break;
        case "HidePicture":
            document.getElementById('screenshot-area').style.display = 'none';
            document.getElementById('video').style.display = 'block';
            break;
        case "Download":
            downloadScreenshot();
            break;
        default:
            console.log("unknown mission: " + actionName);
            break;
    }
}

function captureScreen() {
    const video = document.getElementById("video");
    const unityCanvas = document.getElementById("unity-canvas");
    const outputCanvas = document.createElement("canvas");
    const outputContext = outputCanvas.getContext("2d");

    // 設置輸出畫布大小與 unityCanvas 相同
    outputCanvas.width = unityCanvas.width;
    outputCanvas.height = unityCanvas.height;
    
    // 1. 將 video 當前畫面疊加在 outputCanvas 上
    if (video.videoWidth > 0 && video.videoHeight > 0) {
        outputContext.drawImage(video, 0, 0, outputCanvas.width, outputCanvas.height);
    }

    // 2. 將 unity-canvas 畫布的內容畫到 outputCanvas 上
    outputContext.drawImage(unityCanvas, 0, 0, outputCanvas.width, outputCanvas.height);

    // 3. 將合成結果轉為圖片 URL
    const image = outputCanvas.toDataURL("image/png");

    // 將圖片顯示在 <img id="photo-img"> 元素上
    const photo = document.getElementById("photo-img");
    if (photo) photo.src = image;

    // 暫存圖片 URL 用於下載
    window.screenshotImage = image;

    outputCanvas.remove();
}

function downloadScreenshot() {
    if (window.screenshotImage) {
        const link = document.createElement("a");
        link.href = window.screenshotImage;
        link.download = "screenshot.png";
        link.click();
        link.remove();
    } else {
        console.warn("No screenshot available to download.");
    }
}

