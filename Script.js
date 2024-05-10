//date of birth for register
document.addEventListener("DOMContentLoaded", function () {
    flatpickr("#txtDateofBirth", {
        dateFormat: "d-m-Y",
        maxDate: new Date().setFullYear(new Date().getFullYear() - 18),
    });
});
//Remove the error message after 3 seconds
function removeErrorMessage() {
    $('#lblRegError, #lblerror, #lblDateofBirth, #lblcususe, #lblpass, #lblconfitmpass, #lblmail,#lblwarngender,#lblloginpass,#lblLoginuser,#lblerrorlogin').remove();
}
setTimeout(removeErrorMessage, 3000);
//Booking page check in and out
document.addEventListener('DOMContentLoaded', function () {
    var today = new Date();
    var tomorrow = new Date(today);
    tomorrow.setDate(tomorrow.getDate() + 1);

    flatpickr('#txtcheckindate', {
        dateFormat: "d-m-Y",
        minDate: today
    });

    flatpickr('#txtcheckoutdate', {
        minDate: tomorrow,
        dateFormat: "d-m-Y",
        maxDate: "2099-12-31"
    });
});  