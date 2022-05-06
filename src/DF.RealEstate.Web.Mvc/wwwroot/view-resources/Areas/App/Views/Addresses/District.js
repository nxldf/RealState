(function ($) {
    app.modals.CreateOrEditDistrictModal = function () {

        var _modalManager;
        var _mService = abp.services.app.district;
        var _$InformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$InformationForm = _modalManager.getModal().find('form[name=ModalForm]');
            _$InformationForm.validate({ ignore: "" });
        };

        this.save = function () {
            _modalManager.setBusy(true);
            if (!_$InformationForm.valid())
                return;
            var info = _$InformationForm.serializeJSON({ useIntKeysAsArrayIndex: true });
            console.log(info);
            _mService.createOrEditDistrict(info).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditDistrictModal');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})();




(function () {
    $(function () {
        var _$dTable = $('#DataTable');
        var _mService = abp.services.app.district;


        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration'),
            edit: abp.auth.hasPermission('Pages.Administration'),
            'delete': abp.auth.hasPermission('Pages.Administration')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Addresses/CreateOrEditDistrictModal',
            modalClass: 'CreateOrEditDistrictModal',
        });

        var _cityId = $('#CityId').val();
        var _countryId = $('#CountryId').val();
        var _provinceId = $('#ProvinceId').val();

        var dataTable = _$dTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _mService.getAllDistrict,
                inputFilter: function () {
                    return {
                        filter: $('#TableFilter').val(),
                        cityId: _cityId
                    };
                }
            },
            columnDefs: [{
                className: 'control responsive',
                orderable: false,
                render: function () {
                    return '';
                },
                targets: 0,
            },
            {
                targets: 1,
                data: null,
                orderable: false,
                autoWidth: false,
                defaultContent: '',
                rowAction: {
                    text: '<i class="fa fa-cog"></i> <span class="d-none d-md-inline-block d-lg-inline-block d-xl-inline-block">' +
                        app.localize('Actions') +
                        '</span> <span class="caret"></span>',
                    items: [{
                        text: app.localize('Edit'),
                        visible: function (data) {
                            return _permissions.edit;
                        },
                        action: function (data) {                           
                            _createOrEditModal.open({ id: data.record.id, cityId: _cityId });
                        },
                    },
                    {
                        text: app.localize('Delete'),
                        visible: function (data) {
                            return _permissions.delete;
                        },
                        action: function (data) {
                            deleteObj(data.record);
                        }
                    }

                    ]
                }
            },

            {
                targets: 2,
                data: 'name',
            },
            {
                targets: 3,
                data: 'zipCode',
            },
            ],
            "drawCallback": function (settings) { }
        });


        function deleteObj(data) {
            abp.message.confirm(
                app.localize('DeleteWarningMessage', data.name),
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _mService
                            .deleteDistrict({
                                id: data.id,
                            })
                            .done(function () {
                                getData();
                                abp.notify.success(app.localize('SuccessfullyDeleted'));
                            });
                    }
                }
            );
        }

        $('#CreateNewBtn').click(function () {
            _createOrEditModal.open({ cityId: _cityId });
        });

        $('#Back').click(function () {

            location.href = '/App/Addresses/city?Countryid=' + _countryId + '&provinceId=' + _provinceId + '&cityId=' + _cityId;
        });

        $('#GetButton').click(function (e) {
            e.preventDefault();
            getData();
        });

        $('#TableFilter').on('keydown', function (e) {
            if (e.keyCode !== 13) {
                return;
            }

            e.preventDefault();
            getData();
        });

        function getData() {
            dataTable.ajax.reload();
        }

        abp.event.on('app.createOrEditDistrictModal', function () {
            getData();
        });
    });
})();