using DJ_X100_memory_writer.Util;

using static DJ_X100_memory_writer.domain.MemoryChannnelConfig;

namespace DJ_X100_memory_writer.Service
{
    internal class ExternalDataService
    {
        HexUtils hexUtils = new HexUtils();

        /**
         * 拡張部分のバイナリを作成するServiceクラス
         */
        public string ExternalData(DataGridViewRow row)
        {
            string externalStr = "0000e4000000e480000000000000000000000180018001800180010000800100008001000080000080008000807b1700";
            int initialLength = externalStr.Length;

            // REV_ECの処理
            externalStr = EncodeRevEcOnOff(row, externalStr);

            // REV_EC_FREQの処理
            externalStr = EncodeRevEcFreq(row, externalStr);

            // T98_WCの処理
            externalStr = EncodeT98AndT102AndB54Wc(externalStr, row, "T98", 4);

            // Unknown_1

            // T102_B54_WCの処理
            externalStr = EncodeT98AndT102AndB54Wc(externalStr, row, "T102_B54", 12);

            // T61_WCの処理
            externalStr = EncodeT61Wc(row, externalStr);


            // T98_UCの処理
            externalStr = EncodeT98AndT102AndB54Uc(row, "T98", 36, externalStr);

            // T102_B54_UCの処理
            externalStr = EncodeT98AndT102AndB54Uc(row, "T102_B54", 40, externalStr);


            // T98_ECの処理
            externalStr = EncodeT98AndT102AndB54Ec(row, "T98", 44, externalStr);

            // T102_B54_ECの処理
            externalStr = EncodeT98AndT102AndB54Ec(row, "T102_B54", 48, externalStr);


            // T98_GCの処理
            externalStr = EncodeT98AndT102AndB54AndDmrGc(row, "T98", 52, externalStr);
            // T102_B54_GCの処理
            externalStr = EncodeT98AndT102AndB54AndDmrGc(row, "T102_B54", 60, externalStr);

            // DMR_GCの処理
            externalStr = EncodeT98AndT102AndB54AndDmrGc(row, "DMR", 68, externalStr);
            // DMR_SLOTの処理
            externalStr = EncodeDmrSlot(row, "DMR", 76, externalStr);
            // DMR_CCの処理
            externalStr = EncodeDmrCc(row, "DMR", 78, externalStr);



            // DSTAR_CSの処理
            externalStr = EncodeDstarCs(row, "DSTAR", 82, externalStr);

            // C4FM_DGの処理
            externalStr = EncodeC4fmDg(row, "C4FM", 86, externalStr);

            // T61経度の処理
            externalStr = EncodeT61Lon(row, 90, externalStr);

            // T61緯度の処理
            externalStr = EncodeT61Lat(row, 92, externalStr);


            // Unknown_2


            int finalLength = externalStr.Length;

            if (initialLength != finalLength)
            {
                throw new InvalidOperationException($"拡張文字列長に誤りがありますので操作を中止します。正: {initialLength} 実際: {finalLength}.");
            }
            return externalStr;
        }


        private static string EncodeRevEcOnOff(DataGridViewRow row, string externalStr)
        {
            string revEcValue = row.Cells[Columns.REV_EC.Id].Value?.ToString();
            externalStr = (revEcValue == "ON" ? "01" : "00") + externalStr.Substring(2);
            return externalStr;
        }

        public string DecodeRevEcOnOff(string externalStr)
        {
            string value = externalStr.Substring(0, 2);
            return value == "01" ? "ON" : "OFF";
        }

        private static string EncodeRevEcFreq(DataGridViewRow row, string externalStr)
        {
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
            return externalStr;
        }

        public string DecodeRevEcFreq(string externalStr)
        {
            Dictionary<string, string> revEcFreqValues = new Dictionary<string, string>
            {
                {"00", "2500"},
                {"01", "2600"},
                {"02", "2700"},
                {"03", "2800"},
                {"04", "2900"},
                {"05", "3000"},
                {"06", "3100"},
                {"07", "3200"},
                {"08", "3300"},
                {"09", "3400"},
                {"0A", "3500"},
            };

            string value = externalStr.Substring(2, 2);
            return revEcFreqValues.ContainsKey(value) ? revEcFreqValues[value] : "2500";
        }




