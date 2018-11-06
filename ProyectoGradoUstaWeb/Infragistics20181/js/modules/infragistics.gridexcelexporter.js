/*!@license
 * Infragistics.Web.ClientUI Widget 18.1.235
 *
 * Copyright (c) 2011-2018 Infragistics Inc.
 *
 * http://www.infragistics.com/
 *
 *
 * Depends on:
 *  jquery-1.9.1.js
 *  jquery.ui.core.js
 *  jquery.ui.widget.js
 *  FileSaver.js
 *  Intl.js
 *  infragistics.ui.grid.framework.js
 *  infragistics.excel_core.js
 *  infragistics.excel_serialization_openxml.js
 */
(function(factory){if(typeof define==="function"&&define.amd){define(["jquery","jquery-ui","./infragistics.util","./infragistics.ui.grid.framework","./infragistics.excel_core","./infragistics.excel_serialization_openxml"],factory)}else{return factory(jQuery)}})(function($){$.ig=$.ig||{};$.ig.GridExcelExporter=$.ig.GridExcelExporter||Class.extend({callbacks:{cellExported:null,cellExporting:null,error:null,exportEnding:null,exportStarting:null,headerCellExported:null,headerCellExporting:null,rowExported:null,rowExporting:null,success:null,summaryExported:null,summaryExporting:null},settings:{columnsToSkip:[],dataExportMode:"allRows",fileName:"document",gridFeatureOptions:{columnfixing:"none",filtering:"none",hiding:"none",paging:"allRows",sorting:"none",summaries:"none"},gridStyling:"applied",skipFilteringOn:[],tableStyle:"TableStyleMedium1",worksheetName:"Sheet1"},init:function(){this.settings=$.extend({},this.settings);this.callbacks=$.extend({},this.callbacks)},exportGrid:function(grid,userSettings,userCallbacks){this._setGridReferences(grid);this._setSettings(userSettings);this._setCallbacks(userCallbacks);if(!this._handleEventCallback(this.callbacks.exportStarting,{grid:this._gridWidget})){return}var self=this;setTimeout(function(){self._createExcelWorkbookWithWorksheet();if(self._columnsToExport.length>0){self._getGridStyles();self._setDataToExport();self._exportHeaders();self._createExcelTableRegion();self._exportDataSegment();if(self._shouldResizeTableRegion){self._resizeTableRegion()}self._exportFeatures();self._calculateColumnsSize();self._styleChildGridHeaders()}var noCancel=self._handleEventCallback(self.callbacks.exportEnding,{grid:self._gridWidget,workbook:self._workbook,worksheet:self._worksheet});if(!noCancel){return}self._saveWorkbook()},0)},_setGridReferences:function(grid){if(grid.data().igHierarchicalGrid!==undefined){this._gridType="igHierarchicalGrid";this._dataSource=grid.data().igGrid.options.dataSource}else{if(grid.data().igTreeGrid!==undefined){this._gridType="igTreeGrid"}else{this._gridType="igGrid"}this._dataSource=grid.data().igGrid.dataSource}this._grid=grid.data()[this._gridType];this._gridWidget=grid;this._allColumnLayouts=[]},_setSettings:function(userSettings){this._enableFeaturesAsInGrid();this._setUserSettings(userSettings);this._setColumnsToExport(this._grid.options);if(this._grid.options.columnLayouts!==undefined){this._getAllColumnLayouts(this._grid.options.columnLayouts[0])}},_enableFeaturesAsInGrid:function(){var gridFeatureOption;for(gridFeatureOption in this.settings.gridFeatureOptions){if(!this._getFeature(gridFeatureOption)){continue}if(gridFeatureOption==="paging"){this.settings.gridFeatureOptions[gridFeatureOption]="allRows"}else{this.settings.gridFeatureOptions[gridFeatureOption]="applied"}}},_setUserSettings:function(userSettings){this.settings=$.extend(this.settings,userSettings)},_setColumnsToExport:function(columnsObj,indent){if(columnsObj!==undefined){if(indent!==undefined&&indent>0){this._childColumnsToExport=this._getVisibleColumns(columnsObj.columns)}else{this._columnsToExport=this._getVisibleColumns(columnsObj.columns)}}},_getVisibleColumns:function(columns){var i,visibleColumns=[],columnsToSkip=this.settings.columnsToSkip;if(columns.length===0&&this._gridType==="igHierarchicalGrid"){columns=this._grid.rootWidget().options.columns}for(i=0;i<columns.length;i++){var gridColumn=columns[i];if(gridColumn.hidden&&this.settings.gridFeatureOptions.hiding==="visibleColumnsOnly"){continue}if(columnsToSkip.indexOf(gridColumn.key)<0){visibleColumns.push(gridColumn)}}return visibleColumns},_setCallbacks:function(userCallbacks){this.callbacks=$.extend(this.callbacks,userCallbacks)},_createExcelWorkbookWithWorksheet:function(){this._xlRowIndex=0;this._workbook=new $.ig.excel.Workbook($.ig.excel.WorkbookFormat.excel2007);this._worksheet=this._workbook.worksheets().add(this.settings.worksheetName)},_getGridStyles:function(){var headersTable,gridTable=this._gridWidget.find("#"+this._gridWidget.attr("id")+"_table");if(gridTable.length===0){gridTable=this._gridWidget}headersTable=gridTable.find("thead");if(this.settings.gridStyling==="applied"){if(headersTable.length===0){this._headersBackColor="rgb(136, 136, 136)";this._headersForeColor="rgb(255, 255, 255)";this._altRowColor="rgb(240, 240, 240)"}else{var $stylesTR=$("<tr>").attr({"class":"ui-ig-altrecord ui-iggrid-altrecord"}).appendTo(gridTable).css("display","none");var $th=$(headersTable.find("th[role='columnheader']")[0]).clone().css("display","none");$stylesTR.append($th);this._headersBackColor=$th.removeClass("ui-state-active").css("background-color");$th.remove();this._headersForeColor=headersTable.find(".ui-iggrid-headertext").css("color");if($stylesTR.css("background-color").indexOf("rgba")>-1){var backTopColor=$stylesTR.css("background-color").replace("rgba(","").replace(")","").split(", ");var backBotColor=gridTable.css("background-color").replace("rgb(","").replace(")","").split(", ");this._altRowColor=this._RGBAtoRGB(backTopColor[0],backTopColor[1],backTopColor[2],backTopColor[3],backBotColor[0],backBotColor[1],backBotColor[2])}else{this._altRowColor=$stylesTR.css("background-color")}$stylesTR.remove()}this._notAltRowColor=gridTable.css("background-color");this._rowForeColor=gridTable.css("color");this._getCellFill()}},_RGBAtoRGB:function(r,g,b,a,r2,g2,b2){var r3=a*r+(1-a)*r2,g3=a*g+(1-a)*g2,b3=a*b+(1-a)*b2;return"rgb("+r3+","+g3+","+b3+")"},_setDataToExport:function(){var paging=this.settings.gridFeatureOptions.paging,filtering=this.settings.gridFeatureOptions.filtering;this.dataToExport=[];if(paging==="currentPage"&&this._getFeature("Paging")){this.dataToExport=this._dataSource.dataView()}else if(filtering!=="filteredRowsOnly"){this.dataToExport=this._dataSource.data()}else{this.dataToExport=this._dataSource.filteredData();if(this.dataToExport===undefined){this.dataToExport=this._dataSource.data()}}},_createExcelTableRegion:function(){var i,regionRowsLength,counter=0;regionRowsLength=this.dataToExport.length;for(i=0;i<this._columnsToExport.length;i++){if(this._columnsToExport[i].hidden){counter++}}this._worksheetRegion=new $.ig.excel.WorksheetRegion(this._worksheet,0,0,regionRowsLength,this._columnsToExport.length-1);this._worksheetTable=this._worksheetRegion.formatAsTable(true);this._worksheetTable.style(this._workbook.standardTableStyles(this.settings.tableStyle));this._woorksheetTableColumns=this._worksheetTable.columns()},_exportHeaders:function(){var columnIndex,gridColumn,noCancel,args={},xlRow=this._worksheet.rows(this._xlRowIndex);this._headerFill=$.ig.excel.CellFill.createSolidFill(this._headersBackColor);xlRow.cellFormat().font().bold(1);for(columnIndex=0;columnIndex<this._columnsToExport.length;columnIndex++){gridColumn=this._columnsToExport[columnIndex];args={headerText:gridColumn.headerText,columnKey:gridColumn.key,columnIndex:columnIndex,xlRow:xlRow};noCancel=this._handleEventCallback(this.callbacks.headerCellExporting,args);if(!noCancel){return}if(this.settings.gridStyling==="applied"){xlRow.getCellFormat(columnIndex).fill(this._headerFill);xlRow.getCellFormat(columnIndex).font().colorInfo(new $.ig.excel.WorkbookColorInfo(this._headersForeColor))}this._exportCell(xlRow,gridColumn,columnIndex,args.headerText,true);this._handleEventCallback(this.callbacks.headerCellExported,args)}this._xlRowIndex++;this._worksheet.displayOptions().frozenPaneSettings().frozenRows(this._xlRowIndex);this._worksheet.displayOptions().panesAreFrozen(true)},_exportDataSegment:function(){var xlRow,dataRow,rowIndex,noCancel,rowId,wasRowExportingCancelled=false,args={};this._exportedProps=[];this._subGridHeaderRowsIndices=[];this._colsIndices=[];for(rowIndex=0;rowIndex<this.dataToExport.length;rowIndex++){xlRow=this._worksheet.rows(this._xlRowIndex);dataRow=this.dataToExport[rowIndex];rowId=dataRow[this._gridWidget.data().igGrid.options.primaryKey];args={rowId:rowId,element:this._gridType!=="igTreeGrid"?this._gridWidget.igGrid("rowById",rowId):this._gridWidget.igTreeGrid("rowById",rowId),xlRow:xlRow,grid:this._gridWidget};noCancel=this._handleEventCallback(this.callbacks.rowExporting,args);if(!noCancel){wasRowExportingCancelled=true;continue}this._exportDataSegmentRow(xlRow,dataRow,rowIndex,rowId);this._handleEventCallback(this.callbacks.rowExported,args);this._xlRowIndex++;if(this.settings.dataExportMode==="expandedRows"&&args.element&&args.element.attr("aria-expanded")==="false"){continue}else if(this._gridType!=="igGrid"){this._goThroughChildren(dataRow,xlRow,rowIndex,1,args)}this._exportedProps=[]}if(wasRowExportingCancelled){this._recalculateTableEnd();this._shouldResizeTableRegion=true}},_exportDataSegmentRow:function(xlRow,dataRow,rowIndex,rowId,indent,dataRowProp){var gridColumn,columnLayout,dataCell,columnIndex,chGrColNum,y,prop,layoutColumns=[];if(indent===undefined){layoutColumns=this._columnsToExport}else{for(y=0;y<this._allColumnLayouts.length;y++){if(this._isEqualColumnLayoutsAndDataRowProp(dataRowProp,this._allColumnLayouts[y])){columnLayout=this._allColumnLayouts[y];if(columnLayout.columns!==undefined){this._setColumnsToExport(columnLayout,indent);layoutColumns=this._childColumnsToExport}else{layoutColumns=undefined}}}}if(indent!==undefined&&this._gridType==="igHierarchicalGrid"){if(layoutColumns!==undefined&&layoutColumns.length>0){for(columnIndex=0;columnIndex<layoutColumns.length;columnIndex++){gridColumn=layoutColumns[columnIndex];if(this._colsIndices.indexOf(columnIndex)===-1){this._colsIndices.push(columnIndex)}dataCell=this._handleColumnsFormatting(gridColumn,dataRow);this._exportCell(xlRow,gridColumn,columnIndex,dataCell,false,rowId,indent)}}else{chGrColNum=0;for(prop in dataRow){if(prop!=="__metadata"&&prop!=="ig_pk"){if(typeof dataRow[prop]==="object"||typeof dataRow[prop]==="function"){continue}chGrColNum++;if(this._colsIndices.indexOf(chGrColNum-1)===-1){this._colsIndices.push(chGrColNum-1)}dataCell=dataRow[prop];this._exportCell(xlRow,gridColumn,chGrColNum-1,dataCell,false,rowId,indent)}}}}else{for(columnIndex=0;columnIndex<this._columnsToExport.length;columnIndex++){gridColumn=this._columnsToExport[columnIndex];dataCell=dataRow[gridColumn.key];if(this.settings.gridStyling==="applied"){xlRow.getCellFormat(columnIndex).fill(this._cellFill[xlRow.index()%2]);xlRow.getCellFormat(columnIndex).font().colorInfo(new $.ig.excel.WorkbookColorInfo(this._rowForeColor))}dataCell=this._handleColumnsFormatting(gridColumn,dataRow);this._exportCell(xlRow,gridColumn,columnIndex,dataCell,false,rowId,indent)}}},_goThroughChildren:function(dataRow,xlRow,rowIndex,indent,args,parentDataRowProp){var responseDataKey,dataRowObj,shouldContinue,prop,eventArgs,childGrid,noCancel,rowId,i,y;if(this.dataToExport.indexOf(dataRow)===-1&&this._gridType==="igHierarchicalGrid"){this._exportChildGridHeaders(dataRow,xlRow,indent,parentDataRowProp)}shouldContinue=true;if(this.settings.dataExportMode==="expandedRows"&&args.element&&args.element.attr("aria-expanded")==="false"){shouldContinue=false}if(shouldContinue){childGrid=this._gridType==="igHierarchicalGrid"?$(args.element).next("[data-container='true']").find("table[data-childgrid='true']"):this._gridWidget;for(prop in dataRow){if(dataRow[prop]&&typeof dataRow[prop]==="object"&&$.type(dataRow[prop])!=="date"&&dataRow.hasOwnProperty(prop)&&typeof dataRow[prop]!=="function"&&prop!=="__metadata"&&prop!=="ig_pk"){for(y=0;y<this._allColumnLayouts.length;y++){if(this._isEqualColumnLayoutsAndDataRowProp(prop,this._allColumnLayouts[y])){responseDataKey=this._allColumnLayouts[y].responseDataKey}}if(childGrid.length>0&&this._gridType==="igHierarchicalGrid"){dataRowObj=childGrid.data().igGrid.dataSource.data()}else{dataRowObj=dataRow[prop][responseDataKey]?dataRow[prop][responseDataKey]:dataRow[prop]}if(dataRowObj!==undefined){for(i=0;i<dataRowObj.length;i++){xlRow=this._worksheet.rows(this._xlRowIndex);xlRow.outlineLevel(indent);rowId=childGrid.length>0&&childGrid.data().igGrid.options.primaryKey?dataRowObj[i][childGrid.data().igGrid.options.primaryKey]:dataRowObj[i].ig_pk;if(!rowId){rowId=i}eventArgs={rowId:rowId,element:childGrid.length>0?childGrid.data().igGrid.rowById(rowId):null,xlRow:xlRow,grid:this._gridWidget};noCancel=this._handleEventCallback(this.callbacks.rowExporting,eventArgs);if(!noCancel){continue}this._exportDataSegmentRow(xlRow,dataRowObj[i],rowIndex,rowId,indent,prop);this._handleEventCallback(this.callbacks.rowExported,eventArgs);this._xlRowIndex++;xlRow=this._worksheet.rows(this._xlRowIndex);this._goThroughChildren(dataRowObj[i],xlRow,rowIndex,indent+1,eventArgs,prop,i);this._recalculateTableEnd();this._shouldResizeTableRegion=true}}}}}},_handleColumnsFormatting:function(gridColumn,dataRow){var dataCell=dataRow[gridColumn.key];if(gridColumn.dataType==="object"&&$.type(gridColumn.mapper)==="function"){dataCell=gridColumn.mapper(dataRow)}if($.type(gridColumn.formula)==="function"){dataCell=gridColumn.formula(dataRow)}if($.type(gridColumn.formatter)==="function"){dataCell=gridColumn.formatter(dataCell)}return dataCell},_getAllColumnLayouts:function(obj){this._allColumnLayouts.push(obj);if(obj.columnLayouts!==undefined&&obj.columnLayouts.length){this._getAllColumnLayouts(obj.columnLayouts[0])}},_exportChildGridHeaders:function(dataRow,xlRow,indent,parentDataRowProp){var columnLayout,gridColumn,i,y,columnIndex,propObj,prop,noCancel,args={},subGridColumnIndex=0,layoutColumns=[],keysCounter=0,counter=0;this._columnLayoutKeys=[];this._shouldExportCurrentHeader=true;for(i=0;i<this._allColumnLayouts.length;i++){if(this._isEqualColumnLayoutsAndDataRowProp(parentDataRowProp,this._allColumnLayouts[i])){columnLayout=this._allColumnLayouts[i]}}if(columnLayout!==undefined&&columnLayout.columns!==undefined){this._setColumnsToExport(columnLayout,indent);layoutColumns=this._childColumnsToExport;for(columnIndex=0;columnIndex<layoutColumns.length;columnIndex++){gridColumn=layoutColumns[columnIndex];this._verifyExportedHeader(gridColumn.key,indent);if(this._shouldExportCurrentHeader){if(counter===0){this._worksheet.rows().insert(xlRow.index()-1,1);xlRow=this._worksheet.rows(xlRow.index()-2);xlRow.outlineLevel(indent-1)}xlRow.cells(0).cellFormat().indent((indent-1)*2);xlRow.cells(columnIndex).cellFormat().alignment($.ig.excel.HorizontalCellAlignment.left);xlRow.cells(columnIndex).cellFormat().font().bold(1);args={headerText:gridColumn.headerText,columnKey:gridColumn.key,columnIndex:columnIndex,xlRow:xlRow};noCancel=this._handleEventCallback(this.callbacks.headerCellExporting,args);if(!noCancel){return}xlRow.setCellValue(columnIndex,args.headerText);this._handleEventCallback(this.callbacks.headerCellExported,args);propObj={key:gridColumn.key,level:indent};this._exportedProps.push(propObj);if(this._subGridHeaderRowsIndices.indexOf(xlRow.index())===-1){this._subGridHeaderRowsIndices.push(xlRow.index())}if(counter===0){this._xlRowIndex++}counter++}}}else{for(prop in dataRow){for(y=0;y<this._allColumnLayouts.length;y++){if(this._isEqualColumnLayoutsAndDataRowProp(prop,this._allColumnLayouts[y])){keysCounter++}}if(keysCounter>0){continue}this._verifyExportedHeader(prop,indent);if(prop!=="ig_pk"&&this._shouldExportCurrentHeader){if(counter===0){this._worksheet.rows().insert(xlRow.index()-1,1);xlRow=this._worksheet.rows(xlRow.index()-2);xlRow.outlineLevel(indent-1)}subGridColumnIndex++;xlRow.cells(0).cellFormat().indent((indent-1)*2);xlRow.cells(subGridColumnIndex-1).cellFormat().alignment($.ig.excel.HorizontalCellAlignment.left);xlRow.cells(subGridColumnIndex-1).cellFormat().font().bold(1);args={headerText:prop,columnIndex:subGridColumnIndex-1,xlRow:xlRow};noCancel=this._handleEventCallback(this.callbacks.headerCellExporting,args);if(!noCancel){return}xlRow.setCellValue(subGridColumnIndex-1,args.headerText);this._handleEventCallback(this.callbacks.headerCellExported,args);propObj={key:prop,level:indent};this._exportedProps.push(propObj);if(this._subGridHeaderRowsIndices.indexOf(xlRow.index())===-1){this._subGridHeaderRowsIndices.push(xlRow.index())}if(counter===0){this._xlRowIndex++}counter++}}}},_verifyExportedHeader:function(prop,indent){var currProp,existingProp,currPropValues=[],existingPropValues=[];currProp={key:prop,level:indent};currPropValues=$.ig.util.getArrayOfValues(currProp);for(var i=0;i<this._exportedProps.length;i++){existingProp=this._exportedProps[i];existingPropValues=$.ig.util.getArrayOfValues(existingProp);if($.ig.util.areSetsEqual(currPropValues,existingPropValues)){this._shouldExportCurrentHeader=false;break}}},_isEqualColumnLayoutsAndDataRowProp:function(dataRowProp,obj){if(dataRowProp===obj.childrenDataProperty){return true}else if(dataRowProp===obj.key){return true}else{return false}},_exportCell:function(xlRow,gridColumn,columnIndex,cellValue,isHeader,rowId,indent){var noCancel,args={};if(gridColumn){if(!isHeader&&gridColumn.dataType==="date"&&$.type(cellValue)==="string"){cellValue=new Date(parseInt(cellValue.substr(6),10))}args={columnKey:gridColumn.key,columnIndex:columnIndex,rowId:rowId,xlRow:xlRow,cellValue:cellValue,grid:this._gridWidget}}else{args={cellValue:cellValue,columnIndex:columnIndex,rowId:rowId,xlRow:xlRow,grid:this._gridWidget}}noCancel=this._handleEventCallback(this.callbacks.cellExporting,args);if(!noCancel){return}if(indent!==undefined&&columnIndex===0){xlRow.cells(columnIndex).cellFormat().indent(indent*2);xlRow.cells(columnIndex).cellFormat().alignment($.ig.excel.HorizontalCellAlignment.left);xlRow.setCellValue(columnIndex,args.cellValue)}else{xlRow.setCellValue(columnIndex,args.cellValue)}this._handleEventCallback(this.callbacks.cellExported,args)},_recalculateTableEnd:function(cols){var xlRowIndex=this._xlRowIndex-1;cols=cols?cols:this._worksheetTable.columns().count();this._tableRegionEndCellReference=$.ig.excel.WorksheetCell.getCellAddressString(this._worksheet.rows().item(xlRowIndex),cols-1,$.ig.excel.CellReferenceMode.a1,false).toString()},_resizeTableRegion:function(){var tableRegionStartCellReference=$.ig.excel.WorksheetCell.getCellAddressString(this._worksheet.rows().item(0),0,$.ig.excel.CellReferenceMode.a1,false).toString();if(tableRegionStartCellReference.substring(tableRegionStartCellReference.lastIndexOf("$")+1)===this._tableRegionEndCellReference.substring(this._tableRegionEndCellReference.lastIndexOf("$")+1)){var nextRowIndex=parseInt(this._tableRegionEndCellReference.substring(this._tableRegionEndCellReference.lastIndexOf("$")+1))+1;this._tableRegionEndCellReference=this._tableRegionEndCellReference.substring(0,this._tableRegionEndCellReference.lastIndexOf("$")+1)+nextRowIndex.toString()}this._worksheetTable.resize(tableRegionStartCellReference+":"+this._tableRegionEndCellReference)},_exportFeatures:function(){var featureName,i,features=this.settings.gridFeatureOptions;for(i=0;i<this._grid.options.features.length;i++){featureName=this._grid.options.features[i].name;if(features[featureName.toLowerCase()]===undefined){continue}if(featureName!=="Paging"&&featureName!=="Hiding"){if(features[featureName.toLowerCase()]!=="none"){this["_export"+featureName]()}}}},_getFeature:function(name){var featureIndex,features=this._grid.options.features;for(featureIndex=0;featureIndex<features.length;featureIndex++){var feature=features[featureIndex];if(feature.name.toLowerCase()===name.toLowerCase()){return feature}}return null},_exportSorting:function(){var columnToSort,sortDirection,i,j,orderedSortConditions=this._createSortingConditions(),sortExpressions=this._gridWidget.data("igGrid").dataSource.settings.sorting.expressions;for(i=0;i<sortExpressions.length;i++){for(j=0;j<this._columnsToExport.length;j++){if(sortExpressions[i].fieldName===this._columnsToExport[j].key){columnToSort=this._worksheetTable.columns().item(j);sortDirection=sortExpressions[i].dir;if(columnToSort!==undefined&&sortDirection!==undefined){this._worksheetTable.sortSettings().sortConditions().add(columnToSort,orderedSortConditions[sortDirection])}}}}},_createSortingConditions:function(){var orderedSortConditions={asc:new $.ig.excel.OrderedSortCondition($.ig.excel.SortDirection.ascending),desc:new $.ig.excel.OrderedSortCondition($.ig.excel.SortDirection.descending)};return orderedSortConditions},_exportFiltering:function(){if(this.settings.gridFeatureOptions.filtering==="filteredRowsOnly"){return}var columnToFilter,gridColumn,filtCond,filtExpr,dayStart,dayEnd,i,j,filteringConditions=this._createFilteringConditions(),filtExpressions=this._gridWidget.data("igGrid").dataSource.settings.filtering.expressions;if(filtExpressions.length>0){for(i=0;i<filtExpressions.length;i++){for(j=0;j<this._columnsToExport.length;j++){if(this._columnsToExport[j].key===filtExpressions[i].fieldName&&this.settings.skipFilteringOn.indexOf(filtExpressions[i].fieldName)<0){gridColumn=this._columnsToExport[j];columnToFilter=this._worksheetTable.columns().item(j);filtCond=filtExpressions[i].cond;filtExpr=filtExpressions[i].expr;if(filtCond==="false"||filtCond==="true"){filtExpr=filtCond}switch(filtCond){case"on":columnToFilter.applyFixedValuesFilter(true,$.ig.excel.CalendarType.gregorian,[new $.ig.excel.FixedDateGroup($.ig.excel.FixedDateGroupType.day,filtExpr)]);break;case"notOn":dayStart=new Date(filtExpr.setHours(0));dayEnd=new Date(filtExpr.setHours(24));columnToFilter.applyCustomFilter(new $.ig.excel.CustomFilterCondition(filteringConditions.before,dayStart),new $.ig.excel.CustomFilterCondition(filteringConditions.after,dayEnd),$.ig.excel.ConditionalOperator.or);break;case"after":columnToFilter.applyCustomFilter(new $.ig.excel.CustomFilterCondition(filteringConditions[filtCond],filtExpr));break;case"before":columnToFilter.applyCustomFilter(new $.ig.excel.CustomFilterCondition(filteringConditions[filtCond],filtExpr));break;case"today":columnToFilter.applyRelativeDateRangeFilter($.ig.excel.RelativeDateRangeOffset.current,$.ig.excel.RelativeDateRangeDuration.day);break;case"yesterday":columnToFilter.applyRelativeDateRangeFilter($.ig.excel.RelativeDateRangeOffset.previous,$.ig.excel.RelativeDateRangeDuration.day);break;case"thisMonth":columnToFilter.applyRelativeDateRangeFilter($.ig.excel.RelativeDateRangeOffset.current,$.ig.excel.RelativeDateRangeDuration.month);break;case"lastMonth":columnToFilter.applyRelativeDateRangeFilter($.ig.excel.RelativeDateRangeOffset.previous,$.ig.excel.RelativeDateRangeDuration.month);break;case"nextMonth":columnToFilter.applyRelativeDateRangeFilter($.ig.excel.RelativeDateRangeOffset.next,$.ig.excel.RelativeDateRangeDuration.month);break;case"thisYear":columnToFilter.applyRelativeDateRangeFilter($.ig.excel.RelativeDateRangeOffset.current,$.ig.excel.RelativeDateRangeDuration.year);break;case"lastYear":columnToFilter.applyRelativeDateRangeFilter($.ig.excel.RelativeDateRangeOffset.previous,$.ig.excel.RelativeDateRangeDuration.year);break;case"nextYear":columnToFilter.applyRelativeDateRangeFilter($.ig.excel.RelativeDateRangeOffset.next,$.ig.excel.RelativeDateRangeDuration.year);break;case"null":if(gridColumn.dataType==="number"||gridColumn.dataType==="date"||gridColumn.dataType===undefined){columnToFilter.applyCustomFilter(new $.ig.excel.CustomFilterCondition($.ig.excel.ExcelComparisonOperator.equals,""))}else{columnToFilter.applyCustomFilter(new $.ig.excel.CustomFilterCondition($.ig.excel.ExcelComparisonOperator.notEqual,"*"))}break;case"notNull":if(gridColumn.dataType==="number"||gridColumn.dataType==="date"||gridColumn.dataType===undefined){columnToFilter.applyCustomFilter(new $.ig.excel.CustomFilterCondition($.ig.excel.ExcelComparisonOperator.notEqual,""))}else{columnToFilter.applyCustomFilter(new $.ig.excel.CustomFilterCondition($.ig.excel.ExcelComparisonOperator.equals,"*"))}break;case"empty":columnToFilter.applyCustomFilter(new $.ig.excel.CustomFilterCondition($.ig.excel.ExcelComparisonOperator.equals,""));break;case"notEmpty":columnToFilter.applyCustomFilter(new $.ig.excel.CustomFilterCondition($.ig.excel.ExcelComparisonOperator.notEqual,""));break;default:if(filteringConditions[filtCond]!==undefined){columnToFilter.applyCustomFilter(new $.ig.excel.CustomFilterCondition(filteringConditions[filtCond],filtExpr))}else{throw"Custom filtering conditions cannot be applied to the exported worksheet. "+"Please use the skipFilteringOn property to "+"define columns that filtering will skip"}}}}}}},_createFilteringConditions:function(){var filteringConditions={equals:$.ig.excel.ExcelComparisonOperator.equals,doesNotEqual:$.ig.excel.ExcelComparisonOperator.notEqual,greaterThan:$.ig.excel.ExcelComparisonOperator.greaterThan,greaterThanOrEqualTo:$.ig.excel.ExcelComparisonOperator.greaterThanOrEqual,lessThan:$.ig.excel.ExcelComparisonOperator.lessThan,lessThanOrEqualTo:$.ig.excel.ExcelComparisonOperator.lessThanOrEqual,"null":$.ig.excel.ExcelComparisonOperator.equals,notNull:$.ig.excel.ExcelComparisonOperator.notEqual,empty:$.ig.excel.ExcelComparisonOperator.equals,notEmpty:$.ig.excel.ExcelComparisonOperator.notEqual,startsWith:$.ig.excel.ExcelComparisonOperator.beginsWith,endsWith:$.ig.excel.ExcelComparisonOperator.endsWith,contains:$.ig.excel.ExcelComparisonOperator.contains,doesNotContain:$.ig.excel.ExcelComparisonOperator.doesNotContain,after:$.ig.excel.ExcelComparisonOperator.greaterThan,before:$.ig.excel.ExcelComparisonOperator.lessThan,notOn:$.ig.excel.ExcelComparisonOperator.notEqual,"false":$.ig.excel.ExcelComparisonOperator.equals,"true":$.ig.excel.ExcelComparisonOperator.equals};return filteringConditions},_exportSummaries:function(){var i,columnIndex,column,summaryIndex,summary,summaryType,summariesForColumn,dataStartRowIndex,formatString,xlRow,columnReference,noCancel,args={},argumentSeparator=this._getSummariesArgumentSeparator($.ig.CultureInfo.prototype.currentCulture().numberFormat().numberDecimalSeparator()),dataEndRowIndex=this._xlRowIndex-1,summaries=this._gridWidget.igGridSummaries("summaryCollection"),worksheet=this._worksheet;if(this._xlRowIndex===1){return}for(i=0;i<this._columnsToExport.length;i++){columnIndex=i;column=this._columnsToExport[columnIndex];if(summaries[column.key]===undefined){continue}summariesForColumn=summaries[column.key];dataStartRowIndex=1;columnReference=new $.ig.excel.WorksheetRegion(this._worksheet,dataStartRowIndex,columnIndex,dataEndRowIndex,columnIndex).toString($.ig.excel.CellReferenceMode.a1,false,true,true);for(summaryIndex=0;summaryIndex<summariesForColumn.length;summaryIndex++){summary=summariesForColumn[summaryIndex];switch(summary.type.toLowerCase()){case"avg":summaryType=101;formatString='"'+$.ig.GridSummaries.locale.defaultSummaryRowDisplayLabelAvg+' = "0.00';break;case"min":summaryType=105;formatString='"'+$.ig.GridSummaries.locale.defaultSummaryRowDisplayLabelMin+' = "0.00';break;case"max":summaryType=104;formatString='"'+$.ig.GridSummaries.locale.defaultSummaryRowDisplayLabelMax+' = "0.00';break;case"count":summaryType=103;formatString='"'+$.ig.GridSummaries.locale.defaultSummaryRowDisplayLabelCount+' = "0.00';break;case"sum":summaryType=109;formatString='"'+$.ig.GridSummaries.locale.defaultSummaryRowDisplayLabelSum+' = "0.00';break}args={headerText:this._columnsToExport[columnIndex].headerText,columnKey:this._columnsToExport[columnIndex].key,columnIndex:columnIndex,summary:summariesForColumn[summaryIndex],summaryRowIndex:summaryIndex,xlRowIndex:this._xlRowIndex};noCancel=this._handleEventCallback(this.callbacks.summaryExporting,args);if(!noCancel){return}if(summaryType){xlRow=worksheet.rows(this._xlRowIndex+summaryIndex);xlRow.applyCellFormula(columnIndex,"=SUBTOTAL("+summaryType+argumentSeparator+columnReference+")");xlRow.getCellFormat(columnIndex).formatString(formatString);this._handleEventCallback(this.callbacks.summaryExported,args)}}}},_getSummariesArgumentSeparator:function(decimalSeparator){if(decimalSeparator===","){return";"}return","},_exportColumnFixing:function(){var i,numberOfFrozenCols=0;for(i=0;i<this._columnsToExport.length;i++){if(this._columnsToExport[i].fixed){numberOfFrozenCols++}}this._worksheet.displayOptions().frozenPaneSettings().frozenColumns(numberOfFrozenCols)},_calculateColumnsSize:function(){var i,col,colWidth,column,colElemId,gridTable=this._gridWidget.find("#"+this._gridWidget.attr("id")+"_table"),gridFeatureOptions=this.settings.gridFeatureOptions;if(gridTable.length===0){gridTable=this._gridWidget}for(i=0;i<this._columnsToExport.length;i++){colWidth=0;column=this._columnsToExport[i];if(gridFeatureOptions.hiding==="none"||column.fixed&&!column.hidden){col=this._worksheet.columns().item(i);colWidth=column.width;if(colWidth&&!column.hidden){if(!isNaN(colWidth)){col.setWidth(colWidth,$.ig.excel.WorksheetColumnWidthUnit.pixel)}else if(isNaN(colWidth)){var widthSize=colWidth.match(/\d+/)[0];if(colWidth.contains("px")){col.setWidth(widthSize,$.ig.excel.WorksheetColumnWidthUnit.pixel)}else{col.setWidth(widthSize/100*this._gridWidget.width(),$.ig.excel.WorksheetColumnWidthUnit.pixel)}}}}else{col=this._worksheet.columns().item(i);colElemId=gridTable.attr("id")+"_"+column.key;if(gridTable.find('[aria-describedby="'+colElemId+'"]').length>0){colWidth=gridTable.find('[aria-describedby="'+colElemId+'"]').width()}else if(this._gridWidget.data("igGrid").headersTable().find('[id="'+colElemId+'"]').length>0){colWidth=this._gridWidget.data("igGrid").headersTable().find('[id="'+colElemId+'"]').width()}col.setWidth(colWidth,$.ig.excel.WorksheetColumnWidthUnit.pixel)}}},_getCellFill:function(){this._cellFill=[];this._cellFill.push($.ig.excel.CellFill.createSolidFill(this._altRowColor));this._cellFill.push($.ig.excel.CellFill.createSolidFill(this._notAltRowColor));return this._cellFill},_styleChildGridHeaders:function(){var i,y,tableEndColumn,lastRow;if(this._gridType==="igHierarchicalGrid"&&this._colsIndices&&this._colsIndices.length>0){tableEndColumn=this._colsIndices.length>this._columnsToExport.length?this._colsIndices.length:this._columnsToExport.length;lastRow=this._worksheetRegion.lastRow()+1;this._recalculateTableEnd(tableEndColumn);this._resizeTableRegion();if(this.settings.gridStyling==="applied"){for(i=1;i<lastRow;i++){for(y=0;y<tableEndColumn;y++){if(this._subGridHeaderRowsIndices.indexOf(i)!==-1){this._worksheet.rows(this._subGridHeaderRowsIndices[this._subGridHeaderRowsIndices.indexOf(i)]).getCellFormat(y).fill(this._headerFill);this._worksheet.rows(this._subGridHeaderRowsIndices[this._subGridHeaderRowsIndices.indexOf(i)]).getCellFormat(y).font().colorInfo(new $.ig.excel.WorkbookColorInfo(this._headersForeColor))}else{this._worksheet.rows(i).getCellFormat(y).fill(this._cellFill[i%2]);this._worksheet.rows(i).getCellFormat(y).font().colorInfo(new $.ig.excel.WorkbookColorInfo(this._rowForeColor))}}}}}},_saveWorkbook:function(){var self=this;setTimeout(function(){self._workbook.save({type:"blob"},function(data){saveAs(data,self.settings.fileName+".xlsx");self._handleEventCallback(self.callbacks.success,data)},function(err){self._handleEventCallback(self.callbacks.error,err)})},1)},_handleEventCallback:function(callback,args){if(!$.isFunction(callback)){return true}var noCancel=callback(this,args);if(noCancel===false){return false}return true}});$.ig.GridExcelExporter.exportGrid=function(grid,userSettings,userCallbacks){var exp=new $.ig.GridExcelExporter;exp.exportGrid(grid,userSettings,userCallbacks);exp=null};return $});