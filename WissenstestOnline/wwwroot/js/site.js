$(document).ready(() => {
    console.log('jQuery ready');

    var global_bezirk = "";
    var global_ort = "";
    var global_stufe = "";
    var global_mode = "";
    var global_stations = "";
    var global_aufgabenNr = "";
    var global_aufgabenCount = "";
    var global_aktuelleStation = "";


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
        else if (global_mode === "practise"){
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


        if (selectedMode === "learn") {
            $('#StationSelectError').html('');
            window.open('AufgabeUmgebungLearn');

        } else {
            $('#StationSelectError').html('');
            window.open('AufgabeUmgebungPractise');
        }

    } catch (err) {
        $('#StationSelectError').html('Bitte wähle Sie mindestens eine Station aus!');
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
    const url = '/Main/PressedButtonPractise';
    $.post(url).then(result => {
        console.log(`ServerReply WeiterAufgabePractise: ${result}`);
        location.reload();
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