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
            int rowCount = range.Rows.Count;
            for (int r = startRow; r <= rowCount; r++)
            {
                try
                {
                    string leagueName = GetCellValue(range.Cells[r, 1]); // A열: 리그 이름
                    string parentCountryName = GetCellValue(range.Cells[r, 2]); // B열: 소속 국가 이름

                    if (!string.IsNullOrEmpty(leagueName) && !leagues.ContainsKey(leagueName))
                    {
                        League currentLeague = new League
                        {
                            LeagueName = leagueName,
                            Teams = new List<Team>()
                        };
                        leagues.Add(leagueName, currentLeague);

                        // 부모 국가 존재 여부 확인
                        if (countries.TryGetValue(parentCountryName, out Country parentCountry))
                        {
                            currentLeague.ParentCountry = parentCountry;
                            parentCountry.Leagues.Add(currentLeague);
                        }
                        else
                        {
                            // 부모 국가가 아직 파싱되지 않았거나 존재하지 않음
                            Console.WriteLine($"경고: 리그 '{leagueName}'의 부모 국가 '{parentCountryName}'를 찾을 수 없습니다. (League 시트 {r}행)");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"League 시트 {r}행 파싱 오류: {ex.Message}");
                }
            }
        }

        public void ParseTeamsSheet(Excel.Range range, int startRow)
        {
            int rowCount = range.Rows.Count;
            for (int r = startRow; r <= rowCount; r++)
            {
                try
                {
                    string teamName = GetCellValue(range.Cells[r, 1]); // A열: 팀 이름
                    string parentLeagueName = GetCellValue(range.Cells[r, 2]); // B열: 소속 리그 이름

                    if (!string.IsNullOrEmpty(teamName) && !teams.ContainsKey(teamName))
                    {
                        Team currentTeam = new Team
                        {
                            TeamName = teamName,
                            Players = new List<Player>()
                        };
                        teams.Add(teamName, currentTeam);

                        // 부모 리그 존재 여부 확인
                        if (leagues.TryGetValue(parentLeagueName, out League parentLeague))
                        {
                            currentTeam.ParentLeague = parentLeague;
                            parentLeague.Teams.Add(currentTeam);
                        }
                        else
                        {
                            Console.WriteLine($"경고: 팀 '{teamName}'의 부모 리그 '{parentLeagueName}'를 찾을 수 없습니다. (Team 시트 {r}행)");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Team 시트 {r}행 파싱 오류: {ex.Message}");
                }
            }
        }

        public void ParsePlayersSheet(Excel.Range range, int startRow)
        {
            int rowCount = range.Rows.Count;
            for (int r = startRow; r <= rowCount; r++)
            {
                try
                {
                    string playerName = GetCellValue(range.Cells[r, 1]);       // A열: 선수 이름
                    string playerNumberStr = GetCellValue(range.Cells[r, 2]);  // B열: 등번호
                    string playerPosition = GetCellValue(range.Cells[r, 3]);   // C열: 포지션
                    string parentTeamName = GetCellValue(range.Cells[r, 4]);   // D열: 소속 팀 이름

                    if (string.IsNullOrEmpty(playerNumberStr))
                    {
                        Console.WriteLine($"경고: 선수 '{playerName}'의 등번호 '{playerNumberStr}'가 유효한 숫자가 아닙니다. 기본값 0으로 설정됩니다. (Player 시트 {r}행)");
                    }

                    if (!string.IsNullOrEmpty(playerName) && !players.ContainsKey(playerName))
                    {
                        Player currentPlayer = new Player
                        {
                            PlayerName = playerName,
                            PlayerNumber = playerNumberStr,
                            PlayerPosition = playerPosition
                        };
                        players.Add(playerName, currentPlayer);

                        // 부모 팀 존재 여부 확인
                        if (teams.TryGetValue(parentTeamName, out Team parentTeam))
                        {
                            currentPlayer.ParentTeam = parentTeam;
                            parentTeam.Players.Add(currentPlayer);
                        }
                        else
                        {
                            Console.WriteLine($"경고: 선수 '{playerName}'의 부모 팀 '{parentTeamName}'을(를) 찾을 수 없습니다. (Player 시트 {r}행)");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Player 시트 {r}행 파싱 오류: {ex.Message}");
                }
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