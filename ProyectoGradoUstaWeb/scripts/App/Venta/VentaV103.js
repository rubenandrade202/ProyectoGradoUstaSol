var nsVen = nsVen || {};
//Fields
nsVen.InactivoVentaUno;
nsVen.InactivoVentaDos;
nsVen.InactivoVentaTres;
nsVen.InactivoVentaCuatro;
//Selectores
nsVen.SelCmbVentaUno = $("#cmbVenta_1");
nsVen.SelGridVentaUno = $("#gmVenta_1");
nsVen.SelGridVentaDos = $("#gmVenta_2");
nsVen.SelGridVentaTres = $("#gmVenta_3");
nsVen.SelGridVentaCuatro = $("#gmVenta_4");
//Url
nsVen.UrlCodebarRead;
nsVen.UrlTryVenta;

//#region [UI]
nsVen.HandlerBtnCancelarVenta = function () {
    var idCaja = this[0].id.replace("btn-Cancelar-Venta-", "");;
    nsVen.LimpiarCaja(idCaja);
    nsVen.SetVueltas(idCaja);
}

nsVen.LimpiarCaja = function (idCaja) {
    var bufferCmbBillete = $("#cmbBillete_" + idCaja);
    bufferCmbBillete.igCombo("clearInput");
    var bufferGrid = $("#gmVenta_" + idCaja);
    var bufferCmbCliente = $("#cmbClienteVenta_" + idCaja);
    //var bufferTxtBillete = $("#txtBilleteVenta_" + idCaja);
    //var bufferCambioBillete = $("#cambioVenta_" + idCaja);
    var bufferContentVenta = "bodyContentVenta_" + idCaja;
    var bufferSelectorSpan = $("#cambioVenta_" + idCaja);
    //var bufferSelectorBillete = $("#txtBilleteVenta_" + idCaja);

    $("#chbPrintVenta_" + idCaja).prop("checked", false);

    //bufferSelectorSpan.css("color", "White");
    //bufferSelectorSpan.html(0);
    //bufferSelectorBillete.igCurrencyEditor("value", 0);
    bufferGrid.igGrid("dataBind");
    bufferCmbCliente.igCombo("clearInput");
    //bufferCambioBillete.html(0);
    nsGral.DesbloquearElemento(bufferContentVenta);
    nsVen.SetVueltas(idCaja);
}

nsVen.HandlerBtnRealizarVenta = function () {
    var idCaja = this[0].id.replace("btn-Aprobar-Venta-", "");
    var bufferNameCmbCliente = "#cmbClienteVenta_" + idCaja;
    var bufferCmbCliente = $("#cmbClienteVenta_" + idCaja);
    var bufferContentHtml = "bodyContentVenta_" + idCaja;

    //validar si hay usuario seleccionado para aplicar logica de credito
    var cliente = bufferCmbCliente.igCombo("selectedItems");
    if (cliente != null && cliente != undefined && cliente.length > 0) {
        var idCliente = cliente[0].data.Id;
        var template = $('#templateVentaCliente').html();
        Mustache.parse(template);
        var rendered = Mustache.render(template,
            {
                IdContenedorVenta: idCaja, IdCLiente: idCliente, SelectorContenedorVenta: bufferContentHtml
            });
        $("#" + bufferContentHtml).block({
            theme: true,
            title: "CREDITO?",
            message: rendered
        });
    } else {
        nsVen.RealizarVenta(idCaja, 0, 0, false);
    }
}

nsVen.ChequearCreditoAbono = function (idCaja, idCliente) {
    var abono = null;
    var esCredito = false;
    if ($("#chbCredito_" + idCaja).is(":checked")) {
        abono = $("#iptAbonoDnm_" + idCaja).val();
        esCredito = true;
        if (abono == NaN || abono == undefined || abono == null || abono < 0 || abono == '') {
            nsGral.MensajeAlert("Debe diligenciar algun valor positivo si hay abono, de lo contrario colocar 0 y se cargara todo el valor a la cuenta del cliente");
            return;
        }
    }
    abono = Number(abono);
    nsVen.RealizarVenta(idCaja, idCliente, abono, esCredito);
}

