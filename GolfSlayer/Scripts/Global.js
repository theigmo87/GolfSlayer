$(document).ready(function () {

    $("#admin-message-close").click(function () {
        $(".admin-message").animate({ "top": "-9999px" }, "slow");
    });

    $('.admin-message').change(function () {
        $(this).animate({ "top": "15%" }, "slow");
    });

    previousVal = "";
    function InputChangeListener() {
        if ($('#admin-message-text').html()
           != previousVal)
        {
            previousVal = $('#admin-message-text').html();
            $('#admin-message-text').change();
        }
    }

    setInterval(InputChangeListener, 500);
});

function AdminMessage(msg) {
    $("#admin-message-text").html(msg);
}