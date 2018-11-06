var nsInven = nsInven || {};
nsInven.SelGridProductoFaltantes = $("#gmProductosFaltantes");
nsInven.SelGridProductosAPedir = $("#gmProductosAPedir");
nsInven.SelComboProviders = $("#cmbProveedores");
nsInven.SelChbDia = $("#chbCareDay");
nsInven.UrlAddProductosAPedir;
nsInven.UrlActualizarProviders;
nsInven.UrlDesasociarProviders;
nsInven.UrlDeleteProductosAPedir;
nsInven.UrlActualizarValorProductoAPedir;
nsInven.UrlDinamicContent;
nsInven.UrlInactivarProductos;
nsInven.UrlUpdateProductoGeneric;
nsInven.UrlSetFilterProviderProductosPedidos;

$(document).ready(function () {
    //Inicializa los switch boostrap
    $("[name='my-checkbox-boostrap']").bootstrapSwitch();
});

$('input[name="my-checkbox-boostrap"]').on('switchChange.bootstrapSwitch', function (event, state) {

    $.ajax(
        {
            url: nsInven.UrlSetFilter,
            type: 'POST',
            data: { value: state },
            success: function (rp) {
                if (rp.Success) {
                    nsInven.SelGridProductoFaltantes = $("#gmProductosFaltantes");
                    nsInven.SelGridProductoFaltantes.igGrid("dataBind");
                }
            }, error: function () {

            }
        });
});

$("#btnAyudaProviders").click(function () {
    $("#divProveedoresSemana").css("display", "block");
});

$("#btnDesasociaProviders").click(function () {
    debugger
    nsInven.SelGridProductoFaltantes = $("#gmProductosFaltantes");
    var selectedProducts = nsInven.SelGridProductoFaltantes.igGridSelection("selectedRows");
    if (selectedProducts != null && selectedProducts != undefined && selectedProducts.length > 0) {
        var productosproveedor = new Array();
        $.each(selectedProducts, function (idx) {
            var idRecord = selectedProducts[idx].id;
            var idProducto = nsInven.SelGridProductoFaltantes.igGrid("getCellValue", idRecord, "ProductoId");
            var idProveedor = nsInven.SelGridProductoFaltantes.igGrid("getCellValue", idRecord, "ProveedorId");
            if (idProveedor != 0) {
                productosproveedor.push({ Id: idProducto, Value: idProveedor });
            }
        });
        if (productosproveedor.length > 0) {
            $.ajax({
                url: nsInven.UrlDesasociarProviders,
                type: 'POST',
                data: { productosproveedor: productosproveedor },
                success: function (rp) {
                    nsGral.ProcesarResponseBasicVm(rp);
                    if (rp.Success) {
                        nsInven.SelGridProductoFaltantes.igGrid("dataBind");
                        nsInven.SelGridProductoFaltantes.igGridSelection("clearSelection");
                    }
                }, error: function (er1) {
                    debugger
                }
            });

        } else {
            nsGral.MensajeAlert("Los productos seleccionados no tienen proveedor asociado");
        }
    } else {
        nsGral.MensajeAlert("Debe seleccionar uno o mas productos faltantes");
    }
});

$("#btnAsociaProviders").click(function () {
    nsInven.SelGridProductoFaltantes = $("#gmProductosFaltantes");
    nsInven.SelComboProviders = $("#cmbProveedores");
    var selectedProducts = nsInven.SelGridProductoFaltantes.igGridSelection("selectedRows");
    if (selectedProducts != null && selectedProducts != undefined && selectedProducts.length > 0) {
        var selectedProviders = nsInven.SelComboProviders.igCombo("selectedItems");
        if (selectedProviders != null && selectedProviders != undefined && selectedProviders.length > 0) {
            var productosFaltantes = new Array();
            var proveedoresAAsociar = new Array();
            $.each(selectedProducts, function (idx) {
                debugger
                var actualId = selectedProducts[idx].id.split("_")[1];
                productosFaltantes.push(actualId);
            });
            $.each(selectedProviders, function (idx) {
                proveedoresAAsociar.push(selectedProviders[idx].data.Id);
            });
            $.ajax(
                {
                    url: nsInven.UrlActualizarProviders,
                    type: 'POST',
                    data: { productos: productosFaltantes, proveedores: proveedoresAAsociar },
                    success: function (rp) {
                        nsGral.ProcesarResponseBasicVm(rp);
                        if (rp.Success) {
                            nsInven.SelGridProductoFaltantes.igGrid("dataBind");
                            nsInven.SelGridProductoFaltantes.igGridSelection("clearSelection");
                            nsInven.SelComboProviders.igCombo("deselectAll");
                        }
                    }, error: function (er1) {
                        debugger
                    }
                });
        } else {
            nsGral.MensajeAlert("Debe seleccionar uno o mas proveedores a asociar");
        }
    } else {
        nsGral.MensajeAlert("Debe seleccionar uno o mas productos faltantes");
    }

});

