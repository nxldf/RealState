(function () {

    app.modals.CreateOrEditPhotoModal = function () {

        var _modalManager;
        var _mService = abp.services.app.homePhoto;
        var _$InformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;
            _$InformationForm = _modalManager.getModal().find('form[name=ModalForm]');
            _$InformationForm.validate({ ignore: "" });

            var imageinputPartner = new KTImageInput('kt_image_partner');
            imageinputPartner.on('change', function (imageInput) {
                abp.ui.setBusy();
                var files = $('#image-input-partner')[0].files;
                var fd = new FormData();
                if (files.length > 0)
                    fd.append('file', files[0]);
                abp.ajax({
                    url: '/api/services/app/blobStorage/UploadCommonImages',
                    type: 'post',
                    processData: false,
                    contentType: false,
                    data: fd
                }).done(function (data) {
                    if (!data.succeed) {
                        abp.message.error(data.message);
                        $("#cancelImageBtn").trigger("click");
                    } else {
                        $("#OrginalAddress").val(data.url);
                        $("#ThumbnailAddress").val(data.url_2);
                        abp.notify.info(app.localize('UploadSuccessfully'));
                    }
                }).then(function (result) {
                    abp.ui.clearBusy();
                    console.log(result);
                }).catch(function () {
                    abp.ui.clearBusy();
                    $("#cancelImageBtnPartner").trigger("click");
                    console.log("request failed :(");
                });;
            });
            imageinputPartner.on('cancel', function (imageInput) { });

        };

        this.save = function () {
            _modalManager.setBusy(true);

            if (!_$InformationForm.valid())
                return;


            var info = _$InformationForm.serializeJSON({ useIntKeysAsArrayIndex: true });
            console.log(info);
            _mService.createOrEdit(info).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditPhotosModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });

        };
    };
})();

(function () {
    $(function () {
        var _mService = abp.services.app.homePhoto;
        var _$dTable = $('#DataTable');

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration'),
            edit: abp.auth.hasPermission('Pages.Administration'),
            'delete': abp.auth.hasPermission('Pages.Administration')
        };

        var filterId = $("#HomeId").val();
        
        var dataTable = _$dTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _mService.getAll,
                inputFilter: function () {
                    return {
                        homeId: filterId
                    };
                }
            },
            columnDefs: [
                {
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
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.id });
                            }
                        }, {
                            text: app.localize('Delete'),
                            visible: function (data) {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                DeleteObj(data.record);
                            }
                        }]
                    }
                },
                {
                    targets: 2,
                    data: "orginalAddress",
                    orderable: false,
                    render: function (img) {
                        return '<div class="symbol symbol-40 symbol-2by3"><div class="symbol-label" style="background-image: url(\'' + img + '\')" alt="photo"></div></div>';
                    }
                },

            ],
            "drawCallback": function (settings) { }
        });

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Homes/CreateOrEditPhotoModal',
            modalClass: 'CreateOrEditPhotoModal',
        });

        $('#CreateNewButton').click(function () {            
            _createOrEditModal.open({ homeId: filterId });
        });

        function DeleteObj(obj) {
            abp.message.confirm(
                app.localize('DeleteWarningMessage', obj.title),
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _mService.delete({
                            id: obj.id
                        }).done(function () {
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

        abp.event.on('app.createOrEditPhotosModalSaved', function () {
            dataTable.ajax.reload();
        });

    });
})();