        private string EncodeT98AndT102AndB54Wc(string externalStr, DataGridViewRow row, string mode, int position)
        {
            if (row.Cells[Columns.MODE.Id].Value != null && row.Cells[Columns.MODE.Id].Value.ToString() == mode)
            {
                string value;

                // T98の場合はWCの値が存在しない場合は "0228" にします
                if (row.Cells[Columns.MODE.Id].Value.ToString() == "T98")
                {
                    value = row.Cells[Columns.WC.Id].Value != null ? row.Cells[Columns.WC.Id].Value.ToString() : "0228";
                }
                else
                {
                    // WCの値が存在しない場合は "0000" にします
                    value = row.Cells[Columns.WC.Id].Value != null ? row.Cells[Columns.WC.Id].Value.ToString() : "0000";
                }

                // AUTOだった場合は "0180" に、それ以外は16進数として解釈してエンディアンを反転します
                string replaceValue = (value == "AUTO") ? "0180" : hexUtils.SwapEndianHexForFourHex(value);

                // 指定した位置の値を置換します
                externalStr = externalStr.Remove(position, 4);
                externalStr = externalStr.Insert(position, replaceValue);
            }

            return externalStr;
        }

        public string DecodeT98AndT102AndB54Wc(string externalStr, string mode)
        {
            string value = "";

            switch(mode)
            {
                case "T98":
                    value = externalStr.Substring(4, 4);
                    break;

                case "T102_B54":
                    value = externalStr.Substring(12, 4);
                    break;

                case "T61_typ1":
                    value = externalStr.Substring(16, 4);
                    break;

                case "T61_typ2":
                    value = externalStr.Substring(20, 4);
                    break;


                case "T61_typ3":
                    value = externalStr.Substring(24, 4);
                    break;

                case "T61_typ4":
                    value = externalStr.Substring(28, 4);
                    break;


                case "T61_typx":
                    value = externalStr.Substring(32, 4);
                    break;
                default:
                    return "AUTO";
            }

            // Decode失敗したら
            if (!hexUtils.DecodeFourHex(value, out bool autoScan, out int code))
            {
                return "228";
            }
            
            if(autoScan) return "AUTO";

            return code.ToString("D3");
        }













        private string EncodeT61Wc(DataGridViewRow row, string externalStr)
        {
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
                string replaceValue = hexUtils.SwapEndianHexForFourHex(wcValue);
                int index = modeToIndexMap[modeValue];
                externalStr = externalStr.Remove(index, 4);
                externalStr = externalStr.Insert(index, replaceValue);
            }

            return externalStr;
        }

        private string EncodeT98AndT102AndB54Uc(DataGridViewRow row, string mode, int index, string externalStr)
        {
            if (row.Cells[Columns.MODE.Id].Value != null && row.Cells[Columns.MODE.Id].Value.ToString() == mode)
            {
                string value = row.Cells[Columns.UC.Id].Value != null ? row.Cells[Columns.UC.Id].Value.ToString() : null;
                string replaceValue = "0080";

                if (value != null)
                {
                    replaceValue = (value == "OFF") ? "0080" : hexUtils.SwapEndianHexForFourHex(value);
                }

                externalStr = externalStr.Remove(index, 4);
                externalStr = externalStr.Insert(index, replaceValue);
            }
            return externalStr;

        }

        public string DecodeT98AndT102AndB54UcAndDstarAndC4fm(string externalStr, string mode)
        {
            string value = "";

            switch (mode)
            {
                case "T98":
                    value = externalStr.Substring(36, 4);
                    break;

                case "T102_B54":
                    value = externalStr.Substring(40, 4);
                    break;

                case "DSTAR":
                    value = externalStr.Substring(82, 4);
                    break;

                case "C4FM":
                    value = externalStr.Substring(86, 4);
                    break;

                default:
                    return "OFF";
            }

            // Decode失敗したら
            if (!hexUtils.DecodeFourHex(value, out bool flag, out int code))
            {
                return "OFF";
            }

            if (flag) return "OFF";

            switch (mode)
            {
                case "DSTAR":
                case "C4FM":
                    return code.ToString("D2");

                default:
                    return code.ToString("D3");
            }
        }
