$("#btnEliminarProductosAPedir").click(function () {
    nsInven.SelGridProductosAPedir = $("#gmProductosAPedir");
    var arrayRecords = new Array();
    var selectedProducts = nsInven.SelGridProductosAPedir.igGridSelection("selectedRows");
    if (selectedProducts != null && selectedProducts != undefined && selectedProducts.length > 0) {
        var flagDependencies = false;
        $.each(selectedProducts, function (idx) {
            var idRecord = selectedProducts[idx].id;
            var statusRecord = nsInven.SelGridProductosAPedir.igGrid("getCellValue", idRecord, "Confirmado");
            if (statusRecord) {
                flagDependencies = true;
            } else {
                arrayRecords.push(idRecord);
            }
        });
        if (flagDependencies) {
            nsGral.MensajeAlert("No se pueden modificar valores ya confirmados");
        }
        if (arrayRecords.length > 0) {
            nsInven.EliminarProductosAPedir(arrayRecords);
        }
    } else {
        nsGral.MensajeAlert("Debe seleccionar uno o mas productos pedidos a eliminar");
    }
});

$("#btnConfirmarProductosAPedir").click(function () {
    debugger
    nsInven.SelGridProductosAPedir = $("#gmProductosAPedir");
    var selectedProducts = nsInven.SelGridProductosAPedir.igGridSelection("selectedRows");
    if (selectedProducts != null && selectedProducts != undefined && selectedProducts.length > 0) {
        var arrayRecords = new Array();
        var flagDependencies = false;
        $.each(selectedProducts, function (idx) {
            var idRecord = selectedProducts[idx].id;
            var statusRecord = nsInven.SelGridProductosAPedir.igGrid("getCellValue", idRecord, "Confirmado");
            if (statusRecord) {
                flagDependencies = true;
            } else {
                arrayRecords.push({ Id: idRecord, State: true });
            }
        });
        if (flagDependencies) {
            nsGral.MensajeAlert("Se han seleccionado registros ya confirmados");
        }
        if (arrayRecords.length > 0) {
            nsInven.UpdateConfirmacionProductosAPedir(arrayRecords);
        }
    } else {
        nsGral.MensajeAlert("Debe seleccionar uno o mas productos pedidos a confirmar");
    }
});

$("#btnDesconfirmarProductosAPedir").click(function () {
    nsInven.SelGridProductosAPedir = $("#gmProductosAPedir");
    var selectedProducts = nsInven.SelGridProductosAPedir.igGridSelection("selectedRows");
    if (selectedProducts != null && selectedProducts != undefined && selectedProducts.length > 0) {
        var arrayRecords = new Array();
        var flagDependencies = false;
        $.each(selectedProducts, function (idx) {
            var idRecord = selectedProducts[idx].id;
            var statusRecord = nsInven.SelGridProductosAPedir.igGrid("getCellValue", idRecord, "Confirmado");
            if (statusRecord) {
                arrayRecords.push({ Id: idRecord, State: false });
                //var idProducto = nsInven.SelGridProductosAPedir.igGrid("getCellValue", idRecord, "IdProducto");
                //var idProveedor = nsInven.SelGridProductosAPedir.igGrid("getCellValue", idRecord, "IdProveedor");
                //arrayRecords.push({ Id: idProducto, IdParent: idProveedor, Value: 0 });               
            } else {
                flagDependencies = true;
            }
        });
        if (flagDependencies) {
            nsGral.MensajeAlert("Se han seleccionado registros que su estatus es de no confirmado aún");
        }
        if (arrayRecords.length > 0) {
            nsInven.UpdateConfirmacionProductosAPedir(arrayRecords);
        }
    } else {
        nsGral.MensajeAlert("Debe seleccionar uno o mas productos pedidos a eliminar");
    }
});

