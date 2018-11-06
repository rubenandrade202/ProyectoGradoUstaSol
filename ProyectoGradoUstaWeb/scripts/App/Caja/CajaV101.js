var nsCj = nsCj || {};
nsCj.SelRowValor = "#rowValorAfectar";
nsCj.SelGidCaja = "#gmCaja";
nsCj.SelBtnRegistrarCaja = "#btnRegistrarCaja";
nsCj.SelCmbTipoMovimiento = "#cmbTipoMovimiento";
nsCj.SelTxtValorMovimiento = "#txtValorMovimiento";
nsCj.SelCmbDevolucion = "#cmbDevolucion";
nsCj.UrlRegistrarMovimiento;
nsCj.UrlCodebarRead;

$(nsCj.SelCmbDevolucion).keypress(function (e) {    
    //nsGral.CheckSession();
    //var idCaja = e.currentTarget.id.replace("cmbVenta_", "");
    //var bufferComboSelector = $("#" + e.currentTarget.id);
    //var bufferGridSelector = $("#" + e.currentTarget.id.replace("cmbVenta", "gmVenta"));
    //var nameContent = e.currentTarget.id.replace("cmbVenta", "bodyContentVenta");
    //var idContent = nameContent.replace("bodyContentVenta_", "");
    //var bufferContentHtml = $("#" + nameContent);
    var codeBar = $(nsCj.SelCmbDevolucion).igCombo("text");
    if (e.keyCode == 13 && codeBar.length > 0) {
        if (isNaN(codeBar)) {

        } else {            
            $.ajax(
            {
                url: nsCj.UrlCodebarRead,
                type: 'POST',
                data: { codeBar: codeBar },
                success: function (rp) {                    
                    if (rp.Success) {//El codigo de barras existe en DB                        
                        $(nsCj.SelCmbDevolucion).igCombo("clearInput");
                        var idProducto = rp.Data.Id;
                        var nombreProducto = rp.Data.NewNombre;
                        var precioProducto = rp.Data.NewPrecio;

                        swal({
                            title: "Realizar devolución?",
                            text: nombreProducto + " por $" + precioProducto,
                            type: "warning",
                            showCancelButton: true,
                            confirmButtonClass: "btn-danger",
                            confirmButtonText: "Devolver!",
                            closeOnConfirm: false
                        }, function () {
                            $.ajax(
                                {
                                    url: nsCj.UrlRegistrarDevolucion,
                                    type: 'POST',
                                    data: { idProducto: idProducto },
                                    success: function (rp) {                                        
                                        nsGral.ProcesarResponseBasicVm(rp);
                                        if (rp.Success) {
                                            swal(
                                            'Devuelto!',
                                                'El producto ha sido devuelto al inventario',
                                                'success'
                                            );
                                            $(nsCj.SelCmbDevolucion).igCombo("text", "");
                                        } else {

                                        }
                                    },
                                    error: function (er1) {
                                        debugger
                                    }
                                });
                        });



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


function dropDownClosedCmbDevolucion(evt, ui) {    
    var selectedData = $(nsCj.SelCmbDevolucion).igCombo("selectedItems");
    if (selectedData != null && selectedData != undefined && selectedData.length > 0) {
        var idProducto = selectedData[0].data.Id;
        var precioProducto = selectedData[0].data.IdParent;
        var nombreProducto = selectedData[0].data.Value;
        swal({
            title: "Realizar devolución?",
            text: nombreProducto + " por $" + precioProducto,
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Devolver!",
            closeOnConfirm: false
        }, function () {            
            $.ajax(
                {
                    url: nsCj.UrlRegistrarDevolucion,
                    type: 'POST',
                    data: { idProducto: idProducto },
                    success: function (rp) {
                        debugger
                        nsGral.ProcesarResponseBasicVm(rp);
                        if (rp.Success) {
                            swal(
                            'Devuelto!',
                                'El producto ha sido devuelto al inventario',
                                'success'
                            );
                            $(nsCj.SelCmbDevolucion).igCombo("text", "");
                        } else {

                        }                        
                    },
                    error: function (er1) {
                        debugger
                    }
                });             
        });                
    }
}

function cmbTipoMovimientoSelectionchanged(evt, ui) {    
    if (ui.items.length > 0) {
        $(nsCj.SelRowValor).css("display", "block");
    } else {
        $(nsCj.SelRowValor).css("display", "none");
    }    
}

$(nsCj.SelBtnRegistrarCaja).click(function () {
    debugger
    var dataSelected = $(nsCj.SelCmbTipoMovimiento).igCombo("selectedItems");
    if (dataSelected != null && dataSelected != undefined && dataSelected.length > 0) {
        var idMovimiento = Number(dataSelected[0].data.Id);
        var valorMovimiento = $(nsCj.SelTxtValorMovimiento).val();
        if (valorMovimiento > 0) {
            $.ajax(
            {
                url: nsCj.UrlRegistrarMovimiento,
                type: 'POST',
                data: { Id: idMovimiento, Value: valorMovimiento },
                success: function (rp) {
                    nsGral.ProcesarResponseBasicVm(rp);
                    if (rp.Success) {
                        $(nsCj.SelCmbTipoMovimiento).igCombo("clearInput");
                        $(nsCj.SelTxtValorMovimiento).val("");
                        $(nsCj.SelGidCaja).igGrid("dataBind");
                    }
                }, error: function (er1) {
                    debugger
                    nsGral.MensajeBad(er1);
                }
            });
        } else {
            nsGral.MensajeAlert("Se debe proporcionar un valor numerico positivo");            
        }
    } else {
        nsGral.MensajeAlert("Se debe seleccionar un tipo de movimiento.");
    }
});

