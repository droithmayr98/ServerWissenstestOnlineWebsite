var global_bezirk = "";
var global_ort = "";
var global_stufe = "";
var global_mode = "";
var global_stations = "";
var global_aufgabenNr = "";
var global_aufgabenCount = "";
var global_aktuelleStation = "";
var global_antwortTypPractice = "";

$(document).ready(() => {
    console.log('jQuery ready');


    //Start Testeingaben
    $('#bezirkSelect').val('Schärding');
    $('#FeuerwehrEingabe').val('Eggerding');
    //Ende Testeingaben

    //Click Events
    $('#UserLoginButton').on('click', UserCheck);
    $('#SelectStationButton').on('click', StationInput);

    //Cancel
    $('.cancel').on('click', CancelAufgabe);

    //Weiter
    $('#checkAufgabeLearn').on('click', WeiterAufgabeLearn);
    $('#checkAufgabePractise').on('click', WeiterAufgabePractise);
    //Zurück
    $('#lastAufgabeLearn').on('click', ZurueckAufgabeLearn);

    //Zusatzinfo Click
    $('.showZusatzinfo').on('click', ShowZusatzinfo);

    $('#closeResults').on('click', CloseResults);
    $('#closeZusatzinfo').on('click', CloseZusatzinfo);


    const url = '/Main/GetGlobalData';
    $.getJSON(url).then(map => {
        global_bezirk = map.bezirk;
        global_ort = map.ort;
        global_stufe = map.stufe;
        global_mode = map.mode;
        global_stations = map.stations;
        global_aufgabenNr = map.aufgabenNr;
        global_aufgabenCount = map.aufgabenCount;
        global_aktuelleStation = map.aktuelleStation;
        global_antwortTypPractice = map.antwortTypPractice;


        //Datenzugriff nur hier
        //Methoden hier drinnen

        //Global Testlogs
        console.log('Global Data Testlogs:');
        console.log(`Bezirk: ${global_bezirk}`);
        console.log(`Ort: ${global_ort}`);
        console.log(`Stufe: ${global_stufe}`);
        console.log(`Mode: ${global_mode}`);
        console.log(`Stations: ${global_stations}`);
        console.log(`AufgabenNr: ${global_aufgabenNr}`);
        console.log(`AufgabenCount: ${global_aufgabenCount}`);
        console.log(`AktuelleStation: ${global_aktuelleStation}`);
        console.log(`AktuellerAntwortTypPractice: ${global_antwortTypPractice}`);


        //Aufgaben laden
        if (global_mode === "learn") {

            //Update AufgabeUmgebungLearn
            $('#aktuelleFrageCounterLearn').html(`${global_aufgabenNr}/${global_aufgabenCount}`);
            $('#aktuelleStationLearn').html(global_aktuelleStation);

            //funktioniert
            const url_frage = `/Main/LoadFrage?aufgabenNr=${global_aufgabenNr}`;
            $('#FrageLearn').load(url_frage, () => {
                const url_antwort = `/Main/LoadAntwortLearn?aufgabenNr=${global_aufgabenNr}`;
                $('#AntwortLearn').load(url_antwort);
            }
            );

            //Problem: Beides kann nicht aufgerufen werden
            //soll mit include funktionieren
            //INCLUDE selektiert nicht die FroreignKeys der FroreignKeys

            /*const url_frage = `/Main/LoadFrage?aufgabenNr=${global_aufgabenNr}`;
            $('#FrageLearn').load(url_frage);

            const url_antwort = `/Main/LoadAntwortLearn?aufgabenNr=${global_aufgabenNr}`;
            $('#AntwortLearn').load(url_antwort);*/

            const url_info = '/Main/LoadZusatzinfo';
            $('#loadZusatzinfo').load(url_info);

        }
        else if (global_mode === "practise") {
            //Übungsmodus

            $('#aktuelleFrageCounterPractise').html(`${global_aufgabenNr}/${global_aufgabenCount}`);
            $('#aktuelleStationPractise').html(global_aktuelleStation);


            const url_frage = `/Main/LoadFrage?aufgabenNr=${global_aufgabenNr}`;
            $('#FragePractise').load(url_frage, () => {
                const url_antwort = `/Main/LoadAntwortPractise?aufgabenNr=${global_aufgabenNr}`;
                $('#AntwortPractise').load(url_antwort);
            });


            const url_info = '/Main/LoadZusatzinfo';
            $('#loadZusatzinfo').load(url_info);
        }


    });



});



