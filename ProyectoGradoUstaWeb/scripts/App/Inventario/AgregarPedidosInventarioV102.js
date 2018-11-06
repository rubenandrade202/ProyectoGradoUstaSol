var nsApi = nsApi || {};
nsApi.SelectorGridProductosBuffer = $("#gmProductosBufferAInventario");
nsApi.UrlSetproviderSelected;
nsApi.UrlDeleteProductosBuffer;
nsApi.UrlIngresarProductoABuffer;
nsApi.UrlCodebarRead;
nsApi.SelCmbProveedores = $("#cmbProveedores");
nsApi.SelDpkFecha = $("#dpkFechaPedido");
nsApi.SelProductosSearch = $("#cmbSearchProduct");

$("#cmbSearchProduct").keypress(function (e) {    
    nsGral.CheckSession();
    nsApi.SelProductosSearch = $("#cmbSearchProduct");
    var codeBar = nsApi.SelProductosSearch.igCombo("text");        
    if (e.keyCode == 13 && codeBar.length > 0) {
        if (isNaN(codeBar)) {            
        } else {
            //debugger
            $.ajax(
            {
                url: nsApi.UrlCodebarRead,
                type: 'POST',
                data: { codeBar: codeBar },
                success: function (rp) {
                    if (rp.Success) {//El codigo de barras existe en DB                        
                        nsApi.SelProductosSearch.igCombo("clearInput");
                        var idProducto = rp.Data.Id;
                        var nombreProducto = rp.Data.NewNombre;
                        var precioProducto = rp.Data.NewPrecio;
                        if (rp.Alert) {
                            nsGral.MensajeBad("El producto esta inactivo");
                            //El producto asociado al CB esta inactivo, preguntar si activar!! y dar facilidad gestion precio                            
                            //rp.Data.SelectorContenedorVenta = "bodyContentVentaUno";
                            //rp.Data.SelectorContenedorVenta = nameContent;
                            //rp.Data.IdContenedorVenta = idContent;
                            //nsVen.InactivoVentaUno = rp.Data;

                            //var template = $('#templateFormaDinamica').html();
                            //Mustache.parse(template);
                            //var rendered = Mustache.render(template, rp.Data);

                            ////$("#bodyContentVentaUno").block({
                            //$(bufferContentHtml).block({
                            //    theme: true,
                            //    title: nombreProducto + " INACTIVO",
                            //    message: rendered
                            //});
                        } else {
                            nsApi.CargarProductoABuffer(idProducto);
                            //Ingresar item a tabla buffer
                            //nsVen.AddItemToGrid("#gmVentaUno", idProducto, precioProducto, nombreProducto);
                            //nsVen.AddItemToGrid(idCaja, idProducto, precioProducto, nombreProducto);
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

$("#btnDescartarBuffer").click(function () {
    nsApi.SelectorGridProductosBuffer = $("#gmProductosBufferAInventario");
    var selectedBufferProducts = nsApi.SelectorGridProductosBuffer.igGridSelection("selectedRows");
    if (selectedBufferProducts != null && selectedBufferProducts != undefined && selectedBufferProducts.length > 0) {
        var arrayIds = new Array();
        $.each(selectedBufferProducts, function (idx) {
            arrayIds.push(selectedBufferProducts[idx].id);
        });
        if (arrayIds.length > 0) {
            nsGral.PromptSweet('Desea eliminar los registros seleccionados?', nsApi.EliminarItemsBuffer, arrayIds);
        }
    } else {
        nsGral.MensajeAlert("Debe seleccionar uno o mas registros");
    }
});

$("#btnProcesarBuffer").click(function () {
    nsApi.SelectorGridProductosBuffer = $("#gmProductosBufferAInventario");
    var selectedBufferProducts = nsApi.SelectorGridProductosBuffer.igGridSelection("selectedRows");
    if (selectedBufferProducts != null && selectedBufferProducts != undefined && selectedBufferProducts.length > 0) {
        var arrayIds = new Array();
        $.each(selectedBufferProducts, function (idx) {
            arrayIds.push(selectedBufferProducts[idx].id);
        });
        if (arrayIds.length > 0) {
            nsGral.PromptSweet('Desea procesar los registros seleccionados? Esto afectara el inventario y el costo de adquisición del producto así como de su valor de venta', nsApi.ProcesarRegistrosAInventario, arrayIds);
        }
    } else {
        nsGral.MensajeAlert("Debe seleccionar uno o mas registros");
    }
});

nsApi.ProcesarRegistrosAInventario = function (arrayRecords) {
    $.ajax(
       {
           url: nsApi.UrlProcesarProductosBuffer,
           type: 'POST',
           data: { idRecords: arrayRecords },
           success: function (rp) {
               nsGral.ProcesarResponseBasicVm(rp);
               if (rp.Success) {
                   nsApi.SelectorGridProductosBuffer = $("#gmProductosBufferAInventario");
                   nsApi.SelectorGridProductosBuffer.igGrid("dataBind");
                   nsApi.SelectorGridProductosBuffer.igGridSelection("clearSelection");
               }
           }, error: function (er) {
               debugger
           }
       });
}

function gridProductosBufferEditCellEnded(evt, ui) {
    if (ui.update) {        
        var flagColumnCorrect = false;
        var idCampo = 0;

        if (ui.columnKey == "QtyRealAIngresar") {
            idCampo = 1;
            flagColumnCorrect = true;
        } else if (ui.columnKey == "PrecioAsignado") {
            idCampo = 3;
            flagColumnCorrect = true;
        } else if (ui.columnKey == "CostoItem") {
            idCampo = 2;
            flagColumnCorrect = true;
        } else if (ui.columnKey == "ProductoNombre") {
            idCampo = 4;
            flagColumnCorrect = true;
        }

        if (flagColumnCorrect) {
            $.ajax(
                {
                    url: nsApi.UrlUpdateCampoProductoBuffer,
                    type: 'POST',
                    data: { idCampo: idCampo, valor: ui.value, idRecord: ui.rowID },
                    success: function (rp) {
                        nsGral.ProcesarResponseBasicVm(rp);
                    }, error: function (er) {
                        debugger
                    }
                });
        }
    }
}

function dropDownClosedCmbSearch(evt, ui) {
    var items = $("#cmbSearchProduct").igCombo("selectedItems");
    if (items != undefined && items != null && items.length > 0) {
        var idProducto = items[0].data.Id;
        nsApi.CargarProductoABuffer(idProducto);

        //$.ajax(
        //    {
        //        url: nsApi.UrlIngresarProductoABuffer,
        //        type: 'POST',
        //        data: { idProducto: idProducto },
        //        success: function (rp) {
        //            nsApi.SelectorGridProductosBuffer = $("#gmProductosBufferAInventario");
        //            nsApi.SelectorGridProductosBuffer.igGrid("dataBind");
        //            nsApi.SelectorGridProductosBuffer.igGridSelection("clearSelection");
        //        }, error: function () {
        //            debugger
        //        }
        //    });
        //var precioProducto = items[0].data.IdParent;
        //var nombreProducto = items[0].data.Value;
        //nsVen.AddItemToGrid(idCaja, idProducto, precioProducto, nombreProducto);
    }
    $("#cmbSearchProduct").igCombo("text", "");
}

nsApi.CargarProductoABuffer = function (idProducto) {
    $.ajax(
            {
                url: nsApi.UrlIngresarProductoABuffer,
                type: 'POST',
                data: { idProducto: idProducto },
                success: function (rp) {
                    nsApi.SelectorGridProductosBuffer = $("#gmProductosBufferAInventario");
                    nsApi.SelectorGridProductosBuffer.igGrid("dataBind");
                    nsApi.SelectorGridProductosBuffer.igGridSelection("clearSelection");
                }, error: function () {
                    debugger
                }
            });
}

function cmbProveedoresSelectionchanged(evt, ui) {
    if (ui.items != undefined && ui.items != null && ui.items.length > 0) {
        var fecha = nsApi.SelDpkFecha.igDatePicker("value");
        if (fecha != null && fecha != undefined && fecha != "") {
            //debugger
            $.ajax(
                {
                    url: nsApi.UrlSetproviderAndDateSelected,
                    type: 'POST',
                    data: { idProvider: ui.items[0].data.Id, fechaReferenciaPedido: fecha.toISOString() },
                    success: function (rp) {
                        //debugger
                        nsApi.SelectorGridProductosBuffer = $("#gmProductosBufferAInventario");
                        nsApi.SelectorGridProductosBuffer.igGrid("dataBind");
                        nsApi.SelectorGridProductosBuffer.igGridSelection("clearSelection");
                        $("#contenedorDynamicHidden").removeClass("invisible");
                    }, error: function (er) {
                        debugger
                    }
                });
        } else {
            $("#contenedorDynamicHidden").addClass("invisible");
        }
    } else {
        $("#contenedorDynamicHidden").addClass("invisible");
    }
}

function dpkFechaValueChanged(evt, ui) {
    if (ui.newValue != undefined && ui.newValue != null && ui.newValue != "") {
        nsApi.SelCmbProveedores = $("#cmbProveedores");
        var proveedor = nsApi.SelCmbProveedores.igCombo("selectedItems");
        if (proveedor != null && proveedor != undefined && proveedor.length > 0) {
            //debugger
            $.ajax(
                {
                    url: nsApi.UrlSetproviderAndDateSelected,
                    type: 'POST',
                    data: { idProvider: proveedor[0].data.Id, fechaReferenciaPedido: ui.newValue.toISOString() },
                    success: function (rp) {
                        //debugger
                        nsApi.SelectorGridProductosBuffer = $("#gmProductosBufferAInventario");
                        nsApi.SelectorGridProductosBuffer.igGrid("dataBind");
                        nsApi.SelectorGridProductosBuffer.igGridSelection("clearSelection");
                        $("#contenedorDynamicHidden").removeClass("invisible");
                    }, error: function (er) {
                        debugger
                    }
                });
        } else {
            $("#contenedorDynamicHidden").addClass("invisible");
        }
    } else {
        $("#contenedorDynamicHidden").addClass("invisible");
    }
}

nsApi.EliminarItemsBuffer = function (arrayRecords) {
    $.ajax(
        {
            url: nsApi.UrlDeleteProductosBuffer,
            type: 'POST',
            data: { idRecords: arrayRecords },
            success: function (rp) {
                nsGral.ProcesarResponseBasicVm(rp);
                if (rp.Success) {
                    nsApi.SelectorGridProductosBuffer = $("#gmProductosBufferAInventario");
                    nsApi.SelectorGridProductosBuffer.igGrid("dataBind");
                    nsApi.SelectorGridProductosBuffer.igGridSelection("clearSelection");
                }
            }, error: function (er) {
                debugger
            }
        });
}

function calcularGananciaActual(evt, ui) {
    var costoItem = evt.CostoItem;
    var precioAsignado = evt.PrecioAsignado;
    var baseValor = precioAsignado - costoItem;
    if (baseValor == 0) {
        return "0 %";
    } else {
        var resultado = (baseValor * 100) / costoItem;
        if (resultado < 20) {
            return "<span style='color:red;'>" + resultado.toFixed(2) + " %" + "</span>";
        } else {
            return resultado.toFixed(2) + " %";
        }
    }
}

function calcularPrecioAl20(evt, ui) {
    var costoItem = evt.CostoItem;
    var porcentaje20 = costoItem * 0.20;
    return calcularNearest50(costoItem + porcentaje20);
}

function calcularPrecioAl25(evt, ui) {
    var costoItem = evt.CostoItem;
    var porcentaje25 = costoItem * 0.25;
    return calcularNearest50(costoItem + porcentaje25);
}

function calcularPrecioAl30(evt, ui) {
    var costoItem = evt.CostoItem;
    var porcentaje30 = costoItem * 0.30;
    return calcularNearest50(costoItem + porcentaje30);
}

function calcularNearest50(value) {
    if (value % 50 === 0) {
        return value.toFixed(0);
    } else {
        return (Math.round(value / 50) * 50).toFixed(0);
    }
}