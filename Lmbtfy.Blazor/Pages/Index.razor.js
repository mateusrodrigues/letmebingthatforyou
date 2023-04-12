$(function () {
    var searchForm = $("#sb_form");
    var searchQuery = $("#sb_form_q");
    var searchResult = $("#lmbtfyResult");

    searchForm.submit(function (e) {
        //if (searchQuery.val() != "") {
        //    searchResult.load(
        //        searchForm[0].action + '?q=' + escape(searchQuery.val()),
        //        function (result) {
        //            searchResult.show("bounce", "fast");
        //        });
        //}
        e.preventDefault();
    });
});