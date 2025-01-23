using System.Xml.Serialization;

namespace Generator.Schema
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class configurator
    {
        private rootConfiguratorDomain domainField;

        private rootConfiguratorEnum[] enumField;

        private rootConfiguratorBitmap[] bitmapField;

        private rootConfiguratorStruct[] structField;

        private rootConfiguratorCluster[] clusterField;

        /// <remarks/>
        public rootConfiguratorDomain domain
        {
            get
            {
                return this.domainField;
            }
            set
            {
                this.domainField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("bitmap")]
        public rootConfiguratorBitmap[] bitmap
        {
            get
            {
                return this.bitmapField;
            }
            set
            {
                this.bitmapField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("enum")]
        public rootConfiguratorEnum[] @enum
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
        public rootConfiguratorStruct[] @struct
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("cluster")]
        public rootConfiguratorCluster[] cluster
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorBitmap
    {

        private rootConfiguratorBitmapCluster clusterField;

        private rootConfiguratorBitmapField[] fieldField;

        private string nameField;

        private string typeField;

        /// <remarks/>
        public rootConfiguratorBitmapCluster cluster
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
        [System.Xml.Serialization.XmlElementAttribute("field")]
        public rootConfiguratorBitmapField[] field
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
    public partial class rootConfiguratorBitmapCluster
    {

        private string codeField;

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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorBitmapField
    {

        private string nameField;

        private string maskField;

        private string summaryField;

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
        public string mask
        {
            get
            {
                return this.maskField;
            }
            set
            {
                this.maskField = value;
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
    public partial class rootConfiguratorCluster
    {
        private string domainField;

        private string nameField;

        private string codeField;

        private string defineField;

        private string descriptionField;

        private rootConfiguratorClusterGlobalAttribute[] globalAttributeField;

        private rootConfiguratorClusterFeatures featuresField;

        private rootConfiguratorClusterAttribute[] attributeField;

        private rootConfiguratorClusterCommand[] commandField;

        private rootConfiguratorClusterClient clientField;

        private rootConfiguratorClusterServer serverField;

        private rootConfiguratorClusterEvent[] eventField;

        /// <remarks/>
        public string domain
        {
            get
            {
                return this.domainField;
            }
            set
            {
                this.domainField = value;
            }
        }

        /// <remarks/>
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
        public string define
        {
            get
            {
                return this.defineField;
            }
            set
            {
                this.defineField = value;
            }
        }

        /// <remarks/>
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("globalAttribute")]
        public rootConfiguratorClusterGlobalAttribute[] globalAttribute
        {
            get
            {
                return this.globalAttributeField;
            }
            set
            {
                this.globalAttributeField = value;
            }
        }

        /// <remarks/>
        public rootConfiguratorClusterFeatures features
        {
            get
            {
                return this.featuresField;
            }
            set
            {
                this.featuresField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("attribute")]
        public rootConfiguratorClusterAttribute[] attribute
        {
            get
            {
                return this.attributeField;
            }
            set
            {
                this.attributeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("command")]
        public rootConfiguratorClusterCommand[] command
        {
            get
            {
                return this.commandField;
            }
            set
            {
                this.commandField = value;
            }
        }

        /// <remarks/>
        public rootConfiguratorClusterClient client
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
        public rootConfiguratorClusterServer server
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
        [System.Xml.Serialization.XmlElementAttribute("event")]
        public rootConfiguratorClusterEvent[] @event
        {
            get
            {
                return this.eventField;
            }
            set
            {
                this.eventField = value;
            }
        }

    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterAttribute
    {
        private string sideField;

        private string codeField;

        private string descriptionField;

        private string textField;

        private string defineField;

        private string typeField;

        private string minField;

        private string maxField;

        private string defaultField;

        private string entryTypeField;

        private bool optionalField;

        private bool optionalFieldSpecified;

        private string apiMaturityField;

        private int lengthField;

        private bool lengthFieldSpecified;

        private bool reportableField;

        private bool reportableFieldSpecified;

        private bool writableField;

        private bool writableFieldSpecified;

        private bool isNullableField;

        private bool isNullableFieldSpecified;

        private rootConfiguratorClusterAttributeAccess accessField;

        private rootConfiguratorClusterAttributeMandatoryConform mandatoryConformField;

        private rootConfiguratorClusterAttributeOtherwiseConform otherwiseConformField;

        private object provisionalConformField;


        /// <remarks/>
        public rootConfiguratorClusterAttributeAccess access
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
        public rootConfiguratorClusterAttributeMandatoryConform mandatoryConform
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
        public rootConfiguratorClusterAttributeOtherwiseConform otherwiseConform
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
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        [XmlText]
        public string text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string define
        {
            get
            {
                return this.defineField;
            }
            set
            {
                this.defineField = value;
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
        public string min
        {
            get
            {
                return this.minField;
            }
            set
            {
                this.minField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string max
        {
            get
            {
                return this.maxField;
            }
            set
            {
                this.maxField = value;
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string entryType
        {
            get
            {
                return this.entryTypeField;
            }
            set
            {
                this.entryTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool optional
        {
            get
            {
                return this.optionalField;
            }
            set
            {
                this.optionalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool optionalSpecified
        {
            get
            {
                return this.optionalFieldSpecified;
            }
            set
            {
                this.optionalFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string apiMaturity
        {
            get
            {
                return this.apiMaturityField;
            }
            set
            {
                this.apiMaturityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int length
        {
            get
            {
                return this.lengthField;
            }
            set
            {
                this.lengthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool lengthSpecified
        {
            get
            {
                return this.lengthFieldSpecified;
            }
            set
            {
                this.lengthFieldSpecified = value;
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool reportableSpecified
        {
            get
            {
                return this.reportableFieldSpecified;
            }
            set
            {
                this.reportableFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool writable
        {
            get
            {
                return this.writableField;
            }
            set
            {
                this.writableField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool writableSpecified
        {
            get
            {
                return this.writableFieldSpecified;
            }
            set
            {
                this.writableFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool isNullable
        {
            get
            {
                return this.isNullableField;
            }
            set
            {
                this.isNullableField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isNullableSpecified
        {
            get
            {
                return this.isNullableFieldSpecified;
            }
            set
            {
                this.isNullableFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterAttributeAccess
    {

        private string opField;

        private string privilegeField;

        private string roleField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string op
        {
            get
            {
                return this.opField;
            }
            set
            {
                this.opField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string privilege
        {
            get
            {
                return this.privilegeField;
            }
            set
            {
                this.privilegeField = value;
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterAttributeMandatoryConform
    {

        private rootConfiguratorClusterAttributeMandatoryConformOrTerm orTermField;

        private rootConfiguratorClusterAttributeMandatoryConformFeature featureField;

        private ClusterConditionField conditionField;

        public ClusterConditionField condition
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
        public rootConfiguratorClusterAttributeMandatoryConformOrTerm orTerm
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

        /// <remarks/>
        public rootConfiguratorClusterAttributeMandatoryConformFeature feature
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
    public partial class ClusterConditionField
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
    public partial class rootConfiguratorClusterAttributeMandatoryConformOrTerm
    {

        private rootConfiguratorClusterAttributeMandatoryConformOrTermFeature featureField;

        private rootConfiguratorClusterAttributeMandatoryConformOrTermAttribute attributeField;

        /// <remarks/>
        public rootConfiguratorClusterAttributeMandatoryConformOrTermFeature feature
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

        /// <remarks/>
        public rootConfiguratorClusterAttributeMandatoryConformOrTermAttribute attribute
        {
            get
            {
                return this.attributeField;
            }
            set
            {
                this.attributeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterAttributeMandatoryConformOrTermFeature
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
    public partial class rootConfiguratorClusterAttributeMandatoryConformOrTermAttribute
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
    public partial class rootConfiguratorClusterAttributeMandatoryConformFeature
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
    public partial class rootConfiguratorClusterAttributeOtherwiseConform
    {

        private object provisionalConformField;

        private rootConfiguratorClusterAttributeOtherwiseConformMandatoryConform mandatoryConformField;

        private rootConfiguratorClusterFeaturesFeatureOptionalConform optionalConformField;

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
        public rootConfiguratorClusterAttributeOtherwiseConformMandatoryConform mandatoryConform
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
        public rootConfiguratorClusterFeaturesFeatureOptionalConform optionalConform
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterAttributeOtherwiseConformMandatoryConform
    {

        private rootConfiguratorClusterAttributeOtherwiseConformMandatoryConformGreaterTerm greaterTermField;

        private rootConfiguratorClusterAttributeOtherwiseConformMandatoryConformFeature featureField;

        /// <remarks/>
        public rootConfiguratorClusterAttributeOtherwiseConformMandatoryConformGreaterTerm greaterTerm
        {
            get
            {
                return this.greaterTermField;
            }
            set
            {
                this.greaterTermField = value;
            }
        }

        /// <remarks/>
        public rootConfiguratorClusterAttributeOtherwiseConformMandatoryConformFeature feature
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
    public partial class rootConfiguratorClusterAttributeOtherwiseConformMandatoryConformGreaterTerm
    {

        private rootConfiguratorClusterAttributeOtherwiseConformMandatoryConformGreaterTermAttribute attributeField;

        private rootConfiguratorClusterAttributeOtherwiseConformMandatoryConformGreaterTermLiteral literalField;

        /// <remarks/>
        public rootConfiguratorClusterAttributeOtherwiseConformMandatoryConformGreaterTermAttribute attribute
        {
            get
            {
                return this.attributeField;
            }
            set
            {
                this.attributeField = value;
            }
        }

        /// <remarks/>
        public rootConfiguratorClusterAttributeOtherwiseConformMandatoryConformGreaterTermLiteral literal
        {
            get
            {
                return this.literalField;
            }
            set
            {
                this.literalField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterAttributeOtherwiseConformMandatoryConformGreaterTermAttribute
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
    public partial class rootConfiguratorClusterAttributeOtherwiseConformMandatoryConformGreaterTermLiteral
    {

        private byte valueField;

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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterAttributeOtherwiseConformMandatoryConformFeature
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
    [System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
    public enum ItemsChoiceType
    {

        /// <remarks/>
        access,

        features,

        requireAttribute,

        requireCommand,

        /// <remarks/>
        mandatoryConform,

        /// <remarks/>
        optionalConform,

        /// <remarks/>
        otherwiseConform,

        /// <remarks/>
        provisionalConform,
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterClient
    {

        private bool tickField;

        private bool initField;

        private bool valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool tick
        {
            get
            {
                return this.tickField;
            }
            set
            {
                this.tickField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool init
        {
            get
            {
                return this.initField;
            }
            set
            {
                this.initField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public bool Value
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
    public partial class rootConfiguratorClusterCommand
    {

        private object[] itemsField;

        private string sourceField;

        private string codeField;

        private string nameField;

        private string responseField;

        private string descriptionField;

        private bool isFabricScopedField;

        private bool isFabricScopedFieldSpecified;

        private bool optionalField;

        private bool disableDefaultResponseField;

        private bool disableDefaultResponseFieldSpecified;

        private string cliField;

        private bool mustUseTimedInvokeField;

        private rootConfiguratorClusterCommandArg[] argField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("access", typeof(rootConfiguratorClusterCommandAccess))]
        [System.Xml.Serialization.XmlElementAttribute("mandatoryConform", typeof(rootConfiguratorClusterCommandMandatoryConform))]
        [System.Xml.Serialization.XmlElementAttribute("otherwiseConform", typeof(rootConfiguratorClusterCommandOtherwiseConform))]
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

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool mustUseTimedInvoke
        {
            get
            {
                return this.mustUseTimedInvokeField;
            }
            set
            {
                this.mustUseTimedInvokeField = value;
            }
        }

        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("arg")]
        public rootConfiguratorClusterCommandArg[] arg
        {
            get
            {
                return this.argField;
            }
            set
            {
                this.argField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string source
        {
            get
            {
                return this.sourceField;
            }
            set
            {
                this.sourceField = value;
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool isFabricScoped
        {
            get
            {
                return this.isFabricScopedField;
            }
            set
            {
                this.isFabricScopedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isFabricScopedSpecified
        {
            get
            {
                return this.isFabricScopedFieldSpecified;
            }
            set
            {
                this.isFabricScopedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool optional
        {
            get
            {
                return this.optionalField;
            }
            set
            {
                this.optionalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool disableDefaultResponse
        {
            get
            {
                return this.disableDefaultResponseField;
            }
            set
            {
                this.disableDefaultResponseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool disableDefaultResponseSpecified
        {
            get
            {
                return this.disableDefaultResponseFieldSpecified;
            }
            set
            {
                this.disableDefaultResponseFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cli
        {
            get
            {
                return this.cliField;
            }
            set
            {
                this.cliField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterCommandAccess
    {

        private string opField;

        private string privilegeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string op
        {
            get
            {
                return this.opField;
            }
            set
            {
                this.opField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string privilege
        {
            get
            {
                return this.privilegeField;
            }
            set
            {
                this.privilegeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterCommandArg
    {

        private string nameField;

        private string typeField;

        private bool optionalField;

        private int lengthField;

        private bool lengthFieldSpecified;

        private bool isNullableField;

        private string minField;

        private string maxField;

        private bool arrayField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string min
        {
            get
            {
                return this.minField;
            }
            set
            {
                this.minField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string max
        {
            get
            {
                return this.maxField;
            }
            set
            {
                this.maxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool array
        {
            get
            {
                return this.arrayField;
            }
            set
            {
                this.arrayField = value;
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
        public bool optional
        {
            get
            {
                return this.optionalField;
            }
            set
            {
                this.optionalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int length
        {
            get
            {
                return this.lengthField;
            }
            set
            {
                this.lengthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool lengthSpecified
        {
            get
            {
                return this.lengthFieldSpecified;
            }
            set
            {
                this.lengthFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool isNullable
        {
            get
            {
                return this.isNullableField;
            }
            set
            {
                this.isNullableField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterCommandMandatoryConform
    {

        private rootConfiguratorClusterCommandMandatoryConformFeature featureField;

        /// <remarks/>
        public rootConfiguratorClusterCommandMandatoryConformFeature feature
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
    public partial class rootConfiguratorClusterCommandMandatoryConformFeature
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
    public partial class rootConfiguratorClusterCommandOtherwiseConform
    {

        private rootConfiguratorClusterCommandOtherwiseConformMandatoryConform mandatoryConformField;

        private rootConfiguratorClusterFeaturesFeatureOptionalConform optionalConformField;

        /// <remarks/>
        public rootConfiguratorClusterCommandOtherwiseConformMandatoryConform mandatoryConform
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
        public rootConfiguratorClusterFeaturesFeatureOptionalConform optionalConform
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterCommandOtherwiseConformMandatoryConform
    {

        private rootConfiguratorClusterCommandOtherwiseConformMandatoryConformFeature featureField;

        /// <remarks/>
        public rootConfiguratorClusterCommandOtherwiseConformMandatoryConformFeature feature
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
    public partial class rootConfiguratorClusterCommandOtherwiseConformMandatoryConformFeature
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
    public partial class rootConfiguratorClusterEvent
    {

        private string descriptionField;

        private rootConfiguratorClusterEventField[] fieldField;

        private rootConfiguratorClusterEventMandatoryConform mandatoryConformField;

        private string sideField;

        private string codeField;

        private string priorityField;

        private string nameField;

        private bool optionalField;

        /// <remarks/>
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("field")]
        public rootConfiguratorClusterEventField[] field
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
        public rootConfiguratorClusterEventMandatoryConform mandatoryConform
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
        public string priority
        {
            get
            {
                return this.priorityField;
            }
            set
            {
                this.priorityField = value;
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
        public bool optional
        {
            get
            {
                return this.optionalField;
            }
            set
            {
                this.optionalField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterEventField
    {

        private byte idField;

        private string nameField;

        private string typeField;

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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterEventMandatoryConform
    {

        private rootConfiguratorClusterEventMandatoryConformAndTerm andTermField;

        private rootConfiguratorClusterEventMandatoryConformFeature featureField;

        /// <remarks/>
        public rootConfiguratorClusterEventMandatoryConformAndTerm andTerm
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

        /// <remarks/>
        public rootConfiguratorClusterEventMandatoryConformFeature feature
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
    public partial class rootConfiguratorClusterEventMandatoryConformAndTerm
    {

        private rootConfiguratorClusterEventMandatoryConformAndTermFeature featureField;

        private rootConfiguratorClusterEventMandatoryConformAndTermNotTerm notTermField;

        /// <remarks/>
        public rootConfiguratorClusterEventMandatoryConformAndTermFeature feature
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

        /// <remarks/>
        public rootConfiguratorClusterEventMandatoryConformAndTermNotTerm notTerm
        {
            get
            {
                return this.notTermField;
            }
            set
            {
                this.notTermField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterEventMandatoryConformAndTermFeature
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
    public partial class rootConfiguratorClusterEventMandatoryConformAndTermNotTerm
    {

        private rootConfiguratorClusterEventMandatoryConformAndTermNotTermFeature featureField;

        /// <remarks/>
        public rootConfiguratorClusterEventMandatoryConformAndTermNotTermFeature feature
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
    public partial class rootConfiguratorClusterEventMandatoryConformAndTermNotTermFeature
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
    public partial class rootConfiguratorClusterEventMandatoryConformFeature
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
    public partial class rootConfiguratorClusterFeatures
    {

        private rootConfiguratorClusterFeaturesFeature[] featureField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("feature")]
        public rootConfiguratorClusterFeaturesFeature[] feature
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
    public partial class rootConfiguratorClusterFeaturesFeature
    {

        private rootConfiguratorClusterFeaturesFeatureOptionalConform optionalConformField;

        private rootConfiguratorClusterFeaturesFeatureOtherwiseConform otherwiseConformField;

        private byte bitField;

        private string codeField;

        private string nameField;

        private string summaryField;

        private string apiMaturityField;

        /// <remarks/>
        public rootConfiguratorClusterFeaturesFeatureOptionalConform optionalConform
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
        public rootConfiguratorClusterFeaturesFeatureOtherwiseConform otherwiseConform
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
        public byte bit
        {
            get
            {
                return this.bitField;
            }
            set
            {
                this.bitField = value;
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string apiMaturity
        {
            get
            {
                return this.apiMaturityField;
            }
            set
            {
                this.apiMaturityField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterFeaturesFeatureOptionalConform
    {

        private rootConfiguratorClusterFeaturesFeatureOptionalConformFeature featureField;

        private rootConfiguratorClusterFeaturesFeatureOptionalConformAndTerm andTermField;

        private string choiceField;

        /// <remarks/>
        public rootConfiguratorClusterFeaturesFeatureOptionalConformFeature feature
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

        /// <remarks/>
        public rootConfiguratorClusterFeaturesFeatureOptionalConformAndTerm andTerm
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string choice
        {
            get
            {
                return this.choiceField;
            }
            set
            {
                this.choiceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterFeaturesFeatureOptionalConformFeature
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
    public partial class rootConfiguratorClusterFeaturesFeatureOptionalConformAndTerm
    {

        private rootConfiguratorClusterFeaturesFeatureOptionalConformAndTermFeature featureField;

        private rootConfiguratorClusterFeaturesFeatureOptionalConformAndTermFeature1[] orTermField;

        private rootConfiguratorClusterFeaturesFeatureOptionalConformAndTermNotTerm notTermField;

        /// <remarks/>
        public rootConfiguratorClusterFeaturesFeatureOptionalConformAndTermFeature feature
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

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("feature", IsNullable = false)]
        public rootConfiguratorClusterFeaturesFeatureOptionalConformAndTermFeature1[] orTerm
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

        /// <remarks/>
        public rootConfiguratorClusterFeaturesFeatureOptionalConformAndTermNotTerm notTerm
        {
            get
            {
                return this.notTermField;
            }
            set
            {
                this.notTermField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterFeaturesFeatureOptionalConformAndTermFeature
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
    public partial class rootConfiguratorClusterFeaturesFeatureOptionalConformAndTermFeature1
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
    public partial class rootConfiguratorClusterFeaturesFeatureOptionalConformAndTermNotTerm
    {

        private rootConfiguratorClusterFeaturesFeatureOptionalConformAndTermNotTermFeature featureField;

        /// <remarks/>
        public rootConfiguratorClusterFeaturesFeatureOptionalConformAndTermNotTermFeature feature
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
    public partial class rootConfiguratorClusterFeaturesFeatureOptionalConformAndTermNotTermFeature
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
    public partial class rootConfiguratorClusterFeaturesFeatureOtherwiseConform
    {

        private object provisionalConformField;

        private rootConfiguratorClusterFeaturesFeatureOtherwiseConformMandatoryConform mandatoryConformField;

        private rootConfiguratorClusterFeaturesFeatureOtherwiseConformOptionalConform optionalConformField;

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
        public rootConfiguratorClusterFeaturesFeatureOtherwiseConformMandatoryConform mandatoryConform
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
        public rootConfiguratorClusterFeaturesFeatureOtherwiseConformOptionalConform optionalConform
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterFeaturesFeatureOtherwiseConformMandatoryConform
    {

        private rootConfiguratorClusterFeaturesFeatureOtherwiseConformMandatoryConformFeature featureField;

        /// <remarks/>
        public rootConfiguratorClusterFeaturesFeatureOtherwiseConformMandatoryConformFeature feature
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
    public partial class rootConfiguratorClusterFeaturesFeatureOtherwiseConformMandatoryConformFeature
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
    public partial class rootConfiguratorClusterFeaturesFeatureOtherwiseConformOptionalConform
    {

        private rootConfiguratorClusterFeaturesFeatureOtherwiseConformOptionalConformFeature[] andTermField;

        private rootConfiguratorClusterFeaturesFeatureOtherwiseConformOptionalConformFeature1 featureField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("feature", IsNullable = false)]
        public rootConfiguratorClusterFeaturesFeatureOtherwiseConformOptionalConformFeature[] andTerm
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

        /// <remarks/>
        public rootConfiguratorClusterFeaturesFeatureOtherwiseConformOptionalConformFeature1 feature
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
    public partial class rootConfiguratorClusterFeaturesFeatureOtherwiseConformOptionalConformFeature
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
    public partial class rootConfiguratorClusterFeaturesFeatureOtherwiseConformOptionalConformFeature1
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
    public partial class rootConfiguratorClusterGlobalAttribute
    {

        private string sideField;

        private string codeField;

        private string valueField;

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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorClusterServer
    {

        private bool tickField;

        private bool initField;

        private bool valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool tick
        {
            get
            {
                return this.tickField;
            }
            set
            {
                this.tickField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool init
        {
            get
            {
                return this.initField;
            }
            set
            {
                this.initField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public bool Value
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
    public partial class rootConfiguratorDomain
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
    public partial class rootConfiguratorEnum
    {

        private rootConfiguratorEnumCluster clusterField;

        private rootConfiguratorEnumItem[] itemField;

        private string nameField;

        private string typeField;

        /// <remarks/>
        public rootConfiguratorEnumCluster cluster
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
        [System.Xml.Serialization.XmlElementAttribute("item")]
        public rootConfiguratorEnumItem[] item
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
    public partial class rootConfiguratorEnumCluster
    {

        private string codeField;

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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorEnumItem
    {

        private string valueField;

        private string nameField;

        private string summaryField;

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
    public partial class rootConfiguratorStruct
    {

        private rootConfiguratorStructCluster clusterField;

        private rootConfiguratorStructItem[] itemField;

        private string nameField;

        private bool isFabricScopedField;

        /// <remarks/>
        public rootConfiguratorStructCluster cluster
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
        [System.Xml.Serialization.XmlElementAttribute("item")]
        public rootConfiguratorStructItem[] item
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool isFabricScoped
        {
            get
            {
                return this.isFabricScopedField;
            }
            set
            {
                this.isFabricScopedField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorStructCluster
    {

        private string codeField;

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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootConfiguratorStructItem
    {
        private string nameField;

        private string typeField;

        private bool isFabricSensitiveField;

        private string defaultField;

        private string minField;

        private string maxField;

        private int lengthField;

        private bool optionalField;

        private bool isNullableField;

        private bool arrayField;

        private string fieldIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string min
        {
            get
            {
                return this.minField;
            }
            set
            {
                this.minField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string max
        {
            get
            {
                return this.maxField;
            }
            set
            {
                this.maxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int length
        {
            get
            {
                return this.lengthField;
            }
            set
            {
                this.lengthField = value;
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
        public bool array
        {
            get
            {
                return this.arrayField;
            }
            set
            {
                this.arrayField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool isNullable
        {
            get
            {
                return this.isNullableField;
            }
            set
            {
                this.isNullableField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool optional
        {
            get
            {
                return this.optionalField;
            }
            set
            {
                this.optionalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool isFabricSensitive
        {
            get
            {
                return this.isFabricSensitiveField;
            }
            set
            {
                this.isFabricSensitiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string fieldId
        {
            get
            {
                return this.fieldIdField;
            }
            set
            {
                this.fieldIdField = value;
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
}
