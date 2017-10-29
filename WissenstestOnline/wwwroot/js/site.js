﻿var global_bezirk = "";  //Globale Variablen über das gesamte Prpject sichtbar machen
var global_ort = "";
var global_stufe = "";
var global_mode = "";
var global_selectedStations = "";

var start_mode = 0;


$(document).ready(() => {

    

    console.log('jQuery ready');

    //Start Testeingaben
    $('#bezirkSelect').val('Schärding');
    $('#FeuerwehrEingabe').val('Eggerding');
    //EndeTesteingaben

    $('#UserLoginButton').on('click', UserCheck);
    $('#SelectStationButton').on('click', StationInput);

    //Cancel
    $('#cancelAufgabeLearn').on('click', CancelAufgabeLearn);
    $('#cancelAufgabePractise').on('click', CancelAufgabePractise);

    //Weiter
    $('#checkAufgabeLearn').on('click', WeiterAufgabeLearn);
    $('#checkAufgabePractise').on('click', WeiterAufgabePractise);

    //Zusatzinfo Click
    $('#zusatzinfoLearn').on('click', ShowZusatzinfo);
    $('#zusatzinfoPractise').on('click', ShowZusatzinfo);

    $('#closeResults').on('click', CloseResults);
    $('#closeZusatzinfo').on('click', CloseZusatzinfo);



    console.log(`Start Mode: ${$.mynamespace.global_bezirk}`);
    //Aufgaben laden
    if (start_mode === 1) {
        $('#headingLearn').html('Drücke \'Weiter\' um den Lernmodus zu starten!');
    }
    else {
        $('#headingLearn').html('Du bist hier falsch!');
    }







});



function UserCheck() {
    console.log('enter UserCheck');

    var selectedBezirk = $('#bezirkSelect').val();
    var ff = $('#FeuerwehrEingabe').val();
    var selectedStufe = $('input[name=stufe]:checked').val();


    //Testausgaben
    console.log(`Bezirk: ${selectedBezirk}`);
    console.log(`FF: ${ff}`);
    console.log(`Stufe: ${selectedStufe}`);


    if (ff == "") {
        $('#UserLoginError').html('Bitte geben Sie ihre Feuerwehr an!');
    } else {
        const url = "/Main/CheckUserInfo";
        $.post(url, { bezirk: selectedBezirk, ort: ff, stufe: selectedStufe })
            .then(reply =>{
                console.log(`ServerReply CheckUserInfo: ${reply}`);
                if (reply === 'ok') {
                    global_bezirk = selectedBezirk;
                    global_ort = ff;
                    global_stufe = selectedStufe;
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

    try {
        var selectedStations = $('#stations').val();
        var selectedMode = $('input[name=mode]:checked').val();

        //Testausgaben
        console.log(`StationsCount: ${selectedStations.length}`);
        console.log(`Modus: ${selectedMode}`);

        global_mode = selectedMode;
        global_selectedStations = selectedStations;

        if (selectedMode == "learn") {
            $('#StationSelectError').html('');
            window.open('AufgabeUmgebungLearn');
            start_mode = 1;

        } else {
            $('#StationSelectError').html('');
            window.open('AufgabeUmgebungPractise');
        }

    } catch(err){
        $('#StationSelectError').html('Bitte wähle Sie mindestens eine Station aus!');
    }
   

}

function CancelAufgabeLearn() {
    console.log('enter CancelAufgabeLearn');
    window.open('SelectStation');
    self.close();
    
}

function CancelAufgabePractise() {
    console.log('enter CancelAufgabePractise');
    window.open('ErgebnisOverview');
    self.close();
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
    
}

function WeiterAufgabePractise() {
    console.log('enter WeiterAufgabePractise');
}

function CloseZusatzinfo() {
    self.close();
}