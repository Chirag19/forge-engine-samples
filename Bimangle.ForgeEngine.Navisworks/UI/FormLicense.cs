﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Navisworks.Core;
using Bimangle.ForgeEngine.Navisworks.Utility;
using Color = System.Drawing.Color;
using Form = System.Windows.Forms.Form;
using LicenseManager = Bimangle.ForgeEngine.Navisworks.License.LicenseManager;

namespace Bimangle.ForgeEngine.Navisworks.UI
{
    partial class FormLicense : Form
    {
        private byte[] _LicenseFile;

        public FormLicense()
        {
            InitializeComponent();
        }

        private void FormExport_Load(object sender, EventArgs e)
        {
            Icon =Icon.FromHandle(Properties.Resources.Converter_32px_1061192.GetHicon());
            Text += $@" - {App.TITLE}";

            txtHardwareId.Text = LicenseService.HardwareId;

            RefreshUI();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.CheckPathExists = true;
            dialog.DefaultExt = ".lic";
            dialog.Title = Strings.DialogTitleLoadLicense;
            dialog.Filter = string.Join(@"|", Strings.DialogFilterLicenseFile, Strings.DialogFilterAllFile);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _LicenseFile = File.ReadAllBytes(dialog.FileName);

                var licenseKey = LicenseService.ConvertLicenseFileToKey(dialog.FileName);
                LicenseService.Activate(@"BimAngle", @"ForgeEngine Plugin", licenseKey);
                RefreshUI();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_LicenseFile != null)
            {
                PublishLicense(_LicenseFile);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void RefreshUI()
        {
            bool isValid;
            string status;
            LicenseManager.Check(out isValid, out status);

            txtStatus.Text = status;
            txtStatus.ForeColor = isValid ? Color.Green : Color.Red;

            btnOK.Enabled = isValid;
        }

        /// <summary>
        /// 部署授权文件
        /// </summary>
        /// <param name="buffer"></param>
        private void PublishLicense(byte[] buffer)
        {
            var licFileName = @"Bimangle.ForgeEngine.lic";
            var licFilePath = AppHelper.GetPath(licFileName);
            File.WriteAllBytes(licFilePath, buffer);
        }
    }
}
