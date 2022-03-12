// var validator = {};
// validator.validate_min = function (val,rule) {
// }
//
// validator.validate_maxlength = function (val,rule) {
// }



/*选择页面中所有的input[data-rule]即所有input框中所有自定义了data-rule属性的input框*/


/*解析每一个input的验证规则*/


/*验证*/
var name_validator = new Validator(user_input, rule);
name_validator.is_valid();