$("#btnProgramarProductos").click(function () {
    debugger
    nsInven.SelGridProductoFaltantes = $("#gmProductosFaltantes");
    nsInven.SelGridProductosAPedir = $("#gmProductosAPedir");
    var selectedProducts = nsInven.SelGridProductoFaltantes.igGridSelection("selectedRows");
    if (selectedProducts != null && selectedProducts != undefined && selectedProducts.length > 0) {
        var takeCareDay = nsInven.SelChbDia.is(":checked");
        var arrayRecords = new Array();
        var noCorrectRecords = false;
        $.each(selectedProducts, function (idx) {
            var idRecord = selectedProducts[idx].id;
            var programado = nsInven.SelGridProductoFaltantes.igGrid("getCellValue", idRecord, "Programado");
            if (programado == 0) {
                var idProveedor = nsInven.SelGridProductoFaltantes.igGrid("getCellValue", idRecord, "ProveedorId");
                var idProducto = nsInven.SelGridProductoFaltantes.igGrid("getCellValue", idRecord, "ProductoId");
                var qtyPedir = nsInven.SelGridProductoFaltantes.igGrid("getCellValue", idRecord, "QtyAPedir");
                arrayRecords.push({ IdProducto: idProducto, IdProveedor: idProveedor, Qty: qtyPedir });
            } else {
                noCorrectRecords = true;
            }
        });
        if (noCorrectRecords) {
            nsGral.MensajeAlert("Se han seleccionado registros que ya han sido programados o confirmados");
        }
        if (arrayRecords.length > 0) {
            $.ajax({
                url: nsInven.UrlAddProductosAPedir,
                type: 'POST',
                data: { lstCandidates: arrayRecords, takeCareDay: takeCareDay },
                success: function (rp) {
                    nsGral.ProcesarResponseBasicVm(rp);
                    if (rp.Success) {
                        nsInven.SelGridProductoFaltantes.igGrid("dataBind");
                        nsInven.SelGridProductosAPedir.igGrid("dataBind");
                        //nsInven.SelGridProductoFaltantes.igGridSelection("clearSelection");               
                    }
                }, error: function (er1) {
                    debugger
                }
            });
        }
    } else {
        nsGral.MensajeAlert("Debe seleccionar uno o mas productos faltantes");
    }
});

$("#btnInactivarProductos").click(function () {
    nsInven.SelGridProductoFaltantes = $("#gmProductosFaltantes");
    var selectedProducts = nsInven.SelGridProductoFaltantes.igGridSelection("selectedRows");
    if (selectedProducts != null && selectedProducts != undefined && selectedProducts.length > 0) {
        var arrayRecords = new Array();
        var flagStatusIncorrect = false;
        $.each(selectedProducts, function (idx) {
            debugger
            var idRecord = selectedProducts[idx].id;
            var status = nsInven.SelGridProductoFaltantes.igGrid("getCellValue", idRecord, "Programado");
            if (status == 0) {
                arrayRecords.push(nsInven.SelGridProductoFaltantes.igGrid("getCellValue", idRecord, "ProductoId"));
            } else {
                flagStatusIncorrect = true;
            }
        });
        if (flagStatusIncorrect) {
            nsGral.MensajeAlert("Se han seleccionado productos programados y confirmados los cuales no se pueden inactivar");
        }
        if (arrayRecords.length > 0) {
            nsGral.PromptSweet('Desea eliminar los registros seleccionados?', nsInven.TryInactivarProductos, arrayRecords);
        }
    } else {
        nsGral.MensajeAlert("Debe seleccionar uno o mas productos faltantes");
    }
});

$("#btnUpdateUmbral").click(function () {
    nsInven.SelGridProductoFaltantes = $("#gmProductosFaltantes");
    var selectedProducts = nsInven.SelGridProductoFaltantes.igGridSelection("selectedRows");
    if (selectedProducts != null && selectedProducts != undefined && selectedProducts.length > 0) {
        var arrayRecords = new Array();
        $.each(selectedProducts, function (idx) {
            var idRecord = selectedProducts[idx].id;
            arrayRecords.push(
                {
                    Id: nsInven.SelGridProductoFaltantes.igGrid("getCellValue", idRecord, "ProductoId"),
                    CantidadUmbral: nsInven.SelGridProductoFaltantes.igGrid("getCellValue", idRecord, "CantidadUmbralSistema")
                });
        });
        if (arrayRecords.length > 0) {
            nsGral.PromptSweet('Desea actualizar el umbral de los productos seleccionados?', nsInven.TryUpdateUmbral, arrayRecords);
        }
    } else {
        nsGral.MensajeAlert("Debe seleccionar uno o mas productos faltantes");
    }
});

