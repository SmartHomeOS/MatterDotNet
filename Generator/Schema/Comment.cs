using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Schema
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", ElementName = "comments", IsNullable = false)]
    public partial class Comments
    {

        private commentsItem[] bitmapField;
        private commentsItem[] enumField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("bitmap")]
        public commentsItem[] bitmap
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
        public commentsItem[] @enum
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class commentsItem
    {

        private commentsBitmapItem[] itemField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("item")]
        public commentsBitmapItem[] item
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
    public partial class commentsBitmapItem
    {

        private string valueField;

        private string commentField;

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
        public string comment
        {
            get
            {
                return this.commentField;
            }
            set
            {
                this.commentField = value;
            }
        }
    }


}
