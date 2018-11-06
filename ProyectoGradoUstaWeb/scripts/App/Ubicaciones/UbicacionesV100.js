var nsUbc = nsUbc || {};
nsUbc.SelNombre;
nsUbc.SelCmbTipoUbicacion;
nsUbc.SelCmbDescripcion;
nsUbc.SelBtnFrmUbicacion;
nsUbc.SelGridUbicaciones;
nsUbc.UrlTryAdd;
nsUbc.UrlTryDelete;
nsUbc.UrlTryUpdate;
nsUbc.IdActualizando = 0;

nsUbc.TryNuevaEditarUbicacion = function () {
    nsUbc.SelBtnFrmUbicacion = $("#btnSubmitFrmUbicacion");
    nsUbc.SelNombre = $("#iptNombreUbicacion");
    nsUbc.SelCmbTipoUbicacion = $("#cmbTiposUbicacion");
    nsUbc.SelCmbDescripcion = $("#iptDescripcion");
    nsUbc.SelGridUbicaciones = $("#gmUbicaciones");

    nsUbc.SelBtnFrmUbicacion.click();        
    var validTipoUbicacion = nsUbc.SelCmbTipoUbicacion.igCombo("validate");
    var valueNombreUbicacion = nsUbc.SelNombre.val();
    var valueDescripcion = nsUbc.SelCmbDescripcion.val();
    if (validTipoUbicacion && (valueNombreUbicacion != null && valueNombreUbicacion != undefined && valueNombreUbicacion != '')) {
        var valueIdTipoUbicacion = nsUbc.SelCmbTipoUbicacion.igCombo("value");
        if (nsUbc.IdActualizando == 0) {
            var lstCandidates = [
            { IdTipoUbicacion: valueIdTipoUbicacion, Nombre: valueNombreUbicacion, Descripcion: valueDescripcion, UrlImagen: '' }
            ];
            lstCandidates = JSON.stringify({ 'lstCandidates': lstCandidates });
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                url: nsUbc.UrlTryAdd,
                type: 'POST',
                dataType: 'json',
                data: lstCandidates,
                success: function (rp) {
                    nsGral.ProcesarResponseBasicVm(rp);
                    if (rp.Success) {
                        nsUbc.SelGridUbicaciones.igGrid("dataBind");
                        nsUbc.LimpiarForm();
                    }
                },
                error: function (er1, er2) {
                    nsGral.MensajeBad(er1 + er2);
                }
            });
        } else {
            var lstCandidates = [
            { Id: nsUbc.IdActualizando, NewIdTipoUbicacion: valueIdTipoUbicacion, NewNombre: valueNombreUbicacion, NewDescripcion: valueDescripcion, NewUrlImagen: '' }
            ];
            lstCandidates = JSON.stringify({ 'lstCandidates': lstCandidates });
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                url: nsUbc.UrlTryUpdate,
                type: 'POST',
                dataType: 'json',
                data: lstCandidates,
                success: function (rp) {
                    nsGral.ProcesarResponseBasicVm(rp);
                    if (rp.Success) {
                        nsUbc.SelGridUbicaciones.igGrid("dataBind");
                        nsUbc.LimpiarForm();
                    }
                },
                error: function (er1, er2) {
                    nsGral.MensajeBad(er1 + er2);
                }
            });         
        }
    }else{
        nsGral.MensajeAlert("Debe diligenciar los campos obligatorios");
    }
}

nsUbc.LimpiarForm = function () {
    nsUbc.IdActualizando = 0;
    nsUbc.SelNombre = $("#iptNombreUbicacion");
    nsUbc.SelCmbTipoUbicacion = $("#cmbTiposUbicacion");
    nsUbc.SelCmbDescripcion = $("#iptDescripcion");

    nsUbc.SelNombre.val('');
    nsUbc.SelCmbDescripcion.val('');
    nsUbc.SelCmbTipoUbicacion.igCombo("clearInput");
}

function rowDeletingGridUbicaciones(evt, ui) {
    nsUbc.SelGridUbicaciones = $("#gmUbicaciones");
    var idsAEliminar = new Array();
    idsAEliminar.push(ui.rowID);
    nsGral.PromptSweet('Desea borrar el registro', nsUbc.TryEliminarUbicacion, idsAEliminar);
    return false;
}

nsUbc.TryEliminarUbicacion = function (idsAEliminar) {
    nsUbc.SelGridUbicaciones = $("#gmUbicaciones");
    if (idsAEliminar != null && idsAEliminar != undefined && idsAEliminar.length > 0) {
        $.ajax(
            {
                url: nsUbc.UrlTryDelete,
                type: 'POST',
                data: { idsToDelete: idsAEliminar },
                success: function (rp) {
                    nsGral.ProcesarResponseBasicVm(rp);
                    if (rp.Success) {                       
                        nsUbc.SelGridUbicaciones.igGrid("dataBind");                        
                    }                   
                }, error: function (er1, er2) {
                    nsGral.MensajeBad(er1 + er2);
                }
            });
    } else {
        nsGral.MensajeBad("No se estan enviando datos a funcion en codigo cliente!");
    }    
}

nsUbc.LoadInfoEdit = function (currentId, currentIdTipoUbicacion) {
    nsUbc.SelNombre = $("#iptNombreUbicacion");
    nsUbc.SelCmbTipoUbicacion = $("#cmbTiposUbicacion");
    nsUbc.SelCmbDescripcion = $("#iptDescripcion");
    nsUbc.SelGridUbicaciones = $("#gmUbicaciones");
      
    nsUbc.IdActualizando = currentId;
    nsUbc.SelNombre.val(nsUbc.SelGridUbicaciones.igGrid("getCellValue", currentId, "NombreUbicacion"));
    nsUbc.SelCmbDescripcion.val(nsUbc.SelGridUbicaciones.igGrid("getCellValue", currentId, "Descripcion"));
    nsUbc.SelCmbTipoUbicacion.igCombo("value", currentIdTipoUbicacion);
    nsGral.HideShowTags('btnOpenNewUbicacion', 'divNuevaEdicionUbicacion');
}

