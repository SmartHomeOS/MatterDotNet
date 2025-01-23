// MatterDotNet Copyright (C) 2025
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU Affero General Public License for more details.
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace Generator.Schema
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", ElementName = "configurator", IsNullable = false)]
    public partial class deviceTypeRoot
    {

        private deviceTypeRootDeviceType[] deviceTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("deviceType")]
        public deviceTypeRootDeviceType[] deviceType
        {
            get
            {
                return this.deviceTypeField;
            }
            set
            {
                this.deviceTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeRootDeviceType
    {
        private string typeNameField;
        private string classField;
        private deviceTypeRootDeviceTypeDeviceId deviceIdField;
        private object[] itemsField;

        private ItemsChoiceType1[] itemsElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("clusters", typeof(deviceTypeRootDeviceTypeClusters))]
        [System.Xml.Serialization.XmlElementAttribute("domain", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("endpointComposition", typeof(deviceTypeRootDeviceTypeEndpointComposition))]
        [System.Xml.Serialization.XmlElementAttribute("name", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("profileId", typeof(deviceTypeRootDeviceTypeProfileId))]
        [System.Xml.Serialization.XmlElementAttribute("scope", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("superset", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        public deviceTypeRootDeviceTypeDeviceId deviceId
        {
            get { return deviceIdField; }
            set { deviceIdField = value; }
        }

        public string typeName
        {
            get { return typeNameField; }
            set { typeNameField = value; }
        }

        public string @class
        {
            get { return classField; }
            set { classField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType1[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeRootDeviceTypeClusters
    {

        private deviceTypeRootDeviceTypeClustersInclude[] includeField;

        private bool lockOthersField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("include")]
        public deviceTypeRootDeviceTypeClustersInclude[] include
        {
            get
            {
                return this.includeField;
            }
            set
            {
                this.includeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool lockOthers
        {
            get
            {
                return this.lockOthersField;
            }
            set
            {
                this.lockOthersField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeRootDeviceTypeClustersInclude
    {

        private object[] itemsField;

        private ItemsChoiceType[] itemsElementNameField;

        private string clusterField;

        private bool clientField;

        private bool serverField;

        private bool clientLockedField;

        private bool serverLockedField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("features", typeof(deviceTypeRootDeviceTypeClustersIncludeFeatures))]
        [System.Xml.Serialization.XmlElementAttribute("requireAttribute", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("requireCommand", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cluster
        {
            get
            {
                return this.clusterField;
            }
            set
            {
                this.clusterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool client
        {
            get
            {
                return this.clientField;
            }
            set
            {
                this.clientField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool server
        {
            get
            {
                return this.serverField;
            }
            set
            {
                this.serverField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool clientLocked
        {
            get
            {
                return this.clientLockedField;
            }
            set
            {
                this.clientLockedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool serverLocked
        {
            get
            {
                return this.serverLockedField;
            }
            set
            {
                this.serverLockedField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeRootDeviceTypeClustersIncludeFeatures
    {

        private deviceTypeRootDeviceTypeClustersIncludeFeaturesFeature[] featureField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("feature")]
        public deviceTypeRootDeviceTypeClustersIncludeFeaturesFeature[] feature
        {
            get
            {
                return this.featureField;
            }
            set
            {
                this.featureField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeRootDeviceTypeClustersIncludeFeaturesFeature
    {

        private object disallowConformField;

        private object optionalConformField;

        private deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureMandatoryConform mandatoryConformField;

        private deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureOtherwiseConform otherwiseConformField;

        private string codeField;

        private string nameField;

        /// <remarks/>
        public object disallowConform
        {
            get
            {
                return this.disallowConformField;
            }
            set
            {
                this.disallowConformField = value;
            }
        }

        /// <remarks/>
        public object optionalConform
        {
            get
            {
                return this.optionalConformField;
            }
            set
            {
                this.optionalConformField = value;
            }
        }

        /// <remarks/>
        public deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureMandatoryConform mandatoryConform
        {
            get
            {
                return this.mandatoryConformField;
            }
            set
            {
                this.mandatoryConformField = value;
            }
        }

        /// <remarks/>
        public deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureOtherwiseConform otherwiseConform
        {
            get
            {
                return this.otherwiseConformField;
            }
            set
            {
                this.otherwiseConformField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureMandatoryConform
    {

        private deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureMandatoryConformCondition conditionField;

        private deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureMandatoryConformAndTerm andTermField;

        /// <remarks/>
        public deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureMandatoryConformCondition condition
        {
            get
            {
                return this.conditionField;
            }
            set
            {
                this.conditionField = value;
            }
        }

        /// <remarks/>
        public deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureMandatoryConformAndTerm andTerm
        {
            get
            {
                return this.andTermField;
            }
            set
            {
                this.andTermField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureMandatoryConformCondition
    {

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureMandatoryConformAndTerm
    {

        private deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureMandatoryConformAndTermCondition conditionField;

        private deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureMandatoryConformAndTermFeature[] orTermField;

        /// <remarks/>
        public deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureMandatoryConformAndTermCondition condition
        {
            get
            {
                return this.conditionField;
            }
            set
            {
                this.conditionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("feature", IsNullable = false)]
        public deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureMandatoryConformAndTermFeature[] orTerm
        {
            get
            {
                return this.orTermField;
            }
            set
            {
                this.orTermField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureMandatoryConformAndTermCondition
    {

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureMandatoryConformAndTermFeature
    {

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureOtherwiseConform
    {

        private object provisionalConformField;

        private object optionalConformField;

        private deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureOtherwiseConformMandatoryConform mandatoryConformField;

        /// <remarks/>
        public object provisionalConform
        {
            get
            {
                return this.provisionalConformField;
            }
            set
            {
                this.provisionalConformField = value;
            }
        }

        /// <remarks/>
        public object optionalConform
        {
            get
            {
                return this.optionalConformField;
            }
            set
            {
                this.optionalConformField = value;
            }
        }

        /// <remarks/>
        public deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureOtherwiseConformMandatoryConform mandatoryConform
        {
            get
            {
                return this.mandatoryConformField;
            }
            set
            {
                this.mandatoryConformField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureOtherwiseConformMandatoryConform
    {

        private deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureOtherwiseConformMandatoryConformFeature featureField;

        /// <remarks/>
        public deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureOtherwiseConformMandatoryConformFeature feature
        {
            get
            {
                return this.featureField;
            }
            set
            {
                this.featureField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeRootDeviceTypeClustersIncludeFeaturesFeatureOtherwiseConformMandatoryConformFeature
    {

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeRootDeviceTypeDeviceId
    {

        private bool editableField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool editable
        {
            get
            {
                return this.editableField;
            }
            set
            {
                this.editableField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeRootDeviceTypeEndpointComposition
    {

        private string compositionTypeField;

        private deviceTypeRootDeviceTypeEndpointCompositionEndpoint endpointField;

        /// <remarks/>
        public string compositionType
        {
            get
            {
                return this.compositionTypeField;
            }
            set
            {
                this.compositionTypeField = value;
            }
        }

        /// <remarks/>
        public deviceTypeRootDeviceTypeEndpointCompositionEndpoint endpoint
        {
            get
            {
                return this.endpointField;
            }
            set
            {
                this.endpointField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeRootDeviceTypeEndpointCompositionEndpoint
    {

        private string deviceTypeField;

        private string conformanceField;

        private string constraintField;

        /// <remarks/>
        public string deviceType
        {
            get
            {
                return this.deviceTypeField;
            }
            set
            {
                this.deviceTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string conformance
        {
            get
            {
                return this.conformanceField;
            }
            set
            {
                this.conformanceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string constraint
        {
            get
            {
                return this.constraintField;
            }
            set
            {
                this.constraintField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeRootDeviceTypeProfileId
    {

        private bool editableField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool editable
        {
            get
            {
                return this.editableField;
            }
            set
            {
                this.editableField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
    public enum ItemsChoiceType1
    {

        /// <remarks/>
        @class,

        /// <remarks/>
        clusters,

        /// <remarks/>
        deviceId,

        /// <remarks/>
        domain,

        /// <remarks/>
        endpointComposition,

        /// <remarks/>
        name,

        /// <remarks/>
        profileId,

        /// <remarks/>
        scope,

        /// <remarks/>
        superset,

        /// <remarks/>
        typeName,
    }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}
