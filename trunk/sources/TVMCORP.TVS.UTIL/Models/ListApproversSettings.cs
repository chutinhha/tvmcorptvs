using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TVMCORP.TVS.UTIL.Models
{
    public class ListApproversSettings
    {
        public string TruongBoPhan { get; set; }
        public bool AllowToChangeTruongBoPhan { get; set; }

        public string NguoiMuaHang { get; set; }
        public bool AllowToChangeNguoiMuaHang { get; set; }

        public string NguoiDuyet { get; set; }
        public bool AllowToChangeNguoiDuyet { get; set; }

        public string PhongKeToan { get; set; }
        public bool AllowToChangePhongKeToan { get; set; }

        public string NguoiXacNhan { get; set; }
        public bool AllowToChangeNguoiXacNhan { get; set; }

        public ListApproversSettings()
        {

        }
    }
}
