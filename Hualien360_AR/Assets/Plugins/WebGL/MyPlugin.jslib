var MyPlugin = {
    ShowAlert: function(messagePtr) {
        // Unity 內部的 string 需要轉換
        var message = UTF8ToString(messagePtr);
        alert(message);
    }
};

mergeInto(LibraryManager.library, MyPlugin);
