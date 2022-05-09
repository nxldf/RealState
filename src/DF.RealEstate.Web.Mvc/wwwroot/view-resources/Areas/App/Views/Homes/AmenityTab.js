(function () {

    var _mService = abp.services.app.amenity;
    var newdiv = '';
    _homeId = $("#HomeId").val();
    // i Sholud change value 1 to _homeId
    console.log(_homeId);
    _mService.getSelectedAmenities(_homeId).done(function (info) {
        $.each(info, function (i, p) {

            if (p.selected)
                newdiv += '<label class="checkbox checkbox-success col-md-3"><input type="checkbox" value=' + p.id + ' name="' + p.title + '" checked="'+p.selected+'" /><span></span>' + p.title + '</label>';
            if (!p.selected)
                newdiv += '<label class="checkbox checkbox-success col-md-3"><input type="checkbox" value=' + p.id + ' name="' + p.title + '" /><span></span>' + p.title + '</label>';           
        });
        $("#CheckBoxes").append(newdiv);

    });

    $('#saveInfoBtn').click(function () {
        //btnsave();

        _$InformationForm = $('form[name=informationsForm]');
        _$InformationForm.validate({ ignore: "" });
        var info = _$InformationForm.serializeJSON({ useIntKeysAsArrayIndex: true });
        _homeId = $("#HomeId").val();
        var input = [];
        for (var item in info) {
            input.push(info[item]);
        }
        info.Amenities = input;
        info.HomeId = _homeId;

        console.log(info);
    });

})();