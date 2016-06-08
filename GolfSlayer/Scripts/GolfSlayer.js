var scoresVM;
var leaderboardVM;

$(function () {
    // Reference the auto-generated proxy for the hub.
    var chat = $.connection.scoresHub;

    //Inserts or updates the scores viewmodel
    var updateScores = function (vm) {
        if ($("#Scorecard").length > 0) {
            if (scoresVM) {
                ko.mapping.fromJS(vm, scoresVM);
            } else {
                scoresVM = ko.mapping.fromJS(vm);
                scoresVM.saveScore = saveScore;
                ko.applyBindings(scoresVM, document.getElementById("Scorecard"));
                $("#Scoreboard").show();
            }
        }
    }

    var updateLeaderboard = function (vm) {
        if ($("#Leaderboard").length > 0) {
            if (leaderboardVM)
                ko.mapping.fromJS(vm, leaderboardVM);
            else {
                leaderboardVM = ko.mapping.fromJS(vm);
                ko.applyBindings(leaderboardVM, document.getElementById("Leaderboard"));
            }
        }
    }

    //Calling SignalR server calls
    var getScores = function () {
        chat.server.getTeamScore(_teamID);
    }

    var getLeaderboard = function () {
        chat.server.getLeaderboardUpdate();
    }

    var saveScore = function (data) {
        if (data.Value() !== undefined && data.Value().length > 0 && data.Value() > 0)
        {
            var val = data.Value() > data.ParValue() ? data.ParValue() : data.Value();
            data.Value(val);
            chat.server.saveScore(ko.mapping.toJS(data));
        }   
        else if (data.Value() !== undefined && data.ID() > 0)
        {
            data.Value(0);
            chat.server.deleteScore(ko.mapping.toJS(data));
        }
            
    }

    //SignalR client side javascript calls
    chat.client.updateScores = function (vm) {
        updateScores(vm);
    }

    //leaderboard
    chat.client.updateLeaderboard = function (vm) {
        updateLeaderboard(vm);
    }
    
    // Start the connection.
    $.connection.hub.start().done(function () {
        chat.server.connect(_teamID);
    });
});

// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}

ko.bindingHandlers.formatScore = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var value = valueAccessor()();
        if (isNaN(value) || value <= 0) {
            $(element).val("");
        } else {
            $(element).val(value);
        }
    }
};

ko.bindingHandlers.onEnterPress = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var callback = valueAccessor();
        $(element).keypress(function (event) {
            var keyCode = (event.which ? event.which : event.keyCode);
            if (keyCode === 13) {
                console.log(bindingContext);
                console.log("enter pressed");
                $(element).trigger("blur");
                //callback.call(viewModel, bindingContext.$data);
                return false;
            }
            return true;
        });
    },
}

$("#pagenavigation a").on("click", function (evt) {
    evt.preventDefault();
    var clickTarget = $(evt.target).closest("a");
    $("#page_holder .tabContent").hide();
    $("#pagenavigation a").removeClass("active");
    clickTarget.addClass("active");
    var showTarget = $(clickTarget.attr("href"));
    showTarget.show();
});