(function () {

    var _mService = abp.services.app.amenity;
    var newdiv = '';

    _mService.getSelectedAmenities(3).done(function (info) {
        $.each(info, function (i, p) {

            if (p.selected)
                newdiv += '<label class="checkbox checkbox-success col-md-3"><input type="checkbox" id=' + p.id + ' name="' + p.title + '" checked="'+p.selected+'" ) /><span></span>' + p.title + '</label>';
            if (!p.selected)
                newdiv += '<label class="checkbox checkbox-success col-md-3"><input type="checkbox" id=' + p.id + ' name="' + p.title + '" ) /><span></span>' + p.title + '</label>';           
        });
        $("#CheckBoxes").append(newdiv);

    });

    $('#saveInfoBtn').click(function () {
        //btnsave();
        _$InformationForm = $('form[name=informationsForm]');
        _$InformationForm.validate({ ignore: "" });
        var info = _$InformationForm.serializeJSON({ useIntKeysAsArrayIndex: true });
        console.log(info);
        var serialized = _$InformationForm.map(function () {
            return { name: this.name, id: this.id, value: this.checked ? "checked" : "false" };
            
        });
        console.log("---------------------------");
        console.log(serialized);
    });

})();