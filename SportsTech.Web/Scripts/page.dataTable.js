$(function () {
    // Page variable is defined in the View

    var aoColumnDefs = [];
    var aoColumns = [{ sName: "Id", bVisible: false }];  // always have an invisble Id column

    $.each(Page.dataTableColumns, function () { aoColumns.push(this); });

    if (typeof Page.dataTableEditUrl !== 'undefined' && Page.dataTableEditUrl.length) {
        aoColumns.push({ sName: "Edit", sClass: "action edit-row", sWidth: 8, fnRender: renderEditIcon, bSortable: false });
    }

    if (Page.dataTableShowDelete)
        aoColumns.push({ sName: "", sClass: "action delete-row", sWidth: 8, fnRender: renderDeleteIcon, bSortable: false });  // delete column

    Page.dataTable = $("#dtable").dataTable({
        bServerSide: true,
        sAjaxSource: Page.dataTableLoadUrl,
        sServerMethod: "POST",
        bProcessing: true,
        bAutoWidth: false,
        aoColumns: aoColumns,
        aoColumnDefs: [],
        // define table layout
        sDom: "<'row-fluid dt-header'<'span4'f><'span4'r><'span4 hidden-phone'T>>t<'row-fluid dt-footer'<'span6 visible-desktop'i><'span6'pl>>",
        sPaginationType: "bootstrap",
        oLanguage: { sLengthMenu: "Show: _MENU_", sSearch: "" },
        aLengthMenu: [[20, 50, 100, -1], [20, 50, 100, "All"]],
        iDisplayLength: 20,
        //oTableTools: {
        //    sSwfPath: "/content/swf/copy_csv_xls.swf",
        //    aButtons: [
        //        // print layout
        //        {
        //            "sExtends": "print",
        //            "sButtonText": '<i class="cus-printer oTable-adjust"></i>' + " Print View",
        //        },
        //        {
        //            "sExtends": "xls",
        //            "sButtonText": '<i class="cus-doc-excel-table oTable-adjust"></i>' + " Save for Excel"
        //        }
        //    ]
        //},
    });
    
    $('.dataTables_filter input').attr("placeholder", "Search filter");

    function renderEditIcon(row) {
        var id = row.aData[0];
        return '<a href="#" class="edit" data-item-id="' + id + '"><i title="Edit" class="glyphicon glyphicon-random"></i></a>';
    };

    function renderDeleteIcon(row) {
        var id = row.aData[0];        
        return '<a href="#" class="delete" data-item-id="' + id + '"><i title="Delete" class="glyphicon glyphicon-trash"></i></a>';
    }

    // row click tr td:not(:first-child)
    $("#dtable tbody").on("click", 'tr td:not(.action)', function () {
        var $parent = $(this).parents('tr');
        var position = Page.dataTable.fnGetPosition($parent.get(0)); // get the clicked row position
        
        if (position == null) return;
        var id = Page.dataTable.fnGetData(position)[0];  // Id should be hidden in the 1st column
        site.core.redirect(Page.dataTableDashboardUrl + "/" + id);
    });

    $("#dtable tbody").on("click", ".delete-row", function () {
        var itemId = $(this).children('a').attr("data-item-id");
        return onDelete(itemId);
    });

    $("#dtable tbody").on("click", "a.delete", function () {
        var itemId = $(this).attr("data-item-id");
        return onDelete(itemId);
    });

    function onDelete(itemId) {
        $("#hdnDeleteItemId").val(itemId);
        $("#dlgDelete").modal("show");
        return false;
    }

    $("#create-btn").on("click", function (event) {
        site.core.redirect(Page.dataTableCreateUrl);
    });

    $(document).on('click', '#dtable tbody .edit-row', function () {
        var itemId = $(this).children('a').attr("data-item-id");
        return goToDashboard(itemId);
    });

    $(document).on('click', '#dtable tbody .edit', function () {
        var itemId = $(this).attr("data-item-id");
        return goToDashboard(itemId);
    });

    function goToDashboard(itemId) {
        site.core.redirect(Page.dataTableEditUrl + "/" + itemId);
        return false;
    }
});
