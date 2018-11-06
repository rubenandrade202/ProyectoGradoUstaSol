var nsPro = nsPro || {};
nsPro.IdActualizando = 0;
nsPro.SelNombre;
nsPro.SelDescripcion;
nsPro.SelNit;
nsPro.SelTelefono;
nsPro.SelVisita_Lunes;
nsPro.SelVisita_Martes;
nsPro.SelVisita_Miercoles;
nsPro.SelVisita_Jueves;
nsPro.SelVisita_Viernes;
nsPro.SelVisita_Sabado;
nsPro.SelVisita_Domingo;
nsPro.SelEntrega_Lunes;
nsPro.SelEntrega_Martes;
nsPro.SelEntrega_Miercoles;
nsPro.SelEntrega_Jueves;
nsPro.SelEntrega_Viernes;
nsPro.SelEntrega_Sabado;
nsPro.SelEntrega_Domingo;
nsPro.SelGridProveedores;
nsPro.UrlTryAdd;
nsPro.UrlTryDelete;
nsPro.UrlTryUpdate;


nsPro.LoadInfoEdit = function (id, Visita_Lunes, Visita_Martes, Visita_Miercoles, Visita_Jueves, Visita_Viernes, Visita_Sabado, Visita_Domingo, Entrega_Lunes, Entrega_Martes, Entrega_Miercoles, Entrega_Jueves, Entrega_Viernes, Entrega_Sabado, Entrega_Domingo) {    
    nsPro.SelNombre = $("#iptNombre");
    nsPro.SelDescripcion = $("#iptDescripcion");
    nsPro.SelNit = $("#iptNit");
    nsPro.SelTelefono = $("#iptTelefono");
    nsPro.SelVisita_Lunes = $("#iptVisitaLunes");
    nsPro.SelVisita_Martes = $("#iptVisitaMartes");
    nsPro.SelVisita_Miercoles = $("#iptVisitaMiercoles");
    nsPro.SelVisita_Jueves = $("#iptVisitaJueves");
    nsPro.SelVisita_Viernes = $("#iptVisitaViernes");
    nsPro.SelVisita_Sabado = $("#iptVisitaSabado");
    nsPro.SelVisita_Domingo = $("#iptVisitaDomingo");
    nsPro.SelEntrega_Lunes = $("#iptEntregaLunes");
    nsPro.SelEntrega_Martes = $("#iptEntregaMartes");
    nsPro.SelEntrega_Miercoles = $("#iptEntregaMiercoles");
    nsPro.SelEntrega_Jueves = $("#iptEntregaJueves");
    nsPro.SelEntrega_Viernes = $("#iptEntregaViernes");
    nsPro.SelEntrega_Sabado = $("#iptEntregaSabado");
    nsPro.SelEntrega_Domingo = $("#iptEntregaDomingo");
    nsPro.SelGridProveedores = $("#gmProveedores");

    nsPro.IdActualizando = id;
    nsPro.SelNombre.val(nsPro.SelGridProveedores.igGrid("getCellValue", id, "Nombre"));
    nsPro.SelDescripcion.val(nsPro.SelGridProveedores.igGrid("getCellValue", id, "Descripcion"));
    nsPro.SelNit.val(nsPro.SelGridProveedores.igGrid("getCellValue", id, "Nit"));
    nsPro.SelTelefono.val(nsPro.SelGridProveedores.igGrid("getCellValue", id, "Telefono"));
    nsPro.SelVisita_Lunes.prop("checked", Visita_Lunes);
    nsPro.SelVisita_Martes.prop("checked", Visita_Martes);
    nsPro.SelVisita_Miercoles.prop("checked", Visita_Miercoles);
    nsPro.SelVisita_Jueves.prop("checked", Visita_Jueves);
    nsPro.SelVisita_Viernes.prop("checked", Visita_Viernes);
    nsPro.SelVisita_Sabado.prop("checked", Visita_Sabado);
    nsPro.SelVisita_Domingo.prop("checked", Visita_Domingo);
    nsPro.SelEntrega_Lunes.prop("checked", Entrega_Lunes);
    nsPro.SelEntrega_Martes.prop("checked", Entrega_Martes);
    nsPro.SelEntrega_Miercoles.prop("checked", Entrega_Miercoles);
    nsPro.SelEntrega_Jueves.prop("checked", Entrega_Jueves);
    nsPro.SelEntrega_Viernes.prop("checked", Entrega_Viernes);
    nsPro.SelEntrega_Sabado.prop("checked", Entrega_Sabado);
    nsPro.SelEntrega_Domingo.prop("checked", Entrega_Domingo);
    nsGral.HideShowTags('btnOpenNewProveedor', 'divNuevaEdicionProveedor');    
}

