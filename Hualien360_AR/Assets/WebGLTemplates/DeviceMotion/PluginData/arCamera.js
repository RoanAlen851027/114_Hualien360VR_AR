// Camera.js + MindAR 暫停/恢復整合
let sceneEl = null;
let arSystem = null;
let mindarVideo = null;
let cameraStream = null;
let cameraIsPlaying = false;

window.addEventListener('load', () => {
    sceneEl = document.querySelector('a-scene');
    arSystem = sceneEl.systems["mindar-image-system"];

    // 找到 MindAR 自帶 video element
    mindarVideo = sceneEl.querySelector('video');
    if (!mindarVideo) {
        console.warn("MindAR video element not found!");
    }

    // 監聽 AR 事件
    sceneEl.addEventListener("arReady", () => {
        console.log("MindAR ready");
    });
    sceneEl.addEventListener("arError", () => {
        console.log("MindAR failed to start");
    });

    // 初次啟動
    if (mindarVideo) {
        cameraStream = mindarVideo.srcObject;
        cameraIsPlaying = true;
    }
});

// -----------------------------
// 暫停 / 恢復相機功能
// -----------------------------
function pauseMindARCamera() {
    if (mindarVideo && mindarVideo.srcObject) {
        mindarVideo.pause();
        mindarVideo.srcObject.getTracks().forEach(track => track.enabled = false);
        cameraIsPlaying = false;
        console.log("MindAR camera paused");
    }
}

function resumeMindARCamera() {
    if (mindarVideo && mindarVideo.srcObject) {
        mindarVideo.srcObject.getTracks().forEach(track => track.enabled = true);
        mindarVideo.play();
        cameraIsPlaying = true;
        console.log("MindAR camera resumed");
    } else if (arSystem) {
        // 如果 srcObject 消失，重新啟動 AR
        arSystem.start().then(() => {
            mindarVideo = sceneEl.querySelector('video');
            cameraStream = mindarVideo.srcObject;
            mindarVideo.play();
            cameraIsPlaying = true;
            console.log("MindAR restarted after pause");
        }).catch(err => console.error(err));
    }
}

// -----------------------------
// 分頁切換 / Window focus
// -----------------------------
document.addEventListener("visibilitychange", () => {
    if (document.hidden) {
        pauseMindARCamera();
    } else {
        resumeMindARCamera();
    }
});

window.addEventListener("blur", () => pauseMindARCamera());
window.addEventListener("focus", () => resumeMindARCamera());

// -----------------------------
// Unity 可以呼叫 JS
// -----------------------------
window.StartMindARCamera = resumeMindARCamera;
window.PauseMindARCamera = pauseMindARCamera;
