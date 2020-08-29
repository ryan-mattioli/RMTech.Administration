(function() {
var app = angular.module('app', []);
app.controller('EditInvoiceController', [ '$scope', '$http', '$filter', '$timeout', function($scope, $http, $filter, $timeout) {

    $scope.calculateLineTotal = function (line) {
        var val = line.Hours * line.Rate;
        line.LineTotal = val;
    }

    $scope.saveChanges = function () {
        $http.post('/update-invoice', { invoice: $scope.Invoice }).then(function (resp) {
            ajaxSuccessHandler("Invoice updated OK.");
            $scope.Invoice = resp.data;
        }, function (err) {

        });
    }

    $scope.ajaxMessage = null;

    $scope.addInvoiceLine = function () {
        $scope.Invoice.Lines.push({
            InvoiceId: $scope.Invoice.Id,
            Id: 0,
            SvcDate: new Date(),
            Descr: "",
            Hours: 0,
            Rate: 85
        });
    }

    $scope.removeLine = function (line) {
        var ok = confirm("Are you sure?");
        if (ok) {

            if (line.Id == 0) {
                 $scope.Invoice.Lines = $filter('filter')($scope.Invoice.Lines, function (itm) {
                     return itm.Id !== 0;
                    });
            } else {
                $http.post('/delete-invoice-line', { line: line }).then(function (resp) {
                    $scope.Invoice.Lines = $filter('filter')($scope.Invoice.Lines, function (itm) {
                        return itm.Id !== line.Id;
                    });
                }, function (err) {

                });
            }

        }
    }

    function ajaxSuccessHandler(message) {
        $scope.ajaxMessage = message;
        $timeout(function () {
            $scope.ajaxMessage = null;
        }, 3000);

    }
}
]);
})()