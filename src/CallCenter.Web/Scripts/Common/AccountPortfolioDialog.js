var _selectedCallback;

function popupAccountPortfolioDialog(selectedAccountNo, clientID) {
    var $container = $("#account-portfolio-dialog-body");    

    var strTitle = "Account Portfolio";    
    $.ajax({
        type: "POST",
        url: $container.data("accountportfoliourl"),
        dataType: "json",
        data: { selectedAccountNo: selectedAccountNo,
                clientID: clientID},
        success: function (result) {
            if (!result.valid) {
                NofityErrorMessage(result.message);
            }
            else {


//                $("#btnSearchClient").click(function () {
//                    cbpClientResultContainer.PerformCallback();
//                });

                $container.html(result.message);
                $("#account-portfolio-dialog").modal('show');
            }
        },
        close: function (event, ui) { $container.empty(); }
    });
}