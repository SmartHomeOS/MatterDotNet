﻿// MatterDotNet Copyright (C) 2024 
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
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", ElementName = "cluster", IsNullable = false)]
    public partial class Cluster
    {

        private clusterRevisionHistory revisionHistoryField;

        private clusterClusterIds clusterIdsField;

        private clusterClassification classificationField;

        private clusterDataTypes dataTypesField;

        private clusterAttribute[] attributesField;

        private clusterCommand[] commandsField;

        private string idField;

        private string nameField;

        private byte revisionField;

        /// <remarks/>
        public clusterRevisionHistory revisionHistory
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
        public clusterClusterIds clusterIds
        {
            get
            {
                return this.clusterIdsField;
            }
            set
            {
                this.clusterIdsField = value;
            }
        }

        /// <remarks/>
        public clusterClassification classification
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
        public clusterDataTypes dataTypes
        {
            get
            {
                return this.dataTypesField;
            }
            set
            {
                this.dataTypesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("attribute", IsNullable = false)]
        public clusterAttribute[] attributes
        {
            get
            {
                return this.attributesField;
            }
            set
            {
                this.attributesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("command", IsNullable = false)]
        public clusterCommand[] commands
        {
            get
            {
                return this.commandsField;
            }
            set
            {
                this.commandsField = value;
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
    public partial class clusterRevisionHistory
    {

        private clusterRevisionHistoryRevision revisionField;

        /// <remarks/>
        public clusterRevisionHistoryRevision revision
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
    public partial class clusterRevisionHistoryRevision
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
    public partial class clusterClusterIds
    {

        private clusterClusterIdsClusterId clusterIdField;

        /// <remarks/>
        public clusterClusterIdsClusterId clusterId
        {
            get
            {
                return this.clusterIdField;
            }
            set
            {
                this.clusterIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clusterClusterIdsClusterId
    {

        private string idField;

        private string nameField;

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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clusterClassification
    {

        private string hierarchyField;

        private string roleField;

        private string picsCodeField;

        private string scopeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string hierarchy
        {
            get
            {
                return this.hierarchyField;
            }
            set
            {
                this.hierarchyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string role
        {
            get
            {
                return this.roleField;
            }
            set
            {
                this.roleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string picsCode
        {
            get
            {
                return this.picsCodeField;
            }
            set
            {
                this.picsCodeField = value;
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
    public partial class clusterDataTypes
    {

        private clusterDataTypesNumber[] numberField;

        private clusterDataTypesEnum[] enumField;

        private clusterDataTypesStruct[] structField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("number")]
        public clusterDataTypesNumber[] number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("enum")]
        public clusterDataTypesEnum[] @enum
        {
            get
            {
                return this.enumField;
            }
            set
            {
                this.enumField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("struct")]
        public clusterDataTypesStruct[] @struct
        {
            get
            {
                return this.structField;
            }
            set
            {
                this.structField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clusterDataTypesNumber
    {

        private string nameField;

        private string typeField;

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
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clusterDataTypesEnum
    {

        private clusterDataTypesEnumItem[] itemField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("item")]
        public clusterDataTypesEnumItem[] item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
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
    public partial class clusterDataTypesEnumItem
    {

        private object mandatoryConformField;

        private byte valueField;

        private string nameField;

        private string summaryField;

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
        public byte value
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
    public partial class clusterDataTypesStruct
    {

        private clusterDataTypesStructField[] fieldField;

        private clusterDataTypesStructAccess accessField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("field")]
        public clusterDataTypesStructField[] field
        {
            get
            {
                return this.fieldField;
            }
            set
            {
                this.fieldField = value;
            }
        }

        /// <remarks/>
        public clusterDataTypesStructAccess access
        {
            get
            {
                return this.accessField;
            }
            set
            {
                this.accessField = value;
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
    public partial class clusterDataTypesStructField
    {
        private clusterAttributeEntry entryField;

        private clusterDataTypesStructFieldAccess accessField;

        private clusterDataTypesStructFieldQuality qualityField;

        private object mandatoryConformField;

        private clusterDataTypesStructFieldConstraint constraintField;

        private byte idField;

        private string nameField;

        private string typeField;

        private string defaultField;

        /// <remarks/>
        public clusterAttributeEntry entry
        {
            get
            {
                return this.entryField;
            }
            set
            {
                this.entryField = value;
            }
        }

        /// <remarks/>
        public clusterDataTypesStructFieldAccess access
        {
            get
            {
                return this.accessField;
            }
            set
            {
                this.accessField = value;
            }
        }

        /// <remarks/>
        public clusterDataTypesStructFieldQuality quality
        {
            get
            {
                return this.qualityField;
            }
            set
            {
                this.qualityField = value;
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
        public clusterDataTypesStructFieldConstraint constraint
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte id
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
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @default
        {
            get
            {
                return this.defaultField;
            }
            set
            {
                this.defaultField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clusterDataTypesStructFieldAccess
    {

        private bool fabricSensitiveField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool fabricSensitive
        {
            get
            {
                return this.fabricSensitiveField;
            }
            set
            {
                this.fabricSensitiveField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clusterDataTypesStructFieldQuality
    {

        private bool nullableField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool nullable
        {
            get
            {
                return this.nullableField;
            }
            set
            {
                this.nullableField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clusterDataTypesStructFieldConstraint
    {

        private string typeField;

        private string valueField;

        private bool valueFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
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

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool valueSpecified
        {
            get
            {
                return this.valueFieldSpecified;
            }
            set
            {
                this.valueFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clusterDataTypesStructAccess
    {

        private bool fabricScopedField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool fabricScoped
        {
            get
            {
                return this.fabricScopedField;
            }
            set
            {
                this.fabricScopedField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clusterAttribute
    {

        private clusterAttributeEntry entryField;

        private clusterAttributeAccess accessField;

        private clusterAttributeQuality qualityField;

        private object mandatoryConformField;

        private clusterAttributeConstraint constraintField;

        private string idField;

        private string nameField;

        private string typeField;

        private string defaultField;

        private bool defaultFieldSpecified;

        /// <remarks/>
        public clusterAttributeEntry entry
        {
            get
            {
                return this.entryField;
            }
            set
            {
                this.entryField = value;
            }
        }

        /// <remarks/>
        public clusterAttributeAccess access
        {
            get
            {
                return this.accessField;
            }
            set
            {
                this.accessField = value;
            }
        }

        /// <remarks/>
        public clusterAttributeQuality quality
        {
            get
            {
                return this.qualityField;
            }
            set
            {
                this.qualityField = value;
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
        public clusterAttributeConstraint constraint
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
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @default
        {
            get
            {
                return this.defaultField;
            }
            set
            {
                this.defaultField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool defaultSpecified
        {
            get
            {
                return this.defaultFieldSpecified;
            }
            set
            {
                this.defaultFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clusterAttributeEntry
    {

        private clusterAttributeEntryConstraint constraintField;

        private string typeField;

        /// <remarks/>
        public clusterAttributeEntryConstraint constraint
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clusterAttributeEntryConstraint
    {

        private string typeField;

        private ushort valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort value
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
    public partial class clusterAttributeAccess
    {

        private bool readField;

        private string readPrivilegeField;

        private bool writeField;

        private string writePrivilegeField;

        private bool fabricScopedField;

        private bool fabricScopedFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool read
        {
            get
            {
                return this.readField;
            }
            set
            {
                this.readField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string readPrivilege
        {
            get
            {
                return this.readPrivilegeField;
            }
            set
            {
                this.readPrivilegeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool write
        {
            get
            {
                return this.writeField;
            }
            set
            {
                this.writeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string writePrivilege
        {
            get
            {
                return this.writePrivilegeField;
            }
            set
            {
                this.writePrivilegeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool fabricScoped
        {
            get
            {
                return this.fabricScopedField;
            }
            set
            {
                this.fabricScopedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool fabricScopedSpecified
        {
            get
            {
                return this.fabricScopedFieldSpecified;
            }
            set
            {
                this.fabricScopedFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clusterAttributeQuality
    {

        private bool changeOmittedField;

        private bool nullableField;

        private bool sceneField;

        private string persistenceField;

        private bool reportableField;

        private bool sourceAttributionField;

        private bool quieterReportingField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool changeOmitted
        {
            get
            {
                return this.changeOmittedField;
            }
            set
            {
                this.changeOmittedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool nullable
        {
            get
            {
                return this.nullableField;
            }
            set
            {
                this.nullableField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool scene
        {
            get
            {
                return this.sceneField;
            }
            set
            {
                this.sceneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string persistence
        {
            get
            {
                return this.persistenceField;
            }
            set
            {
                this.persistenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool reportable
        {
            get
            {
                return this.reportableField;
            }
            set
            {
                this.reportableField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool sourceAttribution
        {
            get
            {
                return this.sourceAttributionField;
            }
            set
            {
                this.sourceAttributionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool quieterReporting
        {
            get
            {
                return this.quieterReportingField;
            }
            set
            {
                this.quieterReportingField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clusterAttributeConstraint
    {

        private string typeField;

        private string valueField;

        private int fromField;

        private bool fromFieldSpecified;

        private int toField;

        private bool toFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int from
        {
            get
            {
                return this.fromField;
            }
            set
            {
                this.fromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool fromSpecified
        {
            get
            {
                return this.fromFieldSpecified;
            }
            set
            {
                this.fromFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int to
        {
            get
            {
                return this.toField;
            }
            set
            {
                this.toField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool toSpecified
        {
            get
            {
                return this.toFieldSpecified;
            }
            set
            {
                this.toFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clusterCommand
    {

        private clusterCommandAccess accessField;

        private object mandatoryConformField;

        private clusterCommandField[] fieldField;

        private string idField;

        private string nameField;

        private string directionField;

        private string responseField;

        /// <remarks/>
        public clusterCommandAccess access
        {
            get
            {
                return this.accessField;
            }
            set
            {
                this.accessField = value;
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
        [System.Xml.Serialization.XmlElementAttribute("field")]
        public clusterCommandField[] field
        {
            get
            {
                return this.fieldField;
            }
            set
            {
                this.fieldField = value;
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
        public string direction
        {
            get
            {
                return this.directionField;
            }
            set
            {
                this.directionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clusterCommandAccess
    {

        private string invokePrivilegeField;

        private bool fabricScopedField;

        private bool fabricScopedFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string invokePrivilege
        {
            get
            {
                return this.invokePrivilegeField;
            }
            set
            {
                this.invokePrivilegeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool fabricScoped
        {
            get
            {
                return this.fabricScopedField;
            }
            set
            {
                this.fabricScopedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool fabricScopedSpecified
        {
            get
            {
                return this.fabricScopedFieldSpecified;
            }
            set
            {
                this.fabricScopedFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clusterCommandField
    {

        private object optionalConformField;

        private object mandatoryConformField;

        private clusterCommandFieldConstraint constraintField;

        private byte idField;

        private string nameField;

        private string typeField;

        private string defaultField;

        private bool defaultFieldSpecified;

        private clusterAttributeEntry? entryField;

        public clusterAttributeEntry? entry
        {
            get
            {
                return this.entryField;
            }
            set
            {
                this.entryField = value;
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
        public clusterCommandFieldConstraint constraint
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte id
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
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @default
        {
            get
            {
                return this.defaultField;
            }
            set
            {
                this.defaultField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool defaultSpecified
        {
            get
            {
                return this.defaultFieldSpecified;
            }
            set
            {
                this.defaultFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clusterCommandFieldConstraint
    {

        private string typeField;

        private string valueField;

        private int fromField;

        private bool fromFieldSpecified;

        private int toField;

        private bool toFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int from
        {
            get
            {
                return this.fromField;
            }
            set
            {
                this.fromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool fromSpecified
        {
            get
            {
                return this.fromFieldSpecified;
            }
            set
            {
                this.fromFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int to
        {
            get
            {
                return this.toField;
            }
            set
            {
                this.toField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool toSpecified
        {
            get
            {
                return this.toFieldSpecified;
            }
            set
            {
                this.toFieldSpecified = value;
            }
        }
    }
}