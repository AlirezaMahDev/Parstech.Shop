var PersmissoinData = document.getElementById("PersmissoinData");




var UserRole_NumberuserId = document.getElementById("UserRole_NumberuserId");

var UserRole_UserName2 = document.getElementById("UserRole_UserName2");
var URole_RoleName = document.getElementById("URole_RoleName");





function OnLoadingPersmissoinListGet() {
    PersmissoinData.innerHTML = null;
}

function OnCompletePersmissoinListGet(xhr) {
   // console.log(xhr);
    var list = xhr.responseJSON.object;
    list.forEach(function (element) {

        PersmissoinData.insertAdjacentHTML("afterbegin", " <tr><td>" + element.persianRoleName + "</td><td><button class='btn btn-danger text-white font-weight-bold' id='button" + element.roleName + "'>لغو دسترسی</button></td></tr>")
        var button = document.getElementById("button" + element.roleName);
        button.addEventListener('click', () => DeleteRole(element.roleName, element.userName));
        //UserRole_NumberuserId.value = element.id;
    });
    $('#PersmissoinDataModal').modal('show');
}

function OnErrorPersmissoinListGet() {

}

function DeleteRole(role, user) {
   
    UserRole_UserName2.value = user;

    URole_RoleName.value = role;

   // console.log(UserRole_UserName2.value);
   // console.log("role" + URole_RoleName.value);

    $("#DeleteRoleForm").submit();
}

function ShowRoleCreateModal() {
    $('#RoleCreateModal').modal('show');
}



function OnLoadingAddRole() {
    PersmissoinData.innerHTML = null;
}

function OnCompleteAddRole(xhr) {
   // console.log(xhr);
    Response = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    }

    if (Response.isSuccessed) {
        ToastSuccess(Response.message);
        $('#PersmissoinDataModal').modal('hide');
        $('#RoleCreateModal').modal('hide');
    }
    else {

        ToastError(Response.message);

    }
}

function OnErrorAddRole() {

}



function OnLoadingDeleteRole() {
    PersmissoinData.innerHTML = null;
}

function OnCompleteDeleteRole(xhr) {
   // console.log(xhr);
    Response = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    }

    if (Response.isSuccessed) {
        ToastSuccess(Response.message);
        $('#PersmissoinDataModal').modal('hide');
        $('#RoleCreateModal').modal('hide');
    }
    else {

        ToastError(Response.message);

    }
}

function OnErrorDeleteRole() {

}




