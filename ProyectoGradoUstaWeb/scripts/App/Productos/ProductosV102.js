var nsProduc = nsProduc || {};
nsProduc.IdProductoActualizando = 0;
nsProduc.SelBtnFrmProducto = $("#btnSubmitFrmProducto");
nsProduc.SelNombre = $("#iptNombre");
nsProduc.SelPrecio = $("#iptPrecio");
nsProduc.SelQtyActual = $("#iptQtyActual");
nsProduc.SelQtyUmbral = $("#iptQtyUmbral");
nsProduc.SelCmbUbcNegocio = $("#cmbUbicacionNegocio");
nsProduc.SelCmbUbcStock = $("#cmbUbicacionStock");
nsProduc.SelGridProductos = $("#gmProductos");
nsProduc.SelGridProductosProveedor = $("#gmProductosProveedor");
nsProduc.SelGridProductosCodigosBarras = $("#gmProductosCodigoBarras");
nsProduc.SelIptCodeBar = $("#iptCodigoBarras");
nsProduc.UrlTryDelete;
nsProduc.UrlCheckCodeBar;
nsProduc.UrlFilterCb;
nsProduc.UrlClearFilterCb;
nsProduc.UrlFilterPv;
nsProduc.UrlClearFilterPv;
nsProduc.ProvidersJson;

//#region [START]
function gmProductosRendered(evt, ui) {
    setTimeout(nsProduc.CargarFilterCbEnGrid, 200);
}

nsProduc.CargarFilterCbEnGrid = function () {
    var html = "CB: <input id='filterCbProduct' onkeypress='nsProduc.CbKeyPress(event)' type='text' /> &nbsp; <span id='spnFiltroActivo' style='color:red; display:none;'>Filtro Activo</span>&nbsp;<input type='button' value='Limpiar Filtro' onclick='nsProduc.LimpiarFiltroCb();' />";
    html += "PR: <input id='filterCbProveedor' onkeypress='nsProduc.PrFltKeyPress(event)' type='text' /> &nbsp; <span id='spnFiltroActivoProveedor' style='color:red; display:none;'>Filtro Activo</span>&nbsp;<input type='button' value='Limpiar Filtro' onclick='nsProduc.LimpiarFiltroPv();' />";
    $("#gmProductos_container > div.ui-widget.ui-helper-clearfix.ui-iggrid-pagesizedropdowncontainerabove.ui-iggrid-toolbar.ui-widget-header.and.ui-corner-top").append(html);
}

nsProduc.LimpiarFiltroCb = function () {
    $.ajax(
            {
                url: nsProduc.UrlClearFilterCb,
                type: 'POST',
                data: { codeBar: 0 },
                success: function (rp) {

                    if (rp.Success) {
                        $("#filterCbProduct").val("");
                        $("#spnFiltroActivo").css("display", "none");
                        nsProduc.SelGridProductos = $("#gmProductos");
                        nsProduc.SelGridProductos.igGrid("dataBind");
                    }
                }, error: function (er) {
                    debugger
                }
            });
}

nsProduc.CbKeyPress = function (e) {
    var codeBar = $("#filterCbProduct").val();
    if (e.keyCode == 13) {
        $.ajax(
            {
                url: nsProduc.UrlFilterCb,
                type: 'POST',
                data: { codeBar: codeBar },
                success: function (rp) {
                    nsGral.ProcesarResponseBasicVm(rp);
                    nsProduc.SelGridProductos = $("#gmProductos");
                    nsProduc.SelGridProductos.igGrid("dataBind");
                    if (rp.Success) {
                        $("#spnFiltroActivo").show();
                    } else {
                        $("#spnFiltroActivo").hide();
                    }

                }, error: function (er) {
                    debugger
                }
            });
    }
}

nsProduc.PrFltKeyPress = function (e) {
    var value = $("#filterCbProveedor").val();
    if (e.keyCode == 13) {
        $.ajax(
            {
                url: nsProduc.UrlFilterPv,
                type: 'POST',
                data: { provedor: value },
                success: function (rp) {
                    debugger
                    nsGral.ProcesarResponseBasicVm(rp);
                    nsProduc.SelGridProductos = $("#gmProductos");
                    nsProduc.SelGridProductos.igGrid("dataBind");
                    if (rp.Success) {
                        $("#spnFiltroActivoProveedor").show();
                    } else {
                        $("#spnFiltroActivoProveedor").hide();
                    }

                }, error: function (er) {
                    debugger
                }
            });
    }
}

