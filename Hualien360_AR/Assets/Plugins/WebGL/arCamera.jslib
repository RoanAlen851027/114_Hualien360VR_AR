mergeInto(LibraryManager.library, {
    StartMindARCamera: function() {
        // 確認 MindAR video 已存在
        var video = document.querySelector('a-scene video');
        if(video && video.srcObject){
            video.srcObject.getTracks().forEach(track => track.enabled = true);
            video.play();
            console.log("MindAR camera resumed (from jslib)");
        }
    },

    PauseMindARCamera: function() {
        var video = document.querySelector('a-scene video');
        if(video && video.srcObject){
            video.pause();
            video.srcObject.getTracks().forEach(track => track.enabled = false);
            console.log("MindAR camera paused (from jslib)");
        }
    }
});
