using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class DataObject
    {
        public DataObject() { }
        public string operatorId { get; set; } = string.Empty;
        public string jobday { get; set; } = string.Empty;
        public string jobtime { get; set; } = string.Empty;
        public string jobno { get; set; } = string.Empty;
        public  string drawingno { get; set; } = string.Empty;
        public string handle { get; set; } = string.Empty;
        public string itemno { get; set; } = string.Empty;
        public string insulation { get; set; } = string.Empty;
        public string galvenized { get; set; } = string.Empty;
        public string notes { get; set; } = string.Empty;
        public string weight { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public string qty { get; set; } = string.Empty;
        public string cuttype { get; set; } = string.Empty;
        public string cid { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string doublewall { get; set; } = string.Empty;
        [Range(0.01,999999999)]
        public int pathid { get; set; } = 0;
        public double insulationarea { get; set; } = 0;
        public double metalarea { get; set; } = 0;
        public string boughtout { get; set; } = string.Empty;
        public string linearmeter { get; set; } = string.Empty;
        public string sectionindex { get; set; } = string.Empty;
        public string sectiondescription { get; set; } = string.Empty;
        public string prefixstring { get; set; } = string.Empty;
        public string insulationSpec { get; set; } = string.Empty;
        public string widthDim { get; set; } = string.Empty;
        public string depthDim { get; set; } = string.Empty;
        public string lengthangle { get; set; } = string.Empty;
        public string connector { get; set; } = string.Empty;
        public string material { get; set; } = string.Empty;
        public string equipmentTag { get; set; } = string.Empty;
        public string jobArea { get; set; } = string.Empty;
        public string filename { get; set; } = string.Empty;
        public string custom4 { get; set; } = string.Empty;
        public string emptyString { get; set; } = string.Empty;

    }
}
