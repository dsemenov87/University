/**
 * Widjet for Data Grid
 */
function AjaxGridViewModel(url, hasSearchBox, buttons) {
    var ITEMS_PER_PAGE = 3;
    
    var self = this, data = {};

    self.buttons = [];
    if (buttons) {
        buttons.forEach(function (btn) {
            self.buttons.push({
                name: btn.name,
                action: getBtnAction(btn)
            });
        });
    }
    
    self.rows = ko.observableArray();
    self.columns = ko.observableArray();

    self.searchBox = {
        text: ko.observable(new String)
    }

    self.sorting = {
        column: ko.observable(),
        direction: ko.observable('asc')
    };

    self.paging = {
        pageNumber: ko.observable(1),
        totalPagesCount: ko.computed(function () {
            return getPageNumberByRow(getFilteredRows(self.searchBox.text()).length);
        }),
        next: function () {
            var pn = self.paging.pageNumber();
            if (pn >= self.paging.totalPagesCount()) return;
            self.paging.pageNumber(++pn);
        },
        back: function () {
            var pn = self.paging.pageNumber();
            if (pn == 1) return;
            self.paging.pageNumber(--pn);
        }
    };

    self.applyFilter = function () {
        self.searchBox.text();
    };

    self.sortBy = function (columnName) {
        if (self.sorting.column() === columnName) {
            self.sorting.direction(self.sorting.direction() == 'asc' ? 'desc' : 'asc');
        } else {
            self.sorting.column(columnName);
        }
    };

    ko.dependentObservable(function init() {
        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            context: this,
            success: initData
        });
    }, this);

    self.selectedRows = ko.observableArray();
    self.viewRows = ko.computed(function () {
        var filteredData = getFilteredRows(self.searchBox ? self.searchBox.text() : new String);
        var sortedData = getSortedRows(filteredData, self.sorting.column(), self.sorting.direction());
        var selectedRow = self.selectedRows()[0];

        return getRowsOfPage(sortedData, self.paging.pageNumber());
    });

    ko.dependentObservable(function () {
        var rows = self.selectedRows();
        if (rows.length > 1) {
            rows.shift();
        }
        self.selectedRows(rows);
    }, this);

    self.onDeleteSuccess = function (data) {
        if (data.IsValid) {
            $('#modalForm').modal('hide');
            var rows = self.rows();
            deleteRow(rows, data.Id);
            self.rows(rows);
        }
    };

    self.onCreateOrUpdateSuccess = function (data) {
        if (data.IsValid) {
            $('#modalForm').modal('hide');
            var rows = self.rows();
            if (!data.IsNew) {
                deleteRow(rows, data.Id);
            }
            rows.push(cloneRow(data));
            self.rows(rows);
        } 
    };

    self.onAddItemSuccess = function (data) {
        if (data.IsValid) {
            $('#modalForm').modal('hide');
        }
    }

    function deleteRow(rows, id) {
        var indexToRemove = rows.indexOf(rows.filter(function (row) {
            return row.Id === id;
        })[0]);
        if (indexToRemove > -1) {
            rows.splice(indexToRemove, 1);
        }
    }

    function cloneRow(from, to) {
        to = to || {};
        for (var prop in from) {
            if (prop === 'IsValid' || prop === 'IsNew') continue;
            to[prop] = from[prop];
        }
        return to;
    }

    function initData(data) {
        var header = data[0], columns = [];
        for (var prop in header) {
            if (header.hasOwnProperty(prop)) {
                columns.push({ title: prop });
            }
        }
        self.columns(columns);
        self.rows(data);
    }

    function getBtnAction(btn) {
        return function onActionClick(e) {
            var url = btn.url;
            if (btn.name !== 'Create') {
                var selectedRow = self.selectedRows()[0];
                if (!selectedRow) return;
                var selectedRange = self.rows().filter(function (row) {
                    return row.Id === selectedRow;
                });
                url += '/' + selectedRange[0].Id;
            }

            $.ajax({
                url: url,
                success: function (data) {
                    $('#modalForm').html(data).modal('show');
                }
            });
        }
    };

    function getRowsOfPage(rows, pageNum) {
        var res = [];
        for (var i = 0; i < rows.length; i++) {
            if (getPageNumberByRow(i + 1) === pageNum) {
                res.push(rows[i]);
            }
        }

        return res;
    }

    function getPageNumberByRow(rowNumber) {
        return Math.ceil(rowNumber / ITEMS_PER_PAGE);
    }

    function getSortedRows(rows, column, direction){
        direction = direction || 'asc';
        if (!column) {
            return rows;
        } else {
            return rows.sort(function (a, b) {
                if (direction == 'asc') {
                    return a[column] < b[column];
                } else {
                    return a[column] > b[column];
                }
            })
        }
    }

    function getFilteredRows(searchText) {
        if (searchText) {
            return self.rows().filter(function (row) {
                var accumValue = [];
                for (var prop in row) {
                    if (row.hasOwnProperty(prop)) {
                        accumValue.push(row[prop]);
                    }
                }
                return RegExp(searchText).test(accumValue.join(' '));
            });
        } else {
            return self.rows();
        }
    }
};