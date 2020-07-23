$("#reason").select2();
$("#users").select2({
    minimumInputLength: 3,
    ajax: {
        url: "/Holiday/SearchUsers"
    }
});