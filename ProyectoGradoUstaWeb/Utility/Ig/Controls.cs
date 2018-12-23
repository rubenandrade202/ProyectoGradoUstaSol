using Infragistics.Web.Mvc;
using ProyectoGradoUstaBus;
using ProyectoGradoUstaCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGradoUstaWeb
{
    public static class Controls
    {
        #region [FIELDS]
        static GeneralDomainBl gralDomainBl;
        #endregion

        #region [COMBO]
        public static ComboModel GetComboDemo(string id, bool required, bool multi, string width = "172px")
        {
            gralDomainBl = gralDomainBl == null ? new GeneralDomainBl() : gralDomainBl;
            EditorValidatorOptions objValidator = (required == true) ? new EditorValidatorOptions() { Required = new ValidatorRule() { ErrorMessage = "Required" } } : null;
            ComboMultiSelectionSettings objMulti = new ComboMultiSelectionSettings()
            {
                ShowCheckBoxes = true,
                Enabled = multi,
                ItemSeparator = ";"
            };
            var cmb = new ComboModel()
            {  
                DataSource = gralDomainBl.GetPruebaBorrar(),
                MultiSelectionSettings = objMulti,
                ID = id,
                TextKey = "Value",
                Width = width,
                DropDownWidth = 300,
                ValueKey = "Id",
                ValidatorOptions = objValidator
            }.SetBasicOptions();
            return cmb;
        }

        public static ComboModel GetComboDefault(string id, bool required, bool multi, string width = "172px")
        {
            EditorValidatorOptions objValidator = (required == true) ? new EditorValidatorOptions() { Required = new ValidatorRule() { ErrorMessage = "Required" } } : null;
            ComboMultiSelectionSettings objMulti = new ComboMultiSelectionSettings()
            {
                ShowCheckBoxes = true,
                Enabled = multi,
                ItemSeparator = ";"
            };
            var cmb = new ComboModel()
            {
                Disabled = true,
                MultiSelectionSettings = objMulti,
                ID = id,
                TextKey = "Value",
                Width = width,
                DropDownWidth = 300,
                ValueKey = "Id",
                ValidatorOptions = objValidator
            };
            return cmb;
        }

        public static ComboModel CmbRemoteGeneric(string id, bool required, bool multi, string handler, string width = "172px")
        {
            EditorValidatorOptions objValidator = (required == true) ? new EditorValidatorOptions()
            {
                Required = new ValidatorRule("Required")
            }
            : null;

            ComboMultiSelectionSettings objMulti = new ComboMultiSelectionSettings()
            {
                ShowCheckBoxes = true,
                Enabled = multi,
                ItemSeparator = ";"
            };
            var cmb = new ComboModel()
            {                          
                //MultiSelectionSettings = null,//objMulti,
                FilteringType = ComboFilteringType.Remote,
                HighlightMatchesMode = ComboHighlightMatchesMode.Contains,
                FilteringLogic = ComboFilteringLogic.Or,
                FilterExprUrlKey = "filter",
                CaseSensitive = false,                
                DataSourceUrl = handler,
                FilteringCondition = ComboFilteringCondition.Contains,
                AutoSelectFirstMatch = false,
                ID = id,
                Height = "25px",
                AutoComplete = true,
                TextKey = "Value",
                Width = width,
                DropDownWidth = 400,
                ValueKey = "Id"//,
                //ValidatorOptions = objValidator
            };
            return cmb;
        }

        public static ComboModel CmbTiposUbicacion(string id, bool required, bool multi, string width = "172px")
        {
            gralDomainBl = gralDomainBl == null ? new GeneralDomainBl() : gralDomainBl;
            EditorValidatorOptions objValidator = (required == true) ? new EditorValidatorOptions() { Required = new ValidatorRule() { ErrorMessage = "Required" } } : null;
            ComboMultiSelectionSettings objMulti = new ComboMultiSelectionSettings()
            {
                ShowCheckBoxes = true,
                Enabled = multi,
                ItemSeparator = ";"
            };
            var cmb = new ComboModel()
            {
                MultiSelectionSettings = objMulti,
                ID = id,
                DataSource = gralDomainBl.GetTiposUbicacion(),
                TextKey = "Value",
                Width = width,
                DropDownWidth = 300,
                ValueKey = "Id",
                ValidatorOptions = objValidator
            };
            return cmb;
        }

        public static ComboModel CmbUbicacionesStock(string id, bool required, bool multi, string width = "172px")
        {
            gralDomainBl = gralDomainBl == null ? new GeneralDomainBl() : gralDomainBl;
            EditorValidatorOptions objValidator = (required == true) ? new EditorValidatorOptions() { Required = new ValidatorRule() { ErrorMessage = "Required" } } : null;
            ComboMultiSelectionSettings objMulti = new ComboMultiSelectionSettings()
            {
                ShowCheckBoxes = true,
                Enabled = multi,
                ItemSeparator = ";"
            };
            var cmb = new ComboModel()
            {
                MultiSelectionSettings = objMulti,
                ID = id,
                DataSource = gralDomainBl.GetUbicacionesStock(),
                TextKey = "Value",
                Width = width,
                DropDownWidth = 300,
                ValueKey = "Id",
                ValidatorOptions = objValidator
            };
            return cmb;
        }

        public static ComboModel CmbUbicacionesNegocio(string id, bool required, bool multi, string width = "172px")
        {
            gralDomainBl = gralDomainBl == null ? new GeneralDomainBl() : gralDomainBl;
            EditorValidatorOptions objValidator = (required == true) ? new EditorValidatorOptions() { Required = new ValidatorRule() { ErrorMessage = "Required" } } : null;
            ComboMultiSelectionSettings objMulti = new ComboMultiSelectionSettings()
            {
                ShowCheckBoxes = true,
                Enabled = multi,
                ItemSeparator = ";"
            };
            var cmb = new ComboModel()
            {
                MultiSelectionSettings = objMulti,
                ID = id,
                DataSource = gralDomainBl.GetUbicacionesNegocio(),
                TextKey = "Value",
                Width = width,
                DropDownWidth = 300,
                ValueKey = "Id",
                ValidatorOptions = objValidator
            };
            return cmb;
        }

        public static ComboModel CmbProveedores(string id, bool required, bool multi, string width = "172px")
        {
            gralDomainBl = gralDomainBl == null ? new GeneralDomainBl() : gralDomainBl;
            EditorValidatorOptions objValidator = (required == true) ? new EditorValidatorOptions() { Required = new ValidatorRule() { ErrorMessage = "Required" } } : null;
            ComboMultiSelectionSettings objMulti = new ComboMultiSelectionSettings()
            {
                ShowCheckBoxes = true,
                Enabled = multi,
                ItemSeparator = ";"
            };
            var cmb = new ComboModel()
            {
                MultiSelectionSettings = objMulti,
                ID = id,
                DataSource = gralDomainBl.GetProveedores(),
                TextKey = "Value",
                Width = width,
                DropDownWidth = 300,
                Height = "25px",
                ValueKey = "Id",
                ValidatorOptions = objValidator
            };
            return cmb;
        }

        public static ComboModel CmbClientes(string id, bool required, bool multi, string width = "172px", string height = "30px" )
        {
            gralDomainBl = gralDomainBl == null ? new GeneralDomainBl() : gralDomainBl;
            EditorValidatorOptions objValidator = (required == true) ? new EditorValidatorOptions() { Required = new ValidatorRule() { ErrorMessage = "Required" } } : null;
            ComboMultiSelectionSettings objMulti = new ComboMultiSelectionSettings()
            {
                ShowCheckBoxes = true,
                Enabled = multi,
                ItemSeparator = ";"
            };
            var cmb = new ComboModel()
            {
                MultiSelectionSettings = objMulti,
                ID = id,
                Height = height,
                DataSource = gralDomainBl.GetClientes(),
                TextKey = "Value",
                Width = width,
                DropDownWidth = 300,
                ValueKey = "Id",
                ValidatorOptions = objValidator
            };
            return cmb;
        }

        public static ComboModel CmbTipoMovimiento(string id, bool required, bool multi, string width = "172px", string height = "30px")
        {
            gralDomainBl = gralDomainBl == null ? new GeneralDomainBl() : gralDomainBl;
            EditorValidatorOptions objValidator = (required == true) ? new EditorValidatorOptions() { Required = new ValidatorRule() { ErrorMessage = "Required" } } : null;
            ComboMultiSelectionSettings objMulti = new ComboMultiSelectionSettings()
            {
                ShowCheckBoxes = true,
                Enabled = multi,
                ItemSeparator = ";"
            };
            var cmb = new ComboModel()
            {
                MultiSelectionSettings = objMulti,
                ID = id,
                Height = height,
                DataSource = gralDomainBl.GetTiposMovimiento(),
                TextKey = "Value",
                Width = width,
                DropDownWidth = 300,
                ValueKey = "Id",
                ValidatorOptions = objValidator
            };
            return cmb;
        }

        public static ComboModel CmbBilletes(string id, bool required, bool multi, string width = "172px", string height = "30px")
        {
            gralDomainBl = gralDomainBl == null ? new GeneralDomainBl() : gralDomainBl;
            EditorValidatorOptions objValidator = (required == true) ? new EditorValidatorOptions() { Required = new ValidatorRule() { ErrorMessage = "Required" } } : null;
            ComboMultiSelectionSettings objMulti = new ComboMultiSelectionSettings()
            {
                ShowCheckBoxes = true,
                Enabled = multi,
                ItemSeparator = ";"
            };
            var cmb = new ComboModel()
            {
                MultiSelectionSettings = objMulti,
                ID = id,
                Height = height,
                DataSource = new List<BasicVm>() {
                                                new BasicVm() { Id = 1, Value = "$ 1.000" },
                                                new BasicVm() { Id = 2, Value = "$ 2.000" },
                                                new BasicVm() { Id = 3, Value = "$ 5.000" },
                                                new BasicVm() { Id = 4, Value = "$ 10.000" },
                                                new BasicVm() { Id = 5, Value = "$ 20.000" },
                                                new BasicVm() { Id = 6, Value = "$ 50.000" },
                                                new BasicVm() { Id = 7, Value = "$ 100.000" }
                                                },  
                TextKey = "Value",
                Width = width,
                DropDownWidth = 300,
                ValueKey = "Id",
                ValidatorOptions = objValidator
            };
            return cmb;
        }

        #endregion

        #region [DATEPICKER]
        public static DatePickerModel GetDatePickerModel(string id, bool required)
        {
            EditorValidatorOptions objValidator = (required == true) ? new EditorValidatorOptions() { Required = new ValidatorRule() { ErrorMessage = "Required" } } : null;
            var dpOptions = new Dictionary<string, object>();
            dpOptions.Add("showWeek", true);
            var dpm = new DatePickerModel()
            {
                ID = id,
                DatepickerOptions = dpOptions,
                Width = "172px",
                ValidatorOptions = objValidator,
                DateInputFormat = "yyyy/MM/dd",
                DateDisplayFormat = "yyyy/MM/dd"
            };
            return dpm;
        }
        #endregion

        #region [TEXTEDITOR]
        public static TextEditorModel GetTextEditorDefault(string id, bool required, string width = "172px")
        {
            EditorValidatorOptions objValidator = (required == true) ? new EditorValidatorOptions() { Required = new ValidatorRule() { ErrorMessage = "Required" } } : null;
            var txt = new TextEditorModel();
            txt.ID = id;
            txt.Width = width;            
            txt.ValidatorOptions = objValidator;
            return txt;
        }

        public static NumericEditorModel GetNumericEditorDefault(string id, bool required, string width = "172px")
        {
            EditorValidatorOptions objValidator = (required == true) ? new EditorValidatorOptions() { Required = new ValidatorRule() { ErrorMessage = "Required" } } : null;
            var txt = new NumericEditorModel();
            txt.ID = id;
            
            txt.MinValue = 0;
            txt.MaxValue = 100000;
            txt.DataMode = NumericEditorDataMode.Int;
            txt.Width = width;
            txt.ValidatorOptions = objValidator;
            return txt;
        }

        public static CurrencyEditorModel GetCurrencyEditorDefault(string id, bool required, string width = "172px", string height = "30px")
        {
            EditorValidatorOptions objValidator = (required == true) ? new EditorValidatorOptions() { Required = new ValidatorRule() { ErrorMessage = "Required" } } : null;
            var txt = new CurrencyEditorModel();
            txt.ID = id;
            txt.Height = height;
            txt.Value = 0;
            txt.MinValue = 0;
            txt.MaxValue = 100000;
            txt.DataMode = NumericEditorDataMode.Int;
            txt.Width = width;
            txt.ValidatorOptions = objValidator;
            return txt;
        }
        #endregion
    }
}