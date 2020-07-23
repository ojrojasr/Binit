$(".select2").select2();

$("#tenants").select2({
    minimumInputLength: 3,
    ajax: {
        url: "/FrontUser/SearchTenants"
    }
});