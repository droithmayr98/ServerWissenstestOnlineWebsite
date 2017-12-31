$(document).ready(() => {
    console.log('jQuery Admin ready');

    //Testdaten
    $('#adminUsername').val('testadmin1');
    $('#adminPasswort').val('hallo123');

    //Anmeldevorgang
    $('#adminLoginButton').on('click', CheckAdminInfo);

    //Suchtrigger Aufgaben
    $('#stations_admin').on('change', StationSelected);
    $('#searchFrageText').on('click', SearchFrageText);

    $('#searchFrageField').bind('enterKey', SearchFrageText);
    $('#searchFrageField').keydown(function (e) {
        if (e.keyCode == 13) {
            $(this).trigger('enterKey');
            e.preventDefault();
        }
    });

    //AufgabeItemButtons
    $('.aufgabe_Info').on('click', AufgabeInfoClicked);
    $('.aufgabe_warning').on('click', AufgabeEditClicked);
    $('.aufgabe_danger').on('click', AufgabeDeleteClicked);

    //AdminItemButtons
    $('.admin_Info').on('click', AdminInfoClicked);
    $('.admin_warning').on('click', AdminEditClicked);
    $('.admin_danger').on('click', AdminDeleteClicked);


});

//LoginInformationen prüfen
function CheckAdminInfo() {
    //Eingabewerte holten + Testausgabe
    var input_username = $('#adminUsername').val();
    var input_password = $('#adminPasswort').val();

    console.log('Username: ' + input_username);
    console.log('Password: ' + input_password);

    //C# Überprüfung, ob gültiger User
    const url = '/Admin/CheckAdminInfo';
    $.post(url, {
        username: input_username,
        password: input_password
    }).then(reply => {
        console.log(`ServerReply CheckAdminInfo: ${reply}`);
        if (reply === 'username_fail') {
            $('#LoginError').html('User ist nicht vorhanden!');
        } else if (reply === 'password_fail') {
            $('#LoginError').html('Falsches Passwort!');
        } else if (reply === 'ok') {
            window.open('AdminOverview');
            self.close();
        }
    });

}

function StationSelected() {
    var selectedStation = $('#stations_admin').val();
    console.log(`Selected Station: ${selectedStation}`);

    if (selectedStation != "noItemSelected") {

        var aufgabenLI_list = $('#admin_aufgabenliste').children();
        for (var i = 0; i < aufgabenLI_list.length; i++) {
            var aufgabe_li = aufgabenLI_list[i];
            var id_split = aufgabe_li.id.split("-");
            if (id_split[1] != selectedStation) {
                $(`#${aufgabe_li.id}`).hide();
            } else {
                $(`#${aufgabe_li.id}`).show();
            }
        }

    } else {
        var aufgabenLI_list = $('#admin_aufgabenliste').children();
        for (var i = 0; i < aufgabenLI_list.length; i++) {
            var aufgabe_li = aufgabenLI_list[i];
            $(`#${aufgabe_li.id}`).show();
        }

    }

}

function SearchFrageText() {
    var eingabe = $('#searchFrageField').val();
    console.log(`Eingegebener Text: ${eingabe}`);

    var aufgabenLI_list = $('#admin_aufgabenliste').children();
    for (var i = 0; i < aufgabenLI_list.length; i++) {
        var aufgabe_li = aufgabenLI_list[i];
        var span_frage = $(`#${aufgabe_li.id}`).children().first();
        if (span_frage.text().toUpperCase().search(eingabe.toUpperCase()) != -1) {
            $(`#${aufgabe_li.id}`).show();
        } else {
            $(`#${aufgabe_li.id}`).hide();
        }
    }

}


//AufgabeButtonFunctions
function AufgabeInfoClicked(event) {
    console.log('Aufgabe Info Clicked');
    var id = event.target.id;
    console.log(`Target_ID: ${id}`);
    $('#aufgabeInfo_Modal').modal('show');
}

function AufgabeEditClicked(event) {
    console.log('Aufgabe Edit Clicked');
    var id = event.target.id;
    console.log(`Target_ID: ${id}`);
    $('#aufgabeEdit_Modal').modal('show');
}

function AufgabeDeleteClicked(event) {
    console.log('Aufgabe Delete Clicked');
    var id = event.target.id;
    console.log(`Target_ID: ${id}`);
    $('#aufgabeDelete_Modal').modal('show');
}


//AdminButtonFunctions
function AdminInfoClicked(event) {
    console.log('Admin Info Clicked');
    var id = event.target.id;
    console.log(`Target_ID: ${id}`);
    $('#adminInfo_Modal').modal('show');
}

function AdminEditClicked(event) {
    console.log('Admin Edit Clicked');
    var id = event.target.id;
    console.log(`Target_ID: ${id}`);
    $('#adminEdit_Modal').modal('show');
}

function AdminDeleteClicked(event) {
    console.log('Admin Delete Clicked');
    var id = event.target.id;
    console.log(`Target_ID: ${id}`);
    $('#adminDelete_Modal').modal('show');
}