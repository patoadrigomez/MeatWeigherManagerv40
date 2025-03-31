/*!
 * IMCR Tools
 * Copyright 2022 IMCR.
 * Licensed under the MIT license
 */


/* ========================================================================
    alertMB(message, title)
    Cuadro de dialogo modal del tipo MessageBox.
  * ======================================================================== */
function alertMB(message, title) {
    $("<div></div>").dialog({
        buttons: { "Ok": function () { $(this).dialog("close"); } },
        close: function (event, ui) { $(this).remove(); },
        resizable: false,
        title: title,
        modal: true
    }).text(message);
};


/* ========================================================================
    Elimina un item Option desde un control DropDownList.
    Parametros: ddlID   id del control (ej: "#idddl")
                value   valor del item a eliminar.
  * ======================================================================== */
function DDLItemRemove(ddlID, value) {
    $(ddlID).find("option[value=" + value + "]").remove();
}

/* ========================================================================
    Agrega un item Option a un control DropDownList.
    Parametros: ddlID   id del control (ej: "#idddl")
                value   valor que tendra el item.
                text    texto que tendra el item.
  * ======================================================================== */
function DDLItemAdd(ddlID, value, text) {
    $(ddlID).append($('<option>',
        {
            value: value,
            text: text
        }));
}


/* ========================================================================
    Ordena alfabeticamente una lista de opciones por el texto de las opciones.
    Parametros: ddlID   id del control (ej: "#idddl")
  * ======================================================================== */
function DDLOrderByText(ddlID) {
    $(ddlID).append($(ddlID + " option").remove().sort(function (a, b) {
        var at = $(a).text().toUpperCase();
        var bt = $(b).text().toUpperCase();
        return (at > bt) ? 1 : ((at < bt) ? -1 : 0);
    }));
    //no realiza seleccion de un item
    $(ddlID).val("");
}

/* ========================================================================
    Ordena numericamente una lista de opciones por el valor (int) de las opciones.
    Parametros: ddlID   id del control (ej: "#idddl")
  * ======================================================================== */
function DDLOrderByValue(ddlID) {
    $(ddlID).append($(ddlID + " option").remove().sort(function (a, b) {
        var at = parseInt($(a).val());
        var bt = parseInt($(b).val());
        return (at > bt) ? 1 : ((at < bt) ? -1 : 0);
    }));
    //no realiza seleccion de un item
    $(ddlID).val("");
}

