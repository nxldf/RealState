(function ($) {
    app.modals.CreateOrEditCityModal = function () {

        var _modalManager;
        var _mService = abp.services.app.city;
        var _$InformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$InformationForm = _modalManager.getModal().find('form[name=ModalForm]');
            _$InformationForm.validate({ ignore: "" });
        };

        this.save = function () {
            if (!_$InformationForm.valid())
                return;
            _modalManager.setBusy(true);
            var info = _$InformationForm.serializeJSON({ useIntKeysAsArrayIndex: true });
            console.log(info);
            _mService.createOrEdit(info).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditCityModal');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})();




(function () {
    $(function () {
        var _$dTable = $('#DataTable');
        var _mService = abp.services.app.city;


        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration'),
            edit: abp.auth.hasPermission('Pages.Administration'),
            'delete': abp.auth.hasPermission('Pages.Administration')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Addresses/CreateOrEditCityModal',
            modalClass: 'CreateOrEditCityModal',
        });

        var _provinceId = $('#ProvinceId').val();
        var _countryId = $('#CountryId').val();

        var dataTable = _$dTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _mService.getAllCity,
                inputFilter: function () {
                    return {
                        filter: $('#TableFilter').val(),
                        provinceId: _provinceId
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
                            _createOrEditModal.open({ id: data.record.id, provinceId: _provinceId });
                        },
                    },
                    {
                        text: app.localize('District'),
                        visible: function (data) {
                            return _permissions.create;
                        },
                        action: function (data) {
                            location.href = '/App/Addresses/district?Countryid=' + _countryId + '&provinceId=' + _provinceId+'&cityId='+data.record.id;
                        }
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
                            .deleteCity({
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
            var res = $('#ProvinceId').val();
            _createOrEditModal.open({ provinceId: res });
        });

        $('#Back').click(function () {
            
            console.log(_countryId);
            location.href = '/App/Addresses/province?countryId=' + _countryId;
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

        abp.event.on('app.createOrEditCityModal', function () {
            getData();
        });
    });
})();