using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper.Configuration.Attributes;
using TakeOffAPI.DatabaseUtility;

namespace TakeOffAPI.Entities
{
    [Table("dispatch_detail_test")]
    public class DispatchDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Name("operatorID")]
        public string operatorID { get; set; } = string.Empty;
        [Name("jobday")]
        public string jobday { get; set; } = string.Empty;
        [Name("jobtime")]
        public string jobtime { get; set; } = string.Empty;
        public string duration { get; set; } = string.Empty;
        public string receiverName { get; set; } = string.Empty;
        public string receiverEmail { get; set; } = string.Empty;
        public string stationName { get; set; } = StationEnum.TakeOff;
        [Name("jobno")]
        public string jobno { get; set; } = string.Empty;
        [Name("drawingno")]
        public string drawingno { get; set; } = string.Empty;
        [Name("handle")]
        public string handle { get; set; } = string.Empty;
        [Name("itemno")]
        public string itemno { get; set; } = string.Empty;
        [Name("insulation")]
        public string insulation { get; set; } = string.Empty;
        [Name("galvenized")]
        public string galvenized { get; set; } = string.Empty;
        [Name("notes")]
        public string notes { get; set; } = string.Empty;
        [Name("weight")]
        public string weight { get; set; } = string.Empty;
        [Name("status")]
        public string status { get; set; } = string.Empty;
        [Name("itemQuantity")]
        public int qty { get; set; } = 0;
        [Name("cuttype")]
        public string cuttype { get; set; } = string.Empty;
        [Name("cid")]
        public string cid { get; set; } = string.Empty;
        [Name("description")] 
        public string description { get; set; } = string.Empty;
        [Name("doublewall")] 
        public string doublewall { get; set; } = string.Empty;
        [Name("pathId")]
        public int pathId { get; set; } = 0;
        [Name("insulationarea")] 
        public string insulationarea { get; set; } = string.Empty;
        [Name("metalarea")]
        public string metalarea { get; set; } = string.Empty;
        [Name("boughtout")]
        public string boughtout { get; set; } = string.Empty;
        [Name("linearmeter")]
        public string linearmeter { get; set; } = string.Empty;
        [Name("sectionindex")]
        public string sectionindex { get; set; } = string.Empty;
        [Name("sectiondescription")]
        public string sectiondescription { get; set; } = string.Empty;
        [Name("prefixstring")]
        public string prefixstring { get; set; } = string.Empty;
        [Name("insulationSpec")]
        public string insulationSpec { get; set; } = string.Empty;
        [Name("widthDim")]
        public string widthDim { get; set; } = string.Empty;
        [Name("depthDim")]
        public string depthDim { get; set; } = string.Empty;
        [Name("lengthangle")]
        public string lengthangle { get; set; } = string.Empty;
        [Name("connector")]
        public string connector { get; set; } = string.Empty;
        [Name("material")]
        public string material { get; set; } = string.Empty;
        [Name("equipmentTag")]
        public string equipmentTag { get; set; } = string.Empty;
        [Name("jobArea")]
        public string jobArea { get; set; } = string.Empty;
        public string storageInfo { get; set; } = string.Empty;
        public string packingID { get; set; } = string.Empty;
        public string resetDay { get; set; } = string.Empty;
        public string resetTime { get; set; } = string.Empty;
        [Name("filename")]
        public string filename { get; set; } = string.Empty;
        public string prodQABy { get; set; } = string.Empty;
        public string prodQADay { get; set; } = string.Empty;
        public string prodQATime { get; set; } = string.Empty;

        public DispatchDetail(string operatorID_, string jobday_, string jobtime_, string duration_, string receiverName_, string receiverEmail_, string stationName_, string jobno_, string drawingno_, string handle_, string itemno_, string insulation_, string galvenized_, string notes_, string weight_, string status_, int qty_, string cuttype_, string cid_, string description_, string doublewall_, int pathId_, string insulationarea_, string metalarea_, string boughtout_, string linearmeter_, string sectionindex_, string sectiondescription_, string prefixstring_, string insulationSpec_, string widthDim_, string depthDim_, string lengthangle_, string connector_, string material_, string equipmentTag_, string jobArea_, string storageInfo_, string packingID_, string resetDay_, string resetTime_, string filename_, string prodQABy_, string prodQADay_, string prodQATime_)
        {
            this.operatorID = operatorID_;
            this.jobday = jobday_;
            this.jobtime = jobtime_;
            this.duration = duration_;
            this.receiverName = receiverName_;
            this.receiverEmail = receiverEmail_;
            this.stationName = stationName_;
            this.jobno = jobno_;
            this.drawingno = drawingno_;
            this.handle = handle_;
            this.itemno = itemno_;
            this.insulation = insulation_;
            this.galvenized = galvenized_;
            this.notes = notes_;
            this.weight = weight_;
            this.status = status_;
            this.qty = qty_;
            this.cuttype = cuttype_;
            this.cid = cid_;
            this.description = description_;
            this.doublewall = doublewall_;
            this.pathId = pathId_;
            this.insulationarea = insulationarea_;
            this.metalarea = metalarea_;
            this.boughtout = boughtout_;
            this.linearmeter = linearmeter_;
            this.sectionindex = sectionindex_;
            this.sectiondescription = sectiondescription_;
            this.prefixstring = prefixstring_;
            this.insulationSpec = insulationSpec_;
            this.widthDim = widthDim_;
            this.depthDim = depthDim_;
            this.lengthangle = lengthangle_;
            this.connector = connector_;
            this.material = material_;
            this.equipmentTag = equipmentTag_;
            this.jobArea = jobArea_;
            this.storageInfo = storageInfo_;
            this.packingID = packingID_;
            this.resetDay = resetDay_;
            this.resetTime = resetTime_;
            this.filename = filename_;
            this.prodQABy = prodQABy_;
            this.prodQADay = prodQADay_;
            this.prodQATime = prodQATime_;
        }
        public DispatchDetail()
        {
        }
        public DispatchDetail(DispatchDetail newObject)
        {
            this.operatorID = newObject.operatorID;
            this.jobday = newObject.jobday;
            this.jobtime = newObject.jobtime;
            this.duration = newObject.duration;
            this.receiverName = newObject.receiverName;
            this.receiverEmail = newObject.receiverEmail;
            this.stationName = newObject.stationName;
            this.jobno = newObject.jobno;
            this.drawingno = newObject.drawingno;
            this.handle = newObject.handle;
            this.itemno = newObject.itemno;
            this.insulation = newObject.insulation;
            this.galvenized = newObject.galvenized;
            this.notes = newObject.notes;
            this.weight = newObject.weight;
            this.status = newObject.status;
            this.qty = newObject.qty;
            this.cuttype = newObject.cuttype;
            this.cid = newObject.cid;
            this.description = newObject.description;
            this.doublewall = newObject.doublewall;
            this.pathId = newObject.pathId;
            this.insulationarea = newObject.insulationarea;
            this.metalarea = newObject.metalarea;
            this.boughtout = newObject.boughtout;
            this.linearmeter = newObject.linearmeter;
            this.sectionindex = newObject.sectionindex;
            this.sectiondescription = newObject.sectiondescription;
            this.prefixstring = newObject.prefixstring;
            this.insulationSpec = newObject.insulationSpec;
            this.widthDim = newObject.widthDim;
            this.depthDim = newObject.depthDim;
            this.lengthangle = newObject.lengthangle;
            this.connector = newObject.connector;
            this.material = newObject.material;
            this.equipmentTag = newObject.equipmentTag;
            this.jobArea = newObject.jobArea;
            this.storageInfo = newObject.storageInfo;
            this.packingID = newObject.packingID;
            this.resetDay = newObject.resetDay;
            this.resetTime = newObject.resetTime;
            this.filename = newObject.filename;
            this.prodQABy = newObject.prodQABy;
            this.prodQADay = newObject.prodQADay;
            this.prodQATime = newObject.prodQATime;
        }

    }
}