function cmbProveedoresFltSelectionChanged(evt, ui) {
    var proveedoresSelected = new Array();
    if (ui.items.length > 0) {
        $.each(ui.items, function (idx) {
            proveedoresSelected.push(ui.items[idx].data.Id);
        });
    }

    $.ajax(
        {
            url: nsInven.UrlSetFilterProviderProductosPedidos,
            type: 'POST',
            data: { idProveedores: proveedoresSelected },
            success: function (rp) {
                if (rp.Success) {
                    nsInven.SelGridProductosAPedir = $("#gmProductosAPedir");
                    nsInven.SelGridProductosAPedir.igGrid("dataBind");
                    nsInven.SelGridProductosAPedir.igGrid("clearSelection");
                }
            }, error: function () {

            }
        });
}

function rowDeletingProductosAPedir(evt, ui) {
    nsInven.SelGridProductosAPedir = $("#gmProductosAPedir");
    var statusRecord = nsInven.SelGridProductosAPedir.igGrid("getCellValue", ui.rowID, "Confirmado");
    if (statusRecord) {
        nsGral.MensajeAlert("No se pueden modificar valores ya confirmados");
        return false;
    } else {
        var arrayRecords = new Array();
        arrayRecords.push(ui.rowID);
        nsInven.EliminarProductosAPedir(arrayRecords);
        return false;
    }
}

function editRowEndingGridProductosAPedir(evt, ui) {
    if (ui.update) {
        nsInven.SelGridProductosAPedir = $("#gmProductosAPedir");
        var idRecord = ui.rowID;
        var statusRecord = nsInven.SelGridProductosAPedir.igGrid("getCellValue", idRecord, "Confirmado");
        if (statusRecord) {
            nsGral.MensajeAlert("No se pueden modificar valores ya confirmados");
            return false;
        } else {
            var recordToUpdate = { Id: idRecord, Value: ui.values.Qty };
            $.ajax(
                {
                    url: nsInven.UrlActualizarValorProductoAPedir,
                    type: 'POST',
                    data: recordToUpdate,
                    success: function (rp) {
                        nsGral.ProcesarResponseBasicVm(rp);
                        if (rp.Success) {
                            nsInven.SelGridProductosAPedir.igGrid("dataBind");
                        }
                    },
                    error: function (er1) {
                        alert("error en editRowEndingGridProductosAPedir en inventario.js");
                    }
                });
            return false;
        }
    }
}

function GrProgramarProductosDatafiltered(evt, ui) {    
    nsInven.SelGridProductoFaltantes = $("#gmProductosFaltantes");
    nsInven.SelGridProductoFaltantes.igGridSelection("clearSelection");
}

function GrProductosAPedirDatafiltered(evt, ui) {
    nsInven.SelGridProductosAPedir = $("#gmProductosAPedir");
    nsInven.SelGridProductosAPedir.igGridSelection("clearSelection");
}

function editRowEndingGridProductosFaltantes(evt, ui) {    
    if (ui.update) {
        nsInven.SelGridProductoFaltantes = $("#gmProductosFaltantes");
        var idRecord = ui.rowID;
        var idProducto = nsInven.SelGridProductoFaltantes.igGrid("getCellValue", idRecord, "ProductoId");
        var recordsToUpdate = new Array();
        recordsToUpdate.push(
            {
                Id: idProducto,
                CantidadActual: ui.values.CantidadActual,
                CantidadUmbral: ui.values.CantidadUmbral
            });
        $.ajax(
            {
                url: nsInven.UrlUpdateProductoGeneric,
                type: 'POST',
                data: { lstCandidates: recordsToUpdate },
                success: function (rp) {
                    nsGral.ProcesarResponseBasicVm(rp);
                    if (rp.Success) {
                        nsInven.SelGridProductoFaltantes.igGrid("dataBind");                                               
                    }
                }, error: function (er1, er2) {
                    nsGral.MensajeBad(er1 + er2);
                }
            });
        return false;
    }
}

nsInven.EliminarProductosAPedir = function (arrayToDelete) {
    nsInven.SelGridProductosAPedir = $("#gmProductosAPedir");
    nsInven.SelGridProductoFaltantes = $("#gmProductosFaltantes");

    $.ajax(
        {
            url: nsInven.UrlDeleteProductosAPedir,
            type: 'POST',
            data: { lstCandidates: arrayToDelete },
            success: function (rp) {
                nsGral.ProcesarResponseBasicVm(rp);
                if (rp.Success) {
                    nsInven.SelGridProductosAPedir.igGrid("dataBind");
                    nsInven.SelGridProductoFaltantes.igGrid("dataBind");
                    nsInven.SelGridProductosAPedir.igGridSelection("clearSelection");
                }
            }, error: function (er1) {
                alert("error en nsInven.EliminarProductosAPedir inventario.js");
                debugger
            }
        });
}

