﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.Core.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.8.1.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:5000/api/")]
        public string ApiAddress {
            get {
                return ((string)(this["ApiAddress"]));
            }
            set {
                this["ApiAddress"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("20")]
        public decimal VAT {
            get {
                return ((decimal)(this["VAT"]));
            }
            set {
                this["VAT"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("30")]
        public int MotWarningLimit {
            get {
                return ((int)(this["MotWarningLimit"]));
            }
            set {
                this["MotWarningLimit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("7")]
        public int MotAlertLimit {
            get {
                return ((int)(this["MotAlertLimit"]));
            }
            set {
                this["MotAlertLimit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("30")]
        public int CivilLiabilityWarningLimit {
            get {
                return ((int)(this["CivilLiabilityWarningLimit"]));
            }
            set {
                this["CivilLiabilityWarningLimit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("7")]
        public int CivilLiabilityAlertLimit {
            get {
                return ((int)(this["CivilLiabilityAlertLimit"]));
            }
            set {
                this["CivilLiabilityAlertLimit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("30")]
        public int CarInsuranceWarningLimit {
            get {
                return ((int)(this["CarInsuranceWarningLimit"]));
            }
            set {
                this["CarInsuranceWarningLimit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("7")]
        public int CarInsuranceAlertLimit {
            get {
                return ((int)(this["CarInsuranceAlertLimit"]));
            }
            set {
                this["CarInsuranceAlertLimit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("30")]
        public int VignetteWarningLimit {
            get {
                return ((int)(this["VignetteWarningLimit"]));
            }
            set {
                this["VignetteWarningLimit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("7")]
        public int VignetteAlertLimit {
            get {
                return ((int)(this["VignetteAlertLimit"]));
            }
            set {
                this["VignetteAlertLimit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("65000")]
        public int BeltChangeMileage {
            get {
                return ((int)(this["BeltChangeMileage"]));
            }
            set {
                this["BeltChangeMileage"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1000")]
        public int BeltMileageWarningLimit {
            get {
                return ((int)(this["BeltMileageWarningLimit"]));
            }
            set {
                this["BeltMileageWarningLimit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100")]
        public int BeltMileageAlertLimit {
            get {
                return ((int)(this["BeltMileageAlertLimit"]));
            }
            set {
                this["BeltMileageAlertLimit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("50000")]
        public int BrakeLiningsChangeMileage {
            get {
                return ((int)(this["BrakeLiningsChangeMileage"]));
            }
            set {
                this["BrakeLiningsChangeMileage"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1000")]
        public int BrakeLiningsMileageWarningLimit {
            get {
                return ((int)(this["BrakeLiningsMileageWarningLimit"]));
            }
            set {
                this["BrakeLiningsMileageWarningLimit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100")]
        public int BrakeLiningsMileageAlertLimit {
            get {
                return ((int)(this["BrakeLiningsMileageAlertLimit"]));
            }
            set {
                this["BrakeLiningsMileageAlertLimit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100000")]
        public int BrakeDisksChangeMileage {
            get {
                return ((int)(this["BrakeDisksChangeMileage"]));
            }
            set {
                this["BrakeDisksChangeMileage"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5000")]
        public int BrakeDisksMileageWarningLimit {
            get {
                return ((int)(this["BrakeDisksMileageWarningLimit"]));
            }
            set {
                this["BrakeDisksMileageWarningLimit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1000")]
        public int BrakeDisksMileageAlertLimit {
            get {
                return ((int)(this["BrakeDisksMileageAlertLimit"]));
            }
            set {
                this["BrakeDisksMileageAlertLimit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10000")]
        public int CoolantChangeMileage {
            get {
                return ((int)(this["CoolantChangeMileage"]));
            }
            set {
                this["CoolantChangeMileage"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1000")]
        public int CoolantMileageWarningLimit {
            get {
                return ((int)(this["CoolantMileageWarningLimit"]));
            }
            set {
                this["CoolantMileageWarningLimit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100")]
        public int CoolantMileageAlertLimit {
            get {
                return ((int)(this["CoolantMileageAlertLimit"]));
            }
            set {
                this["CoolantMileageAlertLimit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10000")]
        public int OilChangeMileage {
            get {
                return ((int)(this["OilChangeMileage"]));
            }
            set {
                this["OilChangeMileage"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("500")]
        public int OilMileageWarningLimit {
            get {
                return ((int)(this["OilMileageWarningLimit"]));
            }
            set {
                this["OilMileageWarningLimit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100")]
        public int OilMileageAlertLimit {
            get {
                return ((int)(this["OilMileageAlertLimit"]));
            }
            set {
                this["OilMileageAlertLimit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool EnableBeltNotifications {
            get {
                return ((bool)(this["EnableBeltNotifications"]));
            }
            set {
                this["EnableBeltNotifications"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool EnableOilNotifications {
            get {
                return ((bool)(this["EnableOilNotifications"]));
            }
            set {
                this["EnableOilNotifications"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool EnableBrakeDisksNotifications {
            get {
                return ((bool)(this["EnableBrakeDisksNotifications"]));
            }
            set {
                this["EnableBrakeDisksNotifications"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool EnableBrakeLiningsNotifications {
            get {
                return ((bool)(this["EnableBrakeLiningsNotifications"]));
            }
            set {
                this["EnableBrakeLiningsNotifications"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool EnableCoolantNotifications {
            get {
                return ((bool)(this["EnableCoolantNotifications"]));
            }
            set {
                this["EnableCoolantNotifications"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool EnableMotNotifications {
            get {
                return ((bool)(this["EnableMotNotifications"]));
            }
            set {
                this["EnableMotNotifications"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool EnableCivilLiabilityNotifications {
            get {
                return ((bool)(this["EnableCivilLiabilityNotifications"]));
            }
            set {
                this["EnableCivilLiabilityNotifications"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool EnableCarInsuranceNotifications {
            get {
                return ((bool)(this["EnableCarInsuranceNotifications"]));
            }
            set {
                this["EnableCarInsuranceNotifications"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool EnableVignetteNotifications {
            get {
                return ((bool)(this["EnableVignetteNotifications"]));
            }
            set {
                this["EnableVignetteNotifications"] = value;
            }
        }
    }
}
