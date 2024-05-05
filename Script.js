$(document).ready(function () {
    $('#txtDateofBirth').datepicker({
        dateFormat: "yy/mm/dd",
        changeYear: true,
        changeMonth: true,
        yearRange: "1900:2006",
    });
    $('#txtDateofBirth').datepicker("setDate", new Date("2000-01-01"));
});