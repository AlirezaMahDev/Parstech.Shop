import "ckeditor5";
import "ckeditor5/ckeditor5.css";

(window as any).CKEDITOR.editorConfig = function (config: any) {
    // Define changes to default configuration here. For example:
    // config.uiColor = '#AADC6E';
    config.contentsLangDirection = 'rtl';
    config.language = 'fa';
    config.filebrowserImageUploadUrl = '/file-upload';
    config.toolbar = 'MyToolbar';

    config.toolbar_MyToolbar =
        [
            {
                name: 'document',
                items: ['Source', '-', 'Save', 'NewPage', 'DocProps', 'Preview', 'Print', '-', 'Templates']
            },
            {name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo']},
            {name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'SpellChecker', 'Scayt']},
            {
                name: 'forms',
                items: ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField']
            },
            {
                name: 'basicstyles',
                items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat']
            },
            {name: 'links', items: ['Link', 'Unlink', 'Anchor']},
            {name: 'colors', items: ['TextColor', 'BGColor']},
            '/',
            {
                name: 'insert',
                items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe']
            },
            {
                name: 'paragraph',
                items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl']
            },
            {name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize']}

        ];

};
