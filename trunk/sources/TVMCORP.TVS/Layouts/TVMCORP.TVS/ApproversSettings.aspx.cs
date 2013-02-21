using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using TVMCORP.TVS.UTIL.Models;
using Microsoft.SharePoint.Utilities;
using System.Web;
using TVMCORP.TVS.UTIL;
using TVMCORP.TVS.UTIL.Extensions;

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
            var settingsCollection = new ListApproversSettingsCollection();

            var settingsHC = new ListApproversSettings();

            settingsHC.ApproversGroup = ApproversGroups.HanhChinh;

            if (peTruongBoPhanHC.ResolvedEntities.Count > 0)
            {
                settingsHC.TruongBoPhan = ((PickerEntity)peTruongBoPhanHC.ResolvedEntities[0]).Key;
            }
            else
            {
                settingsHC.TruongBoPhan = string.Empty;
            }
            settingsHC.AllowToChangeTruongBoPhan = chkAllowToChangeTruongBophanHC.Checked;

            if (peNguoiMuaHangHC.ResolvedEntities.Count > 0)
            {
                settingsHC.NguoiMuaHang = ((PickerEntity)peNguoiMuaHangHC.ResolvedEntities[0]).Key;
            }
            else
            {
                settingsHC.NguoiMuaHang = string.Empty;
            }
            settingsHC.AllowToChangeNguoiMuaHang = chkAllowToChangeNguoiMuaHangHC.Checked;

            if (peNguoiDuyetHC.ResolvedEntities.Count > 0)
            {
                settingsHC.NguoiDuyet = ((PickerEntity)peNguoiDuyetHC.ResolvedEntities[0]).Key;
            }
            else
            {
                settingsHC.NguoiDuyet = string.Empty;
            }
            settingsHC.AllowToChangeNguoiDuyet = chkAllowToChangeNguoiDuyetHC.Checked;

            if (pePhongKeToanHC.ResolvedEntities.Count > 0)
            {
                settingsHC.PhongKeToan = ((PickerEntity)pePhongKeToanHC.ResolvedEntities[0]).Key;
            }
            else
            {
                settingsHC.PhongKeToan = string.Empty;
            }
            settingsHC.AllowToChangePhongKeToan = chkAllowToChangePhongKeToanHC.Checked;

            if (peNguoiXacNhanHC.ResolvedEntities.Count > 0)
            {
                settingsHC.NguoiXacNhan = ((PickerEntity)peNguoiXacNhanHC.ResolvedEntities[0]).Key;
            }
            else
            {
                settingsHC.NguoiXacNhan = string.Empty;
            }
            settingsHC.AllowToChangeNguoiXacNhan = chkAllowToChangeNguoiXacNhanHC.Checked;

            settingsCollection.Settings.Add(settingsHC);


            var settingsCNTT = new ListApproversSettings();

            settingsCNTT.ApproversGroup = ApproversGroups.CongNgheThongTin;

            if (peTruongBoPhanCNTT.ResolvedEntities.Count > 0)
            {
                settingsCNTT.TruongBoPhan = ((PickerEntity)peTruongBoPhanCNTT.ResolvedEntities[0]).Key;
            }
            else
            {
                settingsCNTT.TruongBoPhan = string.Empty;
            }
            settingsCNTT.AllowToChangeTruongBoPhan = chkAllowToChangeTruongBophanCNTT.Checked;

            if (peNguoiMuaHangCNTT.ResolvedEntities.Count > 0)
            {
                settingsCNTT.NguoiMuaHang = ((PickerEntity)peNguoiMuaHangCNTT.ResolvedEntities[0]).Key;
            }
            else
            {
                settingsCNTT.NguoiMuaHang = string.Empty;
            }
            settingsCNTT.AllowToChangeNguoiMuaHang = chkAllowToChangeNguoiMuaHangCNTT.Checked;

            if (peNguoiDuyetCNTT.ResolvedEntities.Count > 0)
            {
                settingsCNTT.NguoiDuyet = ((PickerEntity)peNguoiDuyetCNTT.ResolvedEntities[0]).Key;
            }
            else
            {
                settingsCNTT.NguoiDuyet = string.Empty;
            }
            settingsCNTT.AllowToChangeNguoiDuyet = chkAllowToChangeNguoiDuyetCNTT.Checked;

            if (pePhongKeToanCNTT.ResolvedEntities.Count > 0)
            {
                settingsCNTT.PhongKeToan = ((PickerEntity)pePhongKeToanCNTT.ResolvedEntities[0]).Key;
            }
            else
            {
                settingsCNTT.PhongKeToan = string.Empty;
            }
            settingsCNTT.AllowToChangePhongKeToan = chkAllowToChangePhongKeToanCNTT.Checked;

            if (peNguoiXacNhanCNTT.ResolvedEntities.Count > 0)
            {
                settingsCNTT.NguoiXacNhan = ((PickerEntity)peNguoiXacNhanCNTT.ResolvedEntities[0]).Key;
            }
            else
            {
                settingsCNTT.NguoiXacNhan = string.Empty;
            }
            settingsCNTT.AllowToChangeNguoiXacNhan = chkAllowToChangeNguoiXacNhanCNTT.Checked;

            settingsCollection.Settings.Add(settingsCNTT);

            SPContext.Current.List.SetCustomSettings<ListApproversSettingsCollection>(TVMCORPFeatures.TVS, settingsCollection);

            GoToListSettingsPage();
        }

        private void LoadListApproversSettings()
        {
            ListApproversSettingsCollection settingsCollection = SPContext.Current.List.GetCustomSettings<ListApproversSettingsCollection>(TVMCORPFeatures.TVS);

            if (settingsCollection != null && settingsCollection.Settings != null)
            {

                foreach (var settings in settingsCollection.Settings)
                {
                    if (settings != null)
                    {
                        if (settings.ApproversGroup == ApproversGroups.HanhChinh)
                        {
                            peTruongBoPhanHC.CommaSeparatedAccounts = settings.TruongBoPhan;
                            chkAllowToChangeTruongBophanHC.Checked = settings.AllowToChangeTruongBoPhan;

                            peNguoiMuaHangHC.CommaSeparatedAccounts = settings.NguoiMuaHang;
                            chkAllowToChangeNguoiMuaHangHC.Checked = settings.AllowToChangeNguoiMuaHang;

                            peNguoiDuyetHC.CommaSeparatedAccounts = settings.NguoiDuyet;
                            chkAllowToChangeNguoiDuyetHC.Checked = settings.AllowToChangeNguoiDuyet;

                            pePhongKeToanHC.CommaSeparatedAccounts = settings.PhongKeToan;
                            chkAllowToChangePhongKeToanHC.Checked = settings.AllowToChangePhongKeToan;

                            peNguoiXacNhanHC.CommaSeparatedAccounts = settings.NguoiXacNhan;
                            chkAllowToChangeNguoiXacNhanHC.Checked = settings.AllowToChangeNguoiXacNhan;
                        }
                        else if (settings.ApproversGroup == ApproversGroups.CongNgheThongTin)
                        {
                            peTruongBoPhanCNTT.CommaSeparatedAccounts = settings.TruongBoPhan;
                            chkAllowToChangeTruongBophanCNTT.Checked = settings.AllowToChangeTruongBoPhan;

                            peNguoiMuaHangCNTT.CommaSeparatedAccounts = settings.NguoiMuaHang;
                            chkAllowToChangeNguoiMuaHangCNTT.Checked = settings.AllowToChangeNguoiMuaHang;

                            peNguoiDuyetCNTT.CommaSeparatedAccounts = settings.NguoiDuyet;
                            chkAllowToChangeNguoiDuyetCNTT.Checked = settings.AllowToChangeNguoiDuyet;

                            pePhongKeToanCNTT.CommaSeparatedAccounts = settings.PhongKeToan;
                            chkAllowToChangePhongKeToanCNTT.Checked = settings.AllowToChangePhongKeToan;

                            peNguoiXacNhanCNTT.CommaSeparatedAccounts = settings.NguoiXacNhan;
                            chkAllowToChangeNguoiXacNhanCNTT.Checked = settings.AllowToChangeNguoiXacNhan;
                        }
                    }

                }
            }
        }

        private void GoToListSettingsPage()
        {
            SPUtility.Redirect(SPContext.Current.Web.Url.TrimEnd('/') + "/_layouts/listedit.aspx?List={" + SPContext.Current.List.ID.ToString() + "}", SPRedirectFlags.Default, HttpContext.Current);
        }
    }
}
