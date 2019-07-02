function tournamentModel() {
    this.name = ko.observable('test');
    this.date = ko.observable('');
    this.gameMode = ko.observable('');
    this.where = ko.observable('');

    this.createTournament = function() {
        var jsonData = ko.toJSON(this);
        $.ajax({
            type: "POST",
            url: "/api/Tournament/createTournament",
            data: jsonData,
            //success: success,
            dataType: "application/json",
            contentType: "application/json"
        });
        //$.post("/api/Tournament/createTournament", jsonData, function (returnedData) {
        //    alert("ok");
        //})
    }

};

var toCreate = new tournamentModel();

ko.applyBindings(toCreate);




