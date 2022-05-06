(function () {
    app.modals.CreateHomeModal = function () {

        var _modalManager;
        var _mService = abp.services.app.home;
        var _$InformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$InformationForm = _modalManager.getModal().find('form[name=ModalForm]');
            _$InformationForm.validate({ ignore: "" });
            var _countryDropDown = $('#CountryDropDown');
            var _province = $('#Province');
            var _city = $('#City');
            var _district = $('#District');

            //var _$country = $('#Country');
            _countryDropDown.append($("<option selected hidden></option>").val('').html('Please Select Country'));
            $.ajax({
                url: abp.appPath + "api/services/app/country/GetCountryDropdown",
                type: 'GET',
                dataType: 'json',
                success: function (info) {

                    $.each(info.result, function (i, country) {
                        console.log(country.title);
                        var $option = $('<option>' + country.title + '</option>').val(country.id);
                        _countryDropDown.append($option);
                    });
                },
                error: function () {
                    alert('Error!');
                }
            });

            //province details by country id  
            _countryDropDown
            $("#CountryDropDown").change(function () {
                var _countryId = parseInt($(this).val());

                if (!isNaN(_countryId)) {                    
                    _province.empty();

                    //debugger;
                    $.ajax({
                        url: abp.appPath + "api/services/app/province/GetProvinceDropdown",
                        type: 'GET',
                        dataType: 'json',
                        data: { countryId: _countryId },
                        success: function (info) {

                            _province.empty(); // Clear the please wait
                            _city.empty();
                            _district.empty();
                            _province.append($("<option selected hidden></option>").val('').html('Select A Province...'));
                            _city.append($("<option selected hidden></option>").val('').html('Select A Province... '));
                            _district.append($("<option selected hidden></option>").val('').html('Select A City...'));
                            $.each(info.result, function (i, province) {
                                var $option = $('<option>' + province.title + '</option>').val(province.id);
                                _province.append($option);
                            });
                        },
                        error: function () {
                            alert('Error!');
                        }
                    });
                }


            });

            //city details by province id  
            $("#Province").change(function () {
                var _provinceId = parseInt($(this).val());

                if (!isNaN(_provinceId)) {
                    _city.empty();

                    //debugger;
                    $.ajax({
                        url: abp.appPath + "api/services/app/city/GetCityDropdown",
                        type: 'GET',
                        dataType: 'json',
                        data: { provinceId: _provinceId },
                        success: function (info) {

                            _city.empty(); // Clear the please wait
                            _district.empty();
                            _city.append($("<option selected hidden></option>").val('').html('Select A City...'));
                            _district.append($("<option selected hidden></option>").val('').html('Select A City...'));
                            $.each(info.result, function (i, items) {
                                var $option = $('<option>' + items.title + '</option>').val(items.id);
                                _city.append($option);
                            });
                        },
                        error: function () {
                            alert('Error!');
                        }
                    });
                }


            });


            //district details by city id
            $("#City").change(function () {
                var _cityId = parseInt($(this).val());

                if (!isNaN(_cityId)) {
                    _district.empty();


                    //debugger;
                    $.ajax({
                        url: abp.appPath + "api/services/app/district/GetDistrictDropdown",
                        type: 'GET',
                        dataType: 'json',
                        data: { cityId: _cityId },
                        success: function (info) {

                            _district.empty(); // Clear the please wait
                            _district.append($("<option selected hidden></option>").val('').html('Select A District'));
                            $.each(info.result, function (i, items) {
                                var $option = $('<option>' + items.title + '</option>').val(items.id);
                                _district.append($option);
                            });
                        },
                        error: function () {
                            alert('Error!');
                        }
                    });
                }


            });

        };

        this.save = function () {
            _modalManager.setBusy(true);
            if (!_$InformationForm.valid())
                return;
            var info = _$InformationForm.serializeJSON({ useIntKeysAsArrayIndex: true });
            console.log(info);
            _mService.createOrEditHome(info).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createHomeModal');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})();




(function () {
    $(function () {
        var _$dTable = $('#DataTable');
        var _mService = abp.services.app.home;


        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration'),
            edit: abp.auth.hasPermission('Pages.Administration'),
            'delete': abp.auth.hasPermission('Pages.Administration')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Homes/CreateHomeModal',
            modalClass: 'CreateHomeModal',
        });


        var dataTable = _$dTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _mService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#TableFilter').val(),
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
                            location.href = '/app/homes/detail?id=' + data.record.id;
                        },
                    },
                    //{
                    //    text: app.localize('Province'),
                    //    visible: function (data) {
                    //        return _permissions.create;
                    //    },
                    //    action: function (data) {
                    //        console.log(data.record.id);
                    //        location.href = '/App/Addresses/province?countryId=' + data.record.id;
                    //    }
                    //},
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
                data: 'type',
                orderable: false,
                render: function (type) {
                    switch (type) {
                        case 0:
                            return 'House';
                        case 10:
                            return 'Apartment Unit';
                        case 20:
                            return 'Entire Apartment';
                        default:
                            return '';
                    }
                }
            },
            {
                targets: 4,
                data: 'zipCode',
            },
            {
                targets: 5,
                data: 'bedrooms',
            },
            {
                targets: 6,
                data: 'bathrooms',
            },
            {
                targets: 7,
                data: 'space',
            },
            {
                targets: 8,
                data: 'fullAddress',
            },
            {
                targets: 9,
                data: 'latitude',
            },
            {
                targets: 10,
                data: 'longitude',
            },
            {
                targets: 11,
                data: 'description',
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
                            .delete({
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
            _createOrEditModal.open();
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

        abp.event.on('app.createHomeModal', function () {
            getData();
        });
    });
})();