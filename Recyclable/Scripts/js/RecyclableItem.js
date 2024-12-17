function resetForm() {
    $('#RecyclableTypeId, #ItemDescription, #Weight, #ComputedRate').val('');
}

function updateComputedRate() {
    var weight = parseFloat($('#Weight').val().replace(',', '.'));
    var recyclableTypeId = $('#RecyclableTypeId').val();

    if (isNaN(weight) || weight <= 0) {
        return $('#ComputedRate').val('');
    }

    if (recyclableTypeId) {
        $.get('/RecyclableItems/GetRecyclableTypeRate', { recyclableTypeId }, function (response) {
            if (response.success && !isNaN(response.rate)) {
                var computedRate = (response.rate * weight).toFixed(2);

                if (weight < response.minKg || weight > response.maxKg) {
                    alert(`The weight must be between ${response.minKg} kg and ${response.maxKg} kg.`);
                    return $('#ComputedRate').val('');
                }

                $('#ComputedRate').val(computedRate);
            } else {
                $('#ComputedRate').val('');
            }
        }).fail(function () {
            $('#ComputedRate').val('');
        });
    } else {
        $('#ComputedRate').val('');
    }
}

$(document).ready(function () {
    $(document).on('click', '.delete-link', function (e) {
        e.preventDefault();

        var id = $(this).data("id");
        var row = $("#row-" + id);

        if (confirm("Are you sure you want to delete this item?")) {
            $.ajax({
                url: '/RecyclableItems/Delete',
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

