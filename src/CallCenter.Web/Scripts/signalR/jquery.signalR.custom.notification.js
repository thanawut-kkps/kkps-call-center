var notification;

$(function () {
    // Declare a proxy to reference the hub. 
    notification = $.connection.notificationHub;
    // Create a function that the hub can call to nofity messages.
    notification.client.nofityInfoMessage = function (message) {
        NofityInfoMessage(message);
    };
    $.connection.hub.start().done(function () {
    });

    $.connection.hub.disconnected(function () {
        setTimeout(function () {
            $.connection.hub.start();
        }, 4000); // Restart connection after 4 seconds.
    });
});

function sendMessageToOther(message) {
    if (notification != null) {
        notification.server.notifyToOther(message);
    }
}