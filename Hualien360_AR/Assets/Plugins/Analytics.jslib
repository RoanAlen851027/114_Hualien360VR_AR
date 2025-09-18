mergeInto(LibraryManager.library, {
    SendGAEvent: function (eventNamePtr, eventParamsPtr) {
        // 將指針轉換為字符串
        var eventName = UTF8ToString(eventNamePtr);
        var eventParams = JSON.parse(UTF8ToString(eventParamsPtr));
        
        // 調用 JS 中的 GA4 事件
        sendGA4Event(eventName, eventParams);  
    }
});