function dropDownClosedCmbVenta(evt, ui) {

    var idCaja = evt.target.id.replace("cmbVenta_", "");
    var idGridBuffer = evt.target.id.replace("cmbVenta", "#gmVenta");
    var items = $("#" + evt.target.id).igCombo("selectedItems");
    if (items != undefined && items != null && items.length > 0) {
        var idProducto = items[0].data.Id;
        var precioProducto = items[0].data.IdParent;
        var nombreProducto = items[0].data.Value;
        nsVen.AddItemToGrid(idCaja, idProducto, precioProducto, nombreProducto);
    }
    $("#" + evt.target.id).igCombo("text", "");
}

function keyupBillete(evt, ui) {
    var idCaja = ui.owner.id.replace("txtBilleteVenta_", "");
    var bufferSelectorGrid = $("#gmVenta_" + idCaja);
    var bufferSelectorSpan = $("#cambioVenta_" + idCaja);
    var bufferSelectorBillete = $("#txtBilleteVenta_" + idCaja);
    var cambio = 0;

    ////prevalidación limpieza
    if (ui.element.value == null || ui.element.value == undefined || ui.element.value == "" || ui.element.value.length == 0) {
        bufferSelectorSpan.css("color", "White");
        bufferSelectorSpan.html(cambio);
        bufferSelectorBillete.igCurrencyEditor("value", 0);
        return;
    }
    nsVen.UpdateBilleteSumarieGrid(idCaja, ui.element.value);
}

function CmbBilleteSelectionChanged(evt, ui) {
    var currentId = ui.owner.options.inputName;
    nsVen.SetVueltas(currentId.replace("cmbBillete_", ""));
}

function rowDeletedGridVenta(evt, ui) {
    var selector = ui.owner.element[0].id;
    var idCaja = selector.replace("gmVenta_", "");
    nsVen.SetVueltas(idCaja);
}

nsVen.SetVueltas = function (idCaja) {
    var bufferSelectorSpan = $("#cambioVenta_" + idCaja);
    var bufferSelectorBillete = $("#cmbBillete_" + idCaja);
    var valorDefault = 0;
    var colorDefault = "White";

    var selectedBillete = bufferSelectorBillete.igCombo("selectedItems");
    if (selectedBillete != null && selectedBillete != undefined && selectedBillete.length > 0) {
        var bufferSelectorGrid = $("#gmVenta_" + idCaja);

        var totalVenta = bufferSelectorGrid.igGridSummaries("summariesFor", "Subtotal")[0].result;
        var valorSeleccionado = selectedBillete[0].data.Value;
        valorSeleccionado = valorSeleccionado.replace(" ", "").replace("$", "").replace(".", "");
        valorDefault = valorSeleccionado - totalVenta;

        if (valorDefault < 0) {
            colorDefault = "Red";
        } else {
            colorDefault = "Lime";
        }
    } else {
        colorDefault = "white";
        valorDefault = 0;
    }

    bufferSelectorSpan.css("color", colorDefault);
    bufferSelectorSpan.html(valorDefault);
}

nsVen.AddItemToGrid = function (idCaja, idProducto, precioProducto, nombreProducto) {
    precioProducto = Number(precioProducto);
    var bufferGridSelector = $("#gmVenta_" + idCaja);
    if (bufferGridSelector.igGrid("rowById", idProducto).length > 0) {
        var currentQty = bufferGridSelector.igGrid("getCellValue", idProducto, "Cantidad");
        var currentSubtotal = bufferGridSelector.igGrid("getCellValue", idProducto, "Subtotal");
        currentQty = currentQty + 1;
        currentSubtotal = currentSubtotal + precioProducto;
        bufferGridSelector.igGridUpdating("updateRow", idProducto, { Cantidad: currentQty, Subtotal: currentSubtotal });
    } else {
        $("#gmVenta_" + idCaja).igGridUpdating("addRow",
            { Id: idProducto, ProductoNombre: nombreProducto, Cantidad: 1, Subtotal: precioProducto, Precio: precioProducto });
    }
    nsVen.SetVueltas(idCaja);
}

