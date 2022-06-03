(function () {

    var _mService = abp.services.app.advertisement;

    var _$dTable = $('#DataTable');
    var dataTable = _$dTable.DataTable({
        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _mService.getAll,
            inputFilter: function () {
                return {
                    type: $('#Type').val(),

                };
            }
        },
        columnDefs: [{
            className: 'control responsive',
            orderable: false,
            render: function () {
                return '';
            },
            targets: 0
        },
        //{
        //    targets: 1,
        //    data: null,
        //    orderable: false,
        //    autoWidth: false,
        //    defaultContent: '',
        //    rowAction: {
        //        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
        //        items: [{
        //            text: app.localize('Edit'),
        //            action: function (data) {
        //                _createOrEditModal.open({ id: data.record.id });

        //            }
        //        }, {
        //            text: app.localize('Delete'),
        //            action: function (data) {
        //                deleteObj(data.record);

        //            }
        //        }]
        //    }
        //},
        {
            targets: 1,
            data: "availableDate",
        },
        {
            targets: 2,
            data: "netPrice",
        },
        {
            targets: 3,
            data: "type",
            render: function (type) {
                switch (type) {
                    case 0:
                        return "RentShortTerm";
                    case 10:
                        return "RentLongTerm";
                    case 20:
                        return "Sell";
                    default:
                        return '';
                }
            }

        },
        {
            targets: 4,
            data: "hideAddress"
        },
        {
            targets: 5,
            data: "hidePreciseLocation"
        },
        {
            targets: 6,
            data: "contactByPhone"
        },
        {
            targets: 7,
            data: "contactByEmail"
        },
        {
            targets: 8,
            data: "contactSiteMessage"
        },


        ],
        "drawCallback": function (settings) { }
    });


    //function deleteObj(data) {
    //    abp.message.confirm(
    //        app.localize('DeleteWarningMessage', data.id),
    //        app.localize('AreYouSure'),
    //        function (isConfirmed) {
    //            if (isConfirmed) {
    //                _mService
    //                    .delete({ id: data.id })
    //                    .done(function () {
    //                        getData();
    //                        abp.notify.success(app.localize('SuccessfullyDeleted'));
    //                    });
    //            }
    //        }
    //    );
    //}
    $("#Type").change(function () {
        console.log($("#Type").val());
        getData();
    });

    function getData() {
        dataTable.ajax.reload();
    }

    //abp.event.on('app.createOrEditAdvertisementModalSaved', function () {
    //    getData();
    //});

})();
