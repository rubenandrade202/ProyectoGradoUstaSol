var clienNs = clienNs || {};
//selectores
clienNs.SelGridClientes = $("#gmClientes");
//url
clienNs.UrlTryDelete;
clienNs.UrlTryUpdate;
clienNs.UrlTryAdd;
clienNs.UrlCheckName;
clienNs.UrlUpdateSaldoDeuda;
//Proceso
clienNs.NameValidation = false;
clienNs.ValidationPromptSaldarDeuda = false;

//#region [UI]
function textChangedEditorNombreCliente(evt, ui) {
    var value = ui.text;
    if (value != "" && value != undefined) {
        $.ajax(
            {
                url: clienNs.UrlCheckName,
                type: 'POST',
                data: { name: value },
                success: function (rp) {
                    if (rp.Success) {
                        clienNs.NameValidation = true;
                    } else {
                        clienNs.NameValidation = false;
                        nsGral.MensajeAlert("El nombre del cliente ya existe en DB.");
                    }
                }, error: function (er) {
                    debugger
                }
            });
    }
}

function editCellStartedGridClientes(evt, ui) {
    if (ui.rowAdding) {
        clienNs.NameValidation = false;
        if (ui.columnKey == "Nombre") {
            var idCampo = "#" + ui.editor[0].firstChild.id
            $(idCampo).attr('readonly', false);
            return true;
        }
    } else {
        clienNs.NameValidation = true;
        if (ui.columnKey == "Nombre") {
            var idCampo = "#" + ui.editor[0].firstChild.id
            $(idCampo).attr('readonly', true);
            return true;
        }
    }
}

function editRowEndingGridClientes(evt, ui) {
    if (ui.update && clienNs.NameValidation == false) {
        nsGral.MensajeAlert("El nombre del cliente ya existe en DB.");
        return false;
    }
}

clienNs.StartRowEdit = function () {
    clienNs.SelGridClientes = $("#gmClientes");
    clienNs.SelGridClientes.igGridUpdating("startAddRowEdit");
}
//#endregion

//#region [ADD]
function editRowEndedGridClientes(evt, ui) {
    debugger
    clienNs.SelGridClientes = $("#gmClientes");
    //adding
    if (ui.rowAdding === true && ui.rowID === null && ui.update === true) {
        var lstCandidates = [
            { Nombre: ui.values.Nombre, Email: ui.values.Email, Telefono: ui.values.Telefono, CupoAsignado: ui.values.CupoAsignado }
        ];

        lstCandidates = JSON.stringify(lstCandidates);
        $.ajax({
            url: clienNs.UrlTryAdd,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: lstCandidates,
            success: function (rp) {
                nsGral.ProcesarResponseBasicVm(rp);
                //if (rp.Success) {
                clienNs.SelGridClientes.igGrid("dataBind");
                //}
            }, error: function (rp) {
                debugger
            }
        });

    } else if (ui.rowAdding === false && ui.rowID > 0 && ui.update === true) {//updating   
        debugger
        var lstCandidates = [
            { Id: ui.rowID, Nombre: ui.values.Nombre, Email: ui.values.Email, Telefono: ui.values.Telefono, CupoAsignado: ui.values.CupoAsignado }
        ];

        lstCandidates = JSON.stringify(lstCandidates);
        $.ajax({
            url: clienNs.UrlTryUpdate,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: lstCandidates,
            success: function (rp) {
                nsGral.ProcesarResponseBasicVm(rp);
                //if (rp.Success) {
                clienNs.SelGridClientes.igGrid("dataBind");
                //}
            }, error: function (rp) {
                debugger
            }
        });
    }
}

function editRowStartingGridClientes(evt, ui) {
    debugger
    if (clienNs.ValidationPromptSaldarDeuda) {
        return false;
    }

}

clienNs.PromptSaldarDeudaCliente = function (nombreCliente, creditoActual, idCliente) {   
    clienNs.ValidationPromptSaldarDeuda = true;
    clienNs.SelGridClientes = $("#gmClientes");   
    swal({
        title: "Saldar Credito?",
        text: nombreCliente + " tiene un credito por " + creditoActual + "\nDigite el valor a abonar!",
        type: "input",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Saldar Valor",
        closeOnConfirm: false
    },
        function (inputValue) {
            //validacion nulalidad
            if (inputValue === null || inputValue === undefined || inputValue === 0 ||
                inputValue === '' || inputValue === "0" || inputValue < 0 || inputValue === false) {
                swal.showInputError("Debe digitar un valor numerico positivo.");
                return false
            }
            //validación numerico
            var resultPartial = Number(inputValue);
            if (isNaN(inputValue)) {
                swal.showInputError(inputValue + " tiene valores alfabeticos, debe digitar un valor numerico positivo.");
                return false
            }
            //validación valor excedido
            if (inputValue > creditoActual) {
                swal.showInputError(inputValue + " El valor excede la deuda. Debe digitar un valor igual o menor al de la deuda!");
                return false
            }          
            $.ajax(
                {
                    url: clienNs.UrlUpdateSaldoDeuda,
                    type: 'POST',
                    data: { idCliente: idCliente, valorConsignado: inputValue },
                    success: function (rp) {
                        nsGral.ProcesarResponseBasicVm(rp);
                        if (rp.Success) {
                            swal("Saldado!", "El valor ingresado ha sido debitado del usuario e ingresado al sistema", "success");
                            clienNs.SelGridClientes.igGrid("dataBind");
                        }
                    },
                    error: function (er1) {
                        debugger
                    }

                });

        });
}

//#endregion

//#region [DELETE]
function rowDeletingGridClientes(evt, ui) {
    clienNs.SelGridClientes = $("#gmClientes");
    var deudaCliente = clienNs.SelGridClientes.igGrid("getCellValue", ui.rowID, "CupoEmpleado");
    if (deudaCliente > 0) {
        nsGral.MensajeBad("No se puede eliminar un cliente con saldo DEUDA. SALDAR DEUDA PRIMERO!!");
    } else {
        var idsAEliminar = new Array();
        idsAEliminar.push(ui.rowID);
        nsGral.PromptSweet('Desea borrar el cliente?', clienNs.TryEliminarCliente, idsAEliminar);
    }
    return false;
}

clienNs.TryEliminarCliente = function (idsAEliminar) {
    clienNs.SelGridClientes = $("#gmClientes");

    if (idsAEliminar != null && idsAEliminar != undefined && idsAEliminar.length > 0) {
        $.ajax(
            {
                url: clienNs.UrlTryDelete,
                type: 'POST',
                data: { idsToDelete: idsAEliminar },
                success: function (rp) {
                    nsGral.ProcesarResponseBasicVm(rp);
                    if (rp.Success) {
                        clienNs.SelGridClientes.igGrid("dataBind");
                    }
                }, error: function (er1, er2) {
                    nsGral.MensajeBad(er1 + er2);
                }
            });
    } else {
        nsGral.MensajeBad("No se estan enviando datos a funcion en codigo cliente!");
    }
}
//#endregion
