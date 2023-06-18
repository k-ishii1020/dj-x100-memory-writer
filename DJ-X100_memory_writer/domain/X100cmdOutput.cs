using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Xml.Linq;

namespace DJ_X100_memory_writer.domain
{
    internal class X100cmdOutput
    {
        private String channel = "";
        private String freq = "";
        private String mode = "";
        private String step = "";
        private String name = "";
        private String offset = "";
        private String shiftFreq = "";
        private String att = "";
        private String sq = "";
        private String tone = "";
        private String dcs = "";
        private String bank = "";
        private String lat = "";
        private String lon = "";
        private String skip = "";
        private String ext = "";
        private String anfmEcOnoff = "";
        private String anfmEcFreq = "";
        private String t98Wc = "";
        private String unknown1 = "";
        private String t102Wc = "";
        private String t61Typ1Wc = "";
        private String t61Typ2Wc = "";
        private String t61Typ3Wc = "";
        private String t61Typ4Wc = "";
        private String t61TypxWc = "";
        private String t98Uc = "";
        private String t102Uc = "";
        private String t98Ec = "";
        private String t102Ec = "";
        private String t98Gc = "";
        private String t102Gc = "";
        private String dmrGc = "";
        private String dmrSlot = "";
        private String dmrColor = "";
        private String dstarSq = "";
        private String c4fmDgid = "";
        private String t61Lon = "";
        private String t61Lat = "";
        private String unknown2 = "";



        // Getters
        public String getChannel()
        {
            return this.channel;
        }

        public String getFreq()
        {
            return "-f " + this.freq;
        }

        public String getMode()
        {
            return "-m \"" + this.mode + "\"";
        }

        public String getStep()
        {
            return "-s \"" + this.step + "\"";
        }

        public String getName()
        {
            return "-n \"" + this.name + "\"";
        }

        public String getOffset()
        {
            return "--offset \"" + this.offset + "\"";
        }

        public String getShiftFreq()
        {
            return "--shift_freq \"" + this.shiftFreq + "\"";
        }

        public String getAtt()
        {
            return "--att \"" + this.att + "\"";
        }

        public String getSq()
        {
            return "--sq \"" + this.sq + "\"";
        }

        public String getTone()
        {
            return "--tone \"" + this.tone + "\"";
        }

        public String getDcs()
        {
            return "--dcs \"" + this.dcs + "\"";
        }

        public String getBank()
        {
            return "--bank \"" + this.bank + "\"";
        }

        public String getLat()
        {
            return "--lat \"" + this.lat + "\"";
        }

        public String getLon()
        {
            return "--lon \"" + this.lon + "\"";
        }

        public String getSkip()
        {
            return "--skip \"" + this.skip + "\"";
        }

        public String getExt()
        {
            return this.ext;
        }

        public String getAnfmEcOnoff()
        {
            return this.anfmEcOnoff;
        }

        public String getAnfmEcFreq()
        {
            return this.anfmEcFreq;
        }

        public String getT98Wc()
        {
            return this.t98Wc;
        }

        public String getUnknown1()
        {
            return this.unknown1;
        }

        public String getT102Wc()
        {
            return this.t102Wc;
        }

        public String getT61Typ1Wc()
        {
            return this.t61Typ1Wc;
        }

        public String getT61Typ2Wc()
        {
            return this.t61Typ2Wc;
        }

        public String getT61Typ3Wc()
        {
            return this.t61Typ3Wc;
        }

        public String getT61Typ4Wc()
        {
            return this.t61Typ4Wc;
        }

        public String getT61TypxWc()
        {
            return this.t61TypxWc;
        }

        public String getT98Uc()
        {
            return this.t98Uc;
        }

        public String getT102Uc()
        {
            return this.t102Uc;
        }

        public String getT98Ec()
        {
            return this.t98Ec;
        }

        public String getT102Ec()
        {
            return this.t102Ec;
        }

        public String getT98Gc()
        {
            return this.t98Gc;
        }

        public String getT102Gc()
        {
            return this.t102Gc;
        }

        public String getDmrGc()
        {
            return this.dmrGc;
        }

        public String getDmrSlot()
        {
            return this.dmrSlot;
        }

        public String getDmrColor()
        {
            return this.dmrColor;
        }

        public String getDstarSq()
        {
            return this.dstarSq;
        }

