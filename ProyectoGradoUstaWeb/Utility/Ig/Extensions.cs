using Infragistics.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGradoUstaWeb
{
    /// <summary>
    /// Extensiones basicas para controles Ig complejos
    /// Opciones
    /// Crud
    /// Crud-selection
    /// Crud-selection-Summarizes
    /// 
    /// 
    /// NoCrud
    /// NoCrud-selection
    /// NoCrud-selection-sumarizes
    /// </summary>
    public static class Extensions
    {
        #region [BASIC]
        private static GridModel BasicConfiguration(GridModel gm, int pageSize)
        {
            gm.Width = "100%";
            gm.AutoGenerateColumns = false;
            gm.AutoCommit = false;
            gm.Columns = new List<GridColumn>();

            var ftPaging = new GridPaging();
            ftPaging.PageSize = pageSize;
            ftPaging.Type = OpType.Remote;
            ftPaging.Inherit = false;
            //ftPaging.PagerRecordsLabelTemplate = "${startRecord} - ${endRecord} de ${recordCount} registros";            
            //ftPaging.FirstPageTooltip = "Primera pagina";
            //ftPaging.LastPageTooltip = "Ultima pagina";
            //ftPaging.PrevPageTooltip = "Pagina anterior";
            //ftPaging.NextPageTooltip = "Siguiente pagina";
            //ftPaging.PagerRecordsLabelTooltip = "Rango de registros";
            //ftPaging.FirstPageLabelText = "Primera pagina";
            //ftPaging.LastPageLabelText = "Ultima pagina";
            //ftPaging.NextPageLabelText = "Sig";
            ftPaging.PrevPageLabelText = "Ant";
            //ftPaging.PageSizeDropDownLabel = "Muestra";
 

            var ftFiltering = new GridFiltering();
            ftFiltering.Inherit = false;
            ftFiltering.Type = OpType.Remote;
            //ftFiltering.Labels = new Infragistics.Web.Mvc.Grid.Filtering.ColumnFilteringLabels()
            //{
            //    After = "Despues",
            //    Before = "Antes",
            //    Clear = "Limpiar",
            //    Contains = "Contiene",
            //    DoesNotContain = "No contiene",
            //    DoesNotEqual = "No es igual a",
            //    Empty = "Vacio",
            //    Equals = "Igual a",
            //    GreaterThan = "Mayor que",
            //    NextMonth = "Siguiente mes",
            //    ThisYear = "Este año",
            //    NextYear = "Siguiente año",
            //    LastYear = "Año pasado",
            //    Today = "Hoy",
            //    ThisMonth = "Este mes",
            //    StartsWith = "Inicia con",
            //    EndsWith = "Finaliza con",
            //    Yesterday = "Ayer",
            //    LastMonth = "Mes pasado",
            //    LessThanOrEqualTo = "Menor o igual que",
            //    GreaterThanOrEqualTo = "Mayor o igual que",
            //    LessThan = "Menor que",
            //    On = "En",
            //    NotOn = "Que no sea en",
            //    True = "Verdadero",
            //    False = "Falso",
            //    NotEmpty = "No vacio",
            //    NotNull = "No nulo"
                                             
            //};



            var ftSorting = new GridSorting();
            ftSorting.Inherit = false;
            ftSorting.Type = OpType.Remote;
            ftSorting.SortedColumnTooltip = "Ordenada";
            //ftSorting.UnsortedColumnTooltip = "No ordenada";


            //var ftResizing = new GridResizing();
            //ftResizing.DeferredResizing = false;
            //ftResizing.AllowDoubleClickToResize = true;

            var ftHeaders = new GridMultiColumnHeaders();

            gm.Features.Add(ftPaging);
            gm.Features.Add(ftFiltering);
            gm.Features.Add(ftSorting);
            //gm.Features.Add(ftResizing);
            gm.Features.Add(ftHeaders);
            return gm;
        }

        private static GridColumnLayoutModel BasicConfiguration(GridColumnLayoutModel glm, int pageSize)
        {
            glm.Width = "100%";
            glm.AutoGenerateColumns = false;
            glm.AutoGenerateLayouts = false;
            glm.LoadOnDemand = true;
            glm.AutoCommit = true;
            glm.Columns = new List<GridColumn>();

            var ftPaging = new GridPaging();
            ftPaging.Type = OpType.Remote;
            ftPaging.Inherit = false;
            ftPaging.PageSize = 5;
            //ftPaging.PagerRecordsLabelTemplate = "${startRecord} - ${endRecord} de ${recordCount} registros";
            //ftPaging.FirstPageTooltip = "Primera pagina";
            //ftPaging.LastPageTooltip = "Ultima pagina";
            //ftPaging.PrevPageTooltip = "Pagina anterior";
            //ftPaging.NextPageTooltip = "Siguiente pagina";
            //ftPaging.PagerRecordsLabelTooltip = "Rango de registros";
            //ftPaging.FirstPageLabelText = "Primera pagina";
            //ftPaging.LastPageLabelText = "Ultima pagina";
            //ftPaging.NextPageLabelText = "Sig";
            ftPaging.PrevPageLabelText = "Ant";
            //ftPaging.PageSizeDropDownLabel = "Muestra";

            var ftFiltering = new GridFiltering();
            ftFiltering.Inherit = false;
            ftFiltering.Type = OpType.Remote;

            var ftSorting = new GridSorting();
            ftSorting.Inherit = false;
            ftSorting.Type = OpType.Remote;
            glm.Features.Add(ftSorting);

            var ftHeaders = new GridMultiColumnHeaders();
            glm.Features.Add(ftPaging);
            glm.Features.Add(ftFiltering);
            glm.Features.Add(ftSorting);
            glm.Features.Add(ftHeaders);

            return glm;            
        }

        private static TreeGridModel BasicConfiguration(TreeGridModel tgm, int pageSize)
        {
            tgm.Width = "100%";
            tgm.AutoGenerateColumns = false;
            tgm.InitialExpandDepth = 0;

            var ftPaging = new GridPaging();
            ftPaging.PageSize = pageSize;
            ftPaging.Type = OpType.Remote;
            ftPaging.Inherit = false;
            //ftPaging.PagerRecordsLabelTemplate = "${startRecord} - ${endRecord} de ${recordCount} registros";
            //ftPaging.FirstPageTooltip = "Primera pagina";
            //ftPaging.LastPageTooltip = "Ultima pagina";
            //ftPaging.PrevPageTooltip = "Pagina anterior";
            //ftPaging.NextPageTooltip = "Siguiente pagina";
            //ftPaging.PagerRecordsLabelTooltip = "Rango de registros";
            //ftPaging.FirstPageLabelText = "Primera pagina";
            //ftPaging.LastPageLabelText = "Ultima pagina";
            //ftPaging.NextPageLabelText = "Sig";
            ftPaging.PrevPageLabelText = "Ant";
            //ftPaging.PageSizeDropDownLabel = "Muestra";

            var ftFiltering = new GridFiltering();
            ftFiltering.Inherit = false;
            ftFiltering.Type = OpType.Remote;

            var ftSorting = new GridSorting();
            ftSorting.Inherit = false;
            ftSorting.Type = OpType.Remote;

            //var ftResizing = new GridResizing();
            //ftResizing.DeferredResizing = false;
            //ftResizing.AllowDoubleClickToResize = true;

            tgm.Features.Add(ftPaging);
            tgm.Features.Add(ftFiltering);
            tgm.Features.Add(ftSorting);
            //tgm.Features.Add(ftResizing);

            return tgm;
        }
        #endregion


        private static TreeGridModel AddSelection(TreeGridModel tgm)
        {
            var ftSelection = new GridSelection();
            ftSelection.Mode = SelectionMode.Row;
            ftSelection.MultipleSelection = false;

            var ftRowSelector = new GridRowSelectors();
            ftRowSelector.EnableRowNumbering = false;
            ftRowSelector.EnableCheckBoxes = true;

            tgm.Features.Add(ftSelection);
            tgm.Features.Add(ftRowSelector);
            return tgm;
        }
       
        public static TreeGridModel SetBasicNoSelection(this TreeGridModel tgm, int pageSize = 10)
        {
            return BasicConfiguration(tgm, pageSize);
        }

        public static TreeGridModel SetBasicAll(this TreeGridModel tgm, int pageSize = 10)
        {
            var thisTgm = BasicConfiguration(tgm, pageSize);
            return AddSelection(thisTgm);
        }

        public static GridModel SetBasicNoSelection(this GridModel gm, int pageSize = 10)
        {
            return BasicConfiguration(gm, pageSize);
        }

        public static GridModel SetBasicAll(this GridModel gm, int pageSize = 10)
        {
            var thisGm = BasicConfiguration(gm, pageSize);
            return AddSelection(thisGm);
        }

        private static GridModel AddSelection(GridModel gm)
        {
            var ftSelection = new GridSelection();
            ftSelection.Mode = SelectionMode.Row;
            ftSelection.MultipleSelection = false;

            var ftRowSelector = new GridRowSelectors();
            ftRowSelector.EnableRowNumbering = false;
            ftRowSelector.EnableCheckBoxes = true;
            ftRowSelector.EnableSelectAllForPaging = false;

            gm.Features.Add(ftSelection);
            gm.Features.Add(ftRowSelector);
            return gm;
        }

        /*Nuevos */


        #region [GRIDCOLUMNLAYOUTMODEL]
        public static GridColumnLayoutModel NoCrud(this GridColumnLayoutModel glm, int pageSize = 10)
        {
            return BasicConfiguration(glm, pageSize);
        }
        #endregion

        #region [GRIDMODEL]
        public static GridModel Crud(this GridModel gm, int pageSize = 10)
        {
            gm = BasicConfiguration(gm, pageSize);

            var ftUpdating = new GridUpdating();
            ftUpdating.EnableAddRow = true;
            ftUpdating.EnableDeleteRow = true;
            ftUpdating.EditMode = GridEditMode.Row;
            //ftUpdating.AddRowLabel = "Agregar nuevo registro";
            //ftUpdating.CancelLabel = "Cancelar";
            //ftUpdating.DeleteRowTooltip = "Borrar registro";
            //ftUpdating.CancelTooltip = "Cancelar";
            //ftUpdating.DoneLabel = "OK";
            //ftUpdating.DeleteRowLabel = "Borrar";
            gm.Features.Add(ftUpdating);
            return gm;
        }

        public static GridModel Crud_Summaries(this GridModel gm, int pageSize = 10)
        {
            gm = BasicConfiguration(gm, pageSize);

            var ftUpdating = new GridUpdating();
            ftUpdating.EnableAddRow = true;
            ftUpdating.EnableDeleteRow = true;
            ftUpdating.EditMode = GridEditMode.Row;

            var ftSummaries = new GridSummaries();
           

            gm.Features.Add(ftSummaries);
            gm.Features.Add(ftUpdating);
            return gm;
        }

        public static GridModel Crud_Selection(this GridModel gm, int pageSize = 10, bool multipleSelection = false, bool enableAllSelection = false)
        {
            gm = Crud(gm, pageSize);
            
            var ftSelection = new GridSelection();
            ftSelection.Mode = SelectionMode.Row;            
            ftSelection.MultipleSelection = multipleSelection;          
               
            var ftRowSelector = new GridRowSelectors();            
            ftRowSelector.EnableRowNumbering = false;
            ftRowSelector.EnableCheckBoxes = true;
            ftRowSelector.EnableSelectAllForPaging = enableAllSelection;

            gm.Features.Add(ftSelection);
            gm.Features.Add(ftRowSelector);
            return gm;
        }

        public static GridModel Crud_Selection_Summaries(this GridModel gm, int pageSize = 10, bool multipleSelection = false, bool enableAllSelection = false)
        {
            gm = Crud_Selection(gm, pageSize, multipleSelection, enableAllSelection);
            var ftSummaries = new GridSummaries();
            gm.Features.Add(ftSummaries);
            return gm;
        }

        public static GridModel NoCrud(this GridModel gm, int pageSize=10)
        {
            return BasicConfiguration(gm, pageSize);
        }

        public static GridModel NoCrud_Summaries(this GridModel gm, int pageSize = 10)
        {
            var ftSummaries = new GridSummaries();
            gm.Features.Add(ftSummaries);            
            return BasicConfiguration(gm, pageSize);
        }

        public static GridModel NoCrud_Selection(this GridModel gm, int pageSize = 10, bool multipleSelection = false, bool enableAllSelection = false)
        {
            gm = NoCrud(gm, pageSize);
            var ftSelection = new GridSelection();                       
            ftSelection.Mode = SelectionMode.Row;
            ftSelection.MultipleSelection = multipleSelection;

            var ftRowSelector = new GridRowSelectors();
            ftRowSelector.EnableRowNumbering = false;
            ftRowSelector.EnableCheckBoxes = true;
            ftRowSelector.EnableSelectAllForPaging = enableAllSelection;          

            gm.Features.Add(ftSelection);
            gm.Features.Add(ftRowSelector);
            return gm;
        }

        public static GridModel NoCrud_Selection_Summaries(this GridModel gm, int pageSize = 10)
        {
            gm = NoCrud_Selection(gm, pageSize);
            var ftSummaries = new GridSummaries();
            gm.Features.Add(ftSummaries);
            return gm;
        }

        public static ComboModel SetBasicOptions(this ComboModel cm)
        {
            cm.DropDownButtonTitle = "Despliega datos";
            cm.ClearButtonTitle = "Limpia texto";
            return cm;
        }
        #endregion



        //ORGANIZAR
        /*
          private static TreeGridModel SetBasicTreeGridModel(TreeGridModel tgm, int pageSize)
        {
            tgm.Width = "100%";            
            tgm.AutoGenerateColumns = false;
            tgm.InitialExpandDepth = 0;


            var ftPaging = new GridPaging();
            ftPaging.PageSize = pageSize;
            ftPaging.Type = OpType.Remote;
            ftPaging.Inherit = false;

            var ftFiltering = new GridFiltering();
            ftFiltering.Inherit = false;
            ftFiltering.Type = OpType.Remote;

            var ftSorting = new GridSorting();
            ftSorting.Inherit = false;
            ftSorting.Type = OpType.Remote;

            var ftResizing = new GridResizing();
            ftResizing.DeferredResizing = false;
            ftResizing.AllowDoubleClickToResize = true;

            var ftSelection = new GridSelection();
            ftSelection.Mode = SelectionMode.Row;
            ftSelection.MultipleSelection = false;

            var ftRowSelector = new GridRowSelectors();
            ftRowSelector.EnableRowNumbering = false;
            ftRowSelector.EnableCheckBoxes = true;

            tgm.Features.Add(ftPaging);
            tgm.Features.Add(ftFiltering);
            tgm.Features.Add(ftSorting);
            tgm.Features.Add(ftResizing);
            tgm.Features.Add(ftSelection);
            tgm.Features.Add(ftRowSelector);

            return tgm;
        }

        private static GridModel SetBasicGridModel(GridModel gm, int pageSize)
        {
            gm.Width = "100%";
            gm.AutoGenerateColumns = false;
            gm.AutoCommit = false;

            var ftPaging = new GridPaging();
            ftPaging.PageSize = pageSize;
            ftPaging.Type = OpType.Remote;            
            ftPaging.Inherit = false;

            var ftFiltering = new GridFiltering();
            ftFiltering.Inherit = false;
            ftFiltering.Type = OpType.Remote;

            var ftSorting = new GridSorting();
            ftSorting.Inherit = false;
            ftSorting.Type = OpType.Remote;           

            var ftResizing = new GridResizing();
            ftResizing.DeferredResizing = false;
            ftResizing.AllowDoubleClickToResize = true;

            var ftSelection = new GridSelection();
            ftSelection.Mode = SelectionMode.Row;
            ftSelection.MultipleSelection = false;

            var ftRowSelector = new GridRowSelectors();
            ftRowSelector.EnableRowNumbering = false;
            ftRowSelector.EnableCheckBoxes = true;
            ftRowSelector.EnableSelectAllForPaging = false;

            gm.Features.Add(ftPaging);
            gm.Features.Add(ftFiltering);
            gm.Features.Add(ftSorting);
            gm.Features.Add(ftResizing);
            gm.Features.Add(ftSelection);
            gm.Features.Add(ftRowSelector);
            return gm;
        }

        private static GridModel SetBasicGmNoSelection(GridModel gm, int pageSize)
        {
            gm.Width = "100%";
            gm.AutoGenerateColumns = false;
            gm.AutoCommit = false;

            var ftPaging = new GridPaging();
            ftPaging.PageSize = pageSize;
            ftPaging.Type = OpType.Remote;
            ftPaging.Inherit = false;

            var ftFiltering = new GridFiltering();
            ftFiltering.Inherit = false;
            ftFiltering.Type = OpType.Remote;

            var ftSorting = new GridSorting();
            ftSorting.Inherit = false;
            ftSorting.Type = OpType.Remote;

            var ftResizing = new GridResizing();
            ftResizing.DeferredResizing = false;
            ftResizing.AllowDoubleClickToResize = true;            

            gm.Features.Add(ftPaging);
            gm.Features.Add(ftFiltering);
            gm.Features.Add(ftSorting);
            gm.Features.Add(ftResizing);           
            return gm;          
        }

        private static GridModel SetBasicGmNoSelectionWithSummaries(GridModel gm, int pageSize)
        {
            gm.Width = "100%";
            gm.AutoGenerateColumns = false;
            gm.AutoCommit = false;

            var ftPaging = new GridPaging();
            ftPaging.PageSize = pageSize;
            ftPaging.Type = OpType.Remote;
            ftPaging.Inherit = false;

            var ftFiltering = new GridFiltering();
            ftFiltering.Inherit = false;
            ftFiltering.Type = OpType.Remote;

            var ftSorting = new GridSorting();
            ftSorting.Inherit = false;
            ftSorting.Type = OpType.Remote;

            var ftResizing = new GridResizing();
            ftResizing.DeferredResizing = false;
            ftResizing.AllowDoubleClickToResize = true;


            var ftSummaries = new GridSummaries();


            gm.Features.Add(ftPaging);
            gm.Features.Add(ftFiltering);
            gm.Features.Add(ftSorting);
            gm.Features.Add(ftResizing);
            gm.Features.Add(ftSummaries);
            return gm;
        }

        public static TreeGridModel SetBasicExtension(this TreeGridModel tgm, int pageSize = 10)
        {
            return SetBasicTreeGridModel(tgm, pageSize);                       
        }

        public static GridModel SetBasicExtension(this GridModel gm, int pageSize = 10)
        {
            return SetBasicGridModel(gm, pageSize);
        }

        public static GridModel SetBasicNoSelection(this GridModel gm, int pageSize = 10)
        {
            return SetBasicGmNoSelection(gm, pageSize);
        }

        public static GridModel SetBasicNoSelectionWithSummaries(this GridModel gm, int pageSize = 10)
        {
            return SetBasicGmNoSelectionWithSummaries(gm, pageSize);
        }

        public static GridModel SetBasicSelectionWithSummaries(this GridModel gm, int pageSize = 10)
        {
            var gmResult = SetBasicGmNoSelectionWithSummaries(gm, pageSize);
            var ftSelection = new GridSelection();
            ftSelection.Mode = SelectionMode.Row;
            ftSelection.MultipleSelection = false;

            var ftRowSelector = new GridRowSelectors();
            ftRowSelector.EnableRowNumbering = false;
            ftRowSelector.EnableCheckBoxes = true;
            ftRowSelector.EnableSelectAllForPaging = false;
            gmResult.Features.Add(ftSelection);
            gmResult.Features.Add(ftRowSelector);
            return gmResult;
        }
         */
    }
}