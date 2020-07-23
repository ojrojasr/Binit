$('#btn-export-filtered').click(function () {
    const searchTerm = $('.dataTables_filter input').val();
    const downloadUrl = `Holiday/ExportExcel?searchTerm=${searchTerm}`;

    window.location.href = downloadUrl;
});