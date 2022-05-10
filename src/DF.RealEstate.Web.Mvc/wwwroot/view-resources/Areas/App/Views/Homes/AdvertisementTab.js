(function () {
    app.modals.CreateOrEditAdvertisementModal = function () {

        var _modalManager;
        var _mService = abp.services.app.advertisement;
        var _$InformationForm = null;


        this.init = function (modalManager) {
            _modalManager = modalManager;
            _$InformationForm = _modalManager.getModal().find('form[name=ModalForm]');
            _$InformationForm.validate({ ignore: "" });


        };

        //$('.decimal-input-mask').inputmask('decimal', { min: 0 });

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });
        this.save = function () {
            if (!_$InformationForm.valid())
                return;
            _modalManager.setBusy(true);
            var info = _$InformationForm.serializeJSON({ useIntKeysAsArrayIndex: true });

            if (info.ContactByEmail === "on")
                info.ContactByEmail = true;
            if (info.ContactByPhone === "on")
                info.ContactByPhone = true;
            if (info.ContactSiteMessage === "on")
                info.ContactSiteMessage = true;
            if (info.HideAddress === "on")
                info.HideAddress = true;
            if (info.HidePreciseLocation === "on")
                info.HidePreciseLocation = true;



            _mService.createOrEdit(info).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditAdvertisementModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };

    };

})();

(function () {

    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Homes/CreateOrEditAdvertisementModal',
        modalClass: 'CreateOrEditAdvertisementModal',
    });

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
                    homeId: $('#HomeId').val()
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
        {
            targets: 1,
            data: null,
            orderable: false,
            autoWidth: false,
            defaultContent: '',
            rowAction: {
                text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                items: [{
                    text: app.localize('Edit'),
                    action: function (data) {
                        _createOrEditModal.open({ id: data.record.id });
                        //, homeId: data.record.homeId

                    }
                }, {
                    text: app.localize('Delete'),
                    action: function (data) {
                        deleteObj(data.record);

                    }
                }]
            }
        },
        {
            targets: 2,
            data: "availableDate",
        },
        {
            targets: 3,
            data: "netPrice",
        },
        {
            targets: 4,
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
            targets: 5,
            data: "hideAddress"
        },
        {
            targets: 6,
            data: "hidePreciseLocation"
        },
        {
            targets: 7,
            data: "contactByPhone"
        },
        {
            targets: 8,
            data: "contactByEmail"
        },
        {
            targets: 9,
            data: "contactSiteMessage"
        },


        ],
        "drawCallback": function (settings) { }
    });

    $('#CreateNewBtn').click(function () {
        var res = $('#HomeId').val();
        _createOrEditModal.open({ homeId: res, });
    });

    $("#Type").change(function () {
        console.log($("#Type").val());
        getData();
    });
    function deleteObj(data) {
        abp.message.confirm(
            app.localize('DeleteWarningMessage', data.id),
            app.localize('AreYouSure'),
            function (isConfirmed) {
                if (isConfirmed) {
                    _mService
                        .delete({ id: data.id })
                        .done(function () {
                            getData();
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    function getData() {
        dataTable.ajax.reload();
    }

    abp.event.on('app.createOrEditAdvertisementModalSaved', function () {
        getData();
    });

})();