nsProduc.LimpiarFiltroPv = function () {
    //debugger
    $.ajax(
          {
              url: nsProduc.UrlClearFilterPv,
              type: 'POST',
              data: { codeBar: 0 },
              success: function (rp) {

                  if (rp.Success) {
                      $("#filterCbProveedor").val("");
                      $("#spnFiltroActivoProveedor").css("display", "none");
                      nsProduc.SelGridProductos = $("#gmProductos");
                      nsProduc.SelGridProductos.igGrid("dataBind");
                  }
              }, error: function (er) {
                  debugger
              }
          });
}

//#region [UI]
nsProduc.LimpiarForm = function () {
    nsProduc.IdProductoActualizando = 0;
    nsProduc.SelNombre = $("#iptNombre");
    nsProduc.SelPrecio = $("#iptPrecio");
    nsProduc.SelQtyActual = $("#iptQtyActual");
    nsProduc.SelQtyUmbral = $("#iptQtyUmbral");
    nsProduc.SelCmbUbcNegocio = $("#cmbUbicacionNegocio");
    nsProduc.SelCmbUbcStock = $("#cmbUbicacionStock");
    nsProduc.SelGridProductosCodigosBarras = $("#gmProductosCodigoBarras");
    nsProduc.SelGridProductosProveedor = $("#gmProductosProveedor");
    nsProduc.SelIptCodeBar = $("#iptCodigoBarras");

    nsProduc.SelNombre.val('');
    nsProduc.SelIptCodeBar.val('');
    nsProduc.SelPrecio.val('0');
    nsProduc.SelQtyActual.val('0');
    nsProduc.SelQtyUmbral.val('0');
    nsProduc.SelCmbUbcNegocio.igCombo("clearInput");
    nsProduc.SelCmbUbcStock.igCombo("clearInput");
    nsProduc.SelGridProductosCodigosBarras.igGrid("dataBind");
    nsProduc.SelGridProductosProveedor.igGrid("dataBind");
}

$("#iptCodigoBarras").bind("keypress", {}, function (e) {
    nsProduc.SelIptCodeBar = $("#iptCodigoBarras");
    nsProduc.SelGridProductosCodigosBarras = $("#gmProductosCodigoBarras");
    var currentValue = nsProduc.SelIptCodeBar.val().trim();
    if (e.keyCode == 13 && currentValue.length > 0) {
        //chequear si existe en BD
        $.ajax(
            {
                url: nsProduc.UrlCheckCodeBar,
                type: 'POST',
                data: { codeBar: currentValue },
                success: function (rp) {
                    if (rp.Success) {//El codigo de barras existe en DB
                        if (rp.Alert) {
                            nsGral.PromptSweet(rp.MessageAlert[0], nsProduc.LoadInfoEdit, rp.Data);
                        } else {
                            nsGral.MensajeAlert(rp.MessageOk[0]);
                        }
                    } else {
                        debugger
                        if (nsProduc.IdProductoActualizando == 0) {
                            var currentUiCodeBar = nsProduc.SelGridProductosCodigosBarras.igGrid("rows");
                            if (currentUiCodeBar != undefined && currentUiCodeBar.length > 0) {
                                for (var i = 0; i < currentUiCodeBar.length; i++) {
                                    if (currentUiCodeBar[i].cells[0].innerText == currentValue) {
                                        nsProduc.SelIptCodeBar.val('');
                                        return;
                                    }
                                }
                            }
                            nsProduc.SelIptCodeBar.val('');
                            nsProduc.SelGridProductosCodigosBarras.igGridUpdating("addRow", { Codigo: currentValue });
                        } else {
                            //debugger
                            //alert("a agregar a DB");
                            var currentUiCodeBar = nsProduc.SelGridProductosCodigosBarras.igGrid("rows");
                            if (currentUiCodeBar != undefined && currentUiCodeBar.length > 0) {
                                for (var i = 0; i < currentUiCodeBar.length; i++) {
                                    if (currentUiCodeBar[i].cells[0].innerText == currentValue) {
                                        nsProduc.SelIptCodeBar.val('');
                                        return;
                                    }
                                }
                            }
                            nsProduc.SelIptCodeBar.val('');
                            nsProduc.SelGridProductosCodigosBarras.igGridUpdating("addRow", { Codigo: currentValue });
                        }
                    }
                }, error: function (rp) {
                    debugger;
                }
            });
    }
});

