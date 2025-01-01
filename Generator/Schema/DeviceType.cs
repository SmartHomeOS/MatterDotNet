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
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", ElementName = "deviceType", IsNullable = false)]
    public partial class DeviceType
    {

        private deviceTypeRevision[] revisionHistoryField;

        private deviceTypeClassification classificationField;

        private object conditionsField;

        private deviceTypeCluster[] clustersField;

        private string idField;

        private string nameField;

        private byte revisionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("revision", IsNullable = false)]
        public deviceTypeRevision[] revisionHistory
        {
            get
            {
                return this.revisionHistoryField;
            }
            set
            {
                this.revisionHistoryField = value;
            }
        }

        /// <remarks/>
        public deviceTypeClassification classification
        {
            get
            {
                return this.classificationField;
            }
            set
            {
                this.classificationField = value;
            }
        }

        /// <remarks/>
        public object conditions
        {
            get
            {
                return this.conditionsField;
            }
            set
            {
                this.conditionsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("cluster", IsNullable = false)]
        public deviceTypeCluster[] clusters
        {
            get
            {
                return this.clustersField;
            }
            set
            {
                this.clustersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte revision
        {
            get
            {
                return this.revisionField;
            }
            set
            {
                this.revisionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeRevision
    {

        private byte revisionField;

        private string summaryField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte revision
        {
            get
            {
                return this.revisionField;
            }
            set
            {
                this.revisionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string summary
        {
            get
            {
                return this.summaryField;
            }
            set
            {
                this.summaryField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeClassification
    {

        private string classField;

        private string scopeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @class
        {
            get
            {
                return this.classField;
            }
            set
            {
                this.classField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string scope
        {
            get
            {
                return this.scopeField;
            }
            set
            {
                this.scopeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeCluster
    {

        private deviceTypeClusterOtherwiseConform otherwiseConformField;

        private object optionalConformField;

        private object mandatoryConformField;

        private string idField;

        private string nameField;

        private string sideField;

        /// <remarks/>
        public deviceTypeClusterOtherwiseConform otherwiseConform
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
        public object mandatoryConform
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string side
        {
            get
            {
                return this.sideField;
            }
            set
            {
                this.sideField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class deviceTypeClusterOtherwiseConform
    {

        private object provisionalConformField;

        private object optionalConformField;

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
    }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}