nsPro.LimpiarForm = function () {
    nsPro.IdActualizando = 0;
    nsPro.SelNombre = $("#iptNombre");
    nsPro.SelDescripcion = $("#iptDescripcion");
    nsPro.SelNit = $("#iptNit");
    nsPro.SelTelefono = $("#iptTelefono");
    nsPro.SelVisita_Lunes = $("#iptVisitaLunes");
    nsPro.SelVisita_Martes = $("#iptVisitaMartes");
    nsPro.SelVisita_Miercoles = $("#iptVisitaMiercoles");
    nsPro.SelVisita_Jueves = $("#iptVisitaJueves");
    nsPro.SelVisita_Viernes = $("#iptVisitaViernes");
    nsPro.SelVisita_Sabado = $("#iptVisitaSabado");
    nsPro.SelVisita_Domingo = $("#iptVisitaDomingo");
    nsPro.SelEntrega_Lunes = $("#iptEntregaLunes");
    nsPro.SelEntrega_Martes = $("#iptEntregaMartes");
    nsPro.SelEntrega_Miercoles = $("#iptEntregaMiercoles");
    nsPro.SelEntrega_Jueves = $("#iptEntregaJueves");
    nsPro.SelEntrega_Viernes = $("#iptEntregaViernes");
    nsPro.SelEntrega_Sabado = $("#iptEntregaSabado");
    nsPro.SelEntrega_Domingo = $("#iptEntregaDomingo");
    
    nsPro.SelNombre.val('');
    nsPro.SelDescripcion.val('');
    nsPro.SelNit.val('');
    nsPro.SelTelefono.val('');
    nsPro.SelVisita_Lunes.prop("checked", false);
    nsPro.SelVisita_Martes.prop("checked", false);
    nsPro.SelVisita_Miercoles.prop("checked", false);
    nsPro.SelVisita_Jueves.prop("checked", false);
    nsPro.SelVisita_Viernes.prop("checked", false); 
    nsPro.SelVisita_Sabado.prop("checked", false);
    nsPro.SelVisita_Domingo.prop("checked", false); 
    nsPro.SelEntrega_Lunes.prop("checked", false);
    nsPro.SelEntrega_Martes.prop("checked", false); 
    nsPro.SelEntrega_Miercoles.prop("checked", false);
    nsPro.SelEntrega_Jueves.prop( "checked", false );
    nsPro.SelEntrega_Viernes.prop( "checked", false );
    nsPro.SelEntrega_Sabado.prop( "checked", false );
    nsPro.SelEntrega_Domingo.prop( "checked", false );
}

