using System.Data;
using System.Globalization;
using System.Text;
using static DJ_X100_memory_writer.domain.MemoryChannnelConfig;

namespace DJ_X100_memory_writer.Service
{
    internal class CsvFileService
    {
        ExternalDataService externalData = new ExternalDataService();

        string[] columnOrder = new string[]
        {
            Columns.MEMORY_NO.Id, Columns.FREQ.Id, Columns.MODE.Id, Columns.STEP.Id, Columns.MEMORY_NAME.Id,
            Columns.OFFSET.Id, Columns.SHIFT_FREQ.Id, Columns.ATT.Id, Columns.SQL_MODE.Id, Columns.CTCSS.Id,
            Columns.DCS.Id, Columns.BANK.Id, Columns.LAT.Id, Columns.LON.Id, Columns.SKIP.Id, "ext"
        };


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

                    // j=1の列の入力チェック
                    if (j == 1)
                    {
                        string result = ValidateAndFormatDecimalCell(cells[j], i, memoryChDataGridView.Columns[j].HeaderText);

                        if (result.Contains("は範囲外です。") || result.Contains("は無効な値です。"))
                        {
                            errors.Add(result);
                            row.Cells[j].Value = null;
                            continue;
                        }
                        else
                        {
                            row.Cells[j].Value = result;
                        }
                    }


                    // "No" 列および12列目を3桁0埋めで設定
                    if ((j == 0 || j == 12) && int.TryParse(cells[j], out int no))
                    {
                        cells[j] = String.Format("{0:D3}", no);
                    }

                    // コンボボックス列のデータを処理
                    if (memoryChDataGridView.Columns[j] is DataGridViewComboBoxColumn)
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

            // 不足分の行を追加
            int rowsToAdd = 999 - memoryChDataGridView.Rows.Count;
            for (int i = 0; i < rowsToAdd; i++)
            {
                // 新しいDataGridViewの行を作成
                DataGridViewRow row = new DataGridViewRow();
                row.Height = memoryChDataGridView.RowTemplate.Height;
                row.CreateCells(memoryChDataGridView);

                // "No" 列を3桁0埋めで設定
                row.Cells[0].Value = String.Format("{0:D3}", memoryChDataGridView.Rows.Count + 1);

                // 行をDataGridViewに追加
                memoryChDataGridView.Rows.Add(row);
            }

            // エラーがあった場合、ユーザーにメッセージを表示
            if (errors.Count > 0)
            {
                string errorMessage = "以下のエラーが発生しました:\r\n" + string.Join("\r\n", errors);
                ErrorForm errorForm = new ErrorForm(errorMessage);
                errorForm.ShowDialog();
            }
        }

        private string ValidateAndFormatDecimalCell(string cellValue, int rowNumber, string columnName)
        {
            if (decimal.TryParse(cellValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value))
            {
                string decimalPart = cellValue.Contains(".") ? cellValue.Split('.')[1] : "";

                if (value < 20m || value > 470m || decimalPart.Length > 6)
                {
                    return $"行{rowNumber + 1}, 列{columnName}: '{cellValue}' は範囲外です。";
                }
                else
                {
                    return value.ToString("F6", CultureInfo.InvariantCulture);
                }
            }
            else
            {
                return $"行{rowNumber + 1}, 列{columnName}: '{cellValue}' は無効な値です。";
            }
        }

