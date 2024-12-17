function resetForm() {
    $('#Rate, #MinKg, #MaxKg, #Type').val('');
}

$(document).ready(function () {
    $('#Rate, #MinKg, #MaxKg').on('input', function () {
        this.value = this.value.replace(/[^0-9.,]/g, '').replace(/\.(?=.*\.)/g, '').replace(/,(?=.*,)/g, '');
    });

    $('form').on('submit', function (e) {
        var minKg = parseFloat($('#MinKg').val());
        var maxKg = parseFloat($('#MaxKg').val());

        if (minKg >= maxKg) {
            e.preventDefault();
            alert('MinKg should be less than MaxKg');
            return false;
        }
    });
});

$(document).ready(function () {
    $(document).on('click', '.delete-link', function (e) {
        e.preventDefault();

        var id = $(this).data("id");
        var row = $("#row-" + id);

        if (confirm("Are you sure you want to delete this item?")) {
            $.ajax({
                url: '/RecyclableTypes/Delete',
                type: 'POST',
                data: { id: id },
                success: function (result) {
                    if (result.success) {
                        row.fadeOut(function () {
                            $(this).remove();
                        });
                    } else {
                        alert("Error: " + result.message);
                    }
                },
                error: function () {
                    alert("Error deleting the item.");
                }
            });
        }
    });
});