nsPro.TryNuevaEditarProveedor = function () {
    nsPro.SelBtnFrmProveedor = $("#btnSubmitFrmProveedor");
    nsPro.SelNombre = $("#iptNombre");   
    nsPro.SelBtnFrmProveedor.click();

    var nombre = nsPro.SelNombre.val();
    if (nombre != null && nombre != undefined && nombre != '') {
        nsPro.SelNit = $("#iptNit");
        nsPro.SelDescripcion = $("#iptDescripcion");
        nsPro.SelTelefono = $("#iptTelefono");
        nsPro.SelVisita_Lunes = $("#iptVisitaLunes");
        nsPro.SelVisita_Martes = $("#iptVisitaMartes");
        nsPro.SelVisita_Miercoles = $("#iptVisitaMiercoles");
        nsPro.SelVisita_Jueves = $("#iptVisitaJueves");
        nsPro.SelVisita_Viernes = $("#iptVisitaViernes");
        nsPro.SelVisita_Sabado = $("#iptVisitaSabado");
        nsPro.SelVisita_Domingo = $("#iptVisitaDomingo");
        nsPro.SelEntrega_Lunes = $("#iptEntregaLunes");
        nsPro.SelEntrega_Martes = $("#iptEntregaMartes");
        nsPro.SelEntrega_Miercoles = $("#iptEntregaMiercoles");
        nsPro.SelEntrega_Jueves = $("#iptEntregaJueves");
        nsPro.SelEntrega_Viernes = $("#iptEntregaViernes");
        nsPro.SelEntrega_Sabado = $("#iptEntregaSabado");
        nsPro.SelEntrega_Domingo = $("#iptEntregaDomingo");
        nsPro.SelGridProveedores = $("#gmProveedores");

        var descripcion = nsPro.SelDescripcion.val();
        var nit = nsPro.SelNit.val();
        var telefono = nsPro.SelTelefono.val();
        var visita_Lunes = nsPro.SelVisita_Lunes.prop('checked');
        var visita_Martes = nsPro.SelVisita_Martes.prop('checked');
        var visita_Miercoles = nsPro.SelVisita_Miercoles.prop('checked');
        var visita_Jueves = nsPro.SelVisita_Jueves.prop('checked');
        var visita_Viernes = nsPro.SelVisita_Viernes.prop('checked');
        var visita_Sabado = nsPro.SelVisita_Sabado.prop('checked');
        var visita_Domingo = nsPro.SelVisita_Domingo.prop('checked');
        var entrega_Lunes = nsPro.SelEntrega_Lunes.prop('checked');
        var entrega_Martes = nsPro.SelEntrega_Martes.prop('checked');
        var entrega_Miercoles = nsPro.SelEntrega_Miercoles.prop('checked');
        var entrega_Jueves = nsPro.SelEntrega_Jueves.prop('checked');
        var entrega_Viernes = nsPro.SelEntrega_Viernes.prop('checked');
        var entrega_Sabado = nsPro.SelEntrega_Sabado.prop('checked');
        var entrega_Domingo = nsPro.SelEntrega_Domingo.prop('checked');
        
        if (nsPro.IdActualizando == 0) {
            var lstCandidates = [ 
                {
                    Nombre: nombre, Descripcion: descripcion, Nit: nit, Telefono: telefono, Visita_Lunes: visita_Lunes,
                    Visita_Martes: visita_Martes, Visita_Miercoles: visita_Miercoles, Visita_Jueves: visita_Jueves,
                    Visita_Viernes: visita_Viernes, Visita_Sabado: visita_Sabado, Visita_Domingo: visita_Domingo,
                    Entrega_Lunes: entrega_Lunes, Entrega_Martes: entrega_Martes, Entrega_Miercoles: entrega_Miercoles,
                    Entrega_Jueves: entrega_Jueves, Entrega_Viernes: entrega_Viernes, Entrega_Sabado: entrega_Sabado,
                    Entrega_Domingo: entrega_Domingo
                }
            ];
            lstCandidates = JSON.stringify({ 'lstCandidates': lstCandidates });
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                url: nsPro.UrlTryAdd,
                type: 'POST',
                dataType: 'json',
                data: lstCandidates,
                success: function (rp) {
                    nsGral.ProcesarResponseBasicVm(rp);
                    if (rp.Success) {
                        nsPro.SelGridProveedores.igGrid("dataBind");
                        nsPro.LimpiarForm();
                    }
                },
                error: function (er1, er2) {
                    nsGral.MensajeBad(er1 + er2);
                }
            });                                 
        } else {
            debugger
            var lstCandidates = [
               {
                   Id : nsPro.IdActualizando , NewNombre: nombre, NewDescripcion: descripcion, NewNit: nit, NewTelefono: telefono, NewVisita_Lunes: visita_Lunes,
                   NewVisita_Martes: visita_Martes, NewVisita_Miercoles: visita_Miercoles, NewVisita_Jueves: visita_Jueves,
                   NewVisita_Viernes: visita_Viernes, NewVisita_Sabado: visita_Sabado, NewVisita_Domingo: visita_Domingo,
                   NewEntrega_Lunes: entrega_Lunes, Newntrega_Martes: entrega_Martes, NewEntrega_Miercoles: entrega_Miercoles,
                   NewEntrega_Jueves: entrega_Jueves, NewEntrega_Viernes: entrega_Viernes, NewEntrega_Sabado: entrega_Sabado,
                   NewEntrega_Domingo: entrega_Domingo
               }
            ];
            lstCandidates = JSON.stringify({ 'lstCandidates': lstCandidates });
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                url: nsPro.UrlTryUpdate,
                type: 'POST',
                dataType: 'json',
                data: lstCandidates,
                success: function (rp) {
                    nsGral.ProcesarResponseBasicVm(rp);
                    if (rp.Success) {
                        nsPro.SelGridProveedores.igGrid("dataBind");
                        nsPro.LimpiarForm();
                    }
                },
                error: function (er1, er2) {
                    debugger
                    nsGral.MensajeBad(er1 + er2);
                }
            });

        }
    } else {
        nsGral.MensajeAlert("Debe diligenciar los campos obligatorios");
    }        
}

function rowDeletingGridProveedores(evt, ui) {
    nsPro.SelGridProveedores = $("#gmProveedores");
    var idsAEliminar = new Array();
    idsAEliminar.push(ui.rowID);
    nsGral.PromptSweet('Desea borrar el registro', nsPro.TryEliminarProveedor, idsAEliminar);
    return false;
}

nsPro.TryEliminarProveedor = function (idsAEliminar) {
    nsPro.SelGridProveedores = $("#gmProveedores");
    if (idsAEliminar != null && idsAEliminar != undefined && idsAEliminar.length > 0) {
        $.ajax(
            {
                url: nsPro.UrlTryDelete,
                type: 'POST',
                data: { idsToDelete: idsAEliminar },
                success: function (rp) {
                    nsGral.ProcesarResponseBasicVm(rp);
                    if (rp.Success) {
                        nsPro.SelGridProveedores.igGrid("dataBind");
                    }
                }, error: function (er1, er2) {
                    nsGral.MensajeBad(er1 + er2);
                }
            });
    } else {
        nsGral.MensajeBad("No se estan enviando datos a funcion en codigo cliente!");
    }
}