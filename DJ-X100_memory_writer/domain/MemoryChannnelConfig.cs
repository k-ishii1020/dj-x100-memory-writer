using System.Text;
using System.Collections.Generic;

namespace DJ_X100_memory_writer.domain
{
    internal class MemoryChannnelConfig
    {
        private static string[] modeOptions = new string[]
        {
            "FM", "NFM", "AM", "NAM",
            "T98", "T102_B54", "DMR", "T61_typ1", "T61_typ2", "T61_typ3", "T61_typ4", "T61_typx", "ICDU", "dPMR", "DSTAR", "C4FM",
            "AIS", "ACARS", "POCSAG", "12KIF_W", "12KIF_N"
        };

        private static string[] stepOptions = new string[]
        {
            "1k", "5k", "6k25", "8k33", "10k", "12k5","15k", "20k", "25k", "30k", "50k", "100k", "125k", "200k"
        };

        private static string[] bankOptions = new string[]
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
            "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
        };

        private static string[] ctcssOptions = new string[]
        {
            "670", "693", "719", "744", "770", "797", "825", "854", "885",
            "915", "948", "974", "1000", "1035", "1072", "1109", "1148", "1188",
            "1230", "1273", "1318", "1365", "1413", "1462", "1514", "1567", "1598",
            "1622", "1655", "1679", "1713", "1738", "1773", "1799", "1835", "1862",
            "1899", "1928", "1966", "1995", "2035", "2065", "2107", "2181", "2257",
            "2291", "2336", "2418", "2503", "2541"
        };

        private static string[] dcsOptions = new string[]
        {
            "017", "023", "025", "026", "031", "032", "036", "043", "047", "050", "051", "053", "054",
            "065", "071", "072", "073", "074", "114", "115", "116", "122", "125", "131", "132",
            "134", "143", "145", "152", "155", "156", "162", "165", "172", "174", "205", "212",
            "223", "225", "226", "243", "244", "245", "246", "251", "252", "255", "261", "263",
            "265", "266", "271", "274", "306", "311", "315", "325", "331", "332", "343", "346",
            "351", "356", "364", "365", "371", "411", "412", "413", "423", "431", "432", "445",
            "446", "452", "454", "455", "462", "464", "465", "466", "503", "506", "516", "523",
            "526", "532", "546", "565", "606", "612", "624", "627", "631", "632", "654", "662",
            "664", "703", "712", "723", "731", "732", "734", "743", "754"
        };


        private static string[] squelchOptions = new string[]
        {
            "OFF", "CTCSS", "DCS", "R_CTCSS", "R_DCS", "JR", "MSK"
        };

        private static string[] attOptions = new string[]
        {
            "OFF", "10db", "20db"
        };

        private static string[] revFrequencyOptions = new string[]
        {
            "2500", "2600", "2700", "2800", "2900",
            "3000", "3100", "3200", "3300", "3400", "3500"
        };

        private static string[] onOffOptions = new string[]
        {
            "OFF", "ON"
        };






        public enum ColumnType
        {
            Text,
            Checkbox,
            Dropdown
        }

        public static class Columns
        {
            public static readonly ColumnInfo MEMORY_NO = new ColumnInfo("Channel", "No.");

            public static readonly ColumnInfo FREQ = new ColumnInfo("Freq", "FREQ");
            public static readonly ColumnInfo MEMORY_NAME = new ColumnInfo("Name", "NAME");
            public static readonly ColumnInfo MODE = new ColumnInfo("Mode", "MODE");

            public static readonly ColumnInfo BANK = new ColumnInfo("bank", "BANK");
            public static readonly ColumnInfo SKIP = new ColumnInfo("skip", "SKIP");
            public static readonly ColumnInfo STEP = new ColumnInfo("Step", "STEP");
            public static readonly ColumnInfo OFFSET = new ColumnInfo("offset", "OFFSET");
            public static readonly ColumnInfo OFFSET_FREQ = new ColumnInfo("shift_freq", "FREQ");
            public static readonly ColumnInfo ATT = new ColumnInfo("att", "ATT");

