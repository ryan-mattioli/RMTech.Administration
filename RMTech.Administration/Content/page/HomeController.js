(function() {
    var app = angular.module('app', []);
    app.controller('InvoiceController', ['$scope', '$http', '$window', '$location', '$filter',
        function ($scope, $http, $window, $location, $filter) {
        $scope.newInvoice = {
            CustomerName: "",
            InvoiceDate: new Date(),
            Lines: [],
            CustomerAddress1 : "",
            CustomerAddress2 : "",
            CustomerCity : "",
            CustomerState : "",
            CustomerZip : "",
            CustomerPhone: "", 
            CustomerId: 0
            }


        $scope.recentInvoices = [];
   
            $http.get('recent-invoices').then(function (resp) {
                $scope.recentInvoices = resp.data;
            }, function (err) {

            });
        

        $scope.newLine = function () {
            $scope.newInvoice.Lines.push(
                {
                    Id: 0,
                    Descr: "",
                    Hours: 0,
                    Rate: 60,
                    SvcDate: new Date()
                 
                }
            );
        };

        $scope.invoiceTotal = function () {
            var retVal = 0.0;
            angular.forEach($scope.newInvoice.Lines, function (val) {
                retVal += val.LineTotal;
            });
        }

        $scope.createInvoice = function () {
                    
            $http.post('create-invoice', $scope.newInvoice).then(function (resp) {
                $window.open($location.path() + 'get-invoice/' + resp.data);
            }, function (err) { });
        }

        $scope.selectedCustomerChanged = function () {
            var cust = $filter('filter')($scope.customers, function (item) {
                return item.Id == $scope.selectedCustomer;
            })[0];
            $scope.newInvoice.CustomerName = cust.CustName;
            $scope.newInvoice.CustomerAddress1 = cust.CustAddr1;
            $scope.newInvoice.CustomerAddress2 = cust.CustAddr2;
            $scope.newInvoice.CustomerCity = cust.CustCity;
            $scope.newInvoice.CustomerState = cust.CustState;
            $scope.newInvoice.CustomerZip = cust.CustZip;
            $scope.newInvoice.CustomerPhone = cust.CustPhone;
            $scope.newInvoice.CustomerId = cust.Id;
        }
        }])
        .controller('CustomerController', ['$scope', '$http', '$filter', function ($scope, $http, $filter) {
            $scope.selectedCustomerChanged = function () {
                var cust = $filter('filter')($scope.customers, function (item) {
                    return item.Id == $scope.selectedCustomer;
                })[0];
                $scope.editCust = angular.copy(cust);
            };
            $scope.saveCustomer = function () {
                $http.post('save-customer', $scope.editCust).then(function (resp) {
                    alert("saved");
                    var cust = $filter('filter')($scope.customers, function (item) {
                        return item.Id == $scope.editCust.Id;
                    })[0];
                    cust = $scope.editCust;
                }, function (err) {

                });
            }
        }]);

})()