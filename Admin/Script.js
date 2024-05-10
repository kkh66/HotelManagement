function removeErrorMessage() {
    $('#lblwarnuser, #lblwarnname, #lblwarnage, #lblwarnpass, #checkRegmail, #CheckRegGen, #checkRegDob').remove();
}
setTimeout(removeErrorMessage, 3000);
function setEditRoomId(roomId) {
    document.getElementById('<%= hfEditRoomId.ClientID %>').value = roomId;
}

document.querySelector('.nav-link .custom-ddl-main').addEventListener('click', function (event) {
    event.preventDefault();
    console.log = "You are click";
    window.location.href = 'Home.aspx';
});



function setDeleteRoomId(roomId) {
    document.getElementById('hfDeleteRoomId').value = roomId;
}


function setEditEmpID(empID) {
    document.getElementById('hfEditEmpID').value = empID;
}

function setDeleteEmpID(empID) {
    document.getElementById('hfDeleteEmpID').value = empID;
}