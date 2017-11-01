$(document).ready(() => {  
    console.log('jQuery ready');

    var global_bezirk = "";
    var global_ort = "";
    var global_stufe = "";
    var global_mode = "";
    var global_stations = "";
    var global_aufgabenNr = "";


    //Start Testeingaben
    $('#bezirkSelect').val('Schärding');
    $('#FeuerwehrEingabe').val('Eggerding');
    //Ende Testeingaben

    //Click Events
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


    const url = '/Main/GetGlobalData';
    $.getJSON(url).then(map => {
        global_bezirk = map.bezirk;
        global_ort = map.ort;
        global_stufe = map.stufe;
        global_mode = map.mode;
        global_stations = map.stations;
        global_aufgabenNr = map.aufgabenNr;

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


        //Aufgaben laden
        if (global_mode === "learn") {
            $('#headingLearn').html('Drücke \'Weiter\' um den Lernmodus zu starten!');

            const url_aufgabe = `/Main/LoadFrageLearn?aufgabenNr=${global_aufgabenNr}`;
            $('#FrageLearn').load(url_aufgabe);

        }
        else {
            $('#headingLearn').html('Du bist hier falsch!');
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


    if (ff == "") {
        $('#UserLoginError').html('Bitte geben Sie ihre Feuerwehr an!');
    } else {
        const url = "/Main/CheckUserInfo";
        $.post(url, { bezirk: selectedBezirk, ort: ff, stufe: selectedStufe })
            .then(reply =>{
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

    try {
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


        if (selectedMode == "learn") {
            $('#StationSelectError').html('');
            window.open('AufgabeUmgebungLearn');

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