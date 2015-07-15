function getWindowGUID() {

    var windowGUID = function () {
        //----------
        var S4 = function () {
            return (
                    Math.floor(
                            Math.random() * 0x10000 /* 65536 */
                        ).toString(16)
                );
        };

        return (
                S4() + S4()
            );
    };
    var topMostWindow = window;

    while (topMostWindow != topMostWindow.parent) {
        topMostWindow = topMostWindow.parent;
    }
    if (!topMostWindow.name.match(/^GUID-/)) {
        topMostWindow.name = "GUID-" + windowGUID();
    }
    return topMostWindow.name;
}

function duplicateSession(oldGUID) {
    var newGUID = getWindowGUID();
    if (oldGUID != newGUID) {
        document.getElementById('SESSIONID').value = newGUID;
        var location = window.location.toString();
        if (location.indexOf(".aspx?") > 0) {
            window.location = location + '&oldGUID=' + oldGUID + '&newGUID=' + newGUID;
        }
        else {
            window.location = location + '?oldGUID=' + oldGUID + '&newGUID=' + newGUID;
        }
    }
}