nsProduc.ReadDataGrid = function (currentId) {
    nsProduc.SelGridProductos = $("#gmProductos");
    var obj = {
        Id: currentId, NewNombre: nsProduc.SelGridProductos.igGrid("getCellValue", currentId, "Nombre"),
        NewPrecio: nsProduc.SelGridProductos.igGrid("getCellValue", currentId, "Precio"), Activo: true,
        NewCantidadActual: nsProduc.SelGridProductos.igGrid("getCellValue", currentId, "CantidadActual"),
        NewCantidadUmbral: nsProduc.SelGridProductos.igGrid("getCellValue", currentId, "CantidadUmbral"),
        NewIdUbicacionStock: nsProduc.SelGridProductos.igGrid("getCellValue", currentId, "IdUbicacionNegocio"),
        NewIdUbicacionNegocio: nsProduc.SelGridProductos.igGrid("getCellValue", currentId, "IdUbicacionStock"),
        NewProveedores: nsProduc.SelGridProductos.igGrid("getCellValue", currentId, "Proveedores"),
        NewCodigosBarras: nsProduc.SelGridProductos.igGrid("getCellValue", currentId, "CodigosBarras")
    };
    nsProduc.LoadInfoEdit(obj);
}

nsProduc.LoadInfoEdit = function (productoToEdit) {
    debugger
    nsProduc.SelNombre = $("#iptNombre");
    nsProduc.SelPrecio = $("#iptPrecio");
    nsProduc.SelQtyActual = $("#iptQtyActual");
    nsProduc.SelQtyUmbral = $("#iptQtyUmbral");
    nsProduc.SelCmbUbcNegocio = $("#cmbUbicacionNegocio");
    nsProduc.SelCmbUbcStock = $("#cmbUbicacionStock");
    nsProduc.SelCmbProveedor = $("#cmbProveedores");
    nsProduc.SelGridProductos = $("#gmProductos");
    nsProduc.SelGridProductosCodigosBarras = $("#gmProductosCodigoBarras");
    nsProduc.SelGridProductosProveedor = $("#gmProductosProveedor");
    nsProduc.SelIptCodeBar = $("#iptCodigoBarras");

    nsProduc.SelIptCodeBar.val('');
    nsProduc.IdProductoActualizando = productoToEdit.Id;
    nsProduc.SelNombre.val(productoToEdit.NewNombre);
    nsProduc.SelPrecio.val(productoToEdit.NewPrecio);
    nsProduc.SelQtyActual.val(productoToEdit.NewCantidadActual);
    nsProduc.SelQtyUmbral.val(productoToEdit.NewCantidadUmbral);
    nsProduc.SelCmbUbcNegocio.igCombo("value", productoToEdit.NewIdUbicacionStock);
    nsProduc.SelCmbUbcStock.igCombo("value", productoToEdit.NewIdUbicacionNegocio);

    //limpia los posibles registros que puedan estar en el grid de codigos de barras
    var currentUiCodeBar = nsProduc.SelGridProductosCodigosBarras.igGrid("rows");
    if (currentUiCodeBar != undefined && currentUiCodeBar.length > 0) {
        for (var i = 0; i < currentUiCodeBar.length; i++) {
            var rowId = currentUiCodeBar[i].cells[0].innerText;
            nsProduc.SelGridProductosCodigosBarras.igGridUpdating("deleteRow", rowId);
        }
    }

    //limpia los posibles registros que puedan estar en el grid de codigos de proveedores
    var currentUiProvider = nsProduc.SelGridProductosProveedor.igGrid("rows");
    if (currentUiProvider != undefined && currentUiProvider.length > 0) {
        for (var i = 0; i < currentUiProvider.length; i++) {
            var rowId = Number(currentUiProvider[i].cells[0].innerText);
            nsProduc.SelGridProductosProveedor.igGridUpdating("deleteRow", rowId);
        }
    }

    //carga los valores del item a editar al grid de proveedores
    if (productoToEdit.NewProveedores != undefined) {
        for (var i = 0; i < productoToEdit.NewProveedores.length; i++) {
            var nombreProveedor = productoToEdit.NewProveedores[i].Value;
            var idProveedor = productoToEdit.NewProveedores[i].Id;
            var precioProveedor = productoToEdit.NewProveedores[i].IdParent;
            nsProduc.SelGridProductosProveedor.igGridUpdating("addRow",
              { IdProveedor: idProveedor, NombreProveedor: nombreProveedor, Precio: precioProveedor });
        }
    }

    //carga los valores del item a editar al grid de codigo barras
    if (productoToEdit.NewCodigosBarras != undefined) {
        for (var i = 0; i < productoToEdit.NewCodigosBarras.length; i++) {
            nsProduc.SelGridProductosCodigosBarras.igGridUpdating("addRow", { Codigo: productoToEdit.NewCodigosBarras[i] });
        }
    }

    nsGral.HideShowTags('btnOpenNewProducto', 'divNuevaEdicionProducto');

}