        public String getC4fmDgid()
        {
            return this.c4fmDgid;
        }

        public String getT61Lon()
        {
            return this.t61Lon;
        }

        public String getT61Lat()
        {
            return this.t61Lat;
        }

        public String getUnknown2()
        {
            return this.unknown2;
        }


        public void setChannel(Object value)
        {
            this.channel = (value == null) ? "" : value.ToString();
        }

        public void setFreq(Object value)
        {
            this.freq = (value == null) ? "" : value.ToString();
        }

        public void setMode(Object value)
        {
            this.mode = (value == null) ? "" : value.ToString();
        }

        public void setStep(Object value)
        {
            this.step = (value == null) ? "" : value.ToString();
        }

        public void setName(Object value)
        {
            this.name = (value == null) ? "" : value.ToString();
        }

        public void setOffset(Object value)
        {
            this.offset = (value == null) ? "" : value.ToString();
        }

        public void setShiftFreq(Object value)
        {
            this.shiftFreq = (value == null) ? "" : value.ToString();
        }

        public void setAtt(Object value)
        {
            this.att = (value == null) ? "" : value.ToString();
        }

        public void setSq(Object value)
        {
            this.sq = (value == null) ? "" : value.ToString();
        }

        public void setTone(Object value)
        {
            this.tone = (value == null) ? "" : value.ToString();
        }

        public void setDcs(Object value)
        {
            this.dcs = (value == null) ? "" : value.ToString();
        }

        public void setBank(Object value)
        {
            this.bank = (value == null) ? "" : value.ToString();
        }

        public void setLat(Object value)
        {
            this.lat = (value == null) ? "" : value.ToString();
        }

        public void setLon(Object value)
        {
            this.lon = (value == null) ? "" : value.ToString();
        }

        public void setSkip(Object value)
        {
            this.skip = (value == null) ? "" : value.ToString();
        }




        public void setExt(Object value)
        {
            this.ext = value.ToString();
        }

        public void setAnfmEcOnoff(Object value)
        {
            this.anfmEcOnoff = value.ToString();
        }

        public void setAnfmEcFreq(Object value)
        {
            this.anfmEcFreq = value.ToString();
        }

        public void setT98Wc(Object value)
        {
            this.t98Wc = value.ToString();
        }

        public void setUnknown1(Object value)
        {
            this.unknown1 = value.ToString();
        }

        public void setT102Wc(Object value)
        {
            this.t102Wc = value.ToString();
        }

        public void setT61Typ1Wc(Object value)
        {
            this.t61Typ1Wc = value.ToString();
        }

        public void setT61Typ2Wc(Object value)
        {
            this.t61Typ2Wc = value.ToString();
        }

        public void setT61Typ3Wc(Object value)
        {
            this.t61Typ3Wc = value.ToString();
        }

        public void setT61Typ4Wc(Object value)
        {
            this.t61Typ4Wc = value.ToString();
        }

        public void setT61TypxWc(Object value)
        {
            this.t61TypxWc = value.ToString();
        }

        public void setT98Uc(Object value)
        {
            this.t98Uc = value.ToString();
        }

        public void setT102Uc(Object value)
        {
            this.t102Uc = value.ToString();
        }

        public void setT98Ec(Object value)
        {
            this.t98Ec = value.ToString();
        }

        public void setT102Ec(Object value)
        {
            this.t102Ec = value.ToString();
        }

        public void setT98Gc(Object value)
        {
            this.t98Gc = value.ToString();
        }

        public void setT102Gc(Object value)
        {
            this.t102Gc = value.ToString();
        }

        public void setDmrGc(Object value)
        {
            this.dmrGc = value.ToString();
        }

        public void setDmrSlot(Object value)
        {
            this.dmrSlot = value.ToString();
        }

        public void setDmrColor(Object value)
        {
            this.dmrColor = value.ToString();
        }

        public void setDstarSq(Object value)
        {
            this.dstarSq = value.ToString();
        }

        public void setC4fmDgid(Object value)
        {
            this.c4fmDgid = value.ToString();
        }

        public void setT61Lon(Object value)
        {
            this.t61Lon = value.ToString();
        }

        public void setT61Lat(Object value)
        {
            this.t61Lat = value.ToString();
        }

        public void setUnknown2(Object value)
        {
            this.unknown2 = value.ToString();
        }
    }
}
