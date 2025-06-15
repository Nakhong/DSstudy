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
    public static class ExcelHelper
    {
        private static string path = $"{AppDomain.CurrentDomain.BaseDirectory}Data\\Soccer.xlsx";

        public static List<Country> ReadExcel()
        {
            Dictionary<string, Country> countries = new Dictionary<string, Country>();
            Dictionary<string, League> leagues = new Dictionary<string, League>();
            Dictionary<string, Team> teams = new Dictionary<string, Team>();

             Excel.Application excelApp = null;
             Excel.Workbook workBook = null;
             Excel.Worksheet workSheet = null;

            try
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine($"파일을 찾을 수 없습니다. {path}");
                    return null;
                }

                // Excel 애플리케이션 시작
                excelApp = new Excel.Application();
                excelApp.Visible = false; // Excel 창을 보이지 않게
                excelApp.DisplayAlerts = false; // 경고 메시지 표시 안 함

                excelApp = new Excel.Application();                             // 엑셀 어플리케이션 생성
                workBook = excelApp.Workbooks.Open(path);                       // 워크북 열기
                workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet; // 엑셀 첫번째 워크시트 가져오기
                Excel.Range range = workSheet.UsedRange;    // 사용중인 셀 범위를 가져오기

                // 데이터 시작 행 (헤더 다음 행)
                int startRow = 2;
                // 사용된 범위의 마지막 행 (사용된 데이터의 전체 범위 기준)
                int rowCount = range.Rows.Count;
                // 사용된 범위의 마지막 열
                int colCount = range.Columns.Count;

                for (int r = startRow; r <= rowCount; r++)
                {
                    // 셀 값 읽기 (Cells[row, column]은 1-indexed)
                    // GetValue2()는 셀의 서식이 적용되지 않은 값을 반환한다. GetValue()는 셀의 서식이 적용된 값을 반환
                    // ToString()으로 변환 후 스트링으로 처리
                    string uidStr = (range.Cells[r, 1] as Excel.Range).Value.ToString(); // Column A
                    string countryName = (range.Cells[r, 2] as Excel.Range).Value.ToString(); // Column B
                    string countryAddress = (range.Cells[r, 3] as Excel.Range).Value.ToString(); // Column C
                    string leagueName = (range.Cells[r, 4] as Excel.Range).Value.ToString(); // Column D
                    string teamName = (range.Cells[r, 5] as Excel.Range).Value.ToString(); // Column E
                    string playerName = (range.Cells[r, 6] as Excel.Range).Value.ToString(); // Column F
                    string playerNumberStr = (range.Cells[r, 7] as Excel.Range).Value.ToString(); // Column G
                    string playerPosition = (range.Cells[r, 8] as Excel.Range).Value.ToString(); // Column H

                    int playerNumber = 0;
                    if (!string.IsNullOrEmpty(playerNumberStr) && int.TryParse(playerNumberStr, out int parsedPlayerNumber))
                    {
                        playerNumber = parsedPlayerNumber;
                    }

                    Country currentCountry;
                    if (!countries.TryGetValue(countryName, out currentCountry))
                    {
                        currentCountry = new Country
                        {
                            CountryName = countryName,
                            CountryAddress = countryAddress,
                        };
                        countries.Add(countryName, currentCountry);
                    }

                    string leagueKey = $"{countryName}_{leagueName}";
                    League currentLeague;
                    if (!leagues.TryGetValue(leagueKey, out currentLeague))
                    {
                        currentLeague = new League
                        {
                            LeagueName = leagueName,
                            ParentCountry = currentCountry
                        };
                        leagues.Add(leagueKey, currentLeague);
                        currentCountry.Leagues.Add(currentLeague);
                    }

                    string teamKey = $"{leagueName}_{teamName}";

                    Team currentTeam;
                    if (!teams.TryGetValue(teamKey, out currentTeam))
                    {
                        currentTeam = new Team
                        {
                            TeamName = teamName,
                            ParentLeague = currentLeague
                        };
                        teams.Add(teamKey, currentTeam);
                        currentLeague.Teams.Add(currentTeam);
                    }

                    Player currentPlayer = new Player
                    {
                        PlayerName = playerName,
                        PlayerNumber = playerNumber,
                        PlayerPosition = playerPosition,
                        ParentTeam = currentTeam
                    };
                    currentTeam.Players.Add(currentPlayer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"엑셀을 읽을 수 없습니다.: {ex.Message}");
                return null;
            }
            finally
            {   //메모리 해제
                workBook.Close(true);   // 워크북 닫기
                excelApp.Quit();        // 엑셀 어플리케이션 종료
                ReleaseExcelObject(workSheet);
                ReleaseExcelObject(workBook);
                ReleaseExcelObject(excelApp);
            }
            return countries.Values.ToList();
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
    }

}