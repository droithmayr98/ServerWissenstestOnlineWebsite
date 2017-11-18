$(document).ready(() => {
    console.log('jQuery Date ready');

    $('#datepickerLearn').css("background-color", "green");

});

function changeDatum(input, day, month, year) {
    console.log('checkDate');
    console.log('Input: ' + input);


    if (month < 10) {
        month = '0' + month;
    }
    if (day < 10) {
        day = '0' + day;
    }


    if (input == `${year}-${month}-${day}`) {
        $('#datepickerLearn').css("background-color", "green");
    } else {
        $('#datepickerLearn').css("background-color", "red");
    }

}