/**
 * Widjet for Navigation Panel
 */
function NavPanelViewModel(links, url) {
    
    var self = this;

    self.links = ko.observableArray(links);

    self.countOfStudents = ko.observable();
    self.countOfInstructors = ko.observable();
    self.currentTime = ko.observable();

    ko.dependentObservable((function updateTimer() {
        var fn = function () {
            self.currentTime(getFullTime(new Date));
        };
        window.setInterval(fn, 1000);
        return fn;
    })(), this);

    ko.dependentObservable((function () {
        var data = {};
        var fn = function () {
            $.ajax({
                url: url + '/CountOfInstances',
                type: 'POST',
                data: data,
                context: this,
                success: function (data) {
                    self.countOfStudents(data.students);
                    self.countOfInstructors(data.instructors);
                }
            });
        };
        window.setInterval(fn, 3000);
        return fn;
    })(), this);

    function changeCount(event, newCount) {
        this.textContent = newCount;
    }

    function getFullTime(date) {
        return date.getHours() + ':' + date.getMinutes() + ':' + date.getSeconds();
    }

};