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

    var _createOrEditOnMap = new app.ModalManager({
        viewUrl: abp.appPath + 'App/Homes/CreateOrEditOnMap',
        modalClass: 'CreateOrEditOnMap',
    });

    $('#OpenBtn').click(function () {
        var lat = $('#Latitude').val();
        var lng = $('#Longitude').val();
        _createOrEditOnMap.open({ latitude: lat, longitude: lng });
    });

    var btnsave = function () {
        if (!_$InformationForm.valid()) {
            return;
        }
        abp.ui.setBusy();
        var info = _$InformationForm.serializeJSON({ useIntKeysAsArrayIndex: true });
        _mService.createOrEditHome(info).done(function () {
            abp.notify.info(app.localize('SavedSuccessfully'));
        }).always(function () {
            abp.ui.clearBusy();
        });
    };

})();

(function () {
    app.modals.CreateOrEditOnMap = function () {

        var _modalManager;
        var _longitude;
        var _latitude;
        var _latlong;

        var lat = parseFloat(document.getElementById('defLatitude').value);
        var lng = parseFloat(document.getElementById('defLongitude').value);

        this.init = function (modalManager) {
            _modalManager = modalManager;
            var map = null;
            if (!initMap) {
            function initMap() {

                // The location of Uluru
                const uluru = { lat: lat, lng: lng };
                // The map, centered at Uluru
                map = new google.maps.Map(document.getElementById("map"), {
                    zoom: 15,
                    center: uluru,

                });

                //google.maps.event.addListenerOnce(map, 'idle', function () {
                //    google.maps.event.trigger(map, 'resize');
                //    map.setCenter(uluru);
                //})
                //The marker, positioned at Uluru
                var marker = new google.maps.Marker({
                    position: uluru,
                    map: map,
                });

                marker.setMap(map);

                //var infowindow = new google.maps.InfoWindow({ content: "<div class='map_bg_logo'><span style='color:#1270a2;'><b><?=$row->bridge_name?></b> (<?=$row->bridge_no?>)</span><div style='border-top:1px dotted #ccc; height:1px;  margin:5px 0;'></div><span style='color:#555;font-size:11px;'><b>Length: </b><?=$row->bridge_length?> meters</span></div>" });
                ////google.maps.event.addListener(marker, 'click', function (event) {
                ////    alert("Latitude: " + event.latLng.lat() + " " + ", longitude: " + event.latLng.lng());
                //infowindow.open(map, marker);
                //});
                // info-window ENDS


                google.maps.event.addListenerOnce(map, 'shown.bs.modal', function () {
                    google.maps.event.trigger(map, 'resize');
                    map.setCenter(uluru);
                });
                google.maps.event.addListener(map, 'click', function (event) {
                    _latitude = event.latLng.lat();
                    _longitude = event.latLng.lng();
                    _latlong = { lat: _latitude, lng: _longitude };


                    if (marker)
                        marker.setMap(null);
                    marker = new google.maps.Marker({
                        position: _latlong,
                        map: map,
                    });


                });

                }
            }

            //window.initMap = initMap;


            //var map1;
            //function initMap() {
            //    var myLatLng = {
            //        lat: 43.6222102,
            //        lng: -79.6694881
            //    };

            //    map1 = new google.maps.Map(document.getElementById('map'), {
            //        zoom: 15,
            //        center: myLatLng
            //    });

            //    var marker2 = new google.maps.Marker({
            //        position: myLatLng,
            //        map: map1,
            //    });

            //}


            ////google.maps.event.addDomListener(window, 'load', initMap);

            //$('#modal-body').on('shown.bs.modal', function () {
            //    google.maps.event.trigger(map1, "resize");
            //    map1.setCenter(new google.maps.LatLng(36.305484, 50.027613));
            //});
            if (!window.initMap)
            window.initMap = initMap;
           
           


        };


        //google.maps.event.addDomListener(window, 'load', initMap);
        

        $('.close-button').on('click', function () {
            _modalManager.close();
            //location.reload();
            delete window;
            delete initMap();

        });

        this.save = function () {
            $('#Latitude').val(_latitude);
            $('#Longitude').val(_longitude);
            _modalManager.close();


        };

    };

})();
