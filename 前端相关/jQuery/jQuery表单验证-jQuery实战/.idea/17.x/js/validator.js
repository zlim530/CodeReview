$(function () {
    'use strict';

    window.Validator = function (val, rule) {
        this.is_valid = function () {
            return true;
        }

        this.validate_max = function () {
            return true;
        }

        this.validate_min = function () {
            return true;
        }
    }
})