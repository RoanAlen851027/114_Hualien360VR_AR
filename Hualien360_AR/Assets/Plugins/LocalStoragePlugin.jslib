mergeInto(LibraryManager.library, {
    SetLocalStorageItem: function(keyPtr, valuePtr) {
        var key = UTF8ToString(keyPtr);
        var value = UTF8ToString(valuePtr);
        localStorage.setItem(key, value);
    },
    
    GetLocalStorageItem: function(keyPtr) {
        var key = UTF8ToString(keyPtr);
        var value = localStorage.getItem(key) || "";
        var buffer = _malloc(lengthBytesUTF8(value) + 1);
        stringToUTF8(value, buffer, lengthBytesUTF8(value) + 1);
        return buffer;
    },

    RemoveLocalStorageItem: function(keyPtr) {
        var key = UTF8ToString(keyPtr);
        localStorage.removeItem(key);
    }
});