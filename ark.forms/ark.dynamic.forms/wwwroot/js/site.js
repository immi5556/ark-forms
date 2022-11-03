(function ($) {
    function floatLabel(inputType) {
        $(inputType).each(function () {
            var $this = $(this);
            // on focus add cladd active to label
            $this.focus(function () {
                $this.next().addClass("active");
            });
            //on blur check field and remove class if needed
            $this.blur(function () {
                if ($this.val() === '' || $this.val() === 'blank') {
                    $this.next().removeClass();
                }
            });
        });
    }
    // just add a class of "floatLabel to the input field!"
    floatLabel(".floatLabel");

    $("#full_name").on("keyup", () => {
        if ($("#full_name").val()) {
            $("#err").text('');
        } else {
            $("#err").text('Please enter your name.');
        }
    })
    $("#btn-submit").on("click", (evt) => {
        if ($("#full_name").val()) {
            return true;
        } else {
            $("#err").text('Please enter your name.');
            evt.preventDefault();
            return false;
        }
    });
    //$("#chk-attend").on("change", (evt) => {
    //    var chk = $(evt.target).is(":checked");
    //    console.log('checked status', chk);
    //    if (chk)
    //        $("#btn-submit").removeAttr("disabled");
    //    else
    //        $("#btn-submit").prop("disabled", true);
    //});

    $.get("https://api.ipify.org/?format=json", function (response) {
        //alert(response.ip);
        window.my_ip = response.ip;
        $("#ip").val(response.ip);
    }, "json")

})(jQuery);