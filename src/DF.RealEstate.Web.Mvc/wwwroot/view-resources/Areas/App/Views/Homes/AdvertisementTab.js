(function () {

    var _modalManager;
    var _mService = abp.services.app.advertisement;
    var _$InformationForm = null;


    $('.decimal-input-mask').inputmask('decimal', { min: 0 });

    $('.date-picker').datetimepicker({
        locale: abp.localization.currentLanguage.name,
        format: 'L'
    });

    $(document).ready(function () {
        init();
    });

    var init = function (modalManager) {
        _modalManager = modalManager;
        _$InformationForm = _modalManager.getModal().find('form[name=informationsForm]');



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

})();
