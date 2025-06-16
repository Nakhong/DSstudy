using OrganizationTreeForm.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices; // COM 객체 해제를 위해
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;

namespace OrganizationTreeForm.Utils
{
    public class ExcelHelper
    {
        private string path = $"{AppDomain.CurrentDomain.BaseDirectory}Data\\Soccer.xlsx";
        private Excel.Application excelApp = null;
        private Excel.Workbook workBook = null;
        private Excel.Worksheet workSheet = null;
        private int worksheetCount;

        private Dictionary<string, Country> countries = new Dictionary<string, Country>();
        private Dictionary<string, League> leagues = new Dictionary<string, League>();
        private Dictionary<string, Team> teams = new Dictionary<string, Team>();

        public void ReadExcel()
        {

            try
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine($"파일을 찾을 수 없습니다. {path}");
                }

                // Excel 애플리케이션 시작
                excelApp = new Excel.Application();
                excelApp.Visible = false; // Excel 창을 보이지 않게
                excelApp.DisplayAlerts = false; // 경고 메시지 표시 안 함
                
                workBook = excelApp.Workbooks.Open(path);                       // 워크북 열기
                worksheetCount = workBook.Worksheets.Count; // 엑셀 첫번째 워크시트 가져오기
                TreeData(workBook, worksheetCount);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"엑셀을 읽을 수 없습니다.: {ex.Message}");
            }
            finally
            {   //메모리 해제
                workBook.Close(true);   // 워크북 닫기
                excelApp.Quit();        // 엑셀 어플리케이션 종료
                ReleaseExcelObject(workSheet);
                ReleaseExcelObject(workBook);
                ReleaseExcelObject(excelApp);
            }
        }

        private static void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }
        public void TreeData(Excel.Workbook workBooks, int worksheetCount)
        {
            workSheet = workBooks.Worksheets.get_Item(worksheetCount) as Excel.Worksheet;
            Excel.Range range = workSheet.UsedRange;    // 사용중인 셀 범위를 가져오기

            // 데이터 시작 행 (헤더 다음 행)
            int startRow = 2;
            // 사용된 범위의 마지막 행 (사용된 데이터의 전체 범위 기준)
            int rowCount = range.Rows.Count;
            // 사용된 범위의 마지막 열
            int colCount = range.Columns.Count;

            if (workSheet.Name == "Country")
            {

            }else if (workSheet.Name == "League")
            {

            }else if (workSheet.Name == "Team")
            {

            }else
            {

            }

            TreeData(workBooks, --worksheetCount);

        }
    }
}