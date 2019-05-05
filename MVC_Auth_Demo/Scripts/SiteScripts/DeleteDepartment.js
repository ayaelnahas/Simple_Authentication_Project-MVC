function DeleteEmployee(id) {
    var result = confirm("Are You Sure?")
    if (result) {
        location.href = `/Employees/Delete/${id}`;
    }
}