            public static readonly ColumnInfo SQL_MODE = new ColumnInfo("sq", "SQ_TYPE");
            public static readonly ColumnInfo CTCSS = new ColumnInfo("tone", "CTCSS");
            public static readonly ColumnInfo DCS = new ColumnInfo("dcs", "DCS");
            public static readonly ColumnInfo REV_EC = new ColumnInfo("revEc", "REV_EC");
            public static readonly ColumnInfo REV_EC_FREQ = new ColumnInfo("revEcFreq", "FREQ");

            public static readonly ColumnInfo UC = new ColumnInfo("uc", "UC");
            public static readonly ColumnInfo GC = new ColumnInfo("gc", "GC");
            public static readonly ColumnInfo EC = new ColumnInfo("ec", "EC");
            public static readonly ColumnInfo WC = new ColumnInfo("wc", "WC");

            public static readonly ColumnInfo T61_LON = new ColumnInfo("t61_lon", "T61基準経度");
            public static readonly ColumnInfo T61_LAT = new ColumnInfo("t61_lat", "T61基準緯度");

            public static readonly ColumnInfo DMR_DLOT = new ColumnInfo("dmr_dlot", "DMR_SLOT");
            public static readonly ColumnInfo DMR_CC = new ColumnInfo("dmr_cc", "DMR_CC");
            public static readonly ColumnInfo DMR_GC = new ColumnInfo("dmr_gc", "DMR_GC");

            public static readonly ColumnInfo LON = new ColumnInfo("lon", "経度");
            public static readonly ColumnInfo LAT = new ColumnInfo("lat", "緯度");
        }