        private string EncodeT98AndT102AndB54Ec(DataGridViewRow row, string mode, int index, string externalStr)
        {
            if (row.Cells[Columns.MODE.Id].Value != null && row.Cells[Columns.MODE.Id].Value.ToString() == mode)
            {
                string value = row.Cells[Columns.EC.Id].Value != null ? row.Cells[Columns.EC.Id].Value.ToString() : null;
                string replaceValue = "0180";

                if (value != null)
                {
                    replaceValue = (value == "0") ? "0180" : hexUtils.SwapEndianHexForFourHex(value);
                }

                externalStr = externalStr.Remove(index, 4);
                externalStr = externalStr.Insert(index, replaceValue);
            }
            return externalStr;

        }
        public string DecodeT98AndT102AndB54Ec(string externalStr, string mode)
        {
            string value = "";

            switch (mode)
            {
                case "T98":
                    value = externalStr.Substring(44, 4);
                    break;

                case "T102_B54":
                    value = externalStr.Substring(48, 4);
                    break;
                default:
                    return "OFF";
            }

            // Decode失敗したら
            if (!hexUtils.DecodeFourHex(value, out bool autoScan, out int code))
            {
                return "OFF";
            }

            if (autoScan) return "OFF";

            return code.ToString("D5");
        }

        private string EncodeT98AndT102AndB54AndDmrGc(DataGridViewRow row, string mode, int removeIndex, string externalStr)
        {
            if (row.Cells[Columns.MODE.Id].Value != null && row.Cells[Columns.MODE.Id].Value.ToString() == mode)
            {
                string value = row.Cells[Columns.GC.Id].Value != null ? row.Cells[Columns.GC.Id].Value.ToString() : null;
                if (value != null)
                {
                    string replaceValue = (value == "0") ? "01000080" : hexUtils.SwapEndianHexForEightDigits(value);
                    externalStr = externalStr.Remove(removeIndex, 8);
                    externalStr = externalStr.Insert(removeIndex, replaceValue);
                }
                else
                {
                    externalStr = externalStr.Remove(removeIndex, 8);
                    externalStr = externalStr.Insert(removeIndex, "01000080");
                }
            }
            return externalStr;

        }

        public string DecodeT98AndT102AndB54AndDmrGc(string externalStr, string mode)
        {
            string value = "";

            switch (mode)
            {
                case "T98":
                    value = externalStr.Substring(52, 8);
                    break;

                case "T102_B54":
                    value = externalStr.Substring(60, 8);
                    break;

                case "DMR":
                    value = externalStr.Substring(68, 8);
                    break;
                default:
                    return "ALL";
            }

            // Decode失敗したら
            if (!hexUtils.DecodeEightHex(value, out bool all, out long code))
            {
                return "ALL";
            }

            if (all) return "ALL";

            return code.ToString("D8");
        }

        private string EncodeDmrSlot(DataGridViewRow row, string mode, int removeIndex, string externalStr)
        {
            if (row.Cells[Columns.MODE.Id].Value != null && row.Cells[Columns.MODE.Id].Value.ToString() == mode)
            {
                string value = row.Cells[Columns.DMR_SLOT.Id].Value != null ? row.Cells[Columns.DMR_SLOT.Id].Value.ToString() : null;
                string replaceValue;
                if (value != null)
                {
                    switch (value)
                    {
                        case "AUTO":
                            replaceValue = "00";
                            break;
                        case "1":
                            replaceValue = "01";
                            break;
                        case "2":
                            replaceValue = "02";
                            break;
                        default:
                            replaceValue = "00";
                            break;
                    }
                }
                else
                {
                    replaceValue = "00";
                }

                externalStr = externalStr.Remove(removeIndex, 2);
                externalStr = externalStr.Insert(removeIndex, replaceValue);
            }
            return externalStr;
        }

        public string DecodeDmrSlot(string externalStr)
        {
            string value = externalStr.Substring(76, 2);

            switch (value)
            {
                case "00":
                    return "AUTO";
                case "01":
                    return "1";
                case "02":
                    return "2";
                default:
                    return "AUTO";
            }
        }
        private string EncodeDmrCc(DataGridViewRow row, string mode, int removeIndex, string externalStr)
        {
            if (row.Cells[Columns.MODE.Id].Value != null && row.Cells[Columns.MODE.Id].Value.ToString() == mode)
            {
                string value = row.Cells[Columns.DMR_CC.Id].Value != null ? row.Cells[Columns.DMR_CC.Id].Value.ToString() : null;
                string replaceValue;
                if (value != null)
                {
                    replaceValue = (value == "OFF") ? "0080" : hexUtils.SwapEndianHexForFourHex(value);
                }
                else
                {
                    replaceValue = "0080";
                }

                externalStr = externalStr.Remove(removeIndex, 4);
                externalStr = externalStr.Insert(removeIndex, replaceValue);
            }
            return externalStr;
        }

