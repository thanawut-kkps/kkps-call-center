function UpdateWrapupMain(reason) {
    if (self != top) {
        parent.postMessage("{\"method\": \"wrapup\", \"msg\": \"" + reason + "\"}", "*");
        return true;
    }
    else {
        sendMessageToServer("Reason:'" + reason + "' is sent but this page is not contained in Gatget in Finesse");
        return false;
    }

}

function MakeCall(tel) {
    if (self != top) {
        parent.postMessage("{\"method\": \"makecall\", \"msg\": \"" + tel + "\"}", "*");
    }
    else {
        sendMessageToServer("Make call to:'" + tel + "' is sent but this page is not contained in Gatget in Finesse");
    }
}

function IsCalling() {
    var session = getCallSession() + "X";
    if (session.substring(0, "init".length) === "init") {
        return false;
    }
    return true;
}


function CanChangeStatusToReady() {
    var session = getParameterByName("session") + "X";
    if (session.substring(0, "init".length) === "init"
                || session.substring(0, "outbound".length) === "outbound") {
        return false;
    }
    return true;
}

function IsOutboundCalling() {
    var session = getParameterByName("session") + "X";
    if (session.substring(0, "outbound".length) === "outbound") {
        return true;
    }
    return false;
}