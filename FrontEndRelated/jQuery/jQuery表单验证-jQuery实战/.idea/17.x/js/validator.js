$(function () {
    'use strict';

    window.Validator = function (val, rule) {

        /* 
        {
            max:10,
            min:2
        }
        */
        this.is_valid = function (new_val) {
            var key;
            if (new_val !== undefined) {
                val = new_val;
            }

            /* 如果不是必填项且用户未填写任何内容则直接判定为合法 */
            if (!rule.required && !val)
                return true;

            for (key in rule){
                /* 防止对required属性进行重复检查，因为在上面的if语句那已经做了非空判断 */
                if (key === 'required')
                    continue
                
                /* 调用rule中相对应的方法 */
                var r = this['validate_' + key]();
                if(!r) return false;

            }
            
            return true;
        }

        this.validate_max = function () {
            pre_max_min();
            return val <= rule.max;
        }

        this.validate_min = function () {
            pre_max_min();
            return val >= rule.min;
        }

        this.validate_maxlength = function () {
            pre_length();
            return val.length <= rule.maxlength;
        }

        this.validate_minlength = function () {
            pre_length();
            return val.length >= rule.minlength;
        }

        this.validate_numeric = function () {
            return $.isNumeric(val);
        }

        this.validate_required = function () {
            var real = $.trim(val);
            if (!real && real !== 0) return false;
            return true;
        }

        this.validate_pattern = function () {
            var reg = new RegExp(rule.pattern);
            return reg.test(val);
        }

        /*用于完成this.validate_max 或 this.validate_min 的前置工作*/
        function pre_max_min() {
            val = parseFloat(val);
        }

        /*用于完成this.validate_maxlength 或 this.validate_minlength 的前置工作*/
        function pre_length() {
            val = val.toString();
        }

    }
})