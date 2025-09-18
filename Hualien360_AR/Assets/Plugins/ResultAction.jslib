mergeInto(LibraryManager.library, {
    ResultJS: function(actionNamePtr) {
        var actionName = UTF8ToString(actionNamePtr);
        ResultUnity(actionName);
    }
});