function editRowStartedGridProveedorProducto(evt, ui) {
    var memoryNames = new Array();
    var actualDataSource = new Array();

    nsProduc.SelGridProductosProveedor = $("#gmProductosProveedor");
    var some = nsProduc.SelGridProductosProveedor.igGrid("allRows");
    for (var i = 0; i < some.length; i++) {
        memoryNames.push(some[i].cells[1].innerText);
    }

    for (var i = 0; i < nsProduc.ProvidersJson.length; i++) {
        var currentName = nsProduc.ProvidersJson[i].Nombre;
        if ($.inArray(currentName, memoryNames) < 0) {
            actualDataSource.push({ Id: nsProduc.ProvidersJson[i].Id, Nombre: currentName })
        }
    }
    ui.owner._editors["NombreProveedor"].igCombo("option", "dataSource", actualDataSource);
    ui.owner._editors["NombreProveedor"].igCombo("option", "textKey", "Nombre");
    ui.owner._editors["NombreProveedor"].igCombo("option", "valueKey", "Id");
    ui.owner._editors["NombreProveedor"].igCombo("dataBind");
}
//#endregion

//#region [ADD]
nsProduc.TryNuevaEditarProveedor = function () {
    nsProduc.SelBtnFrmProducto = $("#btnSubmitFrmProducto");
    nsProduc.SelNombre = $("#iptNombre");
    nsProduc.SelBtnFrmProducto.click();

    var nombre = nsProduc.SelNombre.val();
    if (nombre != null && nombre != undefined && nombre != '') {
        nsProduc.SelPrecio = $("#iptPrecio");
        nsProduc.SelQtyActual = $("#iptQtyActual");
        nsProduc.SelQtyUmbral = $("#iptQtyUmbral");
        nsProduc.SelCmbUbcNegocio = $("#cmbUbicacionNegocio");
        nsProduc.SelCmbUbcStock = $("#cmbUbicacionStock");
        nsProduc.SelGridProductosProveedor = $("#gmProductosProveedor");
        nsProduc.SelGridProductosCodigosBarras = $("#gmProductosCodigoBarras");
        nsProduc.SelGridProductos = $("#gmProductos");
        var proveedores = new Array();
        var codigosBarras = new Array();

        var currentProductoProveedor = nsProduc.SelGridProductosProveedor.igGrid("allRows");
        for (var i = 0; i < currentProductoProveedor.length; i++) {
            proveedores.push({ Id: currentProductoProveedor[i].cells[0].innerText, IdParent: currentProductoProveedor[i].cells[2].innerText });
        }

        var currentProductoCodebarRows = nsProduc.SelGridProductosCodigosBarras.igGrid("allRows");
        for (var i = 0; i < currentProductoCodebarRows.length; i++) {
            codigosBarras.push(currentProductoCodebarRows[i].cells[0].innerText);
        }

        var precio = nsProduc.SelPrecio.val();
        var qtyActual = nsProduc.SelQtyActual.val();
        var qtyUmbral = nsProduc.SelQtyUmbral.val();
        var ubcNegocio = nsProduc.SelCmbUbcNegocio.igCombo("value");
        var ubcStock = nsProduc.SelCmbUbcStock.igCombo("value");

        if (nsProduc.IdProductoActualizando == 0) {
            var lstCandidates = [
                {
                    Nombre: nombre, Precio: precio, CantidadActual: qtyActual, CantidadUmbral: qtyUmbral,
                    IdUbicacionStock: ubcStock, IdUbicacionNegocio: ubcNegocio, Proveedores: proveedores,
                    CodigosBarras: codigosBarras
                }
            ];
            lstCandidates = JSON.stringify({ 'lstCandidates': lstCandidates });
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                url: nsProduc.UrlTryAdd,
                type: 'POST',
                dataType: 'json',
                data: lstCandidates,
                success: function (rp) {
                    nsGral.ProcesarResponseBasicVm(rp);
                    if (rp.Success) {
                        nsProduc.SelGridProductos.igGrid("dataBind");
                        nsProduc.LimpiarForm();
                    }
                },
                error: function (er1, er2) {
                    debugger
                    nsGral.MensajeBad(er1 + er2);
                }
            });
        } else {
            debugger
            var lstCandidates = [
                {
                    Id: nsProduc.IdProductoActualizando, NewNombre: nombre, NewPrecio: precio, NewCantidadActual: qtyActual,
                    NewCantidadUmbral: qtyUmbral, NewIdUbicacionStock: ubcStock, NewIdUbicacionNegocio: ubcNegocio,
                    NewProveedores: proveedores, NewCodigosBarras: codigosBarras
                }
            ];
            lstCandidates = JSON.stringify({ 'lstCandidates': lstCandidates });
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                url: nsProduc.UrlTryUpdate,
                type: 'POST',
                dataType: 'json',
                data: lstCandidates,
                success: function (rp) {
                    nsGral.ProcesarResponseBasicVm(rp);
                    if (rp.Success) {
                        nsProduc.SelGridProductos.igGrid("dataBind");
                        nsProduc.LimpiarForm();
                    }
                },
                error: function (er1, er2) {
                    nsGral.MensajeBad(er1 + er2);
                }
            });
        }
    } else {
        nsGral.MensajeAlert("Debe diligenciar los campos obligatorios");
    }
}

