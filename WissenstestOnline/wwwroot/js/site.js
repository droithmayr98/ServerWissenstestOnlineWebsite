$(document).ready(() => {
    console.log('jQuery ready');

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
        alert('Bitte geben Sie ihre Feuerwehr an!');
    } else {
        //Ajax Call Daten überprüfen
        //je nach Resultat Weiterlinken order Fehlermeldung Alert
        const url = "";
        window.open('Main/SelectStation');

    }
    
}

function StationInput() {
    console.log('enter StationInput');

    var selectedStations;
    try {
        selectedStations = $('#stations').val();
        var selectedMode = $('input[name=mode]:checked').val();

        //Testausgaben
        console.log(`StationsCount: ${selectedStations.length}`);
        console.log(`Modus: ${selectedMode}`);

        if (selectedMode == "learn") {
            window.open('AufgabeUmgebungLearn');
        } else {
            window.open('AufgabeUmgebungPractise');
        }
    } catch(err){
        alert('Bitte wähle Sie mindestens eine Station aus!');
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