$("[id^='cmbVenta_']").keypress(function (e) {
    nsGral.CheckSession();
    var idCaja = e.currentTarget.id.replace("cmbVenta_", "");
    var bufferComboSelector = $("#" + e.currentTarget.id);
    var bufferGridSelector = $("#" + e.currentTarget.id.replace("cmbVenta", "gmVenta"));
    var nameContent = e.currentTarget.id.replace("cmbVenta", "bodyContentVenta");
    var idContent = nameContent.replace("bodyContentVenta_", "");
    var bufferContentHtml = $("#" + nameContent);
    var codeBar = bufferComboSelector.igCombo("text");
    if (e.keyCode == 13 && codeBar.length > 0) {
        if (isNaN(codeBar)) {

        } else {
            $.ajax(
            {
                url: nsVen.UrlCodebarRead,
                type: 'POST',
                data: { codeBar: codeBar },
                success: function (rp) {
                    if (rp.Success) {//El codigo de barras existe en DB                        
                        bufferComboSelector.igCombo("clearInput");
                        var idProducto = rp.Data.Id;
                        var nombreProducto = rp.Data.NewNombre;
                        var precioProducto = rp.Data.NewPrecio;
                        if (rp.Alert) {//El producto asociado al CB esta inactivo, preguntar si activar!! y dar facilidad gestion precio                            
                            //rp.Data.SelectorContenedorVenta = "bodyContentVentaUno";
                            rp.Data.SelectorContenedorVenta = nameContent;
                            rp.Data.IdContenedorVenta = idContent;
                            nsVen.InactivoVentaUno = rp.Data;

                            var template = $('#templateFormaDinamica').html();
                            Mustache.parse(template);
                            var rendered = Mustache.render(template, rp.Data);

                            //$("#bodyContentVentaUno").block({
                            $(bufferContentHtml).block({
                                theme: true,
                                title: nombreProducto + " INACTIVO",
                                message: rendered
                            });
                        } else {
                            //nsVen.AddItemToGrid("#gmVentaUno", idProducto, precioProducto, nombreProducto);
                            nsVen.AddItemToGrid(idCaja, idProducto, precioProducto, nombreProducto);
                        }
                    } else {//El CB no existe en DB o error en server
                        nsGral.MensajeBad(rp.MessageBad[0]);
                    }
                }, error: function (er) {
                    debugger;
                }
            });
        }
    }
});

//increaseDecrease  true: incrementar, false: decrementar
nsVen.ModificarQtyRowGrid = function (idGrid, idProducto, increaseDecrease) {
    var bufferGrid = $("#gmVenta_" + idGrid);
    var currentQty = bufferGrid.igGrid("getCellValue", idProducto, "Cantidad");
    var currentSubtotal = bufferGrid.igGrid("getCellValue", idProducto, "Subtotal");
    var currentPrice = bufferGrid.igGrid("getCellValue", idProducto, "Precio");
    if (increaseDecrease) {
        currentQty = currentQty + 1;
        currentSubtotal = currentSubtotal + currentPrice;
        bufferGrid.igGridUpdating("updateRow", idProducto, { Cantidad: currentQty, Subtotal: currentSubtotal });
    } else {
        if (currentQty > 1) {
            currentQty = currentQty - 1;
            currentSubtotal = currentSubtotal - currentPrice;
            bufferGrid.igGridUpdating("updateRow", idProducto, { Cantidad: currentQty, Subtotal: currentSubtotal });
        }
    }
    nsVen.SetVueltas(idGrid);
}

$(".click-Generic-Sale").click(function () {
    //ids de ventas genericas
    //150000 venta generica 50
    //150001 venta generica 100
    //150002 venta generica 200
    //150003 venta generica 500  
    var value = Number(this.id.replace("btn-", "").substring(0, 3));
    var caja = Number(this.id.substring(14, 15));
    var bufferGrid;
    var bufferIdVentaGenerica;
    var bufferNombreGenerico;
    switch (caja) {
        case 1:
            bufferGrid = $("#gmVenta_1");
            break;
        case 2:
            bufferGrid = $("#gmVenta_2");
            break;
        case 3:
            bufferGrid = $("#gmVenta_3");
            break;
        case 4:
            bufferGrid = $("#gmVenta_4");
            break;
    }

    switch (value) {
        case 50:
            bufferIdVentaGenerica = 150000;
            bufferNombreGenerico = "GENERICO $50";
            break;
        case 100:
            bufferIdVentaGenerica = 150001;
            bufferNombreGenerico = "GENERICO $100";
            break;
        case 200:
            bufferIdVentaGenerica = 150002;
            bufferNombreGenerico = "GENERICO $200";
            break;
        case 500:
            bufferIdVentaGenerica = 150003;
            bufferNombreGenerico = "GENERICO $500";
            break;
    }
    nsVen.AddItemToGrid(caja, bufferIdVentaGenerica, value, bufferNombreGenerico);
    nsVen.SetVueltas(caja);
});
//#endregion