nsInven.UpdateConfirmacionProductosAPedir = function (arrayRecordsToProcess) {    
    nsInven.SelGridProductosAPedir = $("#gmProductosAPedir");
    nsInven.SelGridProductoFaltantes = $("#gmProductosFaltantes");
    $.ajax(
        {
            url: nsInven.UrlUpdateStatusConfirmacionProductosAPedir,
            type: 'POST',
            data: { candidates: arrayRecordsToProcess },
            success: function (rp) {
                nsGral.ProcesarResponseBasicVm(rp);
                if (rp.Success) {
                    nsInven.SelGridProductosAPedir.igGrid("dataBind");
                    nsInven.SelGridProductoFaltantes.igGrid("dataBind");
                    nsInven.SelGridProductosAPedir.igGridSelection("clearSelection");
                }
            }, error: function (er1) {
                alert("error en nsInven.EliminarProductosAPedir inventario.js");
                debugger
            }
        });
}

nsInven.ProgramarPedidoItem = function (idProveedor, idProducto, programado, qtyPedir) {
    if (programado == 0) {
        nsInven.SelGridProductoFaltantes = $("#gmProductosFaltantes");
        nsInven.SelGridProductosAPedir = $("#gmProductosAPedir");
        var takeCareDay = nsInven.SelChbDia.is(":checked");
        var items = new Array();
        items.push({ IdProducto: idProducto, IdProveedor: idProveedor, Qty: qtyPedir });

        $.ajax({
            url: nsInven.UrlAddProductosAPedir,
            type: 'POST',
            data: { lstCandidates: items, takeCareDay: takeCareDay },
            success: function (rp) {
                nsGral.ProcesarResponseBasicVm(rp);
                if (rp.Success) {
                    nsInven.SelGridProductoFaltantes.igGrid("dataBind");
                    nsInven.SelGridProductosAPedir.igGrid("dataBind");
                    //nsInven.SelGridProductoFaltantes.igGridSelection("clearSelection");               
                }
            }, error: function (er1) {
                debugger
            }
        });
    } else {
        nsGral.MensajeAlert("El producto ya se encuentra programado o confirmado");
    }
}

nsInven.TryInactivarProductos = function (arrayIdsToDelete) {
    nsInven.SelGridProductoFaltantes = $("#gmProductosFaltantes");
    nsInven.SelGridProductosAPedir = $("#gmProductosAPedir");
    if (arrayIdsToDelete != null && arrayIdsToDelete != undefined && arrayIdsToDelete.length > 0) {
        $.ajax(
            {
                url: nsInven.UrlInactivarProductos,
                type: 'POST',
                data: { idsToDelete: arrayIdsToDelete },
                success: function (rp) {
                    nsGral.ProcesarResponseBasicVm(rp);
                    if (rp.Success) {
                        nsInven.SelGridProductoFaltantes.igGrid("dataBind");
                        nsInven.SelGridProductoFaltantes.igGridSelection("clearSelection");
                        nsInven.SelGridProductosAPedir.igGrid("dataBind");
                    }
                }, error: function (er1, er2) {
                    nsGral.MensajeBad(er1 + er2);
                }
            });
    } else {
        nsGral.MensajeBad("No se estan enviando datos a funcion en codigo cliente!");
    }
}

nsInven.TryUpdateUmbral = function (arrayToProcess) {
    debugger
    nsInven.SelGridProductoFaltantes = $("#gmProductosFaltantes");
    if (arrayToProcess != null && arrayToProcess != undefined && arrayToProcess.length > 0) {
        $.ajax(
            {
                url: nsInven.UrlUpdateProductoGeneric,
                type: 'POST',
                data: { lstCandidates: arrayToProcess },
                success: function (rp) {
                    nsGral.ProcesarResponseBasicVm(rp);
                    if (rp.Success) {
                        nsInven.SelGridProductoFaltantes.igGrid("dataBind");
                        //nsInven.SelGridProductoFaltantes.igGridSelection("clearSelection");                        
                    }
                }, error: function (er1, er2) {
                    nsGral.MensajeBad(er1 + er2);
                }
            });
    } else {
        nsGral.MensajeBad("No se estan enviando datos a funcion en codigo cliente!");
    }
}