function NofityInfoMessage(messegeText) {
    var n = noty({
        theme: 'relax',
        type: 'information',
        layout: 'topCenter',
        text: messegeText,
        closeWith: ['button'],
        timeout: false
    });
}

function NofityWarningMessage(messegeText) {
    var n = noty({
        theme: 'relax',
        type: 'warning',
        layout: 'topCenter',
        text: 'Warning!' + '<br>' + messegeText,
        closeWith: ['button'],
        timeout: false
    });
}

function NofityErrorMessage(messegeText) {
    var n = noty({
        theme: 'relax',
        type: 'error',
        layout: 'topCenter',
        text: 'Error!' + '<br>' + messegeText,
        closeWith: ['button'],
        timeout: false
    });
}

function NofityWarningMessageWithCallback(messegeText, closeCallback) {
    var n = noty({
        theme: 'relax',
        type: 'warning',
        layout: 'topCenter',
        text: '<b>Warning!</b>' + '<br>' + messegeText,
        closeWith: ['button'],
        modal: true,
        timeout: false,
        callback: {
            onClose: function () {
                if (closeCallback != null) {
                    closeCallback();
                }
            }
        }
    });
}

function ConfirmMessage(messegeText, okCallback, cancelCallback) {
    var n = noty({
        theme: 'relax',
        layout: 'center',
        modal: true,
        text: messegeText,
        buttons: [{
            addClass: 'btn btn-primary',
            text: 'Ok',
            onClick: function ($noty) {
                // this = button element
                // $noty = $noty element
                $noty.close();
                if (okCallback != null) {
                    okCallback();
                }

            }
        },
		     {
		         addClass: 'btn btn-danger',
		         text: 'Cancel',
		         onClick: function ($noty) {
		             $noty.close();
		             if (cancelCallback != null) {
		                 cancelCallback();
		             }
		         }
		     }]
    });
}

var dialog;
dialog = dialog || (function () {
    var pleaseWaitDiv = $('<div class="modal" id="pleaseWaitDialog" data-backdrop="true" data-keyboard="false"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><h1>Processing...</h1></div><div class="modal-body"><div class="progress"><div class="progress-bar progress-striped active" style="width:100%;"></div></div></div></div></div></div>');
    return {
        showPleaseWait: function() {
            pleaseWaitDiv.modal();
        },
        hidePleaseWait: function () {
            pleaseWaitDiv.modal('hide');
        },

    };
})();