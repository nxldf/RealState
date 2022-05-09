(function () {


    var _mService = abp.services.app.home;
    var _$InformationForm = null;


    $(document).ready(function () {
        init();
    });

    var init = function () {


        _$InformationForm = $('form[name=informationsForm]');
        _$InformationForm.validate({ ignore: "" });
        var _countryDropDown = $('#CountryDropdown');
        var _province = $('#Province');
        var _city = $('#City');
        var _district = $('#District');

      
        var defaultCountryId = $('#defaultCountryId').val();
        var defaultProvinceId = $('#defaultProvinceId').val();
        var defualtCityId = $('#defualtCityId').val();
        var defualtDistrictId = $('#defualtDistrictId').val();

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

                if (!isNaN(defaultCountryId)) {
                    $('#CountryDropdown').val(defaultCountryId).change();
                }
            },
            error: function () {
                alert('Error!');
            }
        });

        //province details by country id  
        $("#CountryDropdown").change(function () {
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
                        if (!isNaN(defaultProvinceId)) {
                            $('#Province').val(defaultProvinceId).change();
                            defaultProvinceId = "";
                        }
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
                        if (!isNaN(defualtCityId)) {
                            $('#City').val(defualtCityId).change();
                            defualtCityId = "";
                        }
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
                        if (!isNaN(defualtDistrictId)) {
                            $('#District').val(defualtDistrictId).change();
                            defualtDistrictId = "";
                        }
                        
                    },
                    error: function () {
                        alert('Error!');
                    }
                });
            }


        });

    };
    

    $('#saveInfoBtn').click(function () {
        btnsave();
    });

    var btnsave = function () {
        abp.ui.setBusy();
        if (!_$InformationForm.valid()) {
            abp.ui.clearBusy();
            return;
        }

        var info = _$InformationForm.serializeJSON({ useIntKeysAsArrayIndex: true });
        console.log(info);
        _mService.createOrEditHome(info).done(function () {
            abp.notify.info(app.localize('SavedSuccessfully'));
            location.reload();
        }).always(function () {
            abp.ui.clearBusy();
        });
    };

})();
