
$(document).ready(function () {
    MVCxClientGlobalEvents.AddCallbackErrorHandler(function (s, e) {
        if (e.message) {
            var obj = JSON.parse(e.message);
            if (obj != null) {
                if (!obj.valid) {
                    alert(obj.message);
                }
            }
        }
        e.handled = true;
        e.cancel = true;
    });
});

window.onerror = function (errorMsg, url, lineNumber, column, errorObj) {
    if (errorMsg.indexOf('Script error.') > -1) {
        return;
    }
    var errorMessage = 'Error: ' + errorMsg + '<br>Script: ' + url + '<br>Line: ' + lineNumber + '<br>Column: ' + column + '<br>StackTrace: ' + errorObj;
    if (window) {
        if (window.location) {
            if (window.location.href) {
                errorMessage = 'URL:' + window.location.href + ' ' + errorMessage;
            }            
        }
    }
    sendErrorMessageToServer(errorMessage);
}

$(document).on({
    ajaxStart: function () { $("body").addClass("loading"); },
    ajaxStop: function () { $("body").removeClass("loading"); }
});

function sendErrorMessageToServer(errorMessage) {
    var strHtml = "ERROR!";
    var isSucess = false;
    $.ajax({
        type: "POST",
        url: $("#viewdata-container").data("senderrorurl"),
        dataType: "json",
        data: {'_errorMessage': Encoder.htmlEncode(errorMessage) },
        success: function (result) {
            strHtml = "[JAVASCRIPT_ERROR]:<br>";
            if (result.valid) {
                strHtml = strHtml + result.message + "<br>please click <a href='" + window.location.href + "'>Refresh</a> to begin agian.";
            }
            else {
                strHtml = "*** " + errorMessage;
            }
            isSucess = true;
        },
        complete: function (result) {
            if (isSucess) {
                NofityWarningMessage(strHtml);
            }
            else {
                alert(errorMessage);
            }
        }
    });
}

function sendMessageToServerCallback(message, callback) {
    var strHtml = "ERROR!";
    var isSucess = false;
    $.ajax({
        type: "POST",
        url: $("#viewdata-container").data("sendmessageurl"),
        dataType: "json",
        data: { '_message': Encoder.htmlEncode(message) },
        success: function (result) {
            isSucess = true;
        },
        complete: function (result) {
            if (!isSucess) {
                NofityWarningMessage(strHtml);
            }
            if (callback) {
                callback();
            }
        }
    });
}

function sendMessageToServer(message) {
    var strHtml = "ERROR!";
    var isSucess = false;
    $.ajax({
        type: "POST",
        url: $("#viewdata-container").data("sendmessageurl"),
        dataType: "json",
        data: { '_message': Encoder.htmlEncode(message) },
        success: function (result) {            
            isSucess = true;
        },
        complete: function (result) {
            if (!isSucess) {
                NofityWarningMessage(strHtml);
            }
        }
    });
}

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function clickButtonWhenEnter(event, $button) {
    if (event.keyCode === 13) {
        $button.click();
    }
}

$.fn.datepicker.defaults.format = "dd/mm/yyyy";
$.fn.datepicker.defaults.todayHighlight = true;
$.fn.datepicker.defaults.todayBtn = true;
$.fn.datepicker.defaults.autoclose = true;

$.fn.visible = function () {
    //this.css('visibility', 'visible');

    $(".can-hide-label", this).removeClass("hide-field-label");
    $(".can-hide-value", this).removeClass("hide-field-value");

    return;
};

$.fn.invisible = function () {
    //this.css('visibility', 'hidden');
    $(".can-hide-label", this).addClass("hide-field-label");
    $(".can-hide-value", this).addClass("hide-field-value");
    return;
};