        public string DecodeDmrCc(string externalStr)
        {
            string value = externalStr.Substring(78, 4);

            // Decode失敗したら
            if (!hexUtils.DecodeFourHex(value, out bool flag, out int code))
            {
                return "OFF";
            }

            if (flag) return "OFF";

            return code.ToString("D2");
        }

        private string EncodeDstarCs(DataGridViewRow row, string mode, int removeIndex, string externalStr)
        {
            if (row.Cells[Columns.MODE.Id].Value != null && row.Cells[Columns.MODE.Id].Value.ToString() == mode)
            {
                string value = row.Cells[Columns.DSTAR_CS.Id].Value != null ? row.Cells[Columns.DSTAR_CS.Id].Value.ToString() : null;
                string replaceValue;
                if (value != null)
                {
                    replaceValue = (value == "OFF") ? "0080" : hexUtils.SwapEndianHexForFourHex(value);
                }
                else
                {
                    replaceValue = "0080";
                }

                externalStr = externalStr.Remove(removeIndex, 4);
                externalStr = externalStr.Insert(removeIndex, replaceValue);
            }
            return externalStr;

        }

        private string EncodeC4fmDg(DataGridViewRow row, string mode, int removeIndex, string externalStr)
        {
            if (row.Cells[Columns.MODE.Id].Value != null && row.Cells[Columns.MODE.Id].Value.ToString() == mode)
            {
                string value = row.Cells[Columns.C4FM_DG.Id].Value != null ? row.Cells[Columns.C4FM_DG.Id].Value.ToString() : null;
                string replaceValue;
                if (value != null)
                {
                    replaceValue = (value == "OFF") ? "0080" : hexUtils.SwapEndianHexForFourHex(value);
                }
                else
                {
                    replaceValue = "0080";
                }

                externalStr = externalStr.Remove(removeIndex, 4);
                externalStr = externalStr.Insert(removeIndex, replaceValue);
            }
            return externalStr;
        }

        private string EncodeT61Lon(DataGridViewRow row, int index, string externalStr)
        {
            if (row.Cells[Columns.MODE.Id].Value != null &&
                (row.Cells[Columns.MODE.Id].Value.ToString() == "T61_typ1" || row.Cells[Columns.MODE.Id].Value.ToString() == "T61_typ2"))
            {
                string value = row.Cells[Columns.T61_LON.Id].Value != null ? row.Cells[Columns.T61_LON.Id].Value.ToString() : null;
                string replaceValue = "7B";

                if (value != null)
                {
                    replaceValue = hexUtils.SwapEndianHexForTwoDigits(value);
                }

                externalStr = externalStr.Remove(index, 2);
                externalStr = externalStr.Insert(index, replaceValue);
            }
            return externalStr;
        }

        private string EncodeT61Lat(DataGridViewRow row, int index, string externalStr)
        {
            if (row.Cells[Columns.MODE.Id].Value != null &&
                (row.Cells[Columns.MODE.Id].Value.ToString() == "T61_typ1" || row.Cells[Columns.MODE.Id].Value.ToString() == "T61_typ2"))
            {
                string value = row.Cells[Columns.T61_LAT.Id].Value != null ? row.Cells[Columns.T61_LAT.Id].Value.ToString() : null;
                string replaceValue = "17";

                if (value != null)
                {
                    replaceValue = hexUtils.SwapEndianHexForTwoDigits(value);
                }

                externalStr = externalStr.Remove(index, 2);
                externalStr = externalStr.Insert(index, replaceValue);
            }
            return externalStr;
        }

        public string Decode61LonLat(string externalStr, string lonLat)
        {
            string value = "";

            switch (lonLat)
            {
                case "LON":
                    value = externalStr.Substring(90, 2);
                    break;

                case "LAT":
                    value = externalStr.Substring(92, 2);
                    break;
                default:
                    return "";
            }
            return hexUtils.HexToDecimal(value).ToString();
        }
    }

}
