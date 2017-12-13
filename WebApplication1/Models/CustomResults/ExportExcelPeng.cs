using ClosedXML.Excel;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models.CustomResults
{
    public class ExportExcelPeng
    {
        public string SheetName { get; set; }
        public string FileName { get; set; }
        public DataTable ExportData { get; set; }

        public ExportExcelPeng(string sheetname, string filename, DataTable exportdata)
        {
            SheetName = sheetname;
            FileName = filename;
            ExportData = exportdata;
        }

        public FileStreamResult 取得FileStreamResult()
        {
            if (ExportData == null)
            {
                throw new InvalidDataException("ExportData");
            }
            if (string.IsNullOrWhiteSpace(this.SheetName))
            {
                this.SheetName = "Sheet1";
            }
            if (string.IsNullOrWhiteSpace(this.FileName))
            {
                this.FileName = string.Concat(
                    "ExportData_",
                    DateTime.Now.ToString("yyyyMMddHHmmss"),
                    ".xlsx");
            }

            // this.ExportExcelEventHandler(context);
            var workbook = new XLWorkbook();
            var memoryStream = new MemoryStream();
            try
            {
                if (this.ExportData != null)
                {
                    // Add all DataTables in the DataSet as a worksheets
                    workbook.Worksheets.Add(this.ExportData, this.SheetName);

                    
                    workbook.SaveAs(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                workbook.Dispose();
            }

            FileStreamResult result = new FileStreamResult(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            result.FileDownloadName = this.FileName;

            return result;
        } 
    }
}