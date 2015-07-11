/**
 * Widjet for Data Grid
 */
var AjaxGridViewModel = function (url) {
    var ITEMS_PER_PAGE = 3;
    
    var self = this,
        data = {};

    self.rows = ko.observableArray();
    self.searchText = ko.observable();
    self.sorting = {
        columns: []
    };

    self.paging = {
        PageNumber: ko.observable(1),
        TotalPagesCount: ko.observable(0),
        next: function () {
            var pn = self.paging.PageNumber();
            if (pn >= self.paging.TotalPagesCount()) return;
            self.paging.PageNumber(++pn);
            showRowsOfPage(pn);
        },
        back: function () {
            var pn = self.paging.PageNumber();
            if (pn == 1) return;
            self.paging.PageNumber(--pn);
            showRowsOfPage(pn);
        }
    };

    self.applyFilter = function () {
        self.rows();
    };

    self.sortBy = function (columnName) {
        var prevSortingColumn = self.sorting.column;
        self.sorting.column = columnName;
        if 
        self.sorting.direcion
        updateRows();
    };

    ko.dependentObservable(function () {
        if (data.length) return;
        data.pageNumber = self.paging.PageNumber();
        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            context: this,
            success: function (json) {
                data = json.Data;
                self.applyFilter();
                self.paging.PageNumber(json.Paging.PageNumber);
                self.paging.TotalPagesCount(json.Paging.TotalPagesCount);
                showRowsOfPage(1);
            }
        });
    }, this);

    function updateRows() {
        var filteredData = data.filter(function (row) {
            var searchText = self.searchText();
            if (!searchText) {
                return true;
            }
            var accumValue = [];
            for (var prop in row) {
                if (row.hasOwnProperty(prop)) {
                    accumValue.push(row[prop]);
                }
            }
            return RegExp(searchText).test(accumValue.join(' '));
        });

        var sortedData = filteredData.sort(function (a, b) {
            return a[columnName] < b[columnName];
        });

        var tmpData = [],
            pageNum = self.paging.PageNumber();

        if (!pageNum || pageNum < 1 || pageNum > self.paging.TotalPagesCount()) {
            pageNum = 1;
        }
        for (var i in filteredData) {
            if (Math.ceil(i / ITEMS_PER_PAGE) === pageNum) {
                tmpData.push(filteredData[i]);
            }
        }

        self.rows(tmpData);
    }
};