function removeErrorMessage() {
    $('#lblwarnuser, #lblwarnname, #lblwarnage, #lblwarnpass, #checkRegmail, #CheckRegGen, #checkRegDob').remove();
}
setTimeout(removeErrorMessage, 3000);
function setEditRoomId(roomId) {
    document.getElementById('<%= hfEditRoomId.ClientID %>').value = roomId;
}

function setDeleteRoomId(roomId) {
    document.getElementById('<%= hfDeleteRoomId.ClientID %>').value = roomId;
}

function setEditEmpID(empID) {
    document.getElementById('<%= hfEditEmpID.ClientID %>').value = empID;
}

function setDeleteEmpID(empID) {
    document.getElementById('<%= hfDeleteEmpID.ClientID %>').value = empID;
}