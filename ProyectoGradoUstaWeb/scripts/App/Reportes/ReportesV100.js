var nsRep = nsRep || {};
nsRep.SelFechaDesdeReporteMovimientoCaja = $("#dpkDesdeMovimientoCaja");
nsRep.SelFechaHastaReporteMovimientoCaja = $("#dpkHastaMovimientoCaja");
nsRep.SelFechaDesdeReporteVentas = $("#dpkDesdeVentas");
nsRep.SelFechaHastaReporteVentas = $("#dpkHastaVentas");
nsRep.SelGridCreditosActuales = $("#gmCreditos");
nsRep.SelGridLogCreditos = $("#gmLogCreditos");
nsRep.SelGridReporteMovimientoCaja = $("#gmMovimientosCaja");
nsRep.SelGridReporteVentas = $("#gmReporteVentas");
nsRep.UrlSetearFechasReporteMovimientoCaja;
nsRep.UrlSetearFechasReporteVentas;
nsRep.UrlClearFechasReporteMovimientoCaja;
nsRep.UrlClearFechasReporteVentas;
nsRep.UrlGetTotalValorInventario;//GetTotalValorInventario

$("#btnFltFechasMovimiento").click(function () {    
    nsRep.SelFechaDesdeReporteMovimientoCaja = $("#dpkDesdeMovimientoCaja");
    nsRep.SelFechaHastaReporteMovimientoCaja = $("#dpkHastaMovimientoCaja");    
    var validFechaDesde = nsRep.SelFechaDesdeReporteMovimientoCaja.igDatePicker("validate");
    var validFechaHasta = nsRep.SelFechaHastaReporteMovimientoCaja.igDatePicker("validate");    
    if (validFechaDesde && validFechaHasta) {
        var valueFechaDesde = nsRep.SelFechaDesdeReporteMovimientoCaja.igDatePicker("displayValue");
        var valueFechaHasta = nsRep.SelFechaHastaReporteMovimientoCaja.igDatePicker("displayValue");
        if (valueFechaDesde < valueFechaHasta) {
            debugger
            $.ajax(
                {
                    url: nsRep.UrlSetearFechasReporteMovimientoCaja,
                    type: 'POST',
                    data: { fechaInicial: valueFechaDesde, fechaFinal: valueFechaHasta },
                    success: function (rp) {
                        if (rp.Success) {
                            nsRep.SelGridReporteMovimientoCaja.igGrid("dataBind");
                        } else {
                            nsGral.ProcesarResponseBasicVm(rp);
                        }
                    }, error: function (er1) {
                        debugger
                    }
                });
        } else {
            nsGral.MensajeAlert("La fecha inicial debe ser menor a la fecha final");
        }
    }
});

$("#btnFltFechasReporteVentas").click(function () {    
    nsRep.SelFechaDesdeReporteVentas = $("#dpkDesdeVentas");
    nsRep.SelFechaHastaReporteVentas = $("#dpkHastaVentas");
    var validFechaDesde = nsRep.SelFechaDesdeReporteVentas.igDatePicker("validate");
    var validFechaHasta = nsRep.SelFechaHastaReporteVentas.igDatePicker("validate");
    if (validFechaDesde && validFechaHasta) {
        var valueFechaDesde = nsRep.SelFechaDesdeReporteVentas.igDatePicker("displayValue");
        var valueFechaHasta = nsRep.SelFechaHastaReporteVentas.igDatePicker("displayValue");
        if (valueFechaDesde < valueFechaHasta) {
            debugger
            $.ajax(
                {
                    url: nsRep.UrlSetearFechasReporteVentas,
                    type: 'POST',
                    data: { fechaInicial: valueFechaDesde, fechaFinal: valueFechaHasta },
                    success: function (rp) {
                        if (rp.Success) {
                            nsRep.SelGridReporteVentas.igGrid("dataBind");
                        } else {
                            nsGral.ProcesarResponseBasicVm(rp);
                        }
                    }, error: function (er1) {
                        debugger
                    }
                });
        } else {
            nsGral.MensajeAlert("La fecha inicial debe ser menor a la fecha final");
        }
    }
});

$("#btnClearFltFechasMovimiento").click(function () {
    debugger
    nsRep.SelFechaDesdeReporteMovimientoCaja = $("#dpkDesdeMovimientoCaja");
    nsRep.SelFechaHastaReporteMovimientoCaja = $("#dpkHastaMovimientoCaja");
    nsRep.SelFechaDesdeReporteMovimientoCaja.igDatePicker("value", "");
    nsRep.SelFechaHastaReporteMovimientoCaja.igDatePicker("value", "");
    $.ajax(
            {
                url: nsRep.UrlClearFechasReporteMovimientoCaja,
                type: 'POST',                
                success: function (rp) {
                    if (rp.Success) {
                        nsRep.SelGridReporteMovimientoCaja.igGrid("dataBind");
                    } else {
                        nsGral.ProcesarResponseBasicVm(rp);
                    }
                }, error: function (er1) {
                    debugger
                }
            });
    
});

$("#btnClearFltFechasReporteVentas").click(function () {
    debugger
    nsRep.SelFechaDesdeReporteVentas = $("#dpkDesdeVentas");
    nsRep.SelFechaHastaReporteVentas = $("#dpkHastaVentas");
    nsRep.SelGridReporteVentas = $("#gmReporteVentas");

    nsRep.SelFechaDesdeReporteVentas.igDatePicker("value", "");
    nsRep.SelFechaHastaReporteVentas.igDatePicker("value", "");
    $.ajax(
            {
                url: nsRep.UrlClearFechasReporteVentas,
                type: 'POST',
                success: function (rp) {
                    if (rp.Success) {
                        nsRep.SelGridReporteVentas.igGrid("dataBind");
                    } else {
                        nsGral.ProcesarResponseBasicVm(rp);
                    }
                }, error: function (er1) {
                    debugger
                }
            });

});

nsRep.RefreshTabHome = function () {    
    nsRep.SelGridCreditosActuales = $("#gmCreditos");
    nsRep.SelGridLogCreditos = $("#gmLogCreditos");
    nsRep.SelGridCreditosActuales.igGrid("dataBind");
    nsRep.SelGridLogCreditos.igGrid("dataBind");   
}

nsRep.RefreshTab2 = function () {    
    nsRep.SelGridReporteMovimientoCaja = $("#gmMovimientosCaja");
    nsRep.SelGridReporteMovimientoCaja.igGrid("dataBind");
}

nsRep.RefreshTab3 = function () {    
    nsRep.SelGridReporteVentas = $("#gmReporteVentas");
    nsRep.SelGridReporteVentas.igGrid("dataBind");
}

nsRep.RefreshTab4 = function () {    
    $.ajax({
        url: nsRep.UrlGetTotalValorInventario,
        type: 'POST',
        success: function(rp){
            if(rp.Success){
                $("#spnValorInventario").html(rp.Data);
            }else{
                $("#spnValorInventario").html(0);
            }            
        },error: function(er){
            debugger
        }
    });
}

