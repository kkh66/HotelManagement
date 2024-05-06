//date of birth for register
document.addEventListener("DOMContentLoaded", function () {
    flatpickr("#txtDateofBirth", {
        dateFormat: "d-m-Y",
        maxDate: new Date().setFullYear(new Date().getFullYear() - 18),
    });
});

//Remove the error message after 3 seconds
function removeErrorMessage() {
    $('#lblRegError, #lblerror, #lblDateofBirth, #lblcususe, #lblpass, #lblconfitmpass, #lblmail').remove();
}
setTimeout(removeErrorMessage, 3000);