using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using TVMCORP.TVS.Util.Extensions;
using TVMCORP.TVS.Util;
using TVMCORP.TVS.UTIL.Models;
using Microsoft.SharePoint.Utilities;
using System.Web;

namespace TVMCORP.TVS.Layouts.TVMCORP.TVS
{
    public partial class ApproversSettings : LayoutsPageBase
    {
        protected override void OnInit(EventArgs e)
        {
            btnCancel.Click += new EventHandler(btnCancel_Click);
            btnSave.Click += new EventHandler(btnSave_Click);

            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadListApproversSettings();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GoToListSettingsPage();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var settings = new ListApproversSettings();

            if (peTruongBoPhan.ResolvedEntities.Count > 0)
            {
                settings.TruongBoPhan = ((PickerEntity)peTruongBoPhan.ResolvedEntities[0]).Key;
            }
            else
            {
                settings.TruongBoPhan = string.Empty;
            }
            settings.AllowToChangeTruongBoPhan = chkAllowToChangeTruongBophan.Checked;

            if (peNguoiMuaHang.ResolvedEntities.Count > 0)
            {
                settings.NguoiMuaHang = ((PickerEntity)peNguoiMuaHang.ResolvedEntities[0]).Key;
            }
            else
            {
                settings.NguoiMuaHang = string.Empty;
            }
            settings.AllowToChangeNguoiMuaHang = chkAllowToChangeNguoiMuaHang.Checked;

            if (peNguoiDuyet.ResolvedEntities.Count > 0)
            {
                settings.NguoiDuyet = ((PickerEntity)peNguoiDuyet.ResolvedEntities[0]).Key;
            }
            else
            {
                settings.NguoiDuyet = string.Empty;
            }
            settings.AllowToChangeNguoiDuyet = chkAllowToChangeNguoiDuyet.Checked;

            if (pePhongKeToan.ResolvedEntities.Count > 0)
            {
                settings.PhongKeToan = ((PickerEntity)pePhongKeToan.ResolvedEntities[0]).Key;
            }
            else
            {
                settings.PhongKeToan = string.Empty;
            }
            settings.AllowToChangePhongKeToan = chkAllowToChangePhongKeToan.Checked;

            if (peNguoiXacNhan.ResolvedEntities.Count > 0)
            {
                settings.NguoiXacNhan = ((PickerEntity)peNguoiXacNhan.ResolvedEntities[0]).Key;
            }
            else
            {
                settings.NguoiXacNhan = string.Empty;
            }
            settings.AllowToChangeNguoiXacNhan = chkAllowToChangeNguoiXacNhan.Checked;

            SPContext.Current.List.SetCustomSettings<ListApproversSettings>(BeachCampFeatures.BeachCamp, settings);

            GoToListSettingsPage();
        }

        private void LoadListApproversSettings()
        {
            var settings = SPContext.Current.List.GetCustomSettings<ListApproversSettings>(BeachCampFeatures.BeachCamp);

            if (settings != null)
            {
                peTruongBoPhan.CommaSeparatedAccounts = settings.TruongBoPhan;
                chkAllowToChangeTruongBophan.Checked = settings.AllowToChangeTruongBoPhan;

                peNguoiMuaHang.CommaSeparatedAccounts = settings.NguoiMuaHang;
                chkAllowToChangeNguoiMuaHang.Checked = settings.AllowToChangeNguoiMuaHang;

                peNguoiDuyet.CommaSeparatedAccounts = settings.NguoiDuyet;
                chkAllowToChangeNguoiDuyet.Checked = settings.AllowToChangeNguoiDuyet;

                pePhongKeToan.CommaSeparatedAccounts = settings.PhongKeToan;
                chkAllowToChangePhongKeToan.Checked = settings.AllowToChangePhongKeToan;

                peNguoiXacNhan.CommaSeparatedAccounts = settings.NguoiXacNhan;
                chkAllowToChangeNguoiXacNhan.Checked = settings.AllowToChangeNguoiXacNhan;
            }
        }

        private void GoToListSettingsPage()
        {
            SPUtility.Redirect(SPContext.Current.Web.Url.TrimEnd('/') + "/_layouts/listedit.aspx?List={" + SPContext.Current.List.ID.ToString() + "}", SPRedirectFlags.Default, HttpContext.Current);
        }
    }
}
