//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2032
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Drawing;

using SharpQuery.SchemaClass;
using SharpReportCore;

/// <summary>
/// This class creates settings for a report
/// </summary>
/// <remarks>
/// 	created by - Forstmeier Peter
/// 	created on - 28.01.2005 10:31:01
/// </remarks>


namespace ReportGenerator {	
	public class ReportGenerator : object,IDisposable {
		
		//BaseSettingsPanel
		
		private string reportName;
		private string fileName;
		private string path;
		
		private GlobalEnums.ReportType reportType;
		private GraphicsUnit graphicsUnit;
		
		//Database
		private GlobalEnums.PushPullModelEnum dataModel;

		private string connectionString;
		private string catalogName;
		private string sqlString;
		
		private CommandType commandType;
		private SharpQueryProcedure sharpQueryProcedure;
		private ReportItemCollection reportItemCollection;
		private ColumnCollection columnCollection;
		
		private SqlParametersCollection queryParameters;
		public ReportGenerator() {
		}
		
		public ReportModel FillReportModel (ReportModel model) {
			if (model == null) {
				throw new MissingModelException();
			}
			model.ReportSettings.ReportName = this.reportName;
			model.ReportSettings.FileName = this.path + this.fileName;
			
			model.ReportSettings.GraphicsUnit = this.graphicsUnit;
			model.ReportSettings.ReportType = this.reportType;

			model.ReportSettings.ConnectionString = this.connectionString;
			model.ReportSettings.CommandText = this.sqlString;
			model.ReportSettings.CommandType = this.commandType;
			model.ReportSettings.DataModel = this.dataModel;
			model.ReportSettings.SqlParametersCollection = this.queryParameters;
			return model;
		}
		
		
		
		#region BaseSettingsPanel property's
		public GraphicsUnit GraphicsUnit {
			get {
				return graphicsUnit;
			}
			set {
				graphicsUnit = value;
			}
		}
		public string ReportName {
			get {
				return reportName;
			}
			set {
				reportName = value;
			}
		}
		public SharpReportCore.GlobalEnums.ReportType ReportType {
			get {
				return reportType;
			}
			set {
				reportType = value;
			}
		}
		public string FileName {
			get {
				return fileName;
			}
			set {
				fileName = value;
			}
		}
		
		public string Path {
			get {
				return path;
			}
			set {
				path = value;
			}
		}
		
		#endregion
		
		#region DatabasePanel
		public string ConnectionString {
			get {
				return connectionString;
			}
			set {
				connectionString = value;
			}
		}
		public string CatalogName {
			get {
				return catalogName;
		}
			set {
				catalogName = value;
			}
		}
		
		public string SqlString {
			get {
				return sqlString;
			}
			set {
				sqlString = value;
			}
		}
		
		public CommandType CommandType 
		{
			get {
				return commandType;
			}
			set {
				commandType = value;
			}
		}
		
		public SharpReportCore.GlobalEnums.PushPullModelEnum DataModel {
			get {
				return dataModel;
			}
			set {
				dataModel = value;
			}
		}
		

		public SqlParametersCollection Parameters {
			get {
				return queryParameters;
			}
			set {
				this.queryParameters = value;
			}
		}
		
		/// <summary>
		/// This Property is only usefull for ResultPanel
		/// </summary>
		public SharpQueryProcedure SharpQueryProcedure {
			get {
				return sharpQueryProcedure;
			}
			set {
				sharpQueryProcedure = value;
			}
		}
		
		#endregion
		
		
		#region PushModelPanel
		
		public ColumnCollection ColumnCollection {
			get {
				return columnCollection;
			}
			set {
				columnCollection = value;
			}
		}
		
		
		public ReportItemCollection ReportItemCollection {
			get {
				return reportItemCollection;
			}
			set {
				reportItemCollection = value;
			}
		}
		
		#endregion
		
		#region IDisposable
		public void Dispose(){
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
		
		~ReportGenerator(){
			Dispose(false);
		}
		
		protected  void Dispose(bool disposing){
			if (disposing) {
				if (this.reportItemCollection != null) {
					this.reportItemCollection.Clear();
					this.reportItemCollection = null;
				}
				if (this.columnCollection == null) {
					this.columnCollection.Clear();
					this.columnCollection = null;
				}
				if (this.sharpQueryProcedure != null) {
					this.sharpQueryProcedure = null;
				}
			}
		
			// Release unmanaged resources.
			// Set large fields to null.
			// Call Dispose on your base class.
		}
		#endregion
		
	}
}
