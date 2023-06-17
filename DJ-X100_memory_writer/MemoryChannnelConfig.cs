using System.Text;

namespace DJ_X100_memory_writer
{
    internal class MemoryChannnelConfig
    {
        private string[] modeOptions = new string[]
        {
            "FM", "NFM", "AM", "NAM", "T98", "T102_B54", "DMR", "T61_typ1",
            "T61_typ2", "T61_typ3", "T61_typ4", "T61_typx", "ICDU", "dPMR",
            "DSTAR", "C4FM", "AIS", "ACARS", "POCSAG", "12KIF_W", "12KIF_N"
        };

        private List<ColumnSetup> columnsAsset = new List<ColumnSetup>
        {
            new ColumnSetup { Name = "memoryNo", HeaderText = "No.", ReadOnly = true, Width = 40 },
            new ColumnSetup { Name = "freq", HeaderText = "周波数" },
            new ColumnSetup { Name = "memoryName", HeaderText = "ネーム" },
            new ColumnSetup { Name = "mode", HeaderText = "モード" },

            new ColumnSetup { Name = "bank", HeaderText = "バンク" },
            new ColumnSetup { Name = "skip", HeaderText = "スキップ" },
            new ColumnSetup { Name = "step", HeaderText = "ステップ" },
            new ColumnSetup { Name = "offset", HeaderText = "オフセット" },
            new ColumnSetup { Name = "offsetFreq", HeaderText = "シフト周波数" },


            new ColumnSetup { Name = "sqlMode", HeaderText = "スケルチモード" },
            new ColumnSetup { Name = "ctcss", HeaderText = "CTCSS" },
            new ColumnSetup { Name = "DCS", HeaderText = "DCS" },



            new ColumnSetup { Name = "uc", HeaderText = "UC" },
            new ColumnSetup { Name = "gc", HeaderText = "GC" },
            new ColumnSetup { Name = "ec", HeaderText = "EC" },
            new ColumnSetup { Name = "wc", HeaderText = "WC" },

            new ColumnSetup { Name = "t61_lon", HeaderText = "T61基準経度" },
            new ColumnSetup { Name = "t61_lat", HeaderText = "T61基準緯度" },

            new ColumnSetup { Name = "dmr_dlot", HeaderText = "DMR_SLOT"},
            new ColumnSetup { Name = "dmr_cc", HeaderText = "DMR_CC" },
            new ColumnSetup { Name = "dmr_gc", HeaderText = "DMR_GC" },

            new ColumnSetup { Name = "lon", HeaderText = "経度" },
            new ColumnSetup { Name = "lat", HeaderText = "緯度" },



        };

        public class ColumnSetup
        {
            public string Name { get; set; } = string.Empty;
            public string HeaderText { get; set; } = string.Empty;
            public bool ReadOnly { get; set; } = false;
            public int Width { get; set; } = 100;
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
