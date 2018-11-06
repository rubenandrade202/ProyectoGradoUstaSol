var nsSecurity = nsSecurity || {};
nsSecurity.SelTxtMailDestiny = $("#txtMailDestiny");
nsSecurity.UrlMakeBkp;

nsSecurity.ProcesarBackup = function () {
    $.ajax(
        {
            url: nsSecurity.UrlMakeBkp,
            type: 'POST',
            success: function (rp) {
                nsGral.ProcesarResponseBasicVm(rp);
            }, error: function (rp) {
                debugger
            }
        });
}