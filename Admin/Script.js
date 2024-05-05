function removeErrorMessage() {
    $('#lblwarnuser, #lblwarnname, #lblwarnage, #lblwarnpass, #checkRegmail, #CheckRegGen, #checkRegDob').remove();
}
setTimeout(removeErrorMessage, 3000);