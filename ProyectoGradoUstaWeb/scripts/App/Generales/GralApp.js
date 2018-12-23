var nsGral = nsGral || {};
nsGral.MsjResponseVmNulo = "ResponseBasicVm Vacio/ResponseBasicVm Empty";
nsGral.TituloPagina = "Pagina No definida";
nsGral.ModuloPagina = "Modulo No definido";
nsGral.ConfiguracionCssBlockUi = { border: 'none', padding: '15px', backgroundColor: '#002744', '-webkit-border-radius': '10px', '-moz-border-radius': '10px', opacity: .5, color: '#fff' }
nsGral.UrlStayAlive;//Seteado en home index
nsGral.UrlLoadPvPedidosVisitas;//Seteado en home index
nsGral.UrlRegistrarPedidoRecibido;//Seteado en home index

//Carga las vistas dinamicamente
nsGral.ProcesarPagina = function (controller, action) {
    //debugger
    nsGral.CheckSession();
    var urlCurrent = "../" + controller + "/" + action + "?thereIsL=1";
    nsGral.BloquearPantalla();
    $.ajax(
        {
            url: urlCurrent,
            cache: false,
            success: function (result) {
                $("#PartialDiv").html(result);
                nsGral.ActualizaThumbnail(nsGral.ModuloPagina, nsGral.TituloPagina);                
            },
            complete: function () {
                nsGral.DesbloquearPantalla();
            }
        });
}

nsGral.ActualizaThumbnail = function (modulo, pagina) {   
    //$("#moduleBreadcrumb").text(modulo);
    //$("#tituloPageBreadcrumb").text(pagina);
}

nsGral.CheckSession = function () {
    var loginUrl = "../Security/Login";
    var urlCheckSession = "../Security/CheckSession";
    $.ajax(
        {
            url: urlCheckSession,
            cache: false,
            async: false,
            type: 'HEAD',
            complete: function (request) {
                if (request.getResponseHeader('SessionChecking') == 'true') {
                    return;
                } else {
                    location.href = loginUrl;
                }
            }
        });
}

nsGral.MensajeOk = function (msj) {
    noty({ text: msj, type: "success", layout: "topCenter", theme: "defaultTheme", timeout: 4000 });
}

nsGral.MensajeBad = function (msj) {
    noty({ text: msj, type: "error", layout: "topCenter", theme: "defaultTheme", timeout: 4000 });
}

nsGral.MensajeAlert = function (msj) {
    noty({ text: msj, type: "warning", layout: "topRight", theme: "defaultTheme", timeout: 4000 });
}

nsGral.MensajeAlertCenter = function (msj) {
    noty({ text: msj, type: "warning", layout: "center", theme: "defaultTheme", timeout: 2500 });
}

nsGral.PromptSweet = function (msj, promiseFunction, arrayIdToDelete) {
    swal({
        title: "",
        text: msj,
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "Ok",
        closeOnConfirm: false
    },
       function (isConfirm) {//SI DECIDE ELIMINAR
           if (isConfirm) {
               promiseFunction(arrayIdToDelete);
               swal("Ok", "", "success");
           }
       });
}

//Procesa la respuesta generica ResponseBasicVm en cuanto a mensajes de usuario
nsGral.ProcesarResponseBasicVm = function (rp) {
    if (rp == null || rp == undefined) {
        //TODO MENSAJE ESPECIFICO
        nsGral.MensajeBad(nsGral.MsjResponseVmNulo);
    } else {
        $.each(rp.MessageOk, function (index, value) {
            nsGral.MensajeOk(value);
        });

        $.each(rp.MessageBad, function (index, value) {
            nsGral.MensajeBad(value);
        });

        $.each(rp.MessageAlert, function (index, value) {
            nsGral.MensajeAlert(value);
        });
    }
}

nsGral.BloquearElemento = function (tag, msg) {
    $("#" + tag).block({ css: nsGral.ConfiguracionCssBlockUi, message: '<img src="../Content/Images/Gif/loading-bar.gif" /></br>' + msg });
}

nsGral.DesbloquearElemento = function (tag, msg) {
    $("#" + tag).unblock();
}

nsGral.BloquearPantalla = function () {
    $("body").block({ css: nsGral.ConfiguracionCssBlockUi, message: '<img src="../Content/Images/Gif/loading-bar.gif" /></br>Loading..' });
}

nsGral.DesbloquearPantalla = function () {
    $("body").unblock();
}

nsGral.CargarPvPedidosVentas = function () {    
    $.ajax(
        {
            url: nsGral.UrlLoadPvPedidosVisitas,
            success: function (pv) {
                $("#contenedorPedidosVisitas").html(pv);
            }, error: function () {
                debugger;
            }
        });
}

//Mantiene viva la sesion para el logueado
nsGral.StayAlive = function () {
    $.ajax({ url: nsGral.UrlStayAlive });
}

setInterval(function () { nsGral.StayAlive(); }, 600000);

//Formatea el valor del grid para cuando es una columna bool
nsGral.FormatterBoolToColor = function (evt, ui) {    
    var htmlBuffer = "<img src='";
    if (evt) {
        htmlBuffer += nsInven.UrlDinamicContent + "Icons/iconGreen.png' >";
    } else {
        htmlBuffer += nsInven.UrlDinamicContent + "Icons/iconRed.png' >";
    }
    return htmlBuffer;
}
//Formatea el valor del grid para cuando es una columna numero 0=rojo, 1=amarillo, 2=verde
nsGral.FormatterNumberToColor = function (evt, ui) {
    var htmlBuffer = "<img src='";
    //var srcImage = "Icons/";
    switch (ui.Programado) {
        case 0:
            htmlBuffer += nsInven.UrlDinamicContent + "Icons/iconRed.png' >";
            break;
        case 1:
            htmlBuffer += nsInven.UrlDinamicContent + "Icons/iconYellow.png' >";
            break;
        case 2:
            htmlBuffer += nsInven.UrlDinamicContent + "Icons/iconGreen.png' >";
            break;
    }
    return htmlBuffer;
}

//Machetazos de Luis Andrade
$(document).ready(function () {
    //Machetazos de Luis Andrade
    $(".page-header.navbar .hor-menu .navbar-nav>li.mega-menu-dropdown").removeClass("active");
    $(".page-header.navbar .hor-menu .navbar-nav>li.mega-menu-dropdown:hover>.dropdown-menu, .page-header.navbar .hor-menu .navbar-nav>li.mega-menu-dropdown.active>.dropdown-menu, .open>.dropdown-menu").removeClass("active");

    $(".page-header.navbar .hor-menu .navbar-nav>li.mega-menu-dropdown, .dropdown.dropdown-user").on("click", function () {
        if (!$(this).hasClass("active")) {
            $(".page-header.navbar .hor-menu .navbar-nav>li.mega-menu-dropdown").removeClass("active");
            $(".page-header.navbar .hor-menu .navbar-nav>li.mega-menu-dropdown:hover>.dropdown-menu, .page-header.navbar .hor-menu .navbar-nav>li.mega-menu-dropdown.active>.dropdown-menu, .open>.dropdown-menu").removeClass("active");
        }
        $(this).toggleClass("active");
    });

});

// First tag hide second tag show
nsGral.HideShowTags = function (firstTag, secondTag) {
    $("#" + firstTag).hide();
    $("#" + secondTag).show();
}

