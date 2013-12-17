$(function () {
    // Page variable is defined in the View

    var aoColumnDefs = [];
    var aoColumns = [{ sName: "Id", bVisible: false }];  // always have an invisble Id column

    $.each(Page.dataTableColumns, function () { aoColumns.push(this); });

    if (Page.dataTableShowDelete)
        aoColumns.push({ sName: "", sWidth: 8, fnRender: renderDeleteIcon, bSortable: false });  // delete column

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

    function renderDeleteIcon(row) {
        var id = row.aData[0];
        return '<a href="#" class="delete" data-item-id="' + id + '"><i title="Delete" class="cus-bin-closed"></i></a>';
    }

    // row click
    $("#dtable tbody").delegate("tr", "click", function () {
        var position = Page.dataTable.fnGetPosition(this); // get the clicked row position
        if (position == null) return;
        var id = Page.dataTable.fnGetData(position)[0];  // Id should be hidden in the 1st column
        site.core.redirect(Page.dataTableEditUrl + "/" + id);
    });

    // delete click
    $("#dtable tbody").delegate("a.delete", "click", function () {
        var itemId = $(this).attr("data-item-id");
        $("#hdnDeleteItemId").val(itemId);
        $("#dlgDelete").modal("show");
        return false;
    });

});
