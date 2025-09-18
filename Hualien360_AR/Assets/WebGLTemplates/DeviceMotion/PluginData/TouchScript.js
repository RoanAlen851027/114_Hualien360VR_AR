// 此腳本做兩件事：1、雙指全屏切換。2、三指發送排行表事件

var touchCount = 0;
var touchStartDistance = 0;
var touchDistance = 0;
var isFullScreen = false;

document.addEventListener("touchstart", function (event) {
  if (event.touches) {
    if (event.touches.length === 2) {
      touchStartDistance = calculateDistance(event.touches[0], event.touches[1]);
    }
    if (event.touches.length > 0) {
      touchCount = Math.max(touchCount, event.touches.length);
    }
  }
});

document.addEventListener("touchend", function (event) {
  if (event.touches.length === 0) {
    if (touchCount >= 3) {
      //gameInstance.SendMessage('VirtualKeyPass', 'CheckInfo');
      touchCount = 0;
    }

    if (Math.abs(touchDistance) > 10) {
      if (touchDistance > 10) {
        enterFullScreen();
      } else if (touchDistance < -10) {
        exitFullScreen();
      }
      touchDistance = 0;
    }
  }
});

document.addEventListener("touchmove", function (event) {
  if (event.touches.length === 2) {
    touchDistance = calculateDistance(event.touches[0], event.touches[1]) - touchStartDistance;
  }
});

function calculateDistance(touch1, touch2) {
  var dx = touch1.clientX - touch2.clientX;
  var dy = touch1.clientY - touch2.clientY;
  return Math.sqrt(dx * dx + dy * dy);
}

function enterFullScreen() {
  if (isFullScreen) return;

  var element = document.documentElement;

  if (element.requestFullscreen) {
    element.requestFullscreen(); isFullScreen = true;
  } else if (element.mozRequestFullScreen) {
    element.mozRequestFullScreen(); isFullScreen = true;
  } else if (element.webkitRequestFullscreen) {
    element.webkitRequestFullscreen(); isFullScreen = true;
  } else if (element.msRequestFullscreen) {
    element.msRequestFullscreen(); isFullScreen = true;
  }

  //window.alert("全螢幕" + isFullScreen);
}

function exitFullScreen() {
  if (!isFullScreen) return;

  if (document.exitFullscreen) {
    document.exitFullscreen(); isFullScreen = false;
  } else if (document.mozCancelFullScreen) {
    document.mozCancelFullScreen(); isFullScreen = false;
  } else if (document.webkitExitFullscreen) {
    document.webkitExitFullscreen(); isFullScreen = false;
  } else if (document.msExitFullscreen) {
    document.msExitFullscreen(); isFullScreen = false;
  }

  //window.alert("全螢幕" + isFullScreen);
}
