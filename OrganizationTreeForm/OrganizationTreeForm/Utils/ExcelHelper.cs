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
        string path = $"{AppDomain.CurrentDomain.BaseDirectory}Data\\Soccer.xlsx";
        Excel.Application excelApp = null;
        Excel.Workbook workBook = null;
        Excel.Worksheet workSheet = null;
        int worksheetCount;

        Dictionary<string, Country> countries = new Dictionary<string, Country>();
        Dictionary<string, League> leagues = new Dictionary<string, League>();
        Dictionary<string, Team> teams = new Dictionary<string, Team>();

        public Dictionary<string, Country> ReadExcel()
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
            return countries;
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
        /// 재귀적으로 엑셀 워크시트를 파싱
        /// </summary>
        /// <param name="workBooks">현재 워크북</param>
        /// <param name="currentWorksheetIndex">현재 파싱할 워크시트 인덱스</param>
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
                ReleaseExcelObject(currentWorksheet);
            }

            // 다음 워크시트로 재귀 호출
            TreeData(workBooks, currentWorksheetIndex - 1);
        }
        // Country sheet 
        private void ParseCountriesSheet(Excel.Range range, int startRow)
        {
            for (int row = startRow; row <= range.Rows.Count; row++)
            {
                string name = range.Cells[row, 2].Value2.ToString().Trim();
                string address = range.Cells[row, 3].Value2.ToString().Trim();
                string level = range.Cells[row, 4].Value2.ToString().Trim();

                // 이름이 없거나 이미 등록된 Country면 건너뛰기
                if (string.IsNullOrEmpty(name) || countries.ContainsKey(name))
                {
                    continue;
                }

                countries[name] = new Country
                {
                    CountryName = name,
                    CountryAddress = address,
                    Level = level
                };
            }
        }

        // League sheet 
        private void ParseLeaguesSheet(Excel.Range range, int startRow)
        {
            for (int row = startRow; row <= range.Rows.Count; row++)
            {
                string leagueName = range.Cells[row, 2].Value2.ToString().Trim();
                string parentCountryName = range.Cells[row, 3].Value2.ToString().Trim();
                string level = range.Cells[row, 4].Value2.ToString().Trim();

                if (string.IsNullOrEmpty(leagueName) || string.IsNullOrEmpty(parentCountryName))
                {
                    continue;
                }

                if (!countries.TryGetValue(parentCountryName, out var parentCountry))
                {
                    Console.WriteLine($"[경고] League의 상위 Country '{parentCountryName}'를 찾을 수 없음");
                    continue;
                }

                var league = new League
                {
                    LeagueName = leagueName,
                    Level = level
                };

                parentCountry.Leagues.Add(league);
                leagues[leagueName] = league;
            }
        }

        // Team sheet 
        private void ParseTeamsSheet(Excel.Range range, int startRow)
        {
            for (int row = startRow; row <= range.Rows.Count; row++)
            {
                string teamName = range.Cells[row, 2].Value2.ToString().Trim();
                string parentLeagueName = range.Cells[row, 3].Value2.ToString().Trim();
                string level = range.Cells[row, 4].Value2.ToString().Trim();

                if (string.IsNullOrEmpty(teamName) || string.IsNullOrEmpty(parentLeagueName))
                {
                    continue;
                };

                if (!leagues.TryGetValue(parentLeagueName, out var parentLeague))
                {
                    Console.WriteLine($"[경고] Team의 상위 League '{parentLeagueName}'를 찾을 수 없음");
                    continue;
                }

                var team = new Team
                {
                    TeamName = teamName,
                    Level = level
                };

                parentLeague.Teams.Add(team);
                teams[teamName] = team;
            }
        }

        // Player sheet 
        private void ParsePlayersSheet(Excel.Range range, int startRow)
        {
            for (int row = startRow; row <= range.Rows.Count; row++)
            {
                string playerName = range.Cells[row, 2].Value2.ToString().Trim();
                string position = range.Cells[row, 3].Value2.ToString().Trim();
                string number = range.Cells[row, 4].Value2.ToString().Trim();
                string parentTeamName = range.Cells[row, 5].Value2.ToString().Trim();
                string level = range.Cells[row, 6].Value2.ToString().Trim();
                string PlayerFoot = range.Cells[row, 7].Value2.ToString().Trim();

                if (string.IsNullOrEmpty(playerName) || string.IsNullOrEmpty(parentTeamName)) {
                    continue;
                }

                if (!teams.TryGetValue(parentTeamName, out var parentTeam))
                {
                    Console.WriteLine($"Player의 상위 Team '{parentTeamName}'를 찾을 수 없습니다.");
                    continue;
                }

                var player = new Player
                {
                    PlayerName = playerName,
                    PlayerNumber = number,
                    PlayerPosition = position,
                    Level = level,
                    PlayerFoot = PlayerFoot
                };

                parentTeam.Players.Add(player);
            }
        }

    }
}