        private List<ColumnSetup> columnsAsset = new List<ColumnSetup>
        {
            new ColumnSetup
            {
                Id = Columns.MEMORY_NO.Id,
                HeaderText = Columns.MEMORY_NO.Name,
                ReadOnly = true,
                Width = 40,
                Type = ColumnType.Text
            },
            new ColumnSetup
            {
                Id = Columns.FREQ.Id,
                HeaderText = Columns.FREQ.Name,
                Width = 65,
                Type = ColumnType.Text
            },
            new ColumnSetup
            {
                Id = Columns.MEMORY_NAME.Id,
                HeaderText = Columns.MEMORY_NAME.Name,
                Width = 200,
                Type = ColumnType.Text
            },
            new ColumnSetup
            {
                Id = Columns.MODE.Id,
                HeaderText = Columns.MODE.Name,
                Width = 80,
                Type = ColumnType.Dropdown,
                Options = modeOptions
            },
            new ColumnSetup
            {
                Id = Columns.BANK.Id,
                HeaderText = Columns.BANK.Name,
                Width = 45,
                Type = ColumnType.Text,
            },
            new ColumnSetup
            {
                Id = Columns.SKIP.Id,
                HeaderText = Columns.SKIP.Name,
                Width = 55,
                Type = ColumnType.Dropdown,
                Options = onOffOptions
            },
            new ColumnSetup
            {
                Id = Columns.STEP.Id,
                HeaderText = Columns.STEP.Name,
                Width = 70,
                Type = ColumnType.Dropdown,
                Options = stepOptions
            },
            new ColumnSetup
            {
                Id = Columns.OFFSET.Id,
                HeaderText = Columns.OFFSET.Name,
                Width = 50,
                Type = ColumnType.Dropdown,
                Options = onOffOptions
            },
            new ColumnSetup
            {
                Id = Columns.OFFSET_FREQ.Id,
                HeaderText = Columns.OFFSET_FREQ.Name,
                Width = 50,
                Type = ColumnType.Text
            },
            new ColumnSetup
            {
                Id = Columns.ATT.Id,
                HeaderText = Columns.ATT.Name,
                Width = 55,
                Type = ColumnType.Dropdown,
                Options = attOptions
            },
            new ColumnSetup
            {
                Id = Columns.SQL_MODE.Id,
                HeaderText = Columns.SQL_MODE.Name,
                Width = 80,
                Type = ColumnType.Dropdown,
                Options = squelchOptions
            },
            new ColumnSetup
            {
                Id = Columns.CTCSS.Id,
                HeaderText = Columns.CTCSS.Name,
                Width = 60,
                Type = ColumnType.Dropdown,
                Options = ctcssOptions
            },
            new ColumnSetup
            {
                Id = Columns.DCS.Id,
                HeaderText = Columns.DCS.Name,
                Width = 60,
                Type = ColumnType.Dropdown,
                Options = dcsOptions
            },
            new ColumnSetup
            {
                Id = Columns.REV_EC.Id,
                HeaderText = Columns.REV_EC.Name,
                Width = 55,
                Type = ColumnType.Dropdown,
                Options = onOffOptions
            },
            new ColumnSetup
            {
                Id = Columns.REV_EC_FREQ.Id,
                HeaderText = Columns.REV_EC_FREQ.Name,
                Width = 70,
                Type = ColumnType.Dropdown,
                Options = revFrequencyOptions
            },
            new ColumnSetup
            {
                Id = Columns.UC.Id,
                HeaderText = Columns.UC.Name,
                Width = 40,
                Type = ColumnType.Text
            },
            new ColumnSetup
            {
                Id = Columns.GC.Id,
                HeaderText = Columns.GC.Name,
                Width = 40,
                Type = ColumnType.Text
            },
            new ColumnSetup
            {
                Id = Columns.EC.Id,
                HeaderText = Columns.EC.Name,
                Width = 40,
                Type = ColumnType.Text
            },
            new ColumnSetup
            {
                Id = Columns.WC.Id,
                HeaderText = Columns.WC.Name,
                Width = 40,
                Type = ColumnType.Text
            },
            new ColumnSetup
            {
                Id = Columns.T61_LON.Id,
                HeaderText = Columns.T61_LON.Name,
                Type = ColumnType.Text
            },
            new ColumnSetup
            {
                Id = Columns.T61_LAT.Id,
                HeaderText = Columns.T61_LAT.Name,
                Type = ColumnType.Text
            },
            new ColumnSetup
            {
                Id = Columns.DMR_DLOT.Id,
                HeaderText = Columns.DMR_DLOT.Name,
                Type = ColumnType.Text
            },
            new ColumnSetup
            {
                Id = Columns.DMR_CC.Id,
                HeaderText = Columns.DMR_CC.Name,
                Type = ColumnType.Text
            },
            new ColumnSetup
            {
                Id = Columns.DMR_GC.Id,
                HeaderText = Columns.DMR_GC.Name,
                Type = ColumnType.Text
            },
            new ColumnSetup
            {
                Id = Columns.LON.Id,
                HeaderText = Columns.LON.Name,
                Type = ColumnType.Text
            },
            new ColumnSetup
            {
                Id = Columns.LAT.Id,
                HeaderText = Columns.LAT.Name,
                Type = ColumnType.Text
            },
        };


        public class ColumnInfo
        {
            public readonly string Id;
            public readonly string Name;

            public ColumnInfo(string id, string name)
            {
                Id = id;
                Name = name;
            }
        }

        public class ColumnSetup
        {
            public string Id { get; set; } = string.Empty;
            public string HeaderText { get; set; } = string.Empty;
            public bool ReadOnly { get; set; } = false;
            public int Width { get; set; } = 100;
            public ColumnType Type { get; set; } = ColumnType.Text;
            public string[] Options { get; set; } = new string[0];
        }

        public string[] GetModeOptions()
        {
            return modeOptions;
        }

        public List<ColumnSetup> GetColumnsAsset()
        {
            return columnsAsset;
        }
    }
}
