var missedCallNotification;

$(function () {
    // Declare a proxy to reference the hub. 
    missedCallNotification = $.connection.missedCallHub;

    // Create a function that the hub can call to nofity messages.
    missedCallNotification.client.notifyStartingOutboundCall = function (phoneNumber) {
        if ($("#missedcall-list") != null) {
            if ($("td.phone-number[data-value='" + phoneNumber + "']").length > 0) {
                NofityWarningMessage(phoneNumber + " is calling back by another agent");
            }            
        }
    };

    // Create a function that the hub can call to nofity messages.
    missedCallNotification.client.notifyFinishingOutboundCall = function (phoneNumber) {
        alert(phoneNumber);
    };

    $.connection.hub.start().done(function () {
        if ($("#current-client-data").length > 0) {
            if (missedCallNotification != null) {
                missedCallNotification.server.notifyOutboundCall(getCustomerPhone());
            }
        }
    });
});


function notifyOutbound(phoneNumber) {
    if ($("#current-client-data") != null) {
        if (missedCallNotification != null) {
            missedCallNotification.server.notifyOutboundCall(phoneNumber);
        }        
    }    
}

