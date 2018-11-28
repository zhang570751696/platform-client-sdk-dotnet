using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PureCloudPlatform.Client.V2.Client;

namespace PureCloudPlatform.Client.V2.Model
{
    /// <summary>
    /// ArchitectPromptNotificationErrorDetail
    /// </summary>
    [DataContract]
    public partial class ArchitectPromptNotificationErrorDetail :  IEquatable<ArchitectPromptNotificationErrorDetail>
    {
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
    
        /// <summary>
        /// Initializes a new instance of the <see cref="ArchitectPromptNotificationErrorDetail" /> class.
        /// </summary>
        /// <param name="ErrorCode">ErrorCode.</param>
        /// <param name="EntityId">EntityId.</param>
        /// <param name="EntityName">EntityName.</param>
        /// <param name="FieldName">FieldName.</param>
        public ArchitectPromptNotificationErrorDetail(string ErrorCode = null, string EntityId = null, string EntityName = null, string FieldName = null)
        {
            this.ErrorCode = ErrorCode;
            this.EntityId = EntityId;
            this.EntityName = EntityName;
            this.FieldName = FieldName;
            
        }
        
        
        
        /// <summary>
        /// Gets or Sets ErrorCode
        /// </summary>
        [DataMember(Name="errorCode", EmitDefaultValue=false)]
        public string ErrorCode { get; set; }
        
        
        
        /// <summary>
        /// Gets or Sets EntityId
        /// </summary>
        [DataMember(Name="entityId", EmitDefaultValue=false)]
        public string EntityId { get; set; }
        
        
        
        /// <summary>
        /// Gets or Sets EntityName
        /// </summary>
        [DataMember(Name="entityName", EmitDefaultValue=false)]
        public string EntityName { get; set; }
        
        
        
        /// <summary>
        /// Gets or Sets FieldName
        /// </summary>
        [DataMember(Name="fieldName", EmitDefaultValue=false)]
        public string FieldName { get; set; }
        
        
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ArchitectPromptNotificationErrorDetail {\n");
            
            sb.Append("  ErrorCode: ").Append(ErrorCode).Append("\n");
            sb.Append("  EntityId: ").Append(EntityId).Append("\n");
            sb.Append("  EntityName: ").Append(EntityName).Append("\n");
            sb.Append("  FieldName: ").Append(FieldName).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as ArchitectPromptNotificationErrorDetail);
        }

        /// <summary>
        /// Returns true if ArchitectPromptNotificationErrorDetail instances are equal
        /// </summary>
        /// <param name="other">Instance of ArchitectPromptNotificationErrorDetail to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ArchitectPromptNotificationErrorDetail other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return true &&
                (
                    this.ErrorCode == other.ErrorCode ||
                    this.ErrorCode != null &&
                    this.ErrorCode.Equals(other.ErrorCode)
                ) &&
                (
                    this.EntityId == other.EntityId ||
                    this.EntityId != null &&
                    this.EntityId.Equals(other.EntityId)
                ) &&
                (
                    this.EntityName == other.EntityName ||
                    this.EntityName != null &&
                    this.EntityName.Equals(other.EntityName)
                ) &&
                (
                    this.FieldName == other.FieldName ||
                    this.FieldName != null &&
                    this.FieldName.Equals(other.FieldName)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)
                
                if (this.ErrorCode != null)
                    hash = hash * 59 + this.ErrorCode.GetHashCode();
                
                if (this.EntityId != null)
                    hash = hash * 59 + this.EntityId.GetHashCode();
                
                if (this.EntityName != null)
                    hash = hash * 59 + this.EntityName.GetHashCode();
                
                if (this.FieldName != null)
                    hash = hash * 59 + this.FieldName.GetHashCode();
                
                return hash;
            }
        }
    }

}