var _selectedCallback;

function popupClientSelectionDialog() {
    var $container = $("#selection-dialog-body");

    var strTitle = "Client Selection";

    $.ajax({
        type: "POST",
        url: $container.data("clientselectionurl"),
        dataType: "json",
        data: {},
        success: function (result) {
            if (!result.valid) {
                NofityErrorMessage(result.message);
            }
            else {
                $container.html(result.message);

                $("#btnSearchClient").click(function () {
                    cbpClientResultContainer.PerformCallback();
                });

                $("#selection-dialog").modal('show');
            }
        },
        close: function (event, ui) { $container.empty(); }
    });

}