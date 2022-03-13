$(function () {
    'use strict';

    /*选择页面中所有的input[data-rule]即所有input框中所有自定义了data-rule属性的input框*/
    var $inputs = $('[data-rule]')
        , $form = $('#signup')
        , inputs = [];

    $inputs.each(function (index,node) {
        /*解析每一个input的验证规则*/
        var tmp = new Input(node);
        inputs.push(tmp);
    })
    
    $form.on('submit', function (e) {
        e.preventDefault();
        /* 通过jQuery选中所有的input框并一起进行blur事件的触发：这是为了显示错误信息用的：如果有错误提示的话 */
        $inputs.trigger('blur');
        for (var i = 0; i < inputs.length; i ++){
            var item = inputs[i];
            var r = item.validator.is_valid();
            if (!r) {
                alert('invalid');
                return;
            }
        }

        alert('注册成功');
    });

});