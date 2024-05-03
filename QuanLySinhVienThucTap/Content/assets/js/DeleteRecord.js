let deleteUrl = '';
let deleteId = '';
let returnUrl = '';

function setDeleteParams(url, id, rurl) {
    deleteUrl = url;
    deleteId = id;
    returnUrl = rurl;
}

function Delete() {
    $.ajax({
        type: "POST",
        url: deleteUrl,
        data: { ID: deleteId },
        success: function (result) {
            window.location.href = returnUrl;
        }
    }); 
}
