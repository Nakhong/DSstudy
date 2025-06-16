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
        private Dictionary<string, Player> players = new Dictionary<string, Player>();

        public void ReadExcel()
        {

            try
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine($"파일을 찾을 수 없습니다. {path}");
                }

                // Excel 애플리케이션 시작
                excelApp = new Excel.Application
                {
                    Visible = false, // Excel 창을 보이지 않게
                    DisplayAlerts = false // 경고 메시지 표시 안 함
                };

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

        private void ReleaseExcelObject(object obj)
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
        /// <summary>
        /// 재귀적으로 엑셀 워크시트를 파싱하여 트리 데이터를 구성합니다.
        /// </summary>
        /// <param name="workBooks">현재 워크북</param>
        /// <param name="currentWorksheetIndex">현재 파싱할 워크시트 인덱스 (1부터 시작)</param>
        public void TreeData(Excel.Workbook workBooks, int currentWorksheetIndex)
        {
            // 재귀 종료 조건: 유효한 워크시트 인덱스가 아닐 경우
            if (currentWorksheetIndex < 1) // 엑셀 시트 인덱스는 보통 1부터 시작
            {
                return;
            }

            Excel.Worksheet currentWorksheet = null;
            Excel.Range range = null;

            try
            {
                currentWorksheet = workBooks.Worksheets.get_Item(currentWorksheetIndex) as Excel.Worksheet;
                if (currentWorksheet == null)
                {
                    Console.WriteLine($"경고: {currentWorksheet.Name}에 해당하는 워크시트를 찾을 수 없습니다.");
                    // 다음 시트로 재귀 호출 계속
                    TreeData(workBooks, currentWorksheetIndex - 1);
                    return;
                }

                range = currentWorksheet.UsedRange;
                int startRow = 2; // 1행은 헤더

                string sheetName = currentWorksheet.Name;

                if (sheetName == "Player")
                {
                    ParsePlayersSheet(range, startRow);
                }
                else if (sheetName == "Team")
                {
                    ParseTeamsSheet(range, startRow);
                }
                else if (sheetName == "League")
                {
                    ParseLeaguesSheet(range, startRow);
                }
                else if (sheetName == "Country")
                {
                    ParseCountriesSheet(range, startRow);
                }
                else
                {
                    Console.WriteLine($"경고: 알 수 없는 시트 이름 '{sheetName}' (인덱스: {currentWorksheetIndex})");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TreeData 처리 중 오류 발생 (시트 인덱스 {currentWorksheetIndex}): {ex.Message}");
            }
            finally
            {
                ReleaseExcelObject(range);
                ReleaseExcelObject(currentWorksheet); // 현재 워크시트 객체 해제
            }

            // 다음 워크시트로 재귀 호출
            TreeData(workBooks, currentWorksheetIndex - 1);
        }

        public void ParseCountriesSheet(Excel.Range range, int startRow)
        {
            int rowCount = range.Rows.Count;
            for (int r = startRow; r <= rowCount; r++)
            {
                try
                {
                    string countryName = GetCellValue(range.Cells[r, 2]); // B열: 국가 이름
                    string countryAddress = GetCellValue(range.Cells[r, 3]); // C열: 국가 주소

                    if (!string.IsNullOrEmpty(countryName) && !countries.ContainsKey(countryName))
                    {
                        Country currentCountry = new Country
                        {
                            CountryName = countryName,
                            CountryAddress = countryAddress,
                            Leagues = new List<League>()
                        };
                        countries.Add(countryName, currentCountry);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Country 시트 {r}행 파싱 오류: {ex.Message}");
                }
            }
        }

        public void ParseLeaguesSheet(Excel.Range range, int startRow)
        {
            // 사용된 범위의 마지막 행 (사용된 데이터의 전체 범위 기준)
            int rowCount = range.Rows.Count;
            // 사용된 범위의 마지막 열
            int colCount = range.Columns.Count;

            for (int r = startRow; r <= rowCount; r++)
            {
                // 셀 값 읽기 (Cells[row, column]
                // GetValue2()는 셀의 서식이 적용되지 않은 값을 반환한다. GetValue()는 셀의 서식이 적용된 값을 반환
                // ToString()으로 변환 후 스트링으로 처리
                string leagueName = (range.Cells[r, 1] as Excel.Range).Value.ToString(); // Column A
                string parentCountry = (range.Cells[r, 2] as Excel.Range).Value.ToString(); // Column B
                string level = (range.Cells[r, 3] as Excel.Range).Value.ToString(); // Column C
                string uidStr = (range.Cells[r, 4] as Excel.Range).Value.ToString(); // Column D

                League currentLeague;
                if (true)
                {
                    currentLeague = new League
                    {
                        LeagueName = leagueName,
                        ParentCountry = countries[parentCountry]
                    };
                    leagues.Add(leagueName,currentLeague);
                    countries[parentCountry].Leagues.Add(currentLeague);
                }
            }

        }

        public void ParseTeamsSheet(Excel.Range range, int startRow)
        {
            // 사용된 범위의 마지막 행 (사용된 데이터의 전체 범위 기준)
            int rowCount = range.Rows.Count;
            // 사용된 범위의 마지막 열
            int colCount = range.Columns.Count;

            for (int r = startRow; r <= rowCount; r++)
            {
                // 셀 값 읽기 (Cells[row, column]
                // GetValue2()는 셀의 서식이 적용되지 않은 값을 반환한다. GetValue()는 셀의 서식이 적용된 값을 반환
                // ToString()으로 변환 후 스트링으로 처리
                string teamName = (range.Cells[r, 1] as Excel.Range).Value.ToString(); // Column A
                string parentLeague = (range.Cells[r, 2] as Excel.Range).Value.ToString(); // Column B
                string level = (range.Cells[r, 3] as Excel.Range).Value.ToString(); // Column C
                string uidStr = (range.Cells[r, 4] as Excel.Range).Value.ToString(); // Column D

                Team currentTeam;
                if (true)
                {
                    currentTeam = new Team
                    {
                        TeamName = teamName,
                        ParentLeague = leagues[parentLeague]
                    };
                    teams.Add(teamName, currentTeam);
                    leagues[parentLeague].Teams.Add(currentTeam);
                }
            }
        }

        public void ParsePlayersSheet(Excel.Range range, int startRow)
        {
            // 사용된 범위의 마지막 행 (사용된 데이터의 전체 범위 기준)
            int rowCount = range.Rows.Count;
            // 사용된 범위의 마지막 열
            int colCount = range.Columns.Count;

            for (int r = startRow; r <= rowCount; r++)
            {
                // 셀 값 읽기 (Cells[row, column]
                // GetValue2()는 셀의 서식이 적용되지 않은 값을 반환한다. GetValue()는 셀의 서식이 적용된 값을 반환
                // ToString()으로 변환 후 스트링으로 처리
                string playerName = (range.Cells[r, 1] as Excel.Range).Value.ToString(); // Column A
                string playerNumber = (range.Cells[r, 2] as Excel.Range).Value.ToString(); // Column B
                string playerPosition = (range.Cells[r, 1] as Excel.Range).Value.ToString(); // Column A
                string parentTeam = (range.Cells[r, 2] as Excel.Range).Value.ToString(); // Column B
                string level = (range.Cells[r, 3] as Excel.Range).Value.ToString(); // Column C
                string uidStr = (range.Cells[r, 4] as Excel.Range).Value.ToString(); // Column D

                Player currentPlayer = new Player
                {
                    PlayerName = playerName,
                    PlayerNumber = playerNumber,
                    PlayerPosition = playerPosition,
                    ParentTeam = teams[parentTeam]
                };
                teams[parentTeam].Players.Add(currentPlayer);
            }

        }

        // 셀 값을 안전하게 가져오는 헬퍼 메서드
        private string GetCellValue(Excel.Range cell)
        {
            if (cell?.Value2 != null)
            {
                return cell.Value2.ToString();
            }
            return string.Empty;
        }
    }
}