//#region [ADD]
nsVen.RealizarVenta = function (idCaja, idCliente, abono, esCredito) {
    nsGral.CheckSession();
    var bufferGrid = $("#gmVenta_" + idCaja);
    var rows = bufferGrid.data('igGrid').dataSource._data;
    if (rows != null && rows != undefined && rows.length > 0) {
        //Id => IdProducto,IdParent => Cantidad, Value => Total
        var venta = new Array();
        for (var i = 0; i < rows.length; i++) {
            //venta.push({ Id: Number(rows[i].cells[0].innerText), IdParent: Number(rows[i].cells[4].innerText), Value: Number(rows[i].cells[5].innerText) });
            venta.push(
                {
                    Id: rows[i].Id,
                    IdParent: rows[i].Cantidad,
                    Value: rows[i].Subtotal
                });
        }
        var candidate = {
            IdCliente: idCliente,
            Abono: abono,
            Venta: venta,
            EsCredito: esCredito,
            ToPrint: $("#chbPrintVenta_" + idCaja).is(":checked")
        }

        $.ajax({
            url: nsVen.UrlTryVenta,
            type: 'POST',
            data: { candidate: candidate },
            success: function (rp) {
                nsGral.ProcesarResponseBasicVm(rp);
                if (rp.Success) {
                    nsVen.LimpiarCaja(idCaja);
                } else {

                }
            }, error: function (er1) {
                debugger
            }
        });
    } else {
        nsGral.MensajeAlert("No hay registros que procesar");
    }
}
//#endregion

nsVen.TryToActivateProduct = function (idElementoDinamico) {
    debugger
    var selectorDinamicoNombre = $("#iptNombreDnm_" + idElementoDinamico);
    var selectorDinamicoPrecio = $("#iptPrecioDnm_" + idElementoDinamico);
    var selectorDinamicoForma = $("#formaDnm_" + idElementoDinamico);
    selectorDinamicoForma.click();
    var nombreActual = selectorDinamicoNombre.val();
    var precioActual = selectorDinamicoPrecio.val();
    if (nombreActual != undefined && nombreActual != null && nombreActual != '' && precioActual != undefined &&
        precioActual != null && precioActual > 0) {
        //debugger;
        var metadataObject = null;
        switch (idElementoDinamico) {
            case 1:
                metadataObject = nsVen.InactivoVentaUno;
                break;
            case 2:
                metadataObject = nsVen.InactivoVentaDos;
                break;
            case 3:
                metadataObject = nsVen.InactivoVentaTres;
                break;
            case 4:
                metadataObject = nsVen.InactivoVentaCuatro;
                break;
        }

        var lstCandidates = [
                {
                    Id: metadataObject.Id, NewNombre: nombreActual, NewPrecio: precioActual, NewCantidadActual: 0,
                    NewCantidadUmbral: metadataObject.NewCantidadUmbral, NewIdUbicacionStock: metadataObject.NewIdUbicacionStock,
                    NewIdUbicacionNegocio: metadataObject.NewIdUbicacionNegocio, NewProveedores: metadataObject.NewProveedores,
                    NewCodigosBarras: metadataObject.NewCodigosBarras
                }
        ];
        lstCandidates = JSON.stringify({ 'lstCandidates': lstCandidates });
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            url: nsVen.UrlTryUpdateProducto,
            type: 'POST',
            dataType: 'json',
            data: lstCandidates,
            success: function (rp) {
                nsGral.ProcesarResponseBasicVm(rp);
                if (rp.Success) {
                    var nameSelectorDinamicoGrid = "#gmVenta_" + idElementoDinamico;
                    var nameSelectorDinamicoCuerpoHtml = "bodyContentVenta_" + idElementoDinamico;
                    nsVen.AddItemToGrid(idElementoDinamico, metadataObject.Id, precioActual, nombreActual)
                    nsGral.DesbloquearElemento(nameSelectorDinamicoCuerpoHtml);
                } else {

                }
            },
            error: function (er1, er2) {
                debugger;
                nsGral.MensajeBad(er1 + er2);
            },
            complete: function () {
            }
        });
    } else {
        nsGral.MensajeAlert("Debe diligenciar los campos 'nombre y 'precio'");
    }
}
