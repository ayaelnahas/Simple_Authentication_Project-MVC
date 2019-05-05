function DeleteDepartment(id) {
    var result = confirm("Are You Sure?")
    if (result) {
        location.href = `/Depatment/Delete/${id}`;
    }
}