function UserCheck() {
    console.log('enter UserCheck');

    var selectedBezirk = $('#bezirkSelect').val();
    var ff = $('#FeuerwehrEingabe').val();
    var selectedStufe = $('input[name=stufe]:checked').val();


    //Testausgaben
    console.log('EingabeWerte Testlogs:');
    console.log(`Bezirk: ${selectedBezirk}`);
    console.log(`FF: ${ff}`);
    console.log(`Stufe: ${selectedStufe}`);


    if (ff === "") {
        $('#UserLoginError').html('Bitte geben Sie ihre Feuerwehr an!');
    } else {
        const url = "/Main/CheckUserInfo";
        $.post(url, { bezirk: selectedBezirk, ort: ff, stufe: selectedStufe })
            .then(reply => {
                console.log(`ServerReply CheckUserInfo: ${reply}`);
                if (reply === 'ok') {
                    $('#UserLoginError').html('');
                    window.open('Main/SelectStation');
                } else {
                    $('#UserLoginError').html('Bitte überprüfen Sie ihre Eingabedaten!');
                }
            });

    }

}

function StationInput() {
    console.log('enter StationInput');

    var selectedStations = $('#stations').val();
    var selectedMode = $('input[name=mode]:checked').val();

    //Testausgaben
    console.log('EingabeWerte Testlogs:');
    console.log(`StationsCount: ${selectedStations.length}`);
    console.log(`Modus: ${selectedMode}`);

    const url = '/Main/GlobalStationData';
    $.post(url, {
        stations: selectedStations,
        mode: selectedMode
    }).then(result => console.log(`ServerReply StationInput: ${result}`));

    if (selectedStations == '') {
        //Errormeldung
        $('#StationSelectError').html('Bitte wähle Sie mindestens eine Station aus!');
    }
    else if (selectedMode === "learn") {
        $('#StationSelectError').html('');
        window.open('AufgabeUmgebungLearn');

    } else {
        $('#StationSelectError').html('');
        window.open('AufgabeUmgebungPractise');
    }


}

function CancelAufgabe() {
    console.log('enter CancelAufgabe');
    const url = '/Main/CancelAufgabe';
    $.post(url).then(result => {
        console.log(`ServerReply CancelAufgabe: ${result}`);
        window.open('SelectStation');
        self.close();
    });


}

function ShowZusatzinfo() {
    console.log('enter ShowZusatzinfo');
    window.open('Zusatzinfo');
}

function CloseResults() {
    console.log('enter CloseResults');
    window.open('SelectStation');
    self.close();
}

function WeiterAufgabeLearn() {
    console.log('enter WeiterAufgabeLearn');
    const url = '/Main/PressedButtonLearn';
    $.post(url, {
        pressedButtonLearn: "weiter"
    }).then(result => {
        console.log(`ServerReply WeiterAufgabeLearn: ${result}`);
        location.reload();
        //window.open('AufgabeUmgebungLearn');
        //self.close();
    });
}

function WeiterAufgabePractise() {
    console.log('enter WeiterAufgabePractise');
    var buttonAction = $('#checkAufgabePractise').val();

    const url = '/Main/PressedButtonPractise';
    $.post(url, {
        buttonActionPractice: buttonAction
    }).then(result => {
        console.log(`ServerReply WeiterAufgabePractise: ${result}`);
        if (result == "Weiter") {
            $('#checkAufgabePractise').val("Check");
            location.reload();
        } else if (result == "Auswertung") {
            window.open('ErgebnisOverview');
            self.close();
        } else {
            switch (global_antwortTypPractice) {
                case "A_T":
                    var texteingabe = $('#textantwort').val();
                    console.log(`Eingabe Text: ${texteingabe}`);
                    //Ajax
                    const url1 = '/Main/CheckAntwortText';
                    $.post(url1, {
                        texteingabe: texteingabe
                    }).then(result => {
                        if (result) {
                            $('#antwortTextEingabe').addClass('has-success');
                            $('#antwortTextEingabe').addClass('has-feedback');
                            $('#textantwort').attr("disabled", "true");
                        } else {
                            $('#antwortTextEingabe').addClass('has-error');
                            $('#antwortTextEingabe').addClass('has-feedback');
                            $('#textantwort').attr("disabled", "true");

                            $('#rightValText').show();
                        }
                    });
                    break;
                case "A_S":
                    var sliderTextEingabe = $('#textfield_slider').val();
                    console.log(`Eingabe Slider: ${sliderTextEingabe}`);
                    //Ajax
                    const url2 = '/Main/CheckAntwortSlider';
                    $.post(url2, {
                        slidervalue: sliderTextEingabe
                    }).then(result => {
                        if (result) {
                            $('#textfield_slider').css("background-color", "green");
                            $('#textfield_slider').attr("disabled", "true");
                            $('#sliderBar').attr("disabled", "true");
                        } else {
                            $('#textfield_slider').css("background-color", "red");
                            $('#textfield_slider').attr("disabled", "true");
                            $('#sliderBar').attr("disabled", "true");

                            $('#rightValSlider').show();
                        }
                    });
                    break;
                case "A_DP":
                    var dateEingabe = $('#datepickerPractice').val();
                    console.log(`Eingabe DatePicker: ${dateEingabe}`);
                    //Ajax
                    const url3 = '/Main/CheckAntwortDate';
                    $.post(url3, {
                        date: dateEingabe
                    }).then(result => {
                        if (result) {
                            $('#datepickerPractice').css("background-color", "green");
                            $('#datepickerPractice').attr("disabled", "true");
                        } else {
                            $('#datepickerPractice').css("background-color", "red");
                            $('#datepickerPractice').attr("disabled", "true");

                            $('#rightValDatePicker').show();
                        }
                    });
                    break;
                case "A_CB:T":

                    var input_cb = $('.checkboxInput');
                    var checkedBoxes = new Array();
                    for (var i = 0; i < input_cb.length; i++) {
                        var id_cb = input_cb.eq(i).attr('id');
                        if ($(`#${id_cb}`).is(":checked")) {
                            checkedBoxes[i] = input_cb.eq(i).attr('id');
                        } else {
                            checkedBoxes[i] = null;
                        }

                    }

                    const url4 = '/Main/CheckAntwortCheckBoxes';
                    $.post(url4, {
                        cbValue: checkedBoxes
                    }).then(result => {
                        if (result) {

                            for (var i = 0; i < input_cb.length; i++) {
                                var id_cb = input_cb.eq(i).attr('id');
                                if ($(`#${id_cb}`).is(":checked")) {
                                    $(`#cb_${id_cb}`).css("background-color", "green");
                                }
                                $(`#${id_cb}`).attr("disabled", "true");
                            }

                        } else {

                            for (var i = 0; i < input_cb.length; i++) {
                                var id_cb = input_cb.eq(i).attr('id');
                                if ($(`#${id_cb}`).is(":checked")) {
                                    $(`#cb_${id_cb}`).css("background-color", "red");
                                }
                                $(`#${id_cb}`).attr("disabled", "true");
                            }

                            $('#rightValCheckBox').show();
                        }
                    });
                    break;
                case "A_RB:T":

                    var input_rb = $('.radioButtonInput');

                    var id_selectedRB = $('input[name=gruppe]:checked').attr('id');
                    console.log(`Selected RadioButton ID: ${id_selectedRB}`);

                    var rbEingabe = $('input[name=gruppe]:checked').val();
                    console.log(`Eingabe RadioButton: ${rbEingabe}`);
                    //Ajax
                    const url5 = '/Main/CheckAntwortRadioButtons';
                    $.post(url5, {
                        rbValue: rbEingabe
                    }).then(result => {
                        if (result) {
                            $(`#rb_${id_selectedRB}`).css("background-color", "green");

                            for (var i = 0; i < input_rb.length; i++) {
                                var id_rb = input_rb.eq(i).attr('id');
                                $(`#${id_rb}`).attr("disabled", "true");
                            }

                        } else {
                            $(`#rb_${id_selectedRB}`).css("background-color", "red");

                            for (var i = 0; i < input_rb.length; i++) {
                                var id_rb = input_rb.eq(i).attr('id');
                                $(`#${id_rb}`).attr("disabled", "true");
                            }

                            $('#rightValRadioButton').show();

                        }
                    });
                    break;
                default:
                    console.log("Noch nicht gemacht!");
            }

            if (global_aufgabenNr == global_aufgabenCount) {
                $('#checkAufgabePractise').val("Auswertung");
            } else {
                $('#checkAufgabePractise').val("Weiter");
            }

        }
        //window.open('AufgabeUmgebungLearn');
        //self.close();
    });
}

function CloseZusatzinfo() {
    self.close();
}

function ZurueckAufgabeLearn() {
    console.log('enter ZurueckAufgabeLearn');
    const url = '/Main/PressedButtonLearn';
    $.post(url, {
        pressedButtonLearn: "zurueck"
    }).then(result => {
        console.log(`ServerReply ZurueckAufgabeLearn: ${result}`);
        location.reload();
        //window.open('AufgabeUmgebungLearn');
        //self.close();
    });

}