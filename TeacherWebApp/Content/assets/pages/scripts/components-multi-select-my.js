var ComponentsDropdowns = function () {

    var handleSelect2 = function () {

        $('#select2_sample1').select2({
            placeholder: "Select an option",
            allowClear: true
        });
    }

    var handleSelect2Modal = function () {

        $('#select2_sample_modal_1').select2({
            placeholder: "Select an option",
            allowClear: true
        });
    }

    var handleBootstrapSelect = function() {
        $('.bs-select').selectpicker({
            iconBase: 'fa',
            tickIcon: 'fa-check'
        });
    }

    var handleMultiSelect = function () {
        $('#my_multi_select1').multiSelect();
    }

    return {
        //main function to initiate the module
        init: function () {            
            handleSelect2();
            handleSelect2Modal();
            handleMultiSelect();
            handleBootstrapSelect();
        }
    };

}();

jQuery(document).ready(function() {    
   ComponentsDropdowns.init(); 
});