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

namespace OrganizationTreeForm.Utils
{
    public static class ExcelHelper
    {
        private static string path = $"{AppDomain.CurrentDomain.BaseDirectory}Data\\Soccer.xlsx";

        public static List<Country> ReadExcel()
        {
            var countries = new Dictionary<string, Country>();
            var leagues = new Dictionary<string, League>();
            var teams = new Dictionary<string, Team>();

            Excel.Application excelApp = null;
            Excel.Workbooks workbooks = null;
            Excel.Workbook workbook = null;
            Excel.Sheets sheets = null;
            Excel.Worksheet worksheet = null;
            Excel.Range usedRange = null;

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

                workbooks = excelApp.Workbooks;
                workbook = workbooks.Open(path); // 파일 열기

                sheets = workbook.Sheets;
                worksheet = (Excel.Worksheet)sheets[1]; // 첫 번째 워크시트 (1-indexed)

                usedRange = worksheet.UsedRange; // 사용된 셀 범위 가져오기

                // 데이터 시작 행 (헤더 다음 행)
                int startRow = 2;
                // 사용된 범위의 마지막 행 (사용된 데이터의 전체 범위 기준)
                int rowCount = usedRange.Rows.Count;
                // 사용된 범위의 마지막 열
                int colCount = usedRange.Columns.Count;

                for (int r = startRow; r <= rowCount; r++)
                {
                    // 셀 값 읽기 (Cells[row, column]은 1-indexed)
                    // GetValue2()는 다양한 타입의 셀 값을 가져올 수 있으나, object로 반환
                    // ToString()으로 변환 후 안전하게 처리
                    string uidStr = (usedRange.Cells[r, 1] as Excel.Range).Value2.ToString(); // Column A
                    string countryName = (usedRange.Cells[r, 2] as Excel.Range).Value2.ToString(); // Column B
                    string countryAddress = (usedRange.Cells[r, 3] as Excel.Range).Value2.ToString(); // Column C
                    string leagueName = (usedRange.Cells[r, 4] as Excel.Range).Value2.ToString(); // Column D
                    string teamName = (usedRange.Cells[r, 5] as Excel.Range)?.Value2.ToString(); // Column E
                    string playerName = (usedRange.Cells[r, 6] as Excel.Range).Value2.ToString(); // Column F
                    string playerNumberStr = (usedRange.Cells[r, 7] as Excel.Range).Value2.ToString(); // Column G
                    string playerPosition = (usedRange.Cells[r, 8] as Excel.Range).Value2.ToString(); // Column H

                    if (string.IsNullOrEmpty(countryName) || string.IsNullOrEmpty(leagueName) ||
                        string.IsNullOrEmpty(teamName) || string.IsNullOrEmpty(playerName))
                    {
                        continue; // 필수 데이터 누락 시 건너뛰기
                    }

                    int playerNumber = 0;
                    if (!string.IsNullOrEmpty(playerNumberStr) && int.TryParse(playerNumberStr, out int parsedPlayerNumber))
                    {
                        playerNumber = parsedPlayerNumber;
                    }

                    // ------------------ 트리 빌드 로직 (이전과 동일) ------------------
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
                ReleaseExcelObject(usedRange);
                ReleaseExcelObject(worksheet);
                ReleaseExcelObject(sheets);
                ReleaseExcelObject(workbooks);
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