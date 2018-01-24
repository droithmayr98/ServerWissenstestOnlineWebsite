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

    //Suchtrigger Fragen
    $('#searchFrageText_AufgabeEditView').on('click', SearchFrageText_AufgabeEditView_Frage);

    $('#searchFrageField_AufgabeEditView').bind('enterKey2', SearchFrageText_AufgabeEditView_Frage);
    $('#searchFrageField_AufgabeEditView').keydown(function (e) {
        if (e.keyCode == 13) {
            $(this).trigger('enterKey2');
            e.preventDefault();
        }
    });

    //Suchtrigger Antworten
    $('#searchAntwortText_AufgabeEditView').on('click', SearchAntwortText_AufgabeEditView_Antwort);

    $('#searchAntwortField_AufgabeEditView').bind('enterKey3', SearchAntwortText_AufgabeEditView_Antwort);
    $('#searchAntwortField_AufgabeEditView').keydown(function (e) {
        if (e.keyCode == 13) {
            $(this).trigger('enterKey3');
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

    //FrageItemButtons
    $('.frage_Info').on('click', FrageInfoClicked);
    $('.frage_warning').on('click', FrageEditClicked);
    $('.frage_danger').on('click', FrageDeleteClicked);

    //AntwortItemButtons
    $('.antwort_Info').on('click', AntwortInfoClicked);
    $('.antwort_warning').on('click', AntwortEditClicked);
    $('.antwort_danger').on('click', AntwortDeleteClicked);

    //new Antwort
    $('#newAntwort_AufgabeEditView').on('click', CreateAntwortClicked);
    $('#create_antwort_button').on('click', CreateAntwort);

    //new Frage
    $('#newFrage_AufgabeEditView').on('click', CreateFrageClicked);
    $('#create_frage_button').on('click', CreateFrage);

    //Frage auswählen
    $('.frageobject').on('click', SelectFrage);

    //Antworttyp Select
    $('#aufgabeEdit_antworttyp_select').on('change', SelectAntworttyp);

    //AntwortDialog Neu Select Typ
    $('#antwortTypSelectNew').on('change', AntwortNewDialogTypeChanged);

    //new Admin
    $('#create_new_admin').on('click', CreateAdminClicked);
    $('#create_admin_button').on('click', CreateAdmin);

    //editAufgabe
    $('#adminEdit_bezirk_select').on('change', SetStandorteBezirk);


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

//Fehler ComboBox wird nicht beachtet
function SearchFrageText_AufgabeEditView_Frage() {
    var eingabe = $('#searchFrageField_AufgabeEditView').val();
    console.log(`Eingegebener Text: ${eingabe}`);

    var station_selected = $('#stations_admin').val();
    console.log(`Ausgewählte Station: ${station_selected}`);
    if (station_selected != "noItemSelected") {

    }

    var fragenLI_list = $('#frageliste_aufgabeEditView').children();
    for (var i = 0; i < fragenLI_list.length; i++) {
        var frage_li = fragenLI_list[i];

        var span_frage = $(`#${frage_li.id}`).children().first();

        if (span_frage.text().toUpperCase().search(eingabe.toUpperCase()) != -1) {
            $(`#${frage_li.id}`).show();
        } else {
            $(`#${frage_li.id}`).hide();
        }
    }

}


//Fehler ComboBox wird nicht beachtet
function SearchAntwortText_AufgabeEditView_Antwort() {
    var eingabe = $('#searchAntwortField_AufgabeEditView').val();
    console.log(`Eingegebener Text: ${eingabe}`);

    var antworttyp_selected = $('#aufgabeEdit_antworttyp_select').val();
    console.log(`Antworttyp Selected: ${antworttyp_selected}`);
    if (antworttyp_selected != "noItemSelected") {

    }

    var antwortenLI_list = $('#antwortliste_aufgabeEditView').children();
    for (var i = 0; i < antwortenLI_list.length; i++) {
        var antwort_li = antwortenLI_list[i];

        var span_antwort = $(`#${antwort_li.id}`).children().first();

        if (span_antwort.text().toUpperCase().search(eingabe.toUpperCase()) != -1) {
            $(`#${antwort_li.id}`).show();
        } else {
            $(`#${antwort_li.id}`).hide();
        }
    }
}

//AufgabeButtonFunctions
function AufgabeInfoClicked(event) {
    console.log('Aufgabe Info Clicked');
    var id = event.target.id;
    console.log(`Target_ID: ${id}`);

    const url = `/Admin/GetAufgabeInfo?aufgabe_id=${id}`;
    $('#loadModal').load(url, () => {
        $('#aufgabeInfo_Modal').modal('show');
    });

}

function AufgabeEditClicked(event) {
    console.log('Aufgabe Edit Clicked');
    var id = event.target.id;
    console.log(`Target_ID: ${id}`);

    window.open(`AufgabeEditView?aufgabe_id=${id}`);
    //$('#aufgabeEdit_Modal').modal('show');


}

function AufgabeDeleteClicked(event) {
    console.log('Aufgabe Delete Clicked');
    var id = event.target.id;
    console.log(`Target_ID: ${id}`);

    const url = `/Admin/GetAufgabeDelete?aufgabe_id=${id}`;
    $('#loadModal').load(url, () => {
        $('#aufgabeDelete_Modal').modal('show');
        $('#deleteAufgabe').on('click', DeleteAufgabe);
    });


}

//AdminButtonFunctions
function AdminInfoClicked(event) {
    console.log('Admin Info Clicked');
    var id = event.target.id;
    console.log(`Target_ID: ${id}`);

    const url = `/Admin/GetAdminInfo?admin_id=${id}`;
    $('#loadModal').load(url, () => {
        $('#adminInfo_Modal').modal('show');
    });

}

function AdminEditClicked(event) {
    console.log('Admin Edit Clicked');
    var id = event.target.id;
    console.log(`Target_ID: ${id}`);

    const url = `/Admin/GetAdminEdit?admin_id=${id}`;
    $('#loadModal').load(url, () => {
        $('#adminEdit_Modal').modal('show');
        $('#saveAdminChanges').on('click', SaveAdminChanges);
    });

}

function AdminDeleteClicked(event) {
    console.log('Admin Delete Clicked');
    var id = event.target.id;
    console.log(`Target_ID: ${id}`);

    const url = `/Admin/GetAdminDelete?admin_id=${id}`;
    $('#loadModal').load(url, () => {
        $('#adminDelete_Modal').modal('show');
        $('#deleteAdmin').on('click', DeleteAdmin);
    });

}

function SaveAdminChanges() {
    console.log('enter SaveAdminChanges');

    //Eingabewerte auslesen und testen
    var admin_id_selected = $('#admin_id_edit').text();
    var username_input = $('#changeUsername').val();
    var password_input = $('#changePassword').val();
    var can_create_acc_input = $('input[name=admin_create_option]:checked').val();
    var can_edit_acc_input = $('input[name=admin_edit_option]:checked').val();
    var can_delete_acc_input = $('input[name=admin_delete_option]:checked').val();

    console.log('Testausgabe-EditAdminData Benutzereingaben:');
    console.log(`Admin_ID: ${admin_id_selected}`);
    console.log(`Username: ${username_input}`);
    console.log(`Password: ${password_input}`);
    console.log(`Can_Create_Acc: ${can_create_acc_input}`);
    console.log(`Can_Edit_Acc: ${can_edit_acc_input}`);
    console.log(`Can_Delete_Acc: ${can_delete_acc_input}`);

    const url = `/Admin/SaveAdminChanges`;
    $.post(url, {
        admin_id: admin_id_selected,
        username: username_input,
        password: password_input,
        can_create_acc: can_create_acc_input,
        can_edit_acc: can_edit_acc_input,
        can_delete_acc: can_delete_acc_input
    }).then(result => {
        console.log(`ServerReply SaveAdminChanges: ${result}`);
        location.reload();
    });


}

function DeleteAdmin() {
    console.log('enter DeleteAdmin');

    var admin_id = $('#admin_id_delete').text();
    console.log(`Admin_ID: ${admin_id}`);

    const url = `/Admin/DeleteAdmin`;
    $.post(url, {
        admin_id: admin_id
    }).then(result => {
        console.log(`ServerReply DeleteAdmin: ${result}`);
        location.reload();
    });

}

function CreateAdminClicked() {
    console.log('enter CreateAdminClicked');
    $('#adminCreate_Modal').modal('show');
}

function CreateAdmin() {
    console.log('enter CreateAdmin');

    //Eingabewerte auslesen und testen
    var username_input = $('#newAdmin_Username').val();
    var password_input = $('#newAdmin_Password').val();
    var can_create_acc_input = $('input[name=admin_create_option]:checked').val();
    var can_edit_acc_input = $('input[name=admin_edit_option]:checked').val();
    var can_delete_acc_input = $('input[name=admin_delete_option]:checked').val();

    console.log(`Username: ${username_input}`);
    console.log(`Password: ${password_input}`);
    console.log(`Can_Create_Acc: ${can_create_acc_input}`);
    console.log(`Can_Edit_Acc: ${can_edit_acc_input}`);
    console.log(`Can_Delete_Acc: ${can_delete_acc_input}`);

    const url = `/Admin/CreateAdmin`;
    $.post(url, {
        username: username_input,
        password: password_input,
        can_create_acc: can_create_acc_input,
        can_edit_acc: can_edit_acc_input,
        can_delete_acc: can_delete_acc_input
    }).then(result => {
        console.log(`ServerReply CreateAdmin: ${result}`);
        location.reload();
    });

}

function SetStandorteBezirk() {
    console.log('enter SetStandorteBezirk');
    var bezirk_selected = $('#adminEdit_bezirk_select').val();
    console.log(`Selected Bezirk: ${bezirk_selected}`);

    const url = `/Admin/SetStandorteBezirkComboBox?bezirk=${bezirk_selected}`;
    $('#adminEdit_ort_select').load(url);

}

//FrageButtonFunctions
function FrageInfoClicked(event) {
    console.log('Frage InfoClicked');
    var id = event.target.id;
    console.log(`Target_ID: ${id}`);

    const url = `/Admin/GetFrageInfo?frage_id=${id}`;
    $('#loadFrageModal').load(url, () => {
        $('#frageInfo_Modal').modal('show');
    });

}

function FrageEditClicked(event) {
    console.log('Frage EditClicked');
    var id = event.target.id;
    console.log(`Target_ID: ${id}`);

    const url = `/Admin/GetFrageEdit?frage_id=${id}`;
    $('#loadFrageModal').load(url, () => {
        $('#frageEdit_Modal').modal('show');
        $('#saveFrageChanges').on('click', SaveFrageEdit);
    });

}

function FrageDeleteClicked(event) {
    console.log('Frage DeleteClicked');
    var id = event.target.id;
    console.log(`Target_ID: ${id}`);

    const url = `/Admin/GetFrageDelete?frage_id=${id}`;
    $('#loadFrageModal').load(url, () => {
        $('#frageDelete_Modal').modal('show');
        $('#deleteFrage').on('click', DeleteFrage);
    });

}

function SaveFrageEdit() {
    console.log('enter SaveFrageEdit');
    var frage_id_selected = $('#frageEdit_Id').text();
    console.log(`Selected Frage ID: ${frage_id_selected}`);
    var fragetext_change = $('#textarea_fragetext').val();

    const url = `/Admin/SaveFrageChanges`;
    $.post(url, {
        frage_id: frage_id_selected,
        fragetext: fragetext_change
    }).then(result => {
        console.log(`ServerReply SaveFrageChanges: ${result}`);
        location.reload();
    });



}

function CreateFrageClicked() {
    console.log('enter CreateFrageClicked');
    $('#frageCreate_Modal').modal('show');

}

function CreateFrage() {
    console.log('enter CreateFrage');
    var new_frage_text = $('#newFrage_Fragetext').val();

    const url = `/Admin/CreateFrage`;
    $.post(url, {
        fragetext: new_frage_text
    }).then(result => {
        console.log(`ServerReply CreateFrage: ${result}`);
        location.reload();
    });

}

function DeleteFrage() {
    console.log('enter DeleteFrage');
    //Nur Frage löschen --> wenn Frage in keiner aufgabe vorkommt
    //Was passiert mit aufgaben, die die Frage beinhalten???
    var frage_id_selected = $('#frage_id_delete').text();

    const url = `/Admin/DeleteFrage`;
    $.post(url, {
        frage_id: frage_id_selected
    }).then(result => {
        console.log(`ServerReply DeleteFrage: ${result}`);
        if (result === "ok") {
            location.reload();
        } else {
            //Frage darf nicht gelöscht werden --> Frage ist noch in einer Aufgabe vorhanden
            $('#frageDeleteError_Modal').modal('show');

        }

    });

}

function SelectFrage(event) {
    console.log('enter SelectFrage');
    var id = event.target.id;
    console.log(`Target_ID: ${id}`);

    $(`#li_${id}`).css("background-color", "green");

}

function SelectAntworttyp() {
    console.log('enter SelectAntworttyp');

    var selectedAntworttyp = $('#aufgabeEdit_antworttyp_select').val();
    console.log(`Selected Antworttyp: ${selectedAntworttyp}`);

    if (selectedAntworttyp != "noItemSelected") {

        var antwortLI_list = $('#antwortliste_aufgabeEditView').children();
        for (var i = 0; i < antwortLI_list.length; i++) {
            var antwort_li = antwortLI_list[i];
            var id_split = antwort_li.id.split("-");
            if (id_split[1] != selectedAntworttyp) {
                $(`#${antwort_li.id}`).hide();
            } else {
                $(`#${antwort_li.id}`).show();
            }
        }

    } else {
        var antwortLI_list = $('#antwortliste_aufgabeEditView').children();
        for (var i = 0; i < antwortLI_list.length; i++) {
            var antwort_li = antwortLI_list[i];
            $(`#${antwort_li.id}`).show();
        }

    }


}

//AntwortButtonFunctions
function AntwortInfoClicked(event) {
    console.log('AntwortInfoClicked');
    var id_string = event.target.id;
    console.log(`Target_ID: ${id_string}`);
    var id = id_string.split("_");
    console.log(`Parameter ID: ${id[1]}`);

    const url = `/Admin/GetAntwortInfo?antwort_id=${id[1]}`;
    $('#loadAntwortModal').load(url, () => {
        $('#antwortInfo_Modal').modal('show');
    });

}

function AntwortEditClicked(event) {
    console.log('AntwortEditClicked');
    var id_string = event.target.id;
    console.log(`Target_ID: ${id_string}`);
    var id = id_string.split("_");
    console.log(`Parameter ID: ${id[1]}`);

    const url = `/Admin/GetAntwortEdit?antwort_id=${id[1]}`;
    $('#loadAntwortModal').load(url, () => {
        $('#antwortEdit_Modal').modal('show');

        //Events
        $('#antwortEditTypSelect').on('change', AntwortEditDialogTypeChanged);

        console.log('AntwortPartialView aufrufen');
        //Antwortteil Spezial laden
        const url2 = `/Admin/LoadAntwortEditAntwort?antwort_id=${id[1]}`;
        $('#partialViewAntworten').load(url2, () => {
            console.log('Load LoadAntwortEditAntwort');

            //Antwort speichern
            $('#saveAntwortChanges').on('click', SaveAntwortChanges);

        });

    });

}

function AntwortDeleteClicked(event) {
    console.log('AntwortDeleteClicked');
    var id_string = event.target.id;
    console.log(`Target_ID: ${id_string}`);
    var id = id_string.split("_");
    console.log(`Parameter ID: ${id[1]}`);

    const url = `/Admin/GetAntwortDelete?antwort_id=${id[1]}`;
    $('#loadAntwortModal').load(url, () => {
        $('#antwortDelete_Modal').modal('show');
        $('#deleteAntwort').on('click', DeleteAntwort);
    });


}

function AntwortEditDialogTypeChanged() {
    var selected_Typ = $('#antwortEditTypSelect').val();
    console.log(`Changed Typ: ${selected_Typ}`);

    const url = `/Admin/SetNewAntwortType?typ_id=${selected_Typ}`;
    $('#partialViewAntworten').load(url, () => {
    });

}

function DeleteAntwort() {
    console.log('enter DeleteAntwort');
    //Nur Antwort löschen --> wenn Antwort in keiner aufgabe vorkommt
    //Was passiert mit aufgaben, die die Antwort beinhalten???
    var antwort_id_selected = $('#antwort_id_delete').text();

    const url = `/Admin/DeleteAntwort`;
    $.post(url, {
        antwort_id: antwort_id_selected
    }).then(result => {
        console.log(`ServerReply DeleteAntwort: ${result}`);
        if (result === "ok") {
            location.reload();
        } else {
            //Antwort darf nicht gelöscht werden --> Antwort ist noch in einer Aufgabe vorhanden
            $('#antwortDeleteError_Modal').modal('show');

        }

    });

}

function DeleteAufgabe() {
    console.log('enter DeleteAufgabe');
    var aufgabe_id_selected = $('#aufgabe_id_delete').text();

    const url = `/Admin/DeleteAufgabe`;
    $.post(url, {
        aufgabe_id: aufgabe_id_selected
    }).then(result => {
        console.log(`ServerReply DeleteAufgabe: ${result}`);
        if (result === "ok") {
            location.reload();
        }

    });

}

function SaveAntwortChanges() {
    console.log('enter SaveAntwortChanges');
    var selected_Typ_string = $('#antwortEditTypSelect').text();

    switch (selected_Typ_string) {
        case "A_T":

            break;
        case "A_S":

            break;
        case "A_DP":

            break;
        case "A_RB:T":

            break;
        case "A_CB:T":

            break;
        default:
            console.log('Keine Antwort zum Speichern');

    }
    
}

function CreateAntwortClicked() {
    console.log('enter AntwortNewClicked');
    $('#antwortCreate_Modal').modal('show');
}

function AntwortNewDialogTypeChanged() {
    var selected_Typ = $('#antwortTypSelectNew').val();
    console.log(`Changed Typ: ${selected_Typ}`);

    const url = `/Admin/SetNewAntwortType?typ_id=${selected_Typ}`;
    $('#partialViewAntwortenNew').load(url, () => {
    });
}

function CreateAntwort() {
    console.log('enter CreateAntwort');
    var selected_Typ_string = $('#antwortEditTypSelect').text();

    switch (selected_Typ_string) {
        case "A_T":

            break;
        case "A_S":

            break;
        case "A_DP":

            break;
        case "A_RB:T":

            break;
        case "A_CB:T":

            break;
        default:
            console.log('Keine Antwort zum Speichern');

    }
}