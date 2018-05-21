using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageService.GUI
{
    class LogData
    {
        private int type_num;
        public LogData(int type, string message)
        {
            if (type <= 2 && 0 <= type)
                type_num = type;
            else
                type_num = (int)LogDataEnum.DISPLAYERROR;
            this.Message = message;
        }
        public LogData(LogDataEnum type, string message)
        {
            if ((int)type <= 2 && 0 <= (int)type)
                type_num = (int)type;
            else
                type_num = (int)LogDataEnum.DISPLAYERROR;
            this.Message = message;
        }
        public string Type {

            get
            {
                switch (type_num)
                {
                    case (int)LogDataEnum.INFO: return "INFO";
                    case (int)LogDataEnum.ERROR: return "ERROR";
                    case (int)LogDataEnum.WARNING: return "WARNING";
                    default: return "DISPLAY ERROR";
                }
            }
            set
            {
                //do nothing, this isn't supposed to be set.
            }
        }
        public string Message { get; set; }
    }
}
