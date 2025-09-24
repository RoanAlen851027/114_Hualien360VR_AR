mergeInto(LibraryManager.library, {
    StartCamera: function () {
        if (typeof StartCamera === "function") {
            StartCamera();
        } else {
            console.error("StartCamera not found in global scope");
        }
    },
    PauseCamera: function () {
        if (typeof PauseCamera === "function") {
            PauseCamera();
        } else {
            console.error("PauseCamera not found in global scope");
        }
    },
    ResumeCamera: function () {
        if (typeof ResumeCamera === "function") {
            ResumeCamera();
        } else {
            console.error("ResumeCamera not found in global scope");
        }
    },
    StopCamera: function () {
        if (typeof StopCamera === "function") {
            StopCamera();
        } else {
            console.error("StopCamera not found in global scope");
        }
    }
});
