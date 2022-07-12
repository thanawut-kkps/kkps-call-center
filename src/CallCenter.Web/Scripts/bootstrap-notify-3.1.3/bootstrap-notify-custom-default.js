//$.notifyDefaults({
//    allow_dismiss: true,
//    delay: 0
//});

function NotifySuccessMessage(message) {
    $.notify({
        // options
        icon: 'glyphicon glyphicon-ok',
        title: '<strong>Success Infomation</strong><hr />',
        message: message
    }, {
        // settings
        type: 'success',
        position: null,
        newest_on_top: true,
        delay: 0,
        allow_dismiss: true,
        placement: {
		        from: "top",
		        align: "center"
	    },
        animate: {
            enter: 'animated fadeInDown',
            exit: 'animated fadeOutUp'
	    }
    });
}