        public void ExportDataGridViewToX100CmdCsv(DataGridView memoryChDataGridView, string filename)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filename, false, Encoding.UTF8))
                {
                    IEnumerable<string> headerValues = columnOrder
                        .Select(columnName => columnName == "ext" ? columnName : memoryChDataGridView.Columns[columnName].Name);

                    string headerLine = string.Join(",", headerValues);
                    writer.WriteLine(headerLine);

                    foreach (DataGridViewRow row in memoryChDataGridView.Rows)
                    {
                        IEnumerable<string> cellValues = columnOrder
                            .Select(columnName =>
                            {
                                if (columnName == "ext")
                                {
                                    return externalData.ExternalData(row);
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
                MessageBox.Show("エラー: " + ex.Message);
            }
        }

        public void ImportX100cmdCsvToDataGridView(DataGridView memoryChDataGridView)
        {
            string[] lines = File.ReadAllLines(".\\x100cmd_temp_export.csv");
            DataTable dataTable = new DataTable();

            // ヘッダー行から列名を取得してDataTableに追加
            string[] columnNames = lines[0].Split(',');
            foreach (string columnName in columnNames)
            {
                dataTable.Columns.Add(columnName);
            }

            // データ行を追加
            for (int i = 1; i < lines.Length; i++)
            {
                dataTable.Rows.Add(lines[i].Split(','));
            }

            memoryChDataGridView.Rows.Clear();

            // 旧のDataTableから新のDataTableにデータをコピー
            foreach (DataRow row in dataTable.Rows)
            {

                if (row["ext"] == null || row["ext"].ToString().Equals(""))
                {
                    memoryChDataGridView.Rows.Add(
                        row[Columns.MEMORY_NO.Id],
                        row[Columns.FREQ.Id],
                        row[Columns.MEMORY_NAME.Id],
                        row[Columns.MODE.Id],
                        row[Columns.BANK.Id],
                        row[Columns.SKIP.Id],
                        row[Columns.STEP.Id],
                        row[Columns.OFFSET.Id],
                        row[Columns.SHIFT_FREQ.Id],
                        row[Columns.ATT.Id],
                        row[Columns.SQL_MODE.Id],
                        row[Columns.CTCSS.Id],
                        row[Columns.DCS.Id],
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        row[Columns.LON.Id],
                        row[Columns.LAT.Id]
                );
                    return;
                }

                DataTable extTable = DecodeExternalData(row["ext"].ToString(), row[Columns.MODE.Id].ToString());

                memoryChDataGridView.Rows.Add(

                row[Columns.MEMORY_NO.Id],
                row[Columns.FREQ.Id],
                row[Columns.MEMORY_NAME.Id],
                row[Columns.MODE.Id],
                row[Columns.BANK.Id],
                row[Columns.SKIP.Id],
                row[Columns.STEP.Id],
                row[Columns.OFFSET.Id],
                row[Columns.SHIFT_FREQ.Id],
                row[Columns.ATT.Id],
                row[Columns.SQL_MODE.Id],
                row[Columns.CTCSS.Id],
                row[Columns.DCS.Id],


                extTable.Rows[0][Columns.REV_EC.Id],
                extTable.Rows[0][Columns.REV_EC_FREQ.Id],
                extTable.Rows[0][Columns.UC.Id],
                extTable.Rows[0][Columns.GC.Id],
                extTable.Rows[0][Columns.EC.Id],
                extTable.Rows[0][Columns.WC.Id],
                "", // extTable.Rows[0][Columns.T61_LON.Id],
                "", // extTable.Rows[0][Columns.T61_LAT.Id],
                "", // extTable.Rows[0][Columns.DMR_SLOT.Id],
                "", // extTable.Rows[0][Columns.DMR_CC.Id],
                "", // extTable.Rows[0][Columns.DMR_GC.Id],
                "", // extTable.Rows[0][Columns.DSTAR_CS.Id],
                "", // extTable.Rows[0][Columns.C4FM_DG.Id],

                row[Columns.LON.Id],
                row[Columns.LAT.Id]
                );
            }

        }

        public DataTable DecodeExternalData(string externalDataStr, string mode)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(Columns.REV_EC.Id);
            dataTable.Columns.Add(Columns.REV_EC_FREQ.Id);
            dataTable.Columns.Add(Columns.UC.Id);
            dataTable.Columns.Add(Columns.GC.Id);
            dataTable.Columns.Add(Columns.EC.Id);
            dataTable.Columns.Add(Columns.WC.Id);
            dataTable.Columns.Add(Columns.T61_LON.Id);
            dataTable.Columns.Add(Columns.T61_LAT.Id);
            dataTable.Columns.Add(Columns.DMR_SLOT.Id);
            dataTable.Columns.Add(Columns.DMR_CC.Id);
            dataTable.Columns.Add(Columns.DMR_GC.Id);
            dataTable.Columns.Add(Columns.DSTAR_CS.Id);
            dataTable.Columns.Add(Columns.C4FM_DG.Id);

            DataRow row = dataTable.NewRow();

            row[Columns.REV_EC.Id] = externalData.DecodeRevEcOnOff(externalDataStr);
            row[Columns.REV_EC_FREQ.Id] = externalData.DecodeRevEcFreq(externalDataStr);
            row[Columns.UC.Id] = externalData.DecodeT98AndT102AndB54Uc(externalDataStr, mode);
            row[Columns.GC.Id] = externalData.DecodeT98AndT102AndB54Gc(externalDataStr, mode);
            row[Columns.EC.Id] = externalData.DecodeT98AndT102AndB54Uc(externalDataStr, mode);
            row[Columns.WC.Id] = externalData.DecodeT98AndT102AndB54Wc(externalDataStr, mode);


            dataTable.Rows.Add(row);
            return dataTable;
        }

    }
}
