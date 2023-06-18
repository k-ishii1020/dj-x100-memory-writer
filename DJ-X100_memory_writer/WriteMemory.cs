using DJ_X100_memory_writer.domain;
using DJ_X100_memory_writer.Util;

namespace DJ_X100_memory_writer
{
    internal class WriteMemory
    {
        DataGridView dataGridView = new DataGridView();
        HexUtils utils = new HexUtils();



        public void Write(DataGridView dataGridView)
        {

            var csvUtils = new CsvUtils();
            csvUtils.ExportDataGridViewToX100CmdCsv(dataGridView, "x100cmd.csv");





        }


        private void setNormalData(DataGridView dataGridView)
        {

            for (int i = 0; i < 3; i++)
            {
                X100cmdOutput output = new X100cmdOutput();

                var row = dataGridView.Rows[i];

                if (row.Cells[1].Value == null)
                {
                    continue;
                }

                output.setChannel(row.Cells[0].Value);
                output.setFreq(row.Cells[1].Value);
                output.setName(row.Cells[2].Value);
                output.setMode(row.Cells[3].Value);
                output.setBank(row.Cells[4].Value);
                output.setSkip(row.Cells[5].Value);
                output.setStep(row.Cells[6].Value);
                output.setOffset(row.Cells[7].Value);
                output.setShiftFreq(row.Cells[8].Value);
                output.setAtt(row.Cells[9].Value);
                output.setSq(row.Cells[10].Value);
                output.setTone(row.Cells[11].Value);
                output.setDcs(row.Cells[12].Value);
                output.setLon(row.Cells[24].Value);
                output.setLat(row.Cells[25].Value);



                string cmd = "write -y ";


                cmd += output.getChannel() + " ";
                cmd += output.getFreq() + " ";
                cmd += output.getName() + " ";
                cmd += output.getMode() + " ";
                cmd += output.getBank() + " ";
                cmd += output.getSkip() + " ";
                cmd += output.getStep() + " ";
                cmd += output.getOffset() + " ";
                cmd += output.getShiftFreq() + " ";
                cmd += output.getAtt() + " ";
                cmd += output.getSq() + " ";
                cmd += output.getTone() + " ";
                cmd += output.getDcs() + " ";
                cmd += output.getLon() + " ";
                cmd += output.getLat() + " ";
                    

                var x100cmdForm = new X100cmdForm();
                x100cmdForm.WriteX100(cmd);
            }
        }




        private void verifyMemoryChannel(X100cmdOutput output)
        {
            // dataGridViewの２行目から最終行まで
            for (int i = 1; i < dataGridView.Rows.Count; i++)
            {
                if (dataGridView.Rows[i].Cells[1].Value == null)
                {
                    return;
                }
                try
                {
                    // utils.SwapEndianHex();
                }
                catch (Exception)
                {
                    throw new Exception("AUTO あるいは 数値ではない値が書き込まれています");
                }

            }
        }


    }
}
