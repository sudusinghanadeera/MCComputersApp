
var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "responsive": true,
        "ajax": {
            "url": "/admin/product/getAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "name",
                "width": "25%", "class": "text-break"
            },
            {
                "data": "price",
                "render": function (data) {
                    var text = priceFormat.format(data.toFixed(2));
                    return text;
                },
                "width": "25%", "class": "d-none d-md-table-cell text-right" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center"><div class="btn-group" role="group">
        <a href="/admin/product/edit?id=${data}" class="btn btn-primary text-white" style="cursor:pointer;">
            <i class="fas fa-edit"></i>
        </a>
        <a class="btn btn-primary text-white" style="cursor: pointer;" onclick=Delete('/admin/product/delete?id=${data}')>
            <i class="fas fa-trash-alt"></i>
        </a>
        </div></div>`;
                }, "width": "50%",
            }
        ],
        "language": {
            "emptyTable": "No data found..."
        },
        "width": "100%"
    });
}