function rowAddingGridProductosProveedor(evt, ui) {
    nsProduc.SelGridProductosProveedor = $("#gmProductosProveedor");
    if (nsProduc.IdProductoActualizando == 0) {
        var idProveedor = ui.values.NombreProveedor;
        var precio = ui.values.Precio;
        var nombreProveedor = "no definido error JS funcion rowAddingGridProductosProveedor";
        for (var i = 0; i < nsProduc.ProvidersJson.length; i++) {
            if (nsProduc.ProvidersJson[i].Id === idProveedor) {
                nombreProveedor = nsProduc.ProvidersJson[i].Nombre;
            }
        }
        nsProduc.SelGridProductosProveedor.igGridUpdating("addRow",
            { IdProveedor: idProveedor, NombreProveedor: nombreProveedor, Precio: precio });
    } else {
        //debugger
        //alert("a agregar a DB");
        var idProveedor = ui.values.NombreProveedor;
        var precio = ui.values.Precio;
        var nombreProveedor = "no definido error JS funcion rowAddingGridProductosProveedor";
        for (var i = 0; i < nsProduc.ProvidersJson.length; i++) {
            if (nsProduc.ProvidersJson[i].Id === idProveedor) {
                nombreProveedor = nsProduc.ProvidersJson[i].Nombre;
            }
        }
        nsProduc.SelGridProductosProveedor.igGridUpdating("addRow",
            { IdProveedor: idProveedor, NombreProveedor: nombreProveedor, Precio: precio });
    }
    return false;
}
//#endregion

//#region [DELETE]
function rowDeletingGridProductos(evt, ui) {
    nsProduc.SelGridProductos = $("#gmProductos");
    var idsAEliminar = new Array();
    idsAEliminar.push(ui.rowID);
    nsGral.PromptSweet('Desea borrar el registro', nsProduc.TryEliminarProducto, idsAEliminar);
    return false;
}

function rowDeletingGridProductosProveedor(evt, ui) {
    nsProduc.SelGridProductosProveedor = $("#gmProductosProveedor");
    if (nsProduc.IdProductoActualizando == 0) {
        nsProduc.SelGridProductosProveedor.igGridUpdating("deleteRow", ui.rowID);
    } else {
        //alert("a eliminar de DB");
        nsProduc.SelGridProductosProveedor.igGridUpdating("deleteRow", ui.rowID);
    }
    return false;
}

function rowDeletingGridProductosCodigoBarras(evt, ui) {
    nsProduc.SelGridProductosCodigosBarras = $("#gmProductosCodigoBarras");
    if (nsProduc.IdProductoActualizando == 0) {
        nsProduc.SelGridProductosCodigosBarras.igGridUpdating("deleteRow", ui.rowID);
    } else {
        //alert("a eliminar de DB");
        nsProduc.SelGridProductosCodigosBarras.igGridUpdating("deleteRow", ui.rowID);
    }
    return false;
}

nsProduc.TryEliminarProducto = function (idsAEliminar) {
    nsProduc.SelGridProductos = $("#gmProductos");
    if (idsAEliminar != null && idsAEliminar != undefined && idsAEliminar.length > 0) {
        $.ajax(
            {
                url: nsProduc.UrlTryDelete,
                type: 'POST',
                data: { idsToDelete: idsAEliminar },
                success: function (rp) {
                    nsGral.ProcesarResponseBasicVm(rp);
                    if (rp.Success) {
                        nsProduc.SelGridProductos.igGrid("dataBind");
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