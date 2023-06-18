using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DJ_X100_memory_writer.domain.MemoryChannnelConfig;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DJ_X100_memory_writer.Util
{
    internal class CsvUtils
    {
        HexUtils hexUtils = new HexUtils();
        public void ExportDataGridViewToCsv(DataGridView memoryChDataGridView, string filename)
        {
            try
            {
                // 出力ストリームを開き、UTF-8エンコーディングで書き込む
                using (StreamWriter writer = new StreamWriter(filename, false, Encoding.UTF8))
                {
                    IEnumerable<string> headerValues = memoryChDataGridView.Columns
                        .OfType<DataGridViewColumn>()
                        .OrderBy(column => column.DisplayIndex)
                        .Select(column => column.HeaderText);

                    string headerLine = string.Join(",", headerValues);
                    writer.WriteLine(headerLine);

                    foreach (DataGridViewRow row in memoryChDataGridView.Rows)
                    {
                        IEnumerable<string> cellValues = row.Cells
                            .OfType<DataGridViewCell>()
                            .OrderBy(cell => cell.OwningColumn.DisplayIndex)
                            .Select(cell =>
                            {
                                if (cell.Value == null)
                                {
                                    return "";
                                }
                                else
                                {
                                    return cell.Value.ToString();
                                }
                            });

                        string line = string.Join(",", cellValues);
                        writer.WriteLine(line);
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("他のプロセスによってファイルが開かれています" + ex.Message);
            }
        }




        public void ImportCsvToDataGridView(DataGridView memoryChDataGridView, string filename)
        {
            // DataGridViewの行をクリア
            memoryChDataGridView.Rows.Clear();

            // CSVファイルの内容を行のリストとして読み込む
            List<string> lines = File.ReadAllLines(filename).Skip(1).ToList(); // 最初の行はスキップ

            // エラーメッセージを格納するリストを作成
            List<string> errors = new List<string>();

            // 残りの行をDataGridViewの行に追加
            for (int i = 0; i < lines.Count; i++)
            {
                string[] cells = lines[i].Split(',');

                // 新しいDataGridViewの行を作成
                DataGridViewRow row = new DataGridViewRow();
                row.Height = memoryChDataGridView.RowTemplate.Height;
                row.CreateCells(memoryChDataGridView);

                for (int j = 0; j < cells.Length; j++)
                {
                    // nullおよび空の文字列値を無視
                    if (string.IsNullOrEmpty(cells[j]))
                    {
                        continue;
                    }
                    // コンボボックス列のデータを処理
                    else if (memoryChDataGridView.Columns[j] is DataGridViewComboBoxColumn)
                    {
                        DataGridViewComboBoxColumn comboBoxColumn = (DataGridViewComboBoxColumn)memoryChDataGridView.Columns[j];
                        if (comboBoxColumn.Items.Contains(cells[j]))
                        {
                            // セルの値がComboBoxの項目に含まれている場合のみ設定
                            row.Cells[j].Value = cells[j];
                        }
                        else
                        {
                            // もし値がComboBoxの項目に存在しない場合、エラーメッセージを追加し、セルの値をnullに設定
                            errors.Add($"行{i + 1}, 列{memoryChDataGridView.Columns[j].HeaderText}: '{cells[j]}' は選択項目に存在しません。");
                            row.Cells[j].Value = null;
                        }
                    }
                    else
                    {
                        row.Cells[j].Value = cells[j];
                    }
                }

                // 行をDataGridViewに追加
                memoryChDataGridView.Rows.Add(row);
            }
            // もしエラーがあった場合、ユーザーにメッセージを表示
            if (errors.Count > 0)
            {
                string errorMessage = "以下のエラーが発生しました:\r\n" + string.Join("\r\n", errors);
                ErrorForm errorForm = new ErrorForm(errorMessage);
                errorForm.ShowDialog();
            }
        }




        public void ExportDataGridViewToX100CmdCsv(DataGridView memoryChDataGridView, string filename)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filename, false, Encoding.UTF8))
                {
                    string[] columnOrder = new string[]
                    { Columns.MEMORY_NO.Id, Columns.FREQ.Id, Columns.MODE.Id, Columns.STEP.Id, Columns.MEMORY_NAME.Id,
                      Columns.OFFSET.Id, Columns.OFFSET_FREQ.Id, Columns.ATT.Id, Columns.SQL_MODE.Id, Columns.CTCSS.Id,
                      Columns.DCS.Id, Columns.BANK.Id, Columns.LAT.Id, Columns.LON.Id, Columns.SKIP.Id, "ext"
                    };

                    IEnumerable<string> headerValues = columnOrder
                        .Select(columnName => columnName == "ext" ? columnName : memoryChDataGridView.Columns[columnName].Name);

                    string headerLine = string.Join(",", headerValues);
                    writer.WriteLine(headerLine);

                    foreach (DataGridViewRow row in memoryChDataGridView.Rows)
                    {
                        if (row.Cells[Columns.FREQ.Id].Value == null) continue;


                        IEnumerable<string> cellValues = columnOrder
                            .Select(columnName =>
                            {
                                if (columnName == "ext")
                                {
                                    return ExternalData(row);
                                }
                                else
                                {
                                    var cell = row.Cells[columnName];
                                    return cell.Value == null ? "" : cell.Value.ToString();
                                }
                            });

                        string line = string.Join(",", cellValues);
                        writer.WriteLine(line);
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("ファイルが他のプロセスによって開かれています: " + ex.Message);
            }
        }

        private string ExternalData(DataGridViewRow row)
        {
            string externalStr = "0000e4000000e480000000000000000000000180018001800180010000800100008001000080000080008000807b1700";

            // REV_ECの処理
            string revEcValue = row.Cells[Columns.REV_EC.Id].Value?.ToString();
            externalStr = (revEcValue == "ON" ? "01" : "00") + externalStr.Substring(2);

            // REV_EC_FREQの処理
            Dictionary<string, string> revEcFreqValues = new Dictionary<string, string>
            {
                {"2500", "00"},
                {"2600", "01"},
                {"2700", "02"},
                {"2800", "03"},
                {"2900", "04"},
                {"3000", "05"},
                {"3100", "06"},
                {"3200", "07"},
                {"3300", "08"},
                {"3400", "09"},
                {"3500", "0A"},
            };

            string revEcFreqValue = row.Cells[Columns.REV_EC_FREQ.Id].Value?.ToString() ?? "2500";

            // revEcFreqValueがDictionaryに存在しない場合は"00"を使います
            string replacementValue = revEcFreqValues.ContainsKey(revEcFreqValue) ? revEcFreqValues[revEcFreqValue] : "00";

            externalStr = externalStr.Substring(0, 2) + replacementValue + externalStr.Substring(4);








            // T98_WCの処理
            externalStr = ReplaceCellValueWithModeAndWC(externalStr, row, "T98", 4);
            // T102_B54_WCの処理
            externalStr = ReplaceCellValueWithModeAndWC(externalStr, row, "T102_B54", 12);




            // T61_WCの処理
            // T61_WCの処理
            Dictionary<string, int> modeToIndexMap = new Dictionary<string, int>
            {
                { "T61_typ1", 16 },
                { "T61_typ2", 20 },
                { "T61_typ3", 24 },
                { "T61_typ4", 28 },
                { "T61_typx", 32 }
            };
            // モード値を取得
            string modeValue = row.Cells[Columns.MODE.Id].Value != null ? row.Cells[Columns.MODE.Id].Value.ToString() : null;
            // wc値を取得
            string wcValue = row.Cells[Columns.WC.Id].Value != null ? row.Cells[Columns.WC.Id].Value.ToString() : "0000";

            if (modeValue != null && modeToIndexMap.ContainsKey(modeValue))
            {
                string replaceValue = hexUtils.SwapEndianHex(wcValue);
                int index = modeToIndexMap[modeValue];
                externalStr = externalStr.Remove(index, 4);
                externalStr = externalStr.Insert(index, replaceValue);
            }








            // T98_UCの処理
            if (row.Cells[Columns.MODE.Id].Value != null && row.Cells[Columns.MODE.Id].Value.ToString() == "T98")
            {
                string ucValue = row.Cells[Columns.UC.Id].Value != null ? row.Cells[Columns.UC.Id].Value.ToString() : null;
                if (ucValue != null)
                {
                    string replaceValue = (ucValue == "0") ? "0180" : hexUtils.SwapEndianHex(ucValue);
                    externalStr = externalStr.Remove(36, 4);
                    externalStr = externalStr.Insert(36, replaceValue);
                }
                else
                {
                    externalStr = externalStr.Remove(36, 4);
                    externalStr = externalStr.Insert(36, "0180");
                }
            }

            // T102_B54_UCの処理
            if (row.Cells[Columns.MODE.Id].Value != null && row.Cells[Columns.MODE.Id].Value.ToString() == "T102_B54")
            {
                string ucValue = row.Cells[Columns.UC.Id].Value != null ? row.Cells[Columns.UC.Id].Value.ToString() : null;
                if (ucValue != null)
                {
                    string replaceValue = (ucValue == "0") ? "0180" : hexUtils.SwapEndianHex(ucValue);
                    externalStr = externalStr.Remove(40, 4);
                    externalStr = externalStr.Insert(40, replaceValue);
                }
                else
                {
                    externalStr = externalStr.Remove(40, 4);
                    externalStr = externalStr.Insert(40, "0180");
                }
            }


            // T98_ECの処理
            if (row.Cells[Columns.MODE.Id].Value != null && row.Cells[Columns.MODE.Id].Value.ToString() == "T98")
            {
                string ecValue = row.Cells[Columns.EC.Id].Value != null ? row.Cells[Columns.EC.Id].Value.ToString() : null;
                if (ecValue != null)
                {
                    string replaceValue = (ecValue == "0") ? "0180" : hexUtils.SwapEndianHex(ecValue);
                    externalStr = externalStr.Remove(44, 4);
                    externalStr = externalStr.Insert(44, replaceValue);
                }
                else
                {
                    externalStr = externalStr.Remove(44, 4);
                    externalStr = externalStr.Insert(44, "0180");
                }
            }
            return externalStr;
        }

        private string ReplaceCellValueWithModeAndWC(string externalStr, DataGridViewRow row, string mode, int position)
        {
            if (row.Cells[Columns.MODE.Id].Value != null && row.Cells[Columns.MODE.Id].Value.ToString() == mode)
            {
                // WCの値が存在しない場合は "0000" にします
                string wcValue = row.Cells[Columns.WC.Id].Value != null ? row.Cells[Columns.WC.Id].Value.ToString() : "0000";

                // AUTOだった場合は "0180" に、それ以外は16進数として解釈してエンディアンを反転します
                string replaceValue = (wcValue == "AUTO") ? "0180" : hexUtils.SwapEndianHex(wcValue);

                // 指定した位置の値を置換します
                externalStr = externalStr.Remove(position, 4);
                externalStr = externalStr.Insert(position, replaceValue);
            }

            return externalStr;
        }
    }
}
