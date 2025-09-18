(function () {
    var gaScript = document.createElement('script');
    gaScript.async = true;
    gaScript.src = '';
    document.head.appendChild(gaScript);
})();

window.dataLayer = window.dataLayer || [];
function gtag() { dataLayer.push(arguments); }
window.gtag = gtag;
gtag('js', new Date());
gtag('config', '');

function sendGA4Event(eventName, eventParams) {
    gtag('event', eventName, eventParams);
}
