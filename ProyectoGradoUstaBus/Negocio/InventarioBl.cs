using ProyectoGradoUstaBus.Negocio;
using ProyectoGradoUstaCommon;
using ProyectoGradoUstaUtility;
using ProyectoUstaDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaBus
{
    public sealed class InventarioBl
    {
        #region [FIELDS]
        ProyectoUstaDomainCtx domainCtx;
        CajaBl cajaBl;
        #endregion

        #region [CONSTRUCTOR]
        public InventarioBl()
        {
            domainCtx = new ProyectoUstaDomainCtx();
        }

        #endregion

        #region [BL METHODS]
        private List<BasicIntDateVm> GetOrdersDay(bool takeCareDay)
        {
            var dayReference = string.Empty;
            var dateNowReferenceWithoutHours = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            if(takeCareDay)
            {
                dayReference = DateTime.Now.DayOfWeek.ToString();
            }
            else
            {
                dayReference = DateTime.Now.AddDays(1).DayOfWeek.ToString();                
            }
            
            var orderList = new List<BasicIntDateVm>();
            switch (dayReference)
            {
                case "Monday":
                    orderList = new List<BasicIntDateVm>()
                                    {
                                        new BasicIntDateVm() { Id = 1, Value = "LUNES", Date = takeCareDay ? dateNowReferenceWithoutHours: dateNowReferenceWithoutHours.AddDays(1)},
                                        new BasicIntDateVm() { Id = 2, Value = "MARTES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(1): dateNowReferenceWithoutHours.AddDays(2)},
                                        new BasicIntDateVm() { Id = 3, Value = "MIERCOLES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(2): dateNowReferenceWithoutHours.AddDays(3)},
                                        new BasicIntDateVm() { Id = 4, Value = "JUEVES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(3): dateNowReferenceWithoutHours.AddDays(4)},
                                        new BasicIntDateVm() { Id = 5, Value = "VIERNES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(4): dateNowReferenceWithoutHours.AddDays(5)},
                                        new BasicIntDateVm() { Id = 6, Value = "SABADO", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(5): dateNowReferenceWithoutHours.AddDays(6)},
                                        new BasicIntDateVm() { Id = 7, Value = "DOMINGO", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(6): dateNowReferenceWithoutHours.AddDays(7)}
                                    };
                    break;
                case "Tuesday":
                    orderList = new List<BasicIntDateVm>()
                                    {
                                        new BasicIntDateVm() { Id = 1, Value = "MARTES", Date = takeCareDay ? dateNowReferenceWithoutHours: dateNowReferenceWithoutHours.AddDays(1)},
                                        new BasicIntDateVm() { Id = 2, Value = "MIERCOLES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(1): dateNowReferenceWithoutHours.AddDays(2)},
                                        new BasicIntDateVm() { Id = 3, Value = "JUEVES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(2): dateNowReferenceWithoutHours.AddDays(3)},
                                        new BasicIntDateVm() { Id = 4, Value = "VIERNES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(3): dateNowReferenceWithoutHours.AddDays(4)},
                                        new BasicIntDateVm() { Id = 5, Value = "SABADO", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(4): dateNowReferenceWithoutHours.AddDays(5)},
                                        new BasicIntDateVm() { Id = 6, Value = "DOMINGO", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(5): dateNowReferenceWithoutHours.AddDays(6)},
                                        new BasicIntDateVm() { Id = 7, Value = "LUNES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(6): dateNowReferenceWithoutHours.AddDays(7)},
                                    };
                    break;
                case "Wednesday":
                    orderList = new List<BasicIntDateVm>()
                                    {
                                        new BasicIntDateVm() { Id = 1, Value = "MIERCOLES", Date = takeCareDay ? dateNowReferenceWithoutHours: dateNowReferenceWithoutHours.AddDays(1)},
                                        new BasicIntDateVm() { Id = 2, Value = "JUEVES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(1): dateNowReferenceWithoutHours.AddDays(2)},
                                        new BasicIntDateVm() { Id = 3, Value = "VIERNES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(2): dateNowReferenceWithoutHours.AddDays(3)},
                                        new BasicIntDateVm() { Id = 4, Value = "SABADO", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(3): dateNowReferenceWithoutHours.AddDays(4)},
                                        new BasicIntDateVm() { Id = 5, Value = "DOMINGO", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(4): dateNowReferenceWithoutHours.AddDays(5)},
                                        new BasicIntDateVm() { Id = 6, Value = "LUNES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(5): dateNowReferenceWithoutHours.AddDays(6)},
                                        new BasicIntDateVm() { Id = 7, Value = "MARTES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(6): dateNowReferenceWithoutHours.AddDays(7)}
                                    };
                    break;
                case "Thursday":
                    orderList = new List<BasicIntDateVm>()
                                    {
                                        new BasicIntDateVm() { Id = 1, Value = "JUEVES", Date = takeCareDay ? dateNowReferenceWithoutHours: dateNowReferenceWithoutHours.AddDays(1)},
                                        new BasicIntDateVm() { Id = 2, Value = "VIERNES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(1): dateNowReferenceWithoutHours.AddDays(2)},
                                        new BasicIntDateVm() { Id = 3, Value = "SABADO", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(2): dateNowReferenceWithoutHours.AddDays(3)},
                                        new BasicIntDateVm() { Id = 4, Value = "DOMINGO", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(3): dateNowReferenceWithoutHours.AddDays(4)},
                                        new BasicIntDateVm() { Id = 5, Value = "LUNES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(4): dateNowReferenceWithoutHours.AddDays(5)},
                                        new BasicIntDateVm() { Id = 6, Value = "MARTES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(5): dateNowReferenceWithoutHours.AddDays(6)},
                                        new BasicIntDateVm() { Id = 7, Value = "MIERCOLES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(6): dateNowReferenceWithoutHours.AddDays(7)}
                                    };
                    break;
                case "Friday":
                    orderList = new List<BasicIntDateVm>()
                                    {
                                        new BasicIntDateVm() { Id = 1, Value = "VIERNES", Date = takeCareDay ? dateNowReferenceWithoutHours: dateNowReferenceWithoutHours.AddDays(1)},
                                        new BasicIntDateVm() { Id = 2, Value = "SABADO", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(1): dateNowReferenceWithoutHours.AddDays(2)},
                                        new BasicIntDateVm() { Id = 3, Value = "DOMINGO", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(2): dateNowReferenceWithoutHours.AddDays(3)},
                                        new BasicIntDateVm() { Id = 4, Value = "LUNES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(3): dateNowReferenceWithoutHours.AddDays(4)},
                                        new BasicIntDateVm() { Id = 5, Value = "MARTES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(4): dateNowReferenceWithoutHours.AddDays(5)},
                                        new BasicIntDateVm() { Id = 6, Value = "MIERCOLES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(5): dateNowReferenceWithoutHours.AddDays(6)},
                                        new BasicIntDateVm() { Id = 7, Value = "JUEVES", Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(6): dateNowReferenceWithoutHours.AddDays(7)}
                                    };
                    break;
                case "Saturday":
                    orderList = new List<BasicIntDateVm>()
                                    {
                                        new BasicIntDateVm() { Id = 1, Value = "SABADO" , Date = takeCareDay ? dateNowReferenceWithoutHours: dateNowReferenceWithoutHours.AddDays(1)},
                                        new BasicIntDateVm() { Id = 2, Value = "DOMINGO" , Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(1): dateNowReferenceWithoutHours.AddDays(2)},
                                        new BasicIntDateVm() { Id = 3, Value = "LUNES" , Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(2): dateNowReferenceWithoutHours.AddDays(3)},
                                        new BasicIntDateVm() { Id = 4, Value = "MARTES" , Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(3): dateNowReferenceWithoutHours.AddDays(4)},
                                        new BasicIntDateVm() { Id = 5, Value = "MIERCOLES" , Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(4): dateNowReferenceWithoutHours.AddDays(5)},
                                        new BasicIntDateVm() { Id = 6, Value = "JUEVES" , Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(5): dateNowReferenceWithoutHours.AddDays(6)},
                                        new BasicIntDateVm() { Id = 7, Value = "VIERNES" , Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(6): dateNowReferenceWithoutHours.AddDays(7)}
                                    };
                    break;
                case "Sunday":
                    orderList = new List<BasicIntDateVm>()
                                    {
                                        new BasicIntDateVm() { Id = 1, Value = "DOMINGO" , Date = takeCareDay ? dateNowReferenceWithoutHours: dateNowReferenceWithoutHours.AddDays(1)},
                                        new BasicIntDateVm() { Id = 2, Value = "LUNES" , Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(1): dateNowReferenceWithoutHours.AddDays(2)},
                                        new BasicIntDateVm() { Id = 3, Value = "MARTES" , Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(2): dateNowReferenceWithoutHours.AddDays(3)},
                                        new BasicIntDateVm() { Id = 4, Value = "MIERCOLES" , Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(3): dateNowReferenceWithoutHours.AddDays(4)},
                                        new BasicIntDateVm() { Id = 5, Value = "JUEVES" , Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(4): dateNowReferenceWithoutHours.AddDays(5)},
                                        new BasicIntDateVm() { Id = 6, Value = "VIERNES" , Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(5): dateNowReferenceWithoutHours.AddDays(6)},
                                        new BasicIntDateVm() { Id = 7, Value = "SABADO" , Date = takeCareDay ? dateNowReferenceWithoutHours.AddDays(6): dateNowReferenceWithoutHours.AddDays(7)}
                                    };
                    break;
            }
            return orderList.OrderBy(x => x.Id).ToList();
        }

        private ProductosAPedirProyectoUsta GetNextEntregaDate(ProveedoresProyectoUsta providerData, ProductosAPedirProyectoUsta candidate)
        {
            if(candidate.DiaVisita == "LUNES")
            {                
                if (providerData.Entrega_Lunes)
                {
                    candidate.DiaEntrega = "LUNES";
                    candidate.FechaEntrega = candidate.FechaVisita;
                }
                else if(providerData.Entrega_Martes)
                {
                    candidate.DiaEntrega = "MARTES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(1);
                }
                else if (providerData.Entrega_Miercoles)
                {
                    candidate.DiaEntrega = "MIERCOLES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(2);
                }
                else if (providerData.Entrega_Jueves)
                {
                    candidate.DiaEntrega = "JUEVES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(3);
                }
                else if (providerData.Entrega_Viernes)
                {
                    candidate.DiaEntrega = "VIERNES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(4);
                }
                else if (providerData.Entrega_Sabado)
                {
                    candidate.DiaEntrega = "SABADO";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(5);
                }
                else if (providerData.Entrega_Domingo)
                {
                    candidate.DiaEntrega = "DOMINGO";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(6);
                }
            }

            if (candidate.DiaVisita == "MARTES")
            {
                if (providerData.Entrega_Martes)
                {
                    candidate.DiaEntrega = "MARTES";
                    candidate.FechaEntrega = candidate.FechaVisita;
                }
                else if (providerData.Entrega_Miercoles)
                {
                    candidate.DiaEntrega = "MIERCOLES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(1);
                }
                else if (providerData.Entrega_Jueves)
                {
                    candidate.DiaEntrega = "JUEVES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(2);
                }
                else if (providerData.Entrega_Viernes)
                {
                    candidate.DiaEntrega = "VIERNES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(3);
                }
                else if (providerData.Entrega_Sabado)
                {
                    candidate.DiaEntrega = "SABADO";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(4);
                }
                else if (providerData.Entrega_Domingo)
                {
                    candidate.DiaEntrega = "DOMINGO";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(5);
                }
                else if (providerData.Entrega_Lunes)
                {
                    candidate.DiaEntrega = "LUNES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(6);
                }
            }

            if (candidate.DiaVisita == "MIERCOLES")
            {
                if (providerData.Entrega_Miercoles)
                {
                    candidate.DiaEntrega = "MIERCOLES";
                    candidate.FechaEntrega = candidate.FechaVisita;
                }
                else if (providerData.Entrega_Jueves)
                {
                    candidate.DiaEntrega = "JUEVES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(1);
                }
                else if (providerData.Entrega_Viernes)
                {
                    candidate.DiaEntrega = "VIERNES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(2);
                }
                else if (providerData.Entrega_Sabado)
                {
                    candidate.DiaEntrega = "SABADO";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(3);
                }
                else if (providerData.Entrega_Domingo)
                {
                    candidate.DiaEntrega = "DOMINGO";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(4);
                }
                else if (providerData.Entrega_Lunes)
                {
                    candidate.DiaEntrega = "LUNES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(5);
                }
                else if (providerData.Entrega_Martes)
                {
                    candidate.DiaEntrega = "MARTES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(6);
                }
            }

            if (candidate.DiaVisita == "JUEVES")
            {
                
                if (providerData.Entrega_Jueves)
                {
                    candidate.DiaEntrega = "JUEVES";
                    candidate.FechaEntrega = candidate.FechaVisita;
                }
                else if (providerData.Entrega_Viernes)
                {
                    candidate.DiaEntrega = "VIERNES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(1);
                }
                else if (providerData.Entrega_Sabado)
                {
                    candidate.DiaEntrega = "SABADO";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(2);
                }
                else if (providerData.Entrega_Domingo)
                {
                    candidate.DiaEntrega = "DOMINGO";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(3);
                }
                else if (providerData.Entrega_Lunes)
                {
                    candidate.DiaEntrega = "LUNES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(4);
                }
                else if (providerData.Entrega_Martes)
                {
                    candidate.DiaEntrega = "MARTES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(5);
                }
                else if (providerData.Entrega_Miercoles)
                {
                    candidate.DiaEntrega = "MIERCOLES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(6);
                }
            }

            if (candidate.DiaVisita == "VIERNES")
            {                
                if (providerData.Entrega_Viernes)
                {
                    candidate.DiaEntrega = "VIERNES";
                    candidate.FechaEntrega = candidate.FechaVisita;
                }
                else if (providerData.Entrega_Sabado)
                {
                    candidate.DiaEntrega = "SABADO";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(1);
                }
                else if (providerData.Entrega_Domingo)
                {
                    candidate.DiaEntrega = "DOMINGO";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(2);
                }
                else if (providerData.Entrega_Lunes)
                {
                    candidate.DiaEntrega = "LUNES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(3);
                }
                else if (providerData.Entrega_Martes)
                {
                    candidate.DiaEntrega = "MARTES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(4);
                }
                else if (providerData.Entrega_Miercoles)
                {
                    candidate.DiaEntrega = "MIERCOLES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(5);
                }
                else if (providerData.Entrega_Jueves)
                {
                    candidate.DiaEntrega = "JUEVES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(6);
                }
            }

            if (candidate.DiaVisita == "SABADO")
            {                
                if (providerData.Entrega_Sabado)
                {
                    candidate.DiaEntrega = "SABADO";
                    candidate.FechaEntrega = candidate.FechaVisita;
                }
                else if (providerData.Entrega_Domingo)
                {
                    candidate.DiaEntrega = "DOMINGO";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(1);
                }
                else if (providerData.Entrega_Lunes)
                {
                    candidate.DiaEntrega = "LUNES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(2);
                }
                else if (providerData.Entrega_Martes)
                {
                    candidate.DiaEntrega = "MARTES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(3);
                }
                else if (providerData.Entrega_Miercoles)
                {
                    candidate.DiaEntrega = "MIERCOLES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(4);
                }
                else if (providerData.Entrega_Jueves)
                {
                    candidate.DiaEntrega = "JUEVES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(5);
                }
                else if (providerData.Entrega_Viernes)
                {
                    candidate.DiaEntrega = "VIERNES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(6);
                }
            }

            if (candidate.DiaVisita == "DOMINGO")
            {                                               
                if (providerData.Entrega_Domingo)
                {
                    candidate.DiaEntrega = "DOMINGO";
                    candidate.FechaEntrega = candidate.FechaVisita;
                }
                if (providerData.Entrega_Lunes)
                {
                    candidate.DiaEntrega = "LUNES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(1);
                }
                else if (providerData.Entrega_Martes)
                {
                    candidate.DiaEntrega = "MARTES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(2);
                }
                else if (providerData.Entrega_Miercoles)
                {
                    candidate.DiaEntrega = "MIERCOLES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(3);
                }
                else if (providerData.Entrega_Jueves)
                {
                    candidate.DiaEntrega = "JUEVES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(4);
                }
                else if (providerData.Entrega_Viernes)
                {
                    candidate.DiaEntrega = "VIERNES";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(5);
                }
                else if (providerData.Entrega_Sabado)
                {
                    candidate.DiaEntrega = "SABADO";
                    candidate.FechaEntrega = candidate.FechaVisita.AddDays(6);
                }               
            }
            return candidate;
        }
        #endregion

        #region [GET]
        public int GetValorTotalInventario()
        {
            return (from productos in domainCtx.ProductosProyectoUsta
                           where productos.CantidadActual > 0
                           select new
                           {
                               Valor = (productos.CantidadActual * productos.Precio)
                           }).Sum(x => x.Valor);
        }

        public IQueryable<ProductoAPedirIgVm> GetProductosAPedir(List<int> idProveedoresFilter)
        {
            var dateReference = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            if (idProveedoresFilter.Count == 0)
            {
                return (from productosAPedir in domainCtx.ProductosAPedirProyectoUsta
                        join productos in domainCtx.ProductosProyectoUsta on productosAPedir.IdProducto equals productos.Id
                        join proveedores in domainCtx.ProveedoresProyectoUsta on productosAPedir.IdProveedor equals proveedores.Id into leftJoin1
                        from resultadoLeft in leftJoin1.DefaultIfEmpty()                                               
                        select new ProductoAPedirIgVm
                        {
                            IdRecord = productosAPedir.IdRecord,
                            IdProveedor = productosAPedir.IdProveedor,
                            IdProducto = productosAPedir.IdProducto,
                            ProductoNombre = productos.Nombre,
                            ProveedorNombre = (resultadoLeft.Nombre == null) ? "SIN PROVEEDOR" : resultadoLeft.Nombre,
                            DiaVisita = productosAPedir.DiaVisita,
                            FechaVisita = productosAPedir.FechaVisita,
                            DiaEntrega = productosAPedir.DiaEntrega,
                            FechaEntrega = productosAPedir.FechaEntrega,
                            Confirmado = productosAPedir.Confirmado,
                            Qty = productosAPedir.Qty,                            
                            Subtotal = productosAPedir.Qty * productos.Precio
                        }).OrderBy(x => x.FechaVisita).AsQueryable();
            }
            else
            {
                return (from productosAPedir in domainCtx.ProductosAPedirProyectoUsta
                        join productos in domainCtx.ProductosProyectoUsta on productosAPedir.IdProducto equals productos.Id
                        join proveedores in domainCtx.ProveedoresProyectoUsta on productosAPedir.IdProveedor equals proveedores.Id into leftJoin1
                        from resultadoLeft in leftJoin1.DefaultIfEmpty()
                        where idProveedoresFilter.Contains(productosAPedir.IdProveedor)                       
                        select new ProductoAPedirIgVm
                        {
                            IdRecord = productosAPedir.IdRecord,
                            IdProveedor = productosAPedir.IdProveedor,
                            IdProducto = productosAPedir.IdProducto,
                            ProductoNombre = productos.Nombre,
                            ProveedorNombre = (resultadoLeft.Nombre == null) ? "SIN PROVEEDOR" : resultadoLeft.Nombre,
                            DiaVisita = productosAPedir.DiaVisita,
                            FechaVisita = productosAPedir.FechaVisita,
                            DiaEntrega = productosAPedir.DiaEntrega,
                            FechaEntrega = productosAPedir.FechaEntrega,
                            Confirmado = productosAPedir.Confirmado,
                            Qty = productosAPedir.Qty,
                            Subtotal = productosAPedir.Qty * productos.Precio
                        }).OrderBy(x => x.FechaVisita).AsQueryable();
                
            }
        }

        /// <summary>
        /// Obtiene los records para procesar el ingreso de items al inventario
        /// </summary>
        /// <returns></returns>
        public IQueryable<ProductosBufferInventarioIgVm> GetProductosBufferAInventario(int idProvider, DateTime fechaReferencia)
        {
            return (from productosBuffer in domainCtx.ProductosBufferAInventarioProyectoUsta
                    join productos in domainCtx.ProductosProyectoUsta on productosBuffer.IdProducto equals productos.Id
                    join proveedores in domainCtx.ProveedoresProyectoUsta on productosBuffer.IdProveedor equals proveedores.Id
                    where productosBuffer.IdProveedor == idProvider && productosBuffer.FechaReferencia == fechaReferencia
                    select new ProductosBufferInventarioIgVm
                    {
                        Id = productosBuffer.Id,
                        ProductoNombre = productos.Nombre,
                        IdProducto = productos.Id,                        
                        ProveedorNombre = proveedores.Nombre,
                        IdProveedor = proveedores.Id,
                        CostoItem = productosBuffer.CostoItem,
                        PrecioAsignado = productosBuffer.PrecioAsignado,
                        QtyPedida =  productosBuffer.QtyPedida,
                        QtyActual = productos.CantidadActual,
                        QtyRealAIngresar = productosBuffer.QtyRealAIngresar
                    }).OrderBy(x => x.Id).AsQueryable();
        }

        public PedidosVisitasVm GetInfoPedidos()
        {           
            var dateReference = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var modelo = new PedidosVisitasVm();
            modelo.ListaPedidosPagadosDia = (from logPedidos in domainCtx.LogPedidosProyectoUsta
                                             where logPedidos.FechaReferencia == dateReference
                                             select new BasicIntDateVm()
                                             {
                                                 Id = logPedidos.ValorPedido,
                                                 Date = logPedidos.FechaRegistro,
                                                 Value = logPedidos.Observaciones
                                             }).OrderBy(x => x.Date).ToList();
            modelo.TotalPagadoDía = modelo.ListaPedidosPagadosDia.Sum(x => x.Id);

            modelo.PedidosPorRecibir = (from productosAPedir in domainCtx.ProductosAPedirProyectoUsta
                                        where productosAPedir.FechaEntrega == dateReference &&
                                        productosAPedir.Confirmado == true
                                        group productosAPedir by new { productosAPedir.IdProveedor } into agrupacion
                                        select new PedidosVisitasBaseVm()
                                        {
                                            IdProveedor = agrupacion.Key.IdProveedor,
                                            NombreProveedor = domainCtx.ProveedoresProyectoUsta.Where(x => x.Id == agrupacion.Key.IdProveedor).FirstOrDefault().Nombre,
                                            Records = (from productosAPedir2 in domainCtx.ProductosAPedirProyectoUsta
                                                       join productos in domainCtx.ProductosProyectoUsta on productosAPedir2.IdProducto equals productos.Id
                                                       where productosAPedir2.IdProveedor == agrupacion.Key.IdProveedor
                                                       && productosAPedir2.FechaEntrega == dateReference
                                                       && productosAPedir2.Confirmado == true
                                                       select new BasicDependantIntWithNameVm()
                                                       {
                                                           Id = productosAPedir2.IdRecord,
                                                           NameId = productos.Nombre,
                                                           IdParent = productosAPedir2.Qty,
                                                           Value = 1//productosAPedir2.Confirmado
                                                       }).ToList()                                            
                                        }).ToList();

            modelo.PedidosPorConfirmar = (from productosAPedir in domainCtx.ProductosAPedirProyectoUsta
                                          where productosAPedir.FechaVisita == dateReference &&
                                          productosAPedir.Confirmado == false
                                          group productosAPedir by new { productosAPedir.IdProveedor } into agrupacion
                                          select new PedidosVisitasBaseVm()
                                          {
                                              IdProveedor = agrupacion.Key.IdProveedor,
                                              NombreProveedor = domainCtx.ProveedoresProyectoUsta.Where(x => x.Id == agrupacion.Key.IdProveedor).FirstOrDefault().Nombre,
                                              Records = (from productosAPedir2 in domainCtx.ProductosAPedirProyectoUsta
                                                         join productos in domainCtx.ProductosProyectoUsta on productosAPedir2.IdProducto equals productos.Id
                                                         where productosAPedir2.IdProveedor == agrupacion.Key.IdProveedor
                                                         && productosAPedir2.FechaVisita == dateReference
                                                         && productosAPedir2.Confirmado == false
                                                         select new BasicDependantIntWithNameVm()
                                                         {
                                                             Id = productosAPedir2.IdRecord,
                                                             NameId = productos.Nombre,
                                                             IdParent = productosAPedir2.Qty,
                                                             Value = 1//productosAPedir2.Confirmado
                                                         }).ToList()
                                          }).ToList();


            //bufferEntityVisita.Records.Add(new BasicDependantIntWithNameVm() { Id = z.IdRecord, NameId = z.NombreProducto, IdParent = z.Qty, Value = z.Confirmado ? 1 : 0 });


            //var allDataVisitaPedidoMandatory = (from productosAPedir in domainCtx.ProductosAPedirProyectoUsta
            //                        join productos in domainCtx.ProductosProyectoUsta on productosAPedir.IdProducto equals productos.Id
            //                        join proveedores in domainCtx.ProveedoresProyectoUsta on productosAPedir.IdProveedor equals proveedores.Id
            //                        where (productosAPedir.IdProveedor != 0) && (productosAPedir.FechaVisita == dateReference || productosAPedir.FechaEntrega == dateReference)                                    
            //                        select new
            //                        {
            //                            productosAPedir.IdRecord,
            //                            productosAPedir.IdProducto,
            //                            NombreProducto = productos.Nombre,
            //                            productosAPedir.IdProveedor,
            //                            NombreProveedor = proveedores.Nombre,
            //                            productosAPedir.Qty,
            //                            productosAPedir.Confirmado,
            //                            productosAPedir.FechaEntrega,
            //                            productosAPedir.FechaVisita                                        
            //                        }).ToList();
            //var allDataPedidosRealizadosMandatory = domainCtx.LogPedidosProyectoUsta.Where(x => x.FechaReferencia == dateReference).ToList();
            //var idsProviderPedidosRegistradosDateReference = allDataPedidosRealizadosMandatory.Select(y => y.IdProveedor).Distinct().ToList();//   domainCtx.LogPedidosProyectoUsta.Where(x => x.FechaReferencia == dateReference).Select(y => y.IdProveedor).Distinct().ToList();
            //var idProvidersVisitas = allDataVisitaPedidoMandatory.Where(x => x.FechaVisita == dateReference).Select(x => x.IdProveedor).Distinct().ToList();
            //var idProvidersPedidos = allDataVisitaPedidoMandatory.Where(x => x.FechaEntrega == dateReference && x.Confirmado == true).Select(x => x.IdProveedor).Distinct().ToList();
            //var dataVisitas = allDataVisitaPedidoMandatory.Where(x => x.FechaVisita == dateReference).ToList();
            //var dataPedidos = allDataVisitaPedidoMandatory.Where(x => x.FechaEntrega == dateReference && x.Confirmado == true).ToList();

            //var collectionVisitas = new List<PedidosVisitasBaseVm>();
            //var collectionPedidos = new List<PedidosVisitasBaseVm>();
            //var collectionOtrosPedidos = new List<BasicVm>();
            //var totalPagadoPedidos = 0;

            //allDataPedidosRealizadosMandatory.Where(x => x.IdProveedor == 0).ToList().ForEach(y =>
            //{
            //    var bufferVm = new BasicVm();
            //    totalPagadoPedidos += y.ValorPedido;
            //    bufferVm.Id = y.ValorPedido;                
            //    bufferVm.Value = y.Observaciones;                
            //    collectionOtrosPedidos.Add(bufferVm);
            //});

            //totalPagadoPedidos += allDataPedidosRealizadosMandatory.Where(x => x.IdProveedor != 0).Sum(y => y.ValorPedido);

            //idProvidersVisitas.ForEach(x =>
            //{
            //    if(idsProviderPedidosRegistradosDateReference.Contains(x))
            //    {

            //    }
            //    else
            //    {
            //        var bufferEntityVisita = new PedidosVisitasBaseVm();
            //        bufferEntityVisita.IdProveedor = x;
            //        bufferEntityVisita.Confirmado = dataVisitas.Where(y => y.IdProveedor == x && y.Confirmado).Count() > 0 ? true : false;
            //        bufferEntityVisita.NombreProveedor = dataVisitas.Where(y => y.IdProveedor == x).FirstOrDefault().NombreProveedor;
            //        dataVisitas.Where(y => y.IdProveedor == x).ToList().ForEach(z =>
            //        {
            //            bufferEntityVisita.Records.Add(new BasicDependantIntWithNameVm() { Id = z.IdRecord, NameId = z.NombreProducto, IdParent = z.Qty, Value = z.Confirmado ? 1 : 0 });
            //        });
            //        collectionVisitas.Add(bufferEntityVisita);
            //    }
                
            //});

            //idProvidersPedidos.ForEach(x =>
            //{
            //    var bufferEntityPedido = new PedidosVisitasBaseVm();
            //    bufferEntityPedido.IdProveedor = x;
            //    bufferEntityPedido.Confirmado = idsProviderPedidosRegistradosDateReference.Contains(x) ? true : false;
            //    bufferEntityPedido.ValorRegistradoPedido = allDataPedidosRealizadosMandatory.Where(m => m.IdProveedor == x).Sum(t => t.ValorPedido);
            //    bufferEntityPedido.NombreProveedor = dataPedidos.Where(y => y.IdProveedor == x).FirstOrDefault().NombreProveedor;
            //    dataPedidos.Where(y => y.IdProveedor == x).ToList().ForEach(z =>
            //    {
            //        bufferEntityPedido.Records.Add(new BasicDependantIntWithNameVm() { Id = z.IdRecord, NameId = z.NombreProducto, IdParent = z.Qty, Value = z.Confirmado ? 1 : 0 });
            //    });
            //    collectionPedidos.Add(bufferEntityPedido);
            //});

            //modelo.TotalPagadoDía = totalPagadoPedidos;
            //modelo.ListaOtrosPedidos = collectionOtrosPedidos;
            //modelo.Pedidos = collectionPedidos.OrderBy(x => x.NombreProveedor).ToList();
            //modelo.Visitas = collectionVisitas.OrderBy(x => x.NombreProveedor).ToList();
            return modelo;                   
        }
 
        #endregion

        #region [ADD]
        public ResponseBasicVm AddProductosAPedir(List<ProductosAPedirProyectoUsta> items, bool takeCare, int idUsuarioRegistro)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var itemsZero = items.Where(x => x.IdProveedor == 0).ToList();
                var itemsProvider = items.Where(x => x.IdProveedor != 0).ToList();
                var actualRecordsProviderToInsert = new List<ProductosAPedirProyectoUsta>();
                if (itemsProvider.Count > 0)
                {
                    var idsProvidersSpecific = itemsProvider.Select(x => x.IdProveedor).Distinct().ToList();
                    var providersData = domainCtx.ProveedoresProyectoUsta.Where(x => idsProvidersSpecific.Contains(x.Id)).ToList();
                    var dayReference = DateTime.Now.DayOfWeek.ToString();
                    
                    var orderList = GetOrdersDay(takeCare);                   

                    foreach (ProductosAPedirProyectoUsta item in itemsProvider)
                    {
                        var currentProvider = providersData.Where(x => x.Id == item.IdProveedor).FirstOrDefault();
                        if (currentProvider != null)
                        {
                            foreach (BasicIntDateVm itemDay in orderList)
                            {
                                var candidateProductoPedir = new ProductosAPedirProyectoUsta()
                                {
                                    Qty = item.Qty <= 0 ? (short)1 : item.Qty,
                                    IdUsuarioRegistro = idUsuarioRegistro,
                                    IdProducto = item.IdProducto,
                                    IdProveedor = item.IdProveedor,
                                    FechaRegistro = DateTime.Now
                                };
                                if (itemDay.Value == "LUNES")
                                {
                                    if (currentProvider.Visita_Lunes)
                                    {
                                        candidateProductoPedir.DiaVisita = itemDay.Value;
                                        candidateProductoPedir.FechaVisita = itemDay.Date;                                                                              
                                        actualRecordsProviderToInsert.Add(GetNextEntregaDate(currentProvider, candidateProductoPedir));
                                        break;
                                    }
                                }
                                else if(itemDay.Value == "MARTES")
                                {
                                    if (currentProvider.Visita_Martes)
                                    {
                                        candidateProductoPedir.DiaVisita = itemDay.Value;
                                        candidateProductoPedir.FechaVisita = itemDay.Date;
                                        actualRecordsProviderToInsert.Add(GetNextEntregaDate(currentProvider, candidateProductoPedir));
                                        break;
                                    }
                                }
                                else if (itemDay.Value == "MIERCOLES")
                                {
                                    if (currentProvider.Visita_Miercoles)
                                    {
                                        candidateProductoPedir.DiaVisita = itemDay.Value;
                                        candidateProductoPedir.FechaVisita = itemDay.Date;
                                        actualRecordsProviderToInsert.Add(GetNextEntregaDate(currentProvider, candidateProductoPedir));
                                        break;
                                    }
                                }
                                else if (itemDay.Value == "JUEVES")
                                {
                                    if (currentProvider.Visita_Jueves)
                                    {
                                        candidateProductoPedir.DiaVisita = itemDay.Value;
                                        candidateProductoPedir.FechaVisita = itemDay.Date;
                                        actualRecordsProviderToInsert.Add(GetNextEntregaDate(currentProvider, candidateProductoPedir));
                                        break;
                                    }
                                }
                                else if (itemDay.Value == "VIERNES")
                                {
                                    if (currentProvider.Visita_Viernes)
                                    {
                                        candidateProductoPedir.DiaVisita = itemDay.Value;
                                        candidateProductoPedir.FechaVisita = itemDay.Date;
                                        actualRecordsProviderToInsert.Add(GetNextEntregaDate(currentProvider, candidateProductoPedir));
                                        break;
                                    }
                                }
                                else if (itemDay.Value == "SABADO")
                                {
                                    if(currentProvider.Visita_Sabado)
                                    {
                                        candidateProductoPedir.DiaVisita = itemDay.Value;
                                        candidateProductoPedir.FechaVisita = itemDay.Date;
                                        actualRecordsProviderToInsert.Add(GetNextEntregaDate(currentProvider, candidateProductoPedir));
                                        break;
                                    }
                                }
                                else if (itemDay.Value == "DOMINGO")
                                {
                                    if (currentProvider.Visita_Domingo)
                                    {
                                        candidateProductoPedir.DiaVisita = itemDay.Value;
                                        candidateProductoPedir.FechaVisita = itemDay.Date;    
                                        actualRecordsProviderToInsert.Add(GetNextEntregaDate(currentProvider, candidateProductoPedir));
                                        break;
                                    }
                                }                                
                            }
                        }
                        else
                        {
                            itemsZero.Add(item);
                        }
                    }
                }
                if(itemsZero.Count > 0)
                {
                    DateTime today = DateTime.Today;
                    // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
                    int daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7;
                    DateTime nextSaturday = today.AddDays(daysUntilSaturday);

                    items.ForEach(x =>
                    {
                        x.DiaEntrega = "SABADO";
                        x.FechaEntrega = new DateTime(nextSaturday.Year, nextSaturday.Month, nextSaturday.Day, 0,0,0);
                        x.DiaVisita = "SABADO";
                        x.FechaRegistro = DateTime.Now;
                        x.FechaVisita = new DateTime(nextSaturday.Year, nextSaturday.Month, nextSaturday.Day, 0, 0, 0);
                        x.IdUsuarioRegistro = idUsuarioRegistro;
                    });
                }               
                domainCtx.ProductosAPedirProyectoUsta.AddRange(actualRecordsProviderToInsert);
                domainCtx.ProductosAPedirProyectoUsta.AddRange(itemsZero);
                domainCtx.SaveChanges();
                rp.Success = true;
                rp.MessageOk.Add("Producto(s) programado para la VISITA");
            }
            catch (Exception ex)
            {
                rp.Success = false;
                rp.MessageBad.Add(ex.ToString());                
            }
            return rp;
        }

        /// <summary>
        /// Se agrega un registro al buffer que va para el inventario y el producto se ha seleccionado desde la caja de busqueda de CB o nombre
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="idProveedor"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public ResponseBasicVm AddProductoABufferInventario(int idProducto, int idProveedor, DateTime fechaReferencia, int idUsuario)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var dateReference = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                var infoProducto = domainCtx.ProductosProyectoUsta.Where(x => x.Id == idProducto).FirstOrDefault();
                var infoProviderProduct = domainCtx.ProductoProveedorProyectoUsta.Where(x => x.IdProducto == idProducto && x.IdProveedor == idProveedor).FirstOrDefault();
                var productoABufferInventario = new ProductosBufferAInventarioProyectoUsta()
                {
                    FechaReferencia = fechaReferencia,
                    FechaRegistro = DateTime.Now,
                    IdProducto = idProducto,
                    IdProveedor = (short)idProveedor,
                    IdUsuarioRegistro = idUsuario,
                    QtyPedida = 0,
                    QtyRealAIngresar = 0,
                    CostoItem =  infoProviderProduct == null ? 0 : Convert.ToInt32(infoProviderProduct.PrecioCompra),
                    PrecioAsignado = infoProducto.Precio                    
                };                

                domainCtx.ProductosBufferAInventarioProyectoUsta.Add(productoABufferInventario);                
                domainCtx.SaveChanges();
                rp.Success = true;
                rp.MessageOk.Add("Registro Ingresado");                
            }
            catch (Exception e)
            {
                rp.Success = false;
                rp.MessageBad.Add(e.ToString());
            }
            return rp;
        }

        public ResponseBasicVm AddLogPedidoRecibido(int idProveedor, string nombreProveedor, int valor, int idUsuario)
        {
            var rp = new ResponseBasicVm();
            try
            {
                nombreProveedor = nombreProveedor.Replace("NO RECIBIDO", string.Empty).Trim().ToUpper();
                var dateReference = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                domainCtx.LogPedidosProyectoUsta.Add(new LogPedidosProyectoUsta()
                {
                    FechaReferencia = dateReference,
                    FechaRegistro = DateTime.Now,
                    Observaciones =  nombreProveedor,
                    IdProveedor = (short)idProveedor,
                    IdUsuarioRegistro = idUsuario,
                    ValorPedido = valor
                });

                //Recuperamos productos del pedido y los movemos a tabla buffer para ser procesados e ingresados a inventario
                var recordsToBufferTable = (from productosAPedir in domainCtx.ProductosAPedirProyectoUsta   
                                            join productos in domainCtx.ProductosProyectoUsta on productosAPedir.IdProducto equals productos.Id                                         
                                            where productosAPedir.FechaEntrega == dateReference &&
                                            productosAPedir.IdProveedor == idProveedor &&
                                            productosAPedir.Confirmado == true
                                            select new
                                            {
                                                productosAPedir.IdRecord,
                                                productosAPedir.IdProducto,
                                                productosAPedir.IdProveedor,
                                                productosAPedir.Qty,
                                                productos.Precio,
                                                CostoItem = domainCtx.ProductoProveedorProyectoUsta.Where(x => x.IdProducto == productos.Id && x.IdProveedor == productosAPedir.IdProveedor).FirstOrDefault().PrecioCompra == 0 ? productos.Precio : domainCtx.ProductoProveedorProyectoUsta.Where(x => x.IdProducto == productos.Id && x.IdProveedor == productosAPedir.IdProveedor).FirstOrDefault().PrecioCompra
                                            }).ToList();

                if(recordsToBufferTable.Count > 0)
                {
                    //removemos los productos de la tabla productos a pedir, para no generar ruido en el estatus de los colores 
                    //y pasamos esos registros a la tabla bufferAInventario para facilitar su ingreso al sistema

                    var idRecordsToDelete = recordsToBufferTable.Select(x => x.IdRecord).ToList();
                    var recordsToDelete = domainCtx.ProductosAPedirProyectoUsta.Where(x => idRecordsToDelete.Contains(x.IdRecord)).ToList();
                    domainCtx.ProductosAPedirProyectoUsta.RemoveRange(recordsToDelete);

                    var lstProductosBuffer = new List<ProductosBufferAInventarioProyectoUsta>();
                    recordsToBufferTable.ForEach(x =>
                    {
                        lstProductosBuffer.Add(new ProductosBufferAInventarioProyectoUsta()
                        {
                            CostoItem = (int)Math.Ceiling(x.CostoItem),
                            FechaRegistro = DateTime.Now,
                            IdProducto = x.IdProducto,
                            IdProveedor = x.IdProveedor,
                            IdUsuarioRegistro = idUsuario,
                            PrecioAsignado = x.Precio,
                            QtyPedida = x.Qty,
                            QtyRealAIngresar = x.Qty,
                            FechaReferencia = dateReference
                        });
                    });
                    domainCtx.ProductosBufferAInventarioProyectoUsta.AddRange(lstProductosBuffer);
                }

                //se registra el valor para afectar la caja
                if(valor > 0)
                {
                    cajaBl = new CajaBl();
                    var rpCaja = cajaBl.RegistrarMovimiento(new BasicIntVm()
                    {
                        Id = 8,
                        Value = valor
                    });

                    rp.MessageBad.AddRange(rpCaja.MessageBad);
                    rp.MessageOk.AddRange(rpCaja.MessageOk);
                }
                
                domainCtx.SaveChanges();
                rp.Success = true;
                rp.MessageOk.Add("Registro Ingresado");
                var htmlBodyMsg = "Se ha registrado el recibo del pedido del proveedor: " + nombreProveedor + " por valor de: $" + valor + " a la hora: " + DateTime.Now.ToString();
                Logging.NotifyMovementGeneric(htmlBodyMsg);
            }
            catch (Exception e)
            {
                rp.Success = false;
                rp.MessageBad.Add(e.ToString());  
            }
            return rp;
        }
        #endregion

        #region [UPDATE]
        /// <summary>
        /// BasicIntVm structure
        /// Id = IdRecord
        /// Value = Qty        
        /// </summary>
        /// <param name="recordToUpdate"></param>
        /// <returns></returns>
        public ResponseBasicVm UpdateCantidadProductoAPedir(BasicIntVm recordToUpdate, int idUsuario)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var actualRecord = domainCtx.ProductosAPedirProyectoUsta.Where(x => x.IdRecord == recordToUpdate.Id).FirstOrDefault();
                if(actualRecord != null)
                {
                    actualRecord.Qty = (short)recordToUpdate.Value;
                    actualRecord.IdUsuarioModificador = idUsuario;
                    actualRecord.FechaModificado = DateTime.Now;
                    domainCtx.SaveChanges();
                    rp.Success = true;
                    rp.MessageOk.Add("Cantidad actualizada");
                }
                else
                {
                    rp.Success = false;
                    rp.MessageBad.Add("El item no se encontro en BD, no se ha actualizado");
                }
            }
            catch (Exception ex)
            {
                rp.Success = false;
                rp.MessageBad.Add(ex.ToString());
            }
            return rp;
        }
        
        /// <summary>
        /// Id = IdRecord
        /// State = Valor del estado (falso(No confirmado),true(Confirmado))        
        /// </summary>
        /// <param name="candidates"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public ResponseBasicVm UpdateStatusConfirmadoProductosAPedir(List<BasicBooleanVm> candidates, int idUsuario, bool confirmPv)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var flagProceso = false;
                var idsCandidates = candidates.Select(x => x.Id).ToList();
                var recordsNoConfirmed = new List<ProductosAPedirProyectoUsta>();
                var actualRecords = domainCtx.ProductosAPedirProyectoUsta.Where(x => idsCandidates.Contains(x.IdRecord)).ToList();                                
                actualRecords.ForEach(x =>
                {
                    x.Confirmado = candidates.Where(y => y.Id == x.IdRecord).FirstOrDefault().State;
                    x.FechaModificado = DateTime.Now;
                    x.IdUsuarioModificador = idUsuario;
                    flagProceso = true;
                });
                recordsNoConfirmed = actualRecords.Where(x => x.Confirmado == false).ToList();
                if (flagProceso)
                {
                    domainCtx.SaveChanges();
                    rp.Success = true;
                    rp.MessageOk.Add("Registros actualizados");
                    //Informamos por correo del posible faltante
                    if(recordsNoConfirmed.Count > 0 && confirmPv)
                    {
                        var idProvider = recordsNoConfirmed.Select(x => x.IdProveedor).FirstOrDefault();
                        var idsProductos = recordsNoConfirmed.Select(x => x.IdProducto).Distinct().ToList();
                        var nameProvider = domainCtx.ProveedoresProyectoUsta.Where(x => x.Id == idProvider).Select(x => x.Nombre).FirstOrDefault();
                        var namesProductos = domainCtx.ProductosProyectoUsta.Where(x => idsProductos.Contains(x.Id)).Select(x => new { x.Id, x.Nombre }).ToList();
                        var modelExpectedMail = new List<BasicVm>();
                        recordsNoConfirmed.ForEach(x =>
                        {
                            modelExpectedMail.Add(new BasicVm() { Id = x.Qty, Value = namesProductos.Where(y => y.Id == x.IdProducto).Select(z => z.Nombre).FirstOrDefault() });
                        });
                        Logging.NotifyMovementConfirmLeft(nameProvider, modelExpectedMail);
                    }
                }
                else
                {
                    rp.Success = false;
                    rp.MessageBad.Add("No se encontraron registros en BD correspondientes");
                }
            }
            catch (Exception ex)
            {
                rp.Success = false;
                rp.MessageBad.Add(ex.ToString());
            }
            return rp;
        }

        /// <summary>
        /// Actualiza el valor del campo de la tabla buffer. Se hace de esta manera para poder aprovechar el procesamiento en el cliente
        /// </summary>
        /// <param name="idCampo"></param>
        /// <param name="valor"></param>
        /// <param name="idRecord"></param>
        /// <returns></returns>
        public ResponseBasicVm UpdateRecordProductoBufferAInventario(int idCampo, string valor, int idRecord, int idUsuario)
        {
            var rp = new ResponseBasicVm();
            try
            {
                ProductosProyectoUsta recordProducto = null;
                ProductosBufferAInventarioProyectoUsta actualBufferRecord = domainCtx.ProductosBufferAInventarioProyectoUsta.Where(x => x.Id == idRecord).FirstOrDefault();
                
                if (idCampo == 4)
                {
                    recordProducto = domainCtx.ProductosProyectoUsta.Where(x => x.Id == actualBufferRecord.IdProducto).FirstOrDefault();
                }
                               
                if(actualBufferRecord != null)
                {
                    switch(idCampo)
                    {
                        case 1:
                            actualBufferRecord.QtyRealAIngresar = Convert.ToInt16(valor);
                            break;
                        case 2:
                            actualBufferRecord.CostoItem = Convert.ToInt32(valor);
                            break;
                        case 3:
                            actualBufferRecord.PrecioAsignado = Convert.ToInt32(valor);
                            break;
                        case 4:
                            if(recordProducto != null)
                            {
                                recordProducto.Nombre = valor.Trim().ToUpper();
                            }
                            break;
                    }
                    actualBufferRecord.IdUsuarioModificado = idUsuario;
                    actualBufferRecord.FechaModificado = DateTime.Now;
                    domainCtx.SaveChanges();
                    rp.Success = true;
                    rp.MessageOk.Add("Registros actualizados");
                }
                else
                {
                    rp.Success = false;
                    rp.MessageBad.Add("No se encontraron registros en BD correspondientes");
                }                
            }
            catch (Exception ex)
            {
                rp.Success = false;
                rp.MessageBad.Add(ex.ToString());
            }
            return rp;
        }

        public ResponseBasicVm ProcesarRecordsBufferAinventario(List<int> idRecords, int idUsuario)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var actualBufferRecords = domainCtx.ProductosBufferAInventarioProyectoUsta.Where(x => idRecords.Contains(x.Id)).ToList();                
                if (actualBufferRecords.Count > 0)
                {
                    var idProvider = actualBufferRecords.FirstOrDefault().IdProveedor;
                    var idProducts = actualBufferRecords.Select(x => x.IdProducto).ToList();
                    //var recordsProviderProduct = domainCtx.ProductoProveedorProyectoUsta.Where(x => idProducts.Contains(x.IdProducto)).ToList().Where(x => x.IdProveedor == idProvider).ToList();// && x.IdProveedor == idProvider
                    var recordsProviderProduct = domainCtx.ProductoProveedorProyectoUsta.Where(x => idProducts.Contains(x.IdProducto) && x.IdProveedor == idProvider).ToList();
                    var recordsProduct = domainCtx.ProductosProyectoUsta.Where(x => idProducts.Contains(x.Id)).ToList();
                    //iteración principal para realizar las actualizaciones correspondientes
                    actualBufferRecords.ForEach(x =>
                    {
                        if(recordsProviderProduct.Where(y => y.IdProducto == x.IdProducto).FirstOrDefault() == null)
                        {
                            domainCtx.ProductoProveedorProyectoUsta.Add(new ProductoProveedorProyectoUsta()
                            {
                                IdUsuarioRegistro = idUsuario,
                                FechaRegistro = DateTime.Now,
                                IdProducto = x.IdProducto,
                                IdProveedor =  x.IdProveedor,
                                PrecioCompra = x.CostoItem
                            });                           
                        }
                        else
                        {
                            recordsProviderProduct.Where(y => y.IdProducto == x.IdProducto).FirstOrDefault().PrecioCompra = x.CostoItem;
                            recordsProviderProduct.Where(y => y.IdProducto == x.IdProducto).FirstOrDefault().FechaModificado = DateTime.Now;
                            recordsProviderProduct.Where(y => y.IdProducto == x.IdProducto).FirstOrDefault().IdUsuarioModificador = idUsuario;                            
                        }
                        recordsProduct.Where(y => y.Id == x.IdProducto).FirstOrDefault().CantidadActual += x.QtyRealAIngresar;
                        recordsProduct.Where(y => y.Id == x.IdProducto).FirstOrDefault().Precio = x.PrecioAsignado;
                    });
                    domainCtx.ProductosBufferAInventarioProyectoUsta.RemoveRange(actualBufferRecords);
                    domainCtx.SaveChanges();
                    rp.Success = true;
                    rp.MessageOk.Add("Inventario actualizado");
                }
                else
                {
                    rp.Success = false;
                    rp.MessageBad.Add("No se encontraron registros en BD correspondientes");
                }
            }
            catch (Exception ex)
            {
                rp.Success = false;
                rp.MessageBad.Add(ex.ToString());
            }
            return rp;
        }


        /// <summary>
        /// Se sobreescribe la cantidad del producto mediante un inventario realizado
        /// El tipo de la colección que se envia corresponde a:
        /// IdParent => El id del producto en cuestión
        /// Id => La cantidad de unidades leidas
        /// Value => El id de la ubicación donde se realiza el inventario
        /// </summary>
        /// <param name="recordsToProcess">La colección de datos a procesar. Revisar la descripción del metodo</param>
        /// <param name="idUsuario">id del usuario que realiza el proceso</param>
        /// <returns></returns>
        public ResponseBasicVm UpdateQtyProductosPorInventario(List<BasicDependantIntVm> recordsToProcess, int idUsuario)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var idProducts = recordsToProcess.Select(x => x.IdParent).ToList();
                var actualRecords = domainCtx.ProductosProyectoUsta.Where(x => idProducts.Contains(x.Id)).ToList();
                if(actualRecords.Count > 0)
                {
                    actualRecords.ForEach(x =>
                    {
                        x.CantidadActual = (short)recordsToProcess.Where(y => y.IdParent == x.Id).FirstOrDefault().Id;
                        x.IdUbicacionNegocio = (short)recordsToProcess.Where(y => y.IdParent == x.Id).FirstOrDefault().Value;
                    });
                    domainCtx.SaveChanges();
                    rp.Success = true;
                    rp.MessageOk.Add("Inventario actualizado");
                }
                else
                {
                    rp.Success = false;
                    rp.MessageBad.Add("No existen registros en base de datos correspondientes a los enviados!");
                }                             
            }
            catch (Exception ex)
            {
                rp.Success = false;
                rp.MessageBad.Add(ex.ToString());
            }
            return rp;
        }

        #endregion

        #region [DELETE]
        public ResponseBasicVm DeleteOldProductosAPedir()
        {
            var rp = new ResponseBasicVm();
            try
            {
                var dateReference = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,0,0,0);
                dateReference = dateReference.AddDays(-15);
                var allRecords = domainCtx.ProductosAPedirProyectoUsta.Where(x => x.FechaEntrega < dateReference).ToList();
                if(allRecords.Count > 0)
                {
                    domainCtx.ProductosAPedirProyectoUsta.RemoveRange(allRecords);
                    domainCtx.SaveChanges();
                    rp.Success = true;
                    rp.MessageOk.Add("Productos a pedir antiguos eliminados");
                }
                else
                {
                    rp.Success = false;
                    rp.MessageBad.Add("No hay productos a pedir antiguos a eliminar");
                }
            }
            catch(Exception ex)
            {
                rp.Success = false;
                rp.MessageBad.Add(ex.ToString());
            }
            return rp;
        }

        /// <summary>
        /// Int value => idRecord 
        /// </summary>
        /// <param name="lstCandidates"></param>
        /// <returns></returns>
        public ResponseBasicVm DeleteProductosAPedir(List<int> candidates)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var actualRecords = domainCtx.ProductosAPedirProyectoUsta.Where(x => candidates.Contains(x.IdRecord)).ToList();
                if(actualRecords.Count > 0)
                {
                    domainCtx.ProductosAPedirProyectoUsta.RemoveRange(actualRecords);
                    domainCtx.SaveChanges();
                    rp.Success = true;
                    rp.MessageOk.Add("Registros eliminados");
                }
                else
                {
                    rp.Success = false;
                    rp.MessageBad.Add("No se encontraron registros coincidentes, no se han eliminado datos");
                }                
            }
            catch (Exception ex)
            {
                rp.Success = false;
                rp.MessageBad.Add(ex.ToString());
            }
            return rp;
        }

        /// <summary>
        /// Elimina productos que se encuentran en tabla buffer para ser ingresados al sistema
        /// </summary>
        /// <param name="idRecords"></param>
        /// <returns></returns>
        public ResponseBasicVm DeleteProductosBufferInventario(List<int> idRecords)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var actualRecords = domainCtx.ProductosBufferAInventarioProyectoUsta.Where(x => idRecords.Contains(x.Id)).ToList();
                if (actualRecords.Count > 0)
                {
                    domainCtx.ProductosBufferAInventarioProyectoUsta.RemoveRange(actualRecords);
                    domainCtx.SaveChanges();
                    rp.Success = true;
                    rp.MessageOk.Add("Registros eliminados");
                }
                else
                {
                    rp.Success = false;
                    rp.MessageBad.Add("No se encontraron registros coincidentes, no se han eliminado datos");
                }
            }
            catch (Exception ex)
            {
                rp.Success = false;
                rp.MessageBad.Add(ex.ToString());
            }
            return rp;
        }
        #endregion

        #region [CHECK]
        